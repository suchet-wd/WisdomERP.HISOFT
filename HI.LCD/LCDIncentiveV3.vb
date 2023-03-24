Imports System.Drawing
Imports System.Threading

Public Class LCDIncentiveV3
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
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigCom AS M WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S2 WITH(NOLOCK)  ON M.FNHSysUnitSectIdTo = S2.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S1 WITH(NOLOCK)  ON M.FNHSysUnitSectId = S1.FNHSysUnitSectId "
        _Qry &= vbCrLf & "  WHERE M.FTComputerName='" & HI.UL.ULF.rpQuoted(System.Environment.MachineName.ToUpper) & "'"

        If StateWindowsUser Then

            _Qry &= vbCrLf & " AND (M.FTUserWindow='" & HI.UL.ULF.rpQuoted(System.Environment.UserName.ToUpper) & "' OR ISNULL(M.FTUserWindow,'')='') "
            _Qry &= vbCrLf & " ORDER BY  ISNULL(M.FTUserWindow,'')  DESC "

        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        ClearLabelData()
        L2ClearLabelData()
        Call SetDefualtControl()


        Me.LineNo = ""
        Me.LineId = 0
        Me.FormularId = 0

        Me.LineNoL2 = ""
        Me.L2LineId = 0
        Me.L2FormularId = 0
        Me.IncenFormulaIdLine1 = 0
        Me.IncenFormulaIdLine2L2 = 0

        For Each R As DataRow In _dt.Rows

            Me.LineNo = R!FTUnitSectCode.ToString
            Me.LineId = Val(R!FNHSysUnitSectId.ToString)
            Me.FormularId = Val(R!FNHSysIncenFormulaId.ToString)
            Me.IncenFormulaIdLine1 = Val(R!FNHSysIncenFormulaId.ToString)

            Me.LineNoL2 = R!FTUnitSectCodeTo.ToString
            Me.L2LineId = Val(R!FNHSysUnitSectIdTo.ToString)
            Me.L2FormularId = Val(R!FNHSysIncenFormulaIdTo.ToString)
            Me.IncenFormulaIdLine2L2 = Val(R!FNHSysIncenFormulaId.ToString)

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

        DateBefore = ""
        StateActualDate = False
        DateBeforeL2 = ""
        StateActualDateL2 = False

        If Me.LineId > 0 Then

            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.LineId & ") "
            _Qry &= vbCrLf & "  And (FDDate < N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            DateBefore = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")


            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.LineId & ") "
            _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            StateActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

        End If

        If Me.L2LineId > 0 Then

            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.L2LineId & ") "
            _Qry &= vbCrLf & "  And (FDDate < N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            DateBeforeL2 = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.L2LineId & ") "
            _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            StateActualDateL2 = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

        End If


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


        '_Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        'Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
        'Me.olbhourL2.Text = Me.olbhour.Text

        'Me.ottime.Enabled = True
        Me.otmcheckswitchtoheader.Enabled = False

        Me.ottime.Enabled = (Me.LineId > 0)
        Me.ottimeL2.Enabled = (Me.L2LineId > 0)

        If _TimeSwitchtoSpeed > 0 Then

            _TimeSwitchtoSpeed = _TimeSwitchtoSpeed * (60 * 1000)



            If _TimeSwitchToHeader > 0 Then

                _TimeSwitchToHeader = _TimeSwitchToHeader * (60 * 1000)


            End If

        End If

        otmcheckswitchtoheader.Enabled = True

    End Sub


    Private Sub SetDefualtControl()
        opnget.Dock = System.Windows.Forms.DockStyle.Fill
        opntalt.Dock = System.Windows.Forms.DockStyle.Fill
        opntalt.Visible = False

        olbsheader02.Visible = True
        olbslvtarget11.Visible = True

        olbsheader2.Visible = True
        olbslv14.Visible = True

        L2opnget.Dock = System.Windows.Forms.DockStyle.Fill
        L2opntalt.Dock = System.Windows.Forms.DockStyle.Fill
        L2opntalt.Visible = False

        L2olbsheader02.Visible = True
        L2olbslvtarget11.Visible = True

        L2olbsheader2.Visible = True
        L2olbslv14.Visible = True

    End Sub
    Private Sub LCDDisplayIncentive_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDDisplayIncentive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub LCDDisplayIncentive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

        Me.Line1LabelDataTextQC = olbprodper.Text
        Me.Line2LabelDataTextQC = L2olbprodper.Text

        Dim _Qry As String = ""

        '_Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        'Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")



        SetData()
        L2SetData()


        If StateActualDate = False And DateBefore <> "" And Me.LineId > 0 Then
            olbprodper.Text = Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEN(DateBefore), 5)
        End If


        If StateActualDateL2 = False And DateBeforeL2 <> "" And Me.L2LineId > 0 Then
            L2olbprodper.Text = Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEN(DateBeforeL2), 5)
        End If

    End Sub

    Private Sub LCDDisplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim _MeHeight As Integer = Me.Height
        Dim _MeWidth As Integer = Me.Width

        Me.opnl1.Width = _MeWidth / 2

        Me.SetSize()
        Me.L2SetSize()

    End Sub

#Region "Process Line 1"

#Region "Property"

    Private _LineId As Integer = 0
    Property LineId As Integer
        Get
            Return _LineId
        End Get
        Set(value As Integer)
            _LineId = value
        End Set
    End Property


    Private _FormularId As Integer = 0
    Property FormularId As Integer
        Get
            Return _FormularId
        End Get
        Set(value As Integer)
            _FormularId = value
        End Set
    End Property

    Private _LineNo As String = ""
    Property LineNo As String
        Get
            Return _LineNo
        End Get
        Set(value As String)
            _LineNo = value
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

    Private _DateBefore As String = ""
    Property DateBefore As String
        Get
            Return _DateBefore
        End Get
        Set(value As String)
            _DateBefore = value
        End Set
    End Property

    Private _DateData As String = ""
    Property DateData As String
        Get
            Return _DateData
        End Get
        Set(value As String)
            _DateData = value
        End Set
    End Property


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


    Private _Line1Data As DataTable = Nothing
    Property Line1Data As DataTable
        Get
            Return _Line1Data
        End Get
        Set(value As DataTable)
            _Line1Data = value
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


    Private _Line1LabelDataTextQC As String = ""
    Property Line1LabelDataTextQC As String
        Get
            Return _Line1LabelDataTextQC
        End Get
        Set(value As String)
            _Line1LabelDataTextQC = value
        End Set
    End Property

#End Region

#Region "Procedure "

    Private Sub GetEmployeeActualFromHR()

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

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
        'If (_StateFTStateDaily) Then
        '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        'End If

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND TT.FTDateTrans = '" & DateBefore & "'"
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
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ") "

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=   '" & DateBefore & "' ) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "') "
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

                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
                'If (_StateFTStateDaily) Then
                '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
                'End If

                If StateActualDate = False And DateBefore <> "" Then
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = '" & DateBefore & "'"
                Else
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "
                End If


                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

                If StateActualDate = False And DateBefore <> "" Then
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBefore & "'	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd > '" & DateBefore & "')"
                Else
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
                End If


                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ") "

                If StateActualDate = False And DateBefore <> "" Then

                    _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBefore & "') "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "') "

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

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBefore & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(LineId)) & "	"
            _Qry &= vbCrLf & "	 And  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"

            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "')) "
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

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBefore & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(LineId)) & "	"
            _Qry &= vbCrLf & "	 AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "')) "
            Else
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBefore & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(LineId)) & "	"
            _Qry &= vbCrLf & "	 AND  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  '" & DateBefore & "') "
                _Qry &= vbCrLf & "  AND (A.FTStartDate <=  '" & DateBefore & "')) "
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

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBefore & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(LineId)) & "	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And DateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "')) "
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
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBefore & "'	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & DateBefore & "')"
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

        If StateActualDate = False And DateBefore <> "" Then

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute1  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBefore & "'And  X1.FTInsDate < '" & DateBefore & "' ) As XX1 "
            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute2  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBefore & "'And  X1.FTInsDate = '" & DateBefore & "' ) As XX2 "


            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNAbsent) As FNAbsent  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBefore & "' AND ISNULL(X1.FTIn1,'') ='' ) As XXT2 "

            _Qry &= vbCrLf & "    OUTER APPLY ( "
            _Qry &= vbCrLf & "                  Select TOP 1 FTOtIn, FTOtOut  "
            _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest As X1 With(NOLOCK)  "
            _Qry &= vbCrLf & "                  WHERE     X1.FNHSysEmpID=Emp.FNHSysEmpID   AND  X1.FTDateRequest = '" & DateBefore & "' ) As XXOT2 "
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


        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
        _Qry &= vbCrLf & "	  And Emp.FNHSysEmpTypeId In(Select FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType As ET With (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBefore & "'	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd > '" & DateBefore & "')"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If


        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ") "

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBefore & "') "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "') "
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



        lblgrade.ForeColor = Drawing.Color.Blue
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
        _Qry &= vbCrLf & "  WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(LineId)) & ")  "

        If StateActualDate = False And DateBefore <> "" Then
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Convert(Datetime,'" & DateBefore & "')),111) ) "
        Else
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Getdate()),111) ) "
        End If


        dtleave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dtleave.Rows

            Select Case True
                Case (R!FTStateMetal.ToString <> "")

                    lblgrade.ForeColor = Drawing.Color.Red
                    lblgrade.Text = "MD"

                Case Else

                    If Val(R!FNReworkPer.ToString) > 2 Then

                        lblgrade.ForeColor = Drawing.Color.Red
                        lblgrade.Text = R!FNReworkPer.ToString

                    Else

                        lblgrade.Text = R!FTGrade.ToString

                    End If
            End Select
        Next

        dtleave.Dispose()



    End Sub
    Private Sub ClearLabelData()
        Try
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

            olbtaktime.Text = ""
            lblgrade.Text = ""

            olbstarget2Eff.Text = ""

        Catch ex As Exception
        End Try

    End Sub
    Private Sub ClearLabelLineData()

        olbslvtarget11.Text = ""

        olbslv13.Text = ""
        olbslv14.Text = ""



    End Sub

    Private Sub otmline1_Tick(sender As Object, e As EventArgs) Handles otmline1.Tick
        Dim _Theard As New Thread(AddressOf CheckStateLine)
        _Theard.Start()
    End Sub

    Private Delegate Sub DelegateStateLine1()
    Private Sub CheckStateLine()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateStateLine1(AddressOf CheckStateLine), New Object() {})
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
                If StateActualDate = False And DateBefore <> "" Then
                    _Qry = "   Select Top 1 FDDate "
                    _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Integer.Parse(Val(LineId)) & ") "
                    _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
                    _Qry &= vbCrLf & "  Order By FDDate DESC "
                    StateActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

                    If (StateActualDate) Then

                        olbprodper.Text = Line1LabelDataTextQC
                        SetData(True)
                        CheckLoadTime()
                        GetEmployeeActualFromHR()

                    End If

                End If

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(LineId)) & ""

                If StateActualDate = False And DateBefore <> "" Then
                    _Qry &= vbCrLf & "  AND FDSDate <='" & Me.DateBefore & "' AND  FDEDate>='" & Me.DateBefore & "'  "
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

                    If StateActualDate = False And DateBefore <> "" Then
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(LineId)) & ",'" & DateBefore & "' "
                    Else
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(LineId)) & ",'" & _DateNow & "' "
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

                        GetEmployeeActualFromHR()

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
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
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

                        If StateActualDate = False And DateBefore <> "" Then
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


                        _Qry &= vbCrLf & "   WHERE O.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & "   And ISNULL(FNStateSewPack, 0)  In (0, 1)  "

                        If StateActualDate = False And DateBefore <> "" Then
                            _Qry &= vbCrLf & "   AND O.FDDate ='" & Me.DateBefore & "'"
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

                        Call ClearLabelLineData()

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


                        If StateActualDate = False And DateBefore <> "" Then
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & DateBefore & "')"
                        Else
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        End If

                        _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.LineId))
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
                        Call ClearLabelLineData()

                    End If

                Else

                    olbtime1.Text = ""
                    olbqa1.Text = "-"
                    olbstarget1.Text = ""
                    olbstarget2.Text = ""
                    olbsscan1.Text = ""
                    olbsscan2.Text = ""
                    Call ClearLabelLineData()
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

                If StateActualDate = False And DateBefore <> "" Then

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

                    ' Me.olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhour.Text)), "HH:mm:ss")
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
            Me.opnqa1.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(olbqa1.Text) >= 99.0)
                    Me.opnqa1.BackColor = Drawing.Color.FromArgb(0, 192, 0)
                Case (Val(olbqa1.Text) < 99.0) And (Val(olbqa1.Text) >= 95.0)
                    Me.opnqa1.BackColor = Drawing.Color.FromArgb(255, 128, 0)
                Case Else
                    Me.opnqa1.BackColor = Drawing.Color.Red
            End Select
        Catch ex As Exception

        End Try

    End Sub



    Private Sub otmline1checkemp09_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp09.Tick

        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) <> Line1CheckTime Or (Microsoft.VisualBasic.Left(Me.olbhour.Text, 5) = "08:30") Then

            Line1CheckTime = Microsoft.VisualBasic.Left(Me.olbhour.Text, 2)
            Call GetEmployeeActualFromHR()

        End If

    End Sub

    Private Sub otmline1checkemp10_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp10.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "10" Then

            Call GetEmployeeActualFromHR()

        End If
    End Sub

    Private Sub otmline1checkemp11_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp11.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "11" Then

            Call GetEmployeeActualFromHR()

        End If
    End Sub



    Private Sub olbslv14_TextChanged(sender As Object, e As EventArgs) Handles olbslv14.TextChanged
        Try
            olbslv14.ForeColor = Drawing.Color.Blue
            If IsNumeric(olbslv14.Text) Then
                If olbslv14.Tag.ToString = "1" AndAlso Me.SysLine1SlaryMax > 0 Then

                    If CDbl(olbslv14.Text) > Me.SysLine1SlaryMax Then
                        olbslv14.ForeColor = Drawing.Color.Green
                    Else
                        olbslv14.ForeColor = Drawing.Color.Red
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

                ' Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")

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


    Private Sub otmcheckswitchtospeed_Tick(sender As Object, e As EventArgs)

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

        SwipShowData()

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
            Me.opnt1.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(olbstarget2Eff.Text) >= 90.0)
                    Me.opnt1.BackColor = Drawing.Color.FromArgb(0, 192, 0)
                Case Else
                    Me.opnt1.BackColor = Drawing.Color.Red
            End Select

        Catch ex As Exception

        End Try
    End Sub



    Private Sub SwipShowData()



        'olbsheader02.Visible = Not olbsheader02.Visible
        'olbslvtarget11.Visible = Not olbslvtarget11.Visible


        'olbsheader2.Visible = Not olbsheader2.Visible
        'olbslv13.Visible = Not olbslv13.Visible



        opntalt.Visible = Not opntalt.Visible
        opnget.Visible = Not opnget.Visible

        If opntalt.Visible Then

            opnget.Dock = System.Windows.Forms.DockStyle.None
            opntalt.Dock = System.Windows.Forms.DockStyle.Fill

        Else

            opntalt.Dock = System.Windows.Forms.DockStyle.None
            opnget.Dock = System.Windows.Forms.DockStyle.Fill

        End If

        'L2olbsheader02.Visible = Not L2olbsheader02.Visible
        'L2olbslvtarget11.Visible = Not L2olbslvtarget11.Visible


        'L2olbsheader2.Visible = Not L2olbsheader2.Visible
        'L2olbslv13.Visible = Not L2olbslv13.Visible


        L2opntalt.Visible = Not L2opntalt.Visible
        L2opnget.Visible = Not L2opnget.Visible

        If L2opntalt.Visible Then

            L2opnget.Dock = System.Windows.Forms.DockStyle.None
            L2opntalt.Dock = System.Windows.Forms.DockStyle.Fill

        Else

            L2opntalt.Dock = System.Windows.Forms.DockStyle.None
            L2opnget.Dock = System.Windows.Forms.DockStyle.Fill

        End If

    End Sub

    Public Sub SetSize()
        Dim _MeHeight As Integer = opnl1.Height
        Dim _MeWidth As Integer = opnl1.Width
        Dim _LineHeaderWidth As Integer = Me.Width
        Dim designheight As Integer = 800
        Dim designwidth As Integer = 638
        Dim _PanalTargetHeightPer As Double = 0.36
        Dim _PanalTargetHeight As Integer = 288
        Dim _opnstargetqty1Width As Integer = 370
        Dim _opnsprodqty1Width As Integer = 321
        Dim _opnalbonuswidth As Integer = 151
        Dim _opnalleavewidth As Integer = 342
        Dim _olbqa1width As Integer = 176
        Dim _opnsprodqty2width As Integer = 317
        Dim _olbsscan02width As Integer = 143

        _PanalTargetHeight = _MeHeight * _PanalTargetHeightPer
        opnstargetqty.Height = _PanalTargetHeight


        opnstargetqty1.Width = _MeWidth * 0.57993730407523514

        olbstarget01.Width = opnstargetqty1.Width / 2
        olbstarget1.Width = olbstarget01.Width
        olbstarget2.Width = olbstarget01.Width
        olbsscan02.Width = olbstarget01.Width


        olbprodper.Width = opndescprod.Width / 2
        opnt1.Width = olbprodper.Width
        olbsprodspeed01.Width = olbprodper.Width
        olbsprodspeed1.Width = olbprodper.Width


        opcstargetqty1.Height = opnstargetqty.Height * 0.38028169014084512
        opcsscan1.Height = opnstargetqty.Height * 0.31338028169014082

        opndescprod.Height = opnsprodqty1.Height * 0.1875
        opnspeed.Height = opnsprodqty1.Height * 0.29166666666666669
        opnspeed3.Height = opnsprodqty1.Height * 0.20833333333333329


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

        ' Me.opnl1.Width = _MeWidth

        _LineHeaderWidth = _MeWidth 'opnsline.Width

        _PLineW = _LineHeaderWidth * 0.25632911
        _PineEmpW = _LineHeaderWidth * 0.25158227848
        _PlineTimeW = _LineHeaderWidth * 0.242088607
        _PLineQAW = _LineHeaderWidth * 0.25

        _P11W = _LineHeaderWidth * 0.507911392

        _PH0W = _LineHeaderWidth * 0.29780564263322878
        _PH1W = _LineHeaderWidth * 0.29780564263322878
        _PH2W = _LineHeaderWidth * 0.29780564263322878
        _PH3W = _LineHeaderWidth * 0.29780564263322878
        _PH4W = _LineHeaderWidth * 0.390282131661442

        Dim FontHourSize As Integer = (_P1 * 0.454545)
        Dim FHour As New Font("Tahoma", FontHourSize, FontStyle.Bold)
        'Me.opnhour.Height = _P1
        'Me.olbhour.Font = FHour

        '-----------Start Set Line 1
        opnsline.Height = _P1

        opnsheader.Height = _PLVTITLE
        opnslv1.Height = _PLV1
        opnslv2.Height = _PLV2

        opnslvtop.Height = _PLVTop

        opnline1.Width = _MeWidth * 0.2476489028213166
        opnemp1.Width = _MeWidth * 0.23510971786833859
        opnincentive.Width = _MeWidth * 0.17241379310344829
        opntime1.Width = _MeWidth * 0.17241379310344829


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


        'Me.opcsscan1.Width = _ImageW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.43 ' 30 '90
        Dim _FontLineH2 As Integer = _P1 * 0.28 ' 30 '90
        Dim _FontLineH3 As Integer = _P1 * 0.22 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Dim FFontLineH2 As New Font("Tahoma", _FontLineH2, FontStyle.Bold)
        Dim FFontLineH3 As New Font("Tahoma", _FontLineH3, FontStyle.Bold)

        Me.olbsline.Font = FFontLineH
        Me.olbemp1.Font = FFontLineH2
        Me.olbemp1incentive.Font = FFontLineH
        Me.olbtime1.Font = FFontLineH

        olbprodper.Font = FFontLineH3
        olbqrate.Font = FFontLineH3

        Dim _FontLineHQA As Integer = _P1 * 0.42 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.olbqa1.Font = FFontLineHQA
        olbstarget2Eff.Font = FFontLineHQA

        '----S Font LV
        Dim _FontLV As Integer = _PLV1 * 0.33 ' 30 '90
        Dim FFontLV As New Font("Tahoma", _FontLV, FontStyle.Bold)

        Dim _FontLVTile As Integer = _PLVTITLE * 0.48 ' 30 '90
        Dim FFontLVTile As New Font("Tahoma", _FontLVTile, FontStyle.Bold)

        'Dim _FontLV001 As Integer = _PLV1 * 0.300465 ' 30 '90
        'Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        'Dim _FontLV002 As Integer = _PLV1 * 0.4005952756 ' 30 '90
        'Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        'Dim _FontLV003 As Integer = _PLV1 * 0.4045952756 ' 30 '90
        'Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)

        Dim _FontLV001 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        Dim _FontLV002 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        Dim _FontLV003 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)


        olbsheader02.Font = FFontLVTile

        olbsheader2.Font = FFontLVTile
        olbsheader3.Font = FFontLVTile

        olbslvtarget11.Font = FFontLV001
        olbslv13.Font = FFontLV002
        olbslv14.Font = FFontLV003

        '----E Font LV
        Dim FontHeaderLineSize As Integer = (_P2 * 0.1902991)
        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)

        Dim FontHeaderLineSize0 As Integer = (_P2 * 0.41428972)
        Dim FHeader0 As New Font("Tahoma", FontHeaderLineSize0, FontStyle.Bold)

        Dim FontHeaderLineSize1 As Integer = (_PLVTITLE * 0.162)
        Dim FHeader1 As New Font("Tahoma", FontHeaderLineSize1, FontStyle.Bold)

        Dim FontHeaderLineSize3 As Integer = (_P2 * 0.1612991)
        Dim FHeader3 As New Font("Tahoma", FontHeaderLineSize3, FontStyle.Bold)

        Dim FontHeaderLineSize4 As Integer = (_P2 * 0.1312991)
        Dim FHeader4 As New Font("Tahoma", FontHeaderLineSize4, FontStyle.Bold)

        olbstarget1.Font = FHeader0
        olbstarget2.Font = FHeader0

        olbsscan1.Font = FHeader0
        olbsscan2.Font = FHeader0

        olbsprodspeed1.Font = FHeader0
        olbsprodspeed2.Font = FHeader0
        lblgrade.Font = FHeader0

        olbtaktime.Font = FHeader0
        olbstarget01.Font = FHeader

        olbsscan01.Font = FHeader

        olbsscan02.Font = FHeader3
        Me.olbsprodspeed01.Font = FHeader3
        Me.olbsprodspeed02.Font = FHeader3

        opcemp.Width = opnemp1.Width * 0.4285714285714286
        opcincentive.Width = opnincentive.Width * 0.52040816326530615
        opntime.Width = opntime1.Width * 0.56043956043956045
    End Sub


    Private Sub lblgrade_TextChanged(sender As Object, e As EventArgs) Handles lblgrade.TextChanged
        Try
            Me.opnbunus.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case Microsoft.VisualBasic.Left(lblgrade.Text, 1)
                Case "A"

                    Me.opnbunus.BackColor = Drawing.Color.FromArgb(0, 192, 0)

                Case "B"

                    Me.opnbunus.BackColor = Drawing.Color.FromArgb(255, 128, 0)

                Case Else

                    Me.opnbunus.BackColor = Drawing.Color.Red

            End Select


        Catch ex As Exception

        End Try
    End Sub


    Public Sub SetData(Optional StateSwipDate As Boolean = False)

        Try
            'Call SwipShowData()
            Call ClearLabelData()

            Dim _Qry As String = ""

            If LineNo <> "" Then
                Dim ST1 As String = ""
                For Each Str As String In Me.LineNo.ToCharArray()

                    If IsNumeric(Str) Then
                        Exit For
                    Else
                        ST1 = Str
                    End If
                Next

                If ST1 = "" Then
                    ST1 = "L"
                End If

                olbsline.Text = ST1 & "." & Microsoft.VisualBasic.Right(LineNo, 2)
                Dim _TotalCountEmp As Integer
                _Qry = "  SELECT Sum(1) AS CountEmp"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

                If StateActualDate = False And DateBefore <> "" Then
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBefore & "' 	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & DateBefore & "' )"
                Else
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
                End If



                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ") "

                If StateActualDate = False And DateBefore <> "" Then
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBefore & "') "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBefore & "') "
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

                Call GetEmployeeActualFromHR()
                Me.otmline1.Enabled = True
                Me.otmline1checkemp09.Enabled = True

                CheckLoadTime()

                Dim _Theard1 As New Thread(AddressOf CheckStateLine)
                _Theard1.Start()

            End If

        Catch ex As Exception

        End Try

    End Sub
#End Region

#End Region

#Region "Process Line 2"


#Region "Property"

    Private _L2LineId As Integer = 0
    Property L2LineId As Integer
        Get
            Return _L2LineId
        End Get
        Set(value As Integer)
            _L2LineId = value
        End Set
    End Property


    Private _L2FormularId As Integer = 0
    Property L2FormularId As Integer
        Get
            Return _L2FormularId
        End Get
        Set(value As Integer)
            _L2FormularId = value
        End Set
    End Property

    Private _LineNoL2 As String = ""
    Property LineNoL2 As String
        Get
            Return _LineNoL2
        End Get
        Set(value As String)
            _LineNoL2 = value
        End Set
    End Property

    Private _StateActualDateL2 As Boolean = False
    Property StateActualDateL2 As Boolean
        Get
            Return _StateActualDateL2
        End Get
        Set(value As Boolean)
            _StateActualDateL2 = value
        End Set
    End Property

    Private _DateBeforeL2 As String = ""
    Property DateBeforeL2 As String
        Get
            Return _DateBeforeL2
        End Get
        Set(value As String)
            _DateBeforeL2 = value
        End Set
    End Property

    Private _DateDataL2 As String = ""
    Property DateDataL2 As String
        Get
            Return _DateDataL2
        End Get
        Set(value As String)
            _DateDataL2 = value
        End Set
    End Property

    Private _TimeInML2 As String = "08:00"
    Property TimeInML2 As String
        Get
            Return _TimeInML2
        End Get
        Set(value As String)
            _TimeInML2 = value
        End Set
    End Property

    Private _TimeOutML2 As String = "12:00"
    Property TimeOutML2 As String
        Get
            Return _TimeOutML2
        End Get
        Set(value As String)
            _TimeOutML2 = value
        End Set
    End Property

    Private _TimeInAL2 As String = "13:00"
    Property TimeInAL2 As String
        Get
            Return _TimeInAL2
        End Get
        Set(value As String)
            _TimeInAL2 = value
        End Set
    End Property

    Private _TimeOutAL2 As String = "17:00"
    Property TimeOutAL2 As String
        Get
            Return _TimeOutAL2
        End Get
        Set(value As String)
            _TimeOutAL2 = value
        End Set
    End Property

    Private _TimeInOTL2 As String = "17:30"
    Property TimeInOTL2 As String
        Get
            Return _TimeInOTL2
        End Get
        Set(value As String)
            _TimeInOTL2 = value
        End Set
    End Property


    Private _TimeOutOTL2 As String = "19:30"
    Property TimeOutOTL2 As String
        Get
            Return _TimeOutOTL2
        End Get
        Set(value As String)
            _TimeOutOTL2 = value
        End Set
    End Property

    Private _Line2L2 As String = ""
    Property Line2L2 As String
        Get
            Return _Line2L2
        End Get
        Set(value As String)
            _Line2L2 = value
        End Set
    End Property

    Private _IncenFormulaIdLine2L2 As Integer = 0
    Property IncenFormulaIdLine2L2 As Integer
        Get
            Return _IncenFormulaIdLine2L2
        End Get
        Set(value As Integer)
            _IncenFormulaIdLine2L2 = value
        End Set
    End Property

    Private _Line2EmpCountTimeL2 As Integer = 0
    Property Line2EmpCountTimeL2 As Integer
        Get
            Return _Line2EmpCountTimeL2
        End Get
        Set(value As Integer)
            _Line2EmpCountTimeL2 = value
        End Set
    End Property

    Private _Line2EmpCountMoneyL2 As Integer = 0
    Property Line2EmpCountMoneyL2 As Integer
        Get
            Return _Line2EmpCountMoneyL2
        End Get
        Set(value As Integer)
            _Line2EmpCountMoneyL2 = value
        End Set
    End Property

    Private _Line2TotalEmpCountTimeL2 As Integer = 0
    Property Line2TotalEmpCountTimeL2 As Integer
        Get
            Return _Line2TotalEmpCountTimeL2
        End Get
        Set(value As Integer)
            _Line2TotalEmpCountTimeL2 = value
        End Set
    End Property


    Private _Line2DataL2 As DataTable = Nothing
    Property Line2DataL2 As DataTable
        Get
            Return _Line2DataL2
        End Get
        Set(value As DataTable)
            _Line2DataL2 = value
        End Set
    End Property


    Private _SysLine2SlaryL2 As Double = 0
    Property SysLine2SlaryL2 As Double
        Get
            Return _SysLine2SlaryL2
        End Get
        Set(value As Double)
            _SysLine2SlaryL2 = value
        End Set
    End Property

    Private _SysLine2SlaryMaxL2 As Double = 0
    Property SysLine2SlaryMaxL2 As Double
        Get
            Return _SysLine2SlaryMaxL2
        End Get
        Set(value As Double)
            _SysLine2SlaryMaxL2 = value
        End Set
    End Property


    Private _Line2CheckTimeL2 As String = ""
    Property Line2CheckTimeL2 As String
        Get
            Return _Line2CheckTimeL2
        End Get
        Set(value As String)
            _Line2CheckTimeL2 = value
        End Set
    End Property


    Private _Line2CheckTimeINML2 As String = ""
    Property Line2CheckTimeINML2 As String
        Get
            Return _Line2CheckTimeINML2
        End Get
        Set(value As String)
            _Line2CheckTimeINML2 = value
        End Set
    End Property

    Private _Line2CheckTimeINAL2 As String = ""
    Property Line2CheckTimeINAL2 As String
        Get
            Return _Line2CheckTimeINAL2
        End Get
        Set(value As String)
            _Line2CheckTimeINAL2 = value
        End Set
    End Property

    Private _Line2LabelDataTextQC As String = ""
    Property Line2LabelDataTextQC As String
        Get
            Return _Line2LabelDataTextQC
        End Get
        Set(value As String)
            _Line2LabelDataTextQC = value
        End Set
    End Property

#End Region

#Region "Procedure "

    Private Sub L2GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer = 0
        Dim _Slary As Double = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 5)
        Dim dtemp As New DataTable
        Dim _CountEmpTime As Integer = 0
        Dim _CountEmpMoney As Integer = 0

        _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

        'If (_StateFTStateDaily) Then
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        ' End If

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
        'If (_StateFTStateDaily) Then
        '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        'End If

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & "	  AND TT.FTDateTrans = '" & DateBeforeL2 & "'"
        Else
            _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "
        End If


        If _Time < Me.Line2CheckTimeINAL2 Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
        End If

        If _Time > Me.Line2CheckTimeINAL2 Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn2,'') <>''	"
        End If

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBeforeL2 & "'	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & DateBeforeL2 & "')"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If


        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBeforeL2 & "') "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "') "
        Else
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
        End If


        _Qry &= vbCrLf & "	  ) "

        dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If dtemp.Rows.Count <= 0 Then

            If _Time > Me.Line2CheckTimeINAL2 Then

                _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

                'If (_StateFTStateDaily) Then
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
                ' End If

                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
                'If (_StateFTStateDaily) Then
                '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
                'End If

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = '" & DateBeforeL2 & "'"
                Else
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "
                End If


                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBeforeL2 & "'	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & DateBeforeL2 & "')"
                Else
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
                End If


                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "') "
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

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBeforeL2 & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(L2LineId)) & "	"
            _Qry &= vbCrLf & "	 And  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"

            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "')) "
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

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBeforeL2 & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(L2LineId)) & "	"
            _Qry &= vbCrLf & "	 AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "')) "
            Else
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBeforeL2 & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & "	"
            _Qry &= vbCrLf & "	 AND  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                _Qry &= vbCrLf & "  AND (A.FTStartDate <= '" & DateBeforeL2 & "')) "
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

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & DateBeforeL2 & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & "	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "')) "
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
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBeforeL2 & "'	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd > '" & DateBeforeL2 & "')"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If


        Me.SysLine2SlaryL2 = 0
        _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

        Me.SysLine2SlaryL2 = _Slary

        If _CountEmpTime <= 0 Then
            If _Time < Me.Line2CheckTimeINAL2 Then
                If _TotalEmpHRmorningLine2 <= 0 Then
                    If _CountEmpTime > 0 Then
                        _TotalEmpHRmorningLine2 = _CountEmpTime
                    Else
                        _CountEmpTime = _TotalEmpFromMasterLine2
                        _CountEmpMoney = _TotalEmpFromMasterLine2
                    End If
                End If
            End If

            If _Time > Me.Line2CheckTimeINAL2 Then
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

        Line2EmpCountTimeL2 = _CountEmpTime
        Line2EmpCountMoneyL2 = _CountEmpMoney

        If _CountEmpTime > 0 Then
            'Me.L2olbemp1.Text = _CountEmpTime.ToString & "/" & _CountEmpMoney.ToString

            Me.L2olbemp1.Text = _CountEmpTime.ToString & "/" & _TotalEmpFromMasterLine2.ToString
        End If

        If _CountEmpMoney > 0 Then
            L2olbemp1incentive.Text = _CountEmpMoney.ToString
        Else
            L2olbemp1incentive.Text = ""
        End If

        Line2TotalEmpCountTimeL2 = _CountEmpTime


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

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute1  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBeforeL2 & "'And  X1.FTInsDate < '" & DateBeforeL2 & "' ) As XX1 "
            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute2  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBeforeL2 & "'And  X1.FTInsDate = '" & DateBeforeL2 & "' ) As XX2 "

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNAbsent) As FNAbsent  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & DateBeforeL2 & "' AND ISNULL(X1.FTIn1,'') ='' ) As XXT2 "

            _Qry &= vbCrLf & "    OUTER APPLY ( "
            _Qry &= vbCrLf & "                  Select TOP 1 FTOtIn, FTOtOut  "
            _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest As X1 With(NOLOCK)  "
            _Qry &= vbCrLf & "                  WHERE     X1.FNHSysEmpID=Emp.FNHSysEmpID   AND  X1.FTDateRequest = '" & DateBeforeL2 & "' ) As XXOT2 "
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


        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
        _Qry &= vbCrLf & "	  And Emp.FNHSysEmpTypeId In(Select FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType As ET With (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"


        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBeforeL2 & "') "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "') "
        Else
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
        End If


        _Qry &= vbCrLf & "	  )   ) AS X1"

        dtleave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dtleave.Rows

            LeaveBeforeMin = Val(R!FNTotalMinute1.ToString)
            LeaveAcidentMin = Val(R!FNTotalMinute2.ToString) ' + Val(R!FNAbsent.ToString)

            TimeInML2 = R!FTIn1.ToString
            TimeOutML2 = R!FTOut1.ToString

            TimeInAL2 = R!FTIn2.ToString
            TimeOutAL2 = R!FTOut2.ToString

            TimeInOTL2 = R!FTOtIn.ToString
            TimeOutOTL2 = R!FTOtOut.ToString

        Next



        L2lblgrade.ForeColor = Drawing.Color.Blue
        L2lblgrade.Text = "-"

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
        _Qry &= vbCrLf & "  WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(L2LineId)) & ")  "

        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Convert(Datetime,'" & DateBeforeL2 & "')),111) ) "
        Else
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Getdate()),111) ) "
        End If


        dtleave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dtleave.Rows

            Select Case True
                Case (R!FTStateMetal.ToString <> "")

                    L2lblgrade.ForeColor = Drawing.Color.Red
                    L2lblgrade.Text = "MD"

                Case Else

                    If Val(R!FNReworkPer.ToString) > 2 Then

                        L2lblgrade.ForeColor = Drawing.Color.Red
                        L2lblgrade.Text = R!FNReworkPer.ToString

                    Else

                        L2lblgrade.Text = R!FTGrade.ToString

                    End If
            End Select
        Next

        dtleave.Dispose()



    End Sub


    Private Sub L2ClearLabelData()
        Try
            L2olbsline.Text = ""

            L2olbemp1.Text = ""
            L2olbemp1incentive.Text = ""

            L2olbtime1.Text = ""

            L2olbqa1.Text = ""

            L2olbstarget1.Text = ""
            L2olbstarget2.Text = ""

            L2olbsscan1.Text = ""
            L2olbsscan2.Text = ""


            L2olbslvtarget11.Text = ""

            L2olbslv13.Text = ""
            L2olbslv14.Text = ""

            L2olbtaktime.Text = ""
            L2lblgrade.Text = ""

            L2olbstarget2Eff.Text = ""

        Catch ex As Exception
        End Try

    End Sub

    Private Sub L2ClearLabelLineData()

        L2olbslvtarget11.Text = ""

        L2olbslv13.Text = ""
        L2olbslv14.Text = ""


    End Sub

    Private Sub L2otmLine2_Tick(sender As Object, e As EventArgs) Handles L2otmline1.Tick
        Dim _Theard As New Thread(AddressOf CheckStateLineL2)
        _Theard.Start()
    End Sub

    Private Delegate Sub DelegateStateLineL2()
    Private Sub CheckStateLineL2()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateStateLineL2(AddressOf CheckStateLineL2), New Object() {})
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

                L2Style.Text = ""

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry = "   Select Top 1 FDDate "
                    _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Integer.Parse(Val(L2LineId)) & ") "
                    _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
                    _Qry &= vbCrLf & "  Order By FDDate DESC "
                    StateActualDateL2 = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

                    If (StateActualDateL2) Then

                        L2olbprodper.Text = Line2LabelDataTextQC
                        L2SetData(True)
                        CheckLoadTimeL2()
                        L2GetEmployeeActualFromHR()

                    End If

                End If


                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & ""

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & "  AND FDSDate <='" & Me.DateBeforeL2 & "' AND  FDEDate>='" & Me.DateBeforeL2 & "'  "
                Else
                    _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                End If

                _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dttimeplan.Rows
                    _TotalTarget = Integer.Parse(Val(R!FNTarget.ToString))
                    _TotalHourTarget = Integer.Parse(Val(R!FNTargetPerHour.ToString))

                    If R!FTWorkTime.ToString <> "" Then
                        Me.L2olbtime1.Text = R!FTWorkTime.ToString.Split(":")(0)
                        Try
                            _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                        Catch ex As Exception

                        End Try

                        If _TotalHourTarget > 0 Then
                            _TotalTarget = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * _TotalHourTarget)) + Integer.Parse(((Val(R!FTWorkTime.ToString.Split(":")(1)) * (_TotalHourTarget / 60.0))))
                        End If

                    Else
                        Me.L2olbtime1.Text = "8"
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
                    If Val(Me.L2olbtime1.Text) >= 8 Then

                        _Salary = Me.SysLine2SlaryL2
                        _Salary = _Salary + Double.Parse(Format((Me.SysLine2SlaryL2 / 8) * (Val(Me.L2olbtime1.Text) - 8) * 1.5, "0.00"))

                    Else
                        _Salary = Double.Parse(Format((Me.SysLine2SlaryL2 / 8) * Val(Me.L2olbtime1.Text), "0.00"))

                    End If

                    Me.SysLine2SlaryMaxL2 = _Salary

                    If _TotalHourTarget > 0 Then
                        L2olbstarget1.Text = _TotalTarget.ToString
                        L2olbstarget2.Text = _TotalHourTarget.ToString
                    Else
                        L2olbstarget1.Text = _TotalTarget.ToString
                        _TotalTargetPerHour = (_TotalTarget / _TimeWorlPlanMinute) * 60.0

                        _TotalHourTarget = _TotalTargetPerHour
                        L2olbstarget2.Text = _TotalTargetPerHour.ToString
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
                    Dim stylecode As String = ""

                    If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(L2LineId)) & ",'" & DateBeforeL2 & "' "
                    Else
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(L2LineId)) & ",'" & _DateNow & "' "
                    End If

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    _FNSam = 0
                    _PriceCost = 0
                    _TotalCountStyle = 0
                    TotalCalEff = 0
                    Dim SawGrandAmt As Decimal = 0

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

                    L2Style.Text = stylecode

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If

                    If Line2TotalEmpCountTimeL2 <= 0 Then

                        L2GetEmployeeActualFromHR()

                    End If

                    Dim TotalTimeMinute As Integer = _TimeWorlPlanMinute
                    TotalTimeMinute = L2GetTimeMinuteData()

                    If TotalTimeMinute <= 0 Then
                        TotalTimeMinute = _TimeWorlPlanMinute
                    End If

                    _TotalCountEmp = Line2TotalEmpCountTimeL2 'Integer.Parse(Val(Me.L2olbemp1.Text))

                    TotalCalEff = Double.Parse(Format((TotalCalEff / (TotalTimeMinute * _TotalCountEmp)) * 100.0, "0.00"))
                    L2olbstarget2Eff.Text = Format(TotalCalEff, "0.0")


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
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
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


                        If StateActualDate = False And DateBefore <> "" Then
                            CountHour = Val(L2olbtime1.Text)
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

                        _Qry &= vbCrLf & " ( "
                        '_Qry &= vbCrLf & "  SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        '_Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        '_Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        '_Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 AS FNCartonNo, FTOrderNo ,FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate AS FDScanDate, O.FTTime AS FDScanTime , O.FNQuantity AS FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo  "

                        _Qry &= vbCrLf & "   WHERE O.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & "   And ISNULL(FNStateSewPack, 0)  In (0, 1)  "

                        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                            _Qry &= vbCrLf & "   And O.FDDate ='" & Me.DateBeforeL2 & "'"
                        Else
                            _Qry &= vbCrLf & "   And O.FDDate ='" & Me.TransactionDate & "'"
                        End If

                        _Qry &= vbCrLf & "  ) AS A  "

                        '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
                        '_Qry &= vbCrLf & "   And FDScanDate ='" & Me.TransactionDate & "'"
                        _Qry &= vbCrLf & "   GROUP BY FDScanDate, FDScanTime"


                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        _TotalProd = 0

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        L2olbsscan1.Text = _TotalProd.ToString
                        'For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                        '    _Prod = _Prod + Val(R!FNScanQuantity)
                        'Next

                        If _EndTime <> "" And _EndTime <= Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 5) And Val(_Hour) >= 8 Then

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

                        L2olbsscan1.Text = _TotalProd.ToString
                        L2olbsscan2.Text = _Prod.ToString

                        Call L2ClearLabelLineData()

                        If Line2EmpCountMoneyL2 > 0 Then
                            SawGrandAmt = CDbl(Format((SawGrandAmt / Line2EmpCountMoneyL2), "0.00"))
                        Else
                            SawGrandAmt = CDbl(Format((SawGrandAmt / _TotalCountEmp), "0.00"))
                        End If

                        L2olbslvtarget11.Text = Format((CountHour * _TotalHourTarget), "0")
                        L2olbslv13.Text = Format(_TotalProd, "0")
                        L2olbslv14.Text = Format(SawGrandAmt, "0.00")

                        '-------New Info ------------

                        _Cmd = "SELECT   Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty "
                        _Cmd &= vbCrLf & "	,(SUM(ISNULL(D.FNTotalDefect,0))) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(ISNULL(D.FNTotalDefect,0)))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B  WITH (NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
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

                        If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & DateBeforeL2 & "')"
                        Else
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        End If

                        _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.L2LineId))
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
                            Me.L2olbqa1.Text = "100"
                        Else
                            If _TotalProd <= 0 Then
                                Me.L2olbqa1.Text = "-"
                            Else
                                Me.L2olbqa1.Text = Format(_QAPer, "0.0")
                            End If

                        End If

                    Else

                        L2olbtime1.Text = ""
                        L2olbqa1.Text = "-"
                        L2olbstarget1.Text = ""
                        L2olbstarget2.Text = ""
                        L2olbsscan1.Text = ""
                        L2olbsscan2.Text = ""
                        Call L2ClearLabelLineData()

                    End If

                Else

                    L2olbtime1.Text = ""
                    L2olbqa1.Text = "-"
                    L2olbstarget1.Text = ""
                    L2olbstarget2.Text = ""
                    L2olbsscan1.Text = ""
                    L2olbsscan2.Text = ""
                    Call L2ClearLabelLineData()
                End If

            Catch ex As Exception
            End Try

        End If
    End Sub



    Private Function L2GetTimeMinuteData() As Integer
        Dim SumMinute As Integer = 0
        Dim _Minute As Integer = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 5)

        If _Time >= TimeInML2 Then

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday


            If _Time >= TimeOutML2 Then
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInML2), CDate(Me.Actualdate & "  " & TimeOutML2))

            Else
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInML2), CDate(Me.Actualdate & "  " & _Time))
            End If


            If _Time > TimeInAL2 Then

                If _Time >= TimeOutAL2 Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInAL2), CDate(Me.Actualdate & "  " & TimeOutAL2))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInAL2), CDate(Me.Actualdate & "  " & _Time))
                End If

            End If

            If _Time > TimeInOTL2 And TimeInOTL2 <> "" And TimeOutOTL2 <> "" Then
                If _Time >= TimeOutOTL2 Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOTL2), CDate(Me.Actualdate & "  " & TimeOutOTL2))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOTL2), CDate(Me.Actualdate & "  " & _Time))
                End If
            End If


        End If

        Return SumMinute
    End Function



    Private Sub L2olbqa1_TextChanged(sender As Object, e As EventArgs) Handles L2olbqa1.TextChanged
        Try
            Me.L2opnqa1.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(L2olbqa1.Text) >= 99.0)
                    Me.L2opnqa1.BackColor = Drawing.Color.FromArgb(0, 192, 0)
                Case (Val(L2olbqa1.Text) < 99.0) And (Val(L2olbqa1.Text) >= 95.0)
                    Me.L2opnqa1.BackColor = Drawing.Color.FromArgb(255, 128, 0)
                Case Else
                    Me.L2opnqa1.BackColor = Drawing.Color.Red
            End Select
        Catch ex As Exception

        End Try

    End Sub



    Private Sub L2otmLine2checkemp09_Tick(sender As Object, e As EventArgs) Handles L2otmline1checkemp09.Tick

        If Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 2) <> Line2CheckTimeL2 Or (Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 5) = "08:30") Then

            Line2CheckTimeL2 = Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 2)
            Call L2GetEmployeeActualFromHR()

        End If

    End Sub

    Private Sub L2otmLine2checkemp10_Tick(sender As Object, e As EventArgs) Handles L2otmline1checkemp10.Tick
        If Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 2) = "10" Then

            Call L2GetEmployeeActualFromHR()

        End If
    End Sub

    Private Sub L2otmLine2checkemp11_Tick(sender As Object, e As EventArgs) Handles L2otmline1checkemp11.Tick
        If Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 2) = "11" Then

            Call L2GetEmployeeActualFromHR()

        End If
    End Sub



    Private Sub L2olbslv14_TextChanged(sender As Object, e As EventArgs) Handles L2olbslv14.TextChanged
        Try
            L2olbslv14.ForeColor = Drawing.Color.Blue
            If IsNumeric(L2olbslv14.Text) Then
                If L2olbslv14.Tag.ToString = "1" AndAlso Me.SysLine2SlaryMaxL2 > 0 Then

                    If CDbl(L2olbslv14.Text) > Me.SysLine2SlaryMaxL2 Then
                        L2olbslv14.ForeColor = Drawing.Color.Green
                    Else
                        L2olbslv14.ForeColor = Drawing.Color.Red
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub L2olbsscan1_TextChanged(sender As Object, e As EventArgs) Handles L2olbsscan1.TextChanged
        Try

            If IsNumeric(L2olbsscan1.Text.Trim) Then

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday

                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _T3 As Integer = 0
                Dim _TotalScan As Integer = Integer.Parse((L2olbsscan1.Text.Trim))
                Dim _Total As Integer = 0
                Dim _TotalH As Integer = Integer.Parse((L2olbtime1.Text))

                'Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")
                Dim _CurrentTime As String = Microsoft.VisualBasic.Left(Me.olbhourL2.Text, 5)
                If _CurrentTime <> "" And _CurrentTime >= "08:00" Then

                    If _CurrentTime >= TimeInML2 And _CurrentTime <= TimeOutML2 Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInML2), CDate(Me.Actualdate & "  " & _CurrentTime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInML2), CDate(Me.Actualdate & "  " & TimeOutML2))
                    End If

                    If _CurrentTime >= TimeInAL2 Then
                        If _CurrentTime <= TimeOutAL2 Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInAL2), CDate(Me.Actualdate & "  " & _CurrentTime))
                        Else
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInAL2), CDate(Me.Actualdate & "  " & TimeOutAL2))
                        End If
                    End If

                    If _TotalH > 8 Then

                        If _CurrentTime >= "17:30" Then
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOTL2), CDate(Me.Actualdate & "  " & _CurrentTime))
                        End If

                    End If

                    _Total = (_T1 + _T2 + _T3) * 60

                    If _Total <= 0 Then

                        L2olbsprodspeed1.Text = "" '_TotalScan.ToString()15960/757
                        L2olbsprodspeed2.Text = ""
                    Else

                        L2olbsprodspeed1.Text = Format((_Total / _TotalScan), "0")
                        L2olbsprodspeed2.Text = Format((3600 / Val(L2olbsprodspeed1.Text)), "0")
                    End If

                Else
                    L2olbsprodspeed1.Text = ""
                    L2olbsprodspeed2.Text = ""
                End If

            Else
                L2olbsprodspeed1.Text = ""
                L2olbsprodspeed2.Text = ""
            End If
        Catch ex As Exception
            L2olbsprodspeed1.Text = ""
            L2olbsprodspeed2.Text = ""
        End Try
    End Sub


    Private Sub L2otmcheckswitchtoheader_Tick(sender As Object, e As EventArgs)

        L2SwipShowData()

    End Sub

    Private Sub L2olbstarget2_Click(sender As Object, e As EventArgs) Handles L2olbstarget2.Click

    End Sub

    Private Sub L2olbstarget2_TextChanged(sender As Object, e As EventArgs) Handles L2olbstarget2.TextChanged
        If IsNumeric(L2olbstarget2.Text) Then
            L2olbtaktime.Text = Format((3600.0 / Double.Parse(L2olbstarget2.Text)), "0")
        Else
            L2olbtaktime.Text = ""
        End If
    End Sub

    Private Sub L2olbstarget2Eff_TextChanged(sender As Object, e As EventArgs) Handles L2olbstarget2Eff.TextChanged
        Try
            Me.L2opnt1.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(L2olbstarget2Eff.Text) >= 90.0)
                    Me.L2opnt1.BackColor = Drawing.Color.FromArgb(0, 192, 0)
                Case Else
                    Me.L2opnt1.BackColor = Drawing.Color.Red
            End Select

        Catch ex As Exception

        End Try
    End Sub



    Private Sub L2SwipShowData()



    End Sub

    Public Sub L2SetSize()
        Dim _MeHeight As Integer = L2opnl1.Height
        Dim _MeWidth As Integer = L2opnl1.Width
        Dim _LineHeaderWidth As Integer = Me.Width
        Dim designheight As Integer = 800
        Dim designwidth As Integer = 638
        Dim _PanalTargetHeightPer As Double = 0.36
        Dim _PanalTargetHeight As Integer = 288
        Dim _opnstargetqty1Width As Integer = 370
        Dim _opnsprodqty1Width As Integer = 321
        Dim _opnalbonuswidth As Integer = 151
        Dim _opnalleavewidth As Integer = 342
        Dim _L2olbqa1width As Integer = 176
        Dim _opnsprodqty2width As Integer = 317
        Dim _L2olbsscan02width As Integer = 143

        _PanalTargetHeight = _MeHeight * _PanalTargetHeightPer
        L2opnstargetqty.Height = _PanalTargetHeight

        L2opnstargetqty1.Width = _MeWidth * 0.57993730407523514
        L2olbstarget01.Width = L2opnstargetqty1.Width / 2
        L2olbstarget1.Width = L2olbstarget01.Width
        L2olbstarget2.Width = L2olbstarget01.Width
        L2olbsscan02.Width = L2olbstarget01.Width


        L2olbprodper.Width = L2opndescprod.Width / 2
        L2opnt1.Width = L2olbprodper.Width
        L2olbsprodspeed01.Width = L2olbprodper.Width
        L2olbsprodspeed1.Width = L2olbprodper.Width

        L2opcstargetqty1.Height = L2opnstargetqty.Height * 0.38028169014084512
        L2opcsscan1.Height = L2opnstargetqty.Height * 0.31338028169014082

        L2opndescprod.Height = L2opnsprodqty1.Height * 0.1875
        L2opnspeed.Height = L2opnsprodqty1.Height * 0.29166666666666669
        L2opnspeed3.Height = L2opnsprodqty1.Height * 0.20833333333333329


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

        Dim _PLineW As Integer = 162 'opnLine2
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

        ' Me.L2opnl1.Width = _MeWidth

        _LineHeaderWidth = _MeWidth 'opnsline.Width

        _PLineW = _LineHeaderWidth * 0.25632911
        _PineEmpW = _LineHeaderWidth * 0.25158227848
        _PlineTimeW = _LineHeaderWidth * 0.242088607
        _PLineQAW = _LineHeaderWidth * 0.25

        _P11W = _LineHeaderWidth * 0.507911392

        _PH0W = _LineHeaderWidth * 0.29780564263322878
        _PH1W = _LineHeaderWidth * 0.29780564263322878
        _PH2W = _LineHeaderWidth * 0.29780564263322878
        _PH3W = _LineHeaderWidth * 0.29780564263322878
        _PH4W = _LineHeaderWidth * 0.390282131661442

        Dim FontHourSize As Integer = (_P1 * 0.454545)
        Dim FHour As New Font("Tahoma", FontHourSize, FontStyle.Bold)
        'Me.opnhour.Height = _P1
        'Me.L2olbhour.Font = FHour

        '-----------Start Set Line 1
        L2opnsline.Height = _P1

        L2opnsheader.Height = _PLVTITLE
        L2opnslv1.Height = _PLV1
        L2opnslv2.Height = _PLV2

        L2opnslvtop.Height = _PLVTop

        L2opnline1.Width = _MeWidth * 0.2476489028213166
        L2opnemp1.Width = _MeWidth * 0.23510971786833859
        L2opnincentive.Width = _MeWidth * 0.17241379310344829
        L2opntime1.Width = _MeWidth * 0.17241379310344829


        L2olbsheader02.Width = _PH2W

        L2olbsheader2.Width = _PH3W
        L2olbsheader3.Width = _PH4W


        L2olbslvtarget11.Width = _PH2W


        L2olbslvtarget11.Width = _PH1W

        L2olbslv13.Width = _PH3W
        L2olbslv14.Width = _PH4W



        _CaptionWidth = _LineWidth / 2



        L2olbsheader02.Width = _PH2W
        L2olbslvtarget11.Width = _PH2W

        '-----------End Set Line 1

        Dim _ImageW As Integer = (_P11W * 0.42056075)

        'Me.opcstargetqty1.Width = _ImageW


        ' Me.L2opcsscan1.Width = _ImageW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.43 ' 30 '90
        Dim _FontLineH2 As Integer = _P1 * 0.28 ' 30 '90
        Dim _FontLineH3 As Integer = _P1 * 0.22 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Dim FFontLineH2 As New Font("Tahoma", _FontLineH2, FontStyle.Bold)
        Dim FFontLineH3 As New Font("Tahoma", _FontLineH3, FontStyle.Bold)


        Me.L2olbsline.Font = FFontLineH
        Me.L2olbemp1.Font = FFontLineH2
        Me.L2olbemp1incentive.Font = FFontLineH
        Me.L2olbtime1.Font = FFontLineH

        L2olbprodper.Font = FFontLineH3
        L2olbqrate.Font = FFontLineH3

        Dim _FontLineHQA As Integer = _P1 * 0.42 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.L2olbqa1.Font = FFontLineHQA
        L2olbstarget2Eff.Font = FFontLineHQA

        '----S Font LV
        Dim _FontLV As Integer = _PLV1 * 0.33 ' 30 '90
        Dim FFontLV As New Font("Tahoma", _FontLV, FontStyle.Bold)

        Dim _FontLVTile As Integer = _PLVTITLE * 0.48 ' 30 '90
        Dim FFontLVTile As New Font("Tahoma", _FontLVTile, FontStyle.Bold)

        'Dim _FontLV001 As Integer = _PLV1 * 0.300465 ' 30 '90
        'Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        'Dim _FontLV002 As Integer = _PLV1 * 0.4005952756 ' 30 '90
        'Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        'Dim _FontLV003 As Integer = _PLV1 * 0.4045952756 ' 30 '90
        'Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)

        Dim _FontLV001 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        Dim _FontLV002 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        Dim _FontLV003 As Integer = _PLV1 * 0.3535952756 ' 30 '90
        Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)


        L2olbsheader02.Font = FFontLVTile

        L2olbsheader2.Font = FFontLVTile
        L2olbsheader3.Font = FFontLVTile


        L2olbslvtarget11.Font = FFontLV001

        L2olbslv13.Font = FFontLV002
        L2olbslv14.Font = FFontLV003



        '----E Font LV
        Dim FontHeaderLineSize As Integer = (_P2 * 0.1902991)
        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)

        Dim FontHeaderLineSize0 As Integer = (_P2 * 0.41428972)
        Dim FHeader0 As New Font("Tahoma", FontHeaderLineSize0, FontStyle.Bold)

        Dim FontHeaderLineSize1 As Integer = (_PLVTITLE * 0.162)
        Dim FHeader1 As New Font("Tahoma", FontHeaderLineSize1, FontStyle.Bold)

        Dim FontHeaderLineSize3 As Integer = (_P2 * 0.1612991)
        Dim FHeader3 As New Font("Tahoma", FontHeaderLineSize3, FontStyle.Bold)

        Dim FontHeaderLineSize4 As Integer = (_P2 * 0.1312991)
        Dim FHeader4 As New Font("Tahoma", FontHeaderLineSize4, FontStyle.Bold)

        L2olbstarget1.Font = FHeader0
        L2olbstarget2.Font = FHeader0

        L2olbsscan1.Font = FHeader0
        L2olbsscan2.Font = FHeader0

        L2olbsprodspeed1.Font = FHeader0
        L2olbsprodspeed2.Font = FHeader0
        L2lblgrade.Font = FHeader0

        L2olbtaktime.Font = FHeader0
        L2olbstarget01.Font = FHeader

        L2olbsscan01.Font = FHeader

        L2olbsscan02.Font = FHeader3
        Me.L2olbsprodspeed01.Font = FHeader3
        Me.L2olbsprodspeed02.Font = FHeader3

        L2opcemp.Width = L2opnemp1.Width * 0.4285714285714286
        L2opcincentive.Width = L2opnincentive.Width * 0.52040816326530615
        L2opntime.Width = L2opntime1.Width * 0.56043956043956045
    End Sub

    Private Sub L2lblgrade_Click(sender As Object, e As EventArgs) Handles L2lblgrade.Click
    End Sub

    Private Sub L2lblgrade_TextChanged(sender As Object, e As EventArgs) Handles L2lblgrade.TextChanged
        Try
            Me.L2opnbunus.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case Microsoft.VisualBasic.Left(L2lblgrade.Text, 1)
                Case "A"

                    Me.L2opnbunus.BackColor = Drawing.Color.FromArgb(0, 192, 0)

                Case "B"

                    Me.L2opnbunus.BackColor = Drawing.Color.FromArgb(255, 128, 0)

                Case Else

                    Me.L2opnbunus.BackColor = Drawing.Color.Red

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Public Sub L2SetData(Optional StateSwipDate As Boolean = False)

        Try
            'Call SwipShowData()
            Call L2ClearLabelData()

            Dim _Qry As String = ""

            If LineNoL2 <> "" Then

                Dim ST1 As String = ""
                For Each Str As String In Me.LineNoL2.ToCharArray()

                    If IsNumeric(Str) Then
                        Exit For
                    Else
                        ST1 = Str
                    End If
                Next

                If ST1 = "" Then
                    ST1 = "L"
                End If

                L2olbsline.Text = ST1 & "." & Microsoft.VisualBasic.Right(LineNoL2, 2)
                Dim _TotalCountEmp As Integer
                _Qry = "  SELECT Sum(1) AS CountEmp"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & DateBeforeL2 & "'	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd > '" & DateBeforeL2 & "' )"
                Else
                    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
                End If

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & DateBeforeL2 & "') "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & DateBeforeL2 & "') "
                Else
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
                End If


                _Qry &= vbCrLf & "	  ) "

                _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
                _TotalEmpFromMasterLine2 = _TotalCountEmp

                If StateSwipDate Then
                    Exit Sub
                End If

                Me.L2olbemp1.Text = _TotalCountEmp.ToString() & "/" & _TotalCountEmp.ToString()
                L2olbemp1incentive.Text = _TotalCountEmp.ToString()
                Line2TotalEmpCountTimeL2 = _TotalCountEmp
                Line2EmpCountTimeL2 = _TotalCountEmp
                Line2EmpCountMoneyL2 = _TotalCountEmp
                Line2TotalEmpCountTimeL2 = _TotalCountEmp

                Line2CheckTimeINML2 = Me.TimeInML2
                Line2CheckTimeINAL2 = Me.TimeInAL2

                'Select Case e.FDDateEnd, ET.FTEmpTypeCode, TS.FTShiftCode, TS.FTIn1, TS.FTIn2, E.FNHSysUnitSectId
                'From THRMEmployee As E INNER Join
                '              THRMTimeShift As TS On e.FNHSysShiftID = TS.FNHSysShiftID INNER Join
                '              HITECH_MASTER.dbo.THRMEmpType AS ET ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId
                'Where (ISNULL(e.FDDateEnd, '') = '') AND (ET.FTEmpTypeCode = N'S')

                Call L2GetEmployeeActualFromHR()
                Me.L2otmline1.Enabled = True
                Me.L2otmline1checkemp09.Enabled = True

                CheckLoadTimeL2()

                Dim _Theard1 As New Thread(AddressOf CheckStateLineL2)
                _Theard1.Start()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub olbqa1_Click(sender As Object, e As EventArgs) Handles olbqa1.Click

    End Sub

    Private Delegate Sub DelegateLoadTimeL2()
    Private Sub CheckLoadTimeL2()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateLoadTime(AddressOf CheckLoadTimeL2), New Object() {})
        Else
            Try


                Dim _Qry As String
                _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "

                If StateActualDateL2 = False And DateBeforeL2 <> "" Then

                    If TimeOutOTL2 <> "" Then
                        Me.olbhourL2.Text = TimeOutOTL2
                    Else
                        Me.olbhourL2.Text = TimeOutAL2
                    End If

                    L2opnline1.BackColor = Color.FromArgb(192, 255, 192)
                    L2olbsline.ForeColor = Color.Black

                    '255, 192, 192
                Else

                    L2opnline1.BackColor = Color.FromArgb(255, 192, 192)
                    L2olbsline.ForeColor = Color.Black

                    ' Me.olbhourL2.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhourL2.Text)), "HH:mm:ss")

                    Me.olbhourL2.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                End If


            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub ottimeL2_Tick(sender As Object, e As EventArgs) Handles ottimeL2.Tick
        Try
            'Me.olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhour.Text)), "HH:mm:ss")
            CheckLoadTimeL2()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub olbslv14_Click(sender As Object, e As EventArgs) Handles olbslv14.Click

    End Sub

    Private Sub olbslvt4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub L2olbslv14_Click(sender As Object, e As EventArgs) Handles L2olbslv14.Click

    End Sub
#End Region

#End Region

End Class
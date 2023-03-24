Imports System.Drawing
Imports System.Threading

Public Class LCDDisplayIncentive_Back_20160503
    Private _StateFTStateDaily As Boolean = False

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

        _Actualdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Try
            _Qry = "SELECT TOP 1  '1' AS FTStateDaily"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE ISNULL(FTStateDaily,'') <> '2'"

            _StateFTStateDaily = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "1")
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


#End Region


    Private Sub LCDDisplayIncentive_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.otmline1.Enabled = False
            Me.otmline2.Enabled = False
            Me.ottime.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDDisplayIncentive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub Line1GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer
        Dim _Slary As Double = 0


        _TotalCountEmp = 0
        _Slary = 0

        _Qry = "  SELECT Sum(1) AS CountEmp "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

        If (_StateFTStateDaily) Then
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        End If

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
        If (_StateFTStateDaily) Then
            _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        End If

        _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) AND ISNULL(TT.FTIn1,'') <>''	"
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        If _TotalCountEmp > 0 Then
            _Qry = "    SELECT Count(1) AS CountEmp "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "    SELECT Count(1) AS CountEmp "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))
        End If


        _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        Me.SysLine1Slary = 0
        _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

        Me.SysLine1Slary = _Slary
        If _TotalCountEmp > 0 Then
            Me.olbemp1.Text = _TotalCountEmp.ToString
        End If

        If Me.otmline1checkemp09.Enabled = True Then
            otmline1checkemp09.Enabled = False
            otmline1checkemp10.Enabled = True
        End If

        If Me.otmline1checkemp10.Enabled = True Then
            otmline1checkemp10.Enabled = False
            otmline1checkemp11.Enabled = True
        End If

        If Me.otmline1checkemp11.Enabled = True Then
            otmline1checkemp11.Enabled = False
        End If

    End Sub

    Private Sub Line2GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer
        Dim _Slary As Double = 0

        _Qry = "  SELECT Sum(1) AS CountEmp "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

        If (_StateFTStateDaily) Then
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        End If

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
        If (_StateFTStateDaily) Then
            _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        End If

        _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) AND ISNULL(TT.FTIn1,'') <>''	"
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        If _TotalCountEmp > 0 Then
            _Qry = "    SELECT Count(1) AS CountEmp "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "    SELECT Count(1) AS CountEmp "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))
        End If


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
        If _TotalCountEmp > 0 Then
            Me.olbemp2.Text = _TotalCountEmp.ToString
        End If

        If Me.otmline2checkemp09.Enabled = True Then
            otmline2checkemp09.Enabled = False
            otmline2checkemp10.Enabled = True
        End If

        If Me.otmline2checkemp10.Enabled = True Then
            otmline2checkemp10.Enabled = False
            otmline2checkemp11.Enabled = True
        End If

        If Me.otmline2checkemp11.Enabled = True Then
            otmline2checkemp11.Enabled = False
        End If

    End Sub

    Private Sub LCDDisplayIncentive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Dim _Qry As String
        Call ClearLabelData()
        olbsline.Text = ""
        If Me.Line1 <> "" Then
            olbsline.Text = "L." & Microsoft.VisualBasic.Right(Me.Line1, 2)
            Dim _TotalCountEmp As Integer
            _Qry = "  SELECT Sum(1) AS CountEmp"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
            _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

            Me.olbemp1.Text = _TotalCountEmp.ToString()

            Call Line1GetEmployeeActualFromHR()
            Me.otmline1.Enabled = True
            Me.otmline1checkemp09.Enabled = True

            Dim _Theard1 As New Thread(AddressOf CheckStateLine1)
            _Theard1.Start()
        End If

        olbeline.Text = ""
        If Me.Line2 <> "" Then
            olbeline.Text = "L." & Microsoft.VisualBasic.Right(Me.Line2, 2)
            Dim _TotalCountEmp As Integer
            _Qry = "  SELECT Sum(1) AS CountEmp"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
            _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

            Me.olbemp2.Text = _TotalCountEmp.ToString()

            Call Line2GetEmployeeActualFromHR()
            Me.otmline2.Enabled = True
            Me.otmline2checkemp09.Enabled = True

            Dim _Theard2 As New Thread(AddressOf CheckStateLine2)
            _Theard2.Start()
        End If




        _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        Me.ottime.Enabled = True



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
        olbslv12.Text = ""
        olbslv13.Text = ""
        olbslv14.Text = ""
        olbslv21.Text = ""
        olbslv22.Text = ""
        olbslv23.Text = ""
        olbslv24.Text = ""

        olbslvt1.Text = ""
        olbslvt2.Text = ""
        olbslvt3.Text = ""
        olbslvt4.Text = ""

        olbelv11.Text = ""
        olbelv12.Text = ""
        olbelv13.Text = ""
        olbelv14.Text = ""
        olbelv21.Text = ""
        olbelv22.Text = ""
        olbelv23.Text = ""
        olbelv24.Text = ""

        olbelvt1.Text = ""
        olbelvt2.Text = ""
        olbelvt3.Text = ""
        olbelvt4.Text = ""
    End Sub

    Private Sub ClearLabelLine1Data()
        olbslv11.Text = ""
        olbslv12.Text = ""
        olbslv13.Text = ""
        olbslv14.Text = ""
        olbslv21.Text = ""
        olbslv22.Text = ""
        olbslv23.Text = ""
        olbslv24.Text = ""

        olbslvt1.Text = ""
        olbslvt2.Text = ""
        olbslvt3.Text = ""
        olbslvt4.Text = ""
    End Sub

    Private Sub ClearLabelLine2Data()
        olbelv11.Text = ""
        olbelv12.Text = ""
        olbelv13.Text = ""
        olbelv14.Text = ""
        olbelv21.Text = ""
        olbelv22.Text = ""
        olbelv23.Text = ""
        olbelv24.Text = ""

        olbelvt1.Text = ""
        olbelvt2.Text = ""
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
        Dim _PH4W As Integer = 164

        Dim _P1 As Integer = 80
        Dim _P2 As Integer = 110
        Dim _P3 As Integer = 110
        Dim _PLVTITLE As Integer = 133
        Dim _PLV1 As Integer = 90
        Dim _PLV2 As Integer = 90
        Dim _PLV3 As Integer = 90
        Dim _PLVTop As Integer = 90

        Dim _PLineW As Integer = 162 'opnline1
        Dim _PineEmpW As Integer = 159 'opnemp1
        Dim _PlineTimeW As Integer = 153 'opntime1
        Dim _PLineQaRate As Integer = 76 'opnqarate1
        Dim _PLineQAW As Integer = 82 'opnqa1

        Dim _PM1 As Integer = 49

        Dim _LineWidth As Integer = 0
        Dim _CaptionWidth As Integer = 0

        _P1 = _MeHeight * 0.1
        _P2 = _MeHeight * 0.1375
        _P3 = _MeHeight * 0.1375
        _PLVTITLE = _MeHeight * 0.16625
        _PLV1 = _MeHeight * 0.1125
        _PLV2 = _MeHeight * 0.1125
        _PLV3 = _MeHeight * 0.1125
        _PLVTop = _MeHeight * 0.1125

        _PM1 = (_P2 * 0.57647)

        If Me.opnl1.Visible And Me.opnl0.Visible Then
            Me.opnl1.Width = ((_MeWidth - 8) / 2)
        Else
        End If
        _LineHeaderWidth = opnsline.Width

        _PLineW = _LineHeaderWidth * 0.25632911
        _PineEmpW = _LineHeaderWidth * 0.25158227848
        _PlineTimeW = _LineHeaderWidth * 0.242088607
        _PLineQaRate = _LineHeaderWidth * 0.120253164
        _PLineQAW = _LineHeaderWidth * 0.129746835

        _P11W = _LineHeaderWidth * 0.507911392

        _PH1W = _LineHeaderWidth * 0.25632911 '162
        _PH2W = _LineHeaderWidth * 0.242088607 ' 153
        _PH3W = _LineHeaderWidth * 0.242088607 '153
        _PH4W = _LineHeaderWidth * 0.25949367088 '164


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
        opnslv3.Height = _PLV3
        opnslvtop.Height = _PLVTop

        opnline1.Width = _PLineW
        opnemp1.Width = _PineEmpW
        opntime1.Width = _PlineTimeW
        opnqarate1.Width = _PLineQaRate
        opnqa1.Width = _PLineQAW

        opnstargetqty1.Width = _P11W
        opnsprodqty1.Width = _P11W
        opnsprodspeed11.Width = _P11W

        opnsh1.Width = _PH1W
        opnsh2.Width = _PH2W
        opnsh3.Width = _PH3W
        opnsh4.Width = _PH3W


        olbslv11.Width = _PH1W
        olbslv12.Width = _PH2W
        olbslv13.Width = _PH3W
        olbslv14.Width = _PH4W

        olbslv21.Width = _PH1W
        olbslv22.Width = _PH2W
        olbslv23.Width = _PH3W
        olbslv24.Width = _PH4W



        olbslvt1.Width = _PH1W
        olbslvt2.Width = _PH2W
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
        opnqarate2.Width = _PLineQaRate
        opnqa2.Width = _PLineQAW

        opnetargetqty1.Width = _P11W
        opneprodqty1.Width = _P11W
        opneprodspeed11.Width = _P11W

        opneh1.Width = _PH1W
        opneh2.Width = _PH2W
        opneh3.Width = _PH3W
        opneh4.Width = _PH4W

        olbelv11.Width = _PH1W
        olbelv12.Width = _PH2W
        olbelv13.Width = _PH3W
        olbelv14.Width = _PH4W

        olbelv21.Width = _PH1W
        olbelv22.Width = _PH2W
        olbelv23.Width = _PH3W
        olbelv24.Width = _PH4W

        olbelvt1.Width = _PH1W
        olbelvt2.Width = _PH2W
        olbelvt3.Width = _PH3W
        olbelvt4.Width = _PH4W

        '-----------End Set Line 2
        Dim _ImageW As Integer = (_P11W * 0.34891)

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

        Dim _ImageEmpW As Integer = (_PineEmpW * 0.45283)
        Me.opcemp1.Width = _ImageEmpW
        Me.opcemp2.Width = _ImageEmpW

        Dim _ImageTimepW As Integer = (_PlineTimeW * 0.457516)
        Me.opctime1.Width = _ImageTimepW
        Me.opctime2.Width = _ImageTimepW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.4375 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Me.olbsline.Font = FFontLineH
        Me.olbemp1.Font = FFontLineH
        Me.olbtime1.Font = FFontLineH


        Me.olbeline.Font = FFontLineH
        Me.olbemp2.Font = FFontLineH
        Me.olbtime2.Font = FFontLineH


        Dim _FontLineHQA As Integer = _P1 * 0.3125 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.olbqa1.Font = FFontLineHQA
        Me.olbqa2.Font = FFontLineHQA
        '----S Font LV
        Dim _FontLV As Integer = _PLV1 * 0.35 ' 30 '90
        Dim FFontLV As New Font("Tahoma", _FontLV, FontStyle.Bold)

        olbsh1.Font = FFontLV
        olbsh2.Font = FFontLV
        olbsh3.Font = FFontLV
        olbsh4.Font = FFontLV

        olbslv11.Font = FFontLV
        olbslv12.Font = FFontLV
        olbslv13.Font = FFontLV
        olbslv14.Font = FFontLV

        olbslv21.Font = FFontLV
        olbslv22.Font = FFontLV
        olbslv23.Font = FFontLV
        olbslv24.Font = FFontLV



        olbslvt1.Font = FFontLV
        olbslvt2.Font = FFontLV
        olbslvt3.Font = FFontLV
        olbslvt4.Font = FFontLV

        olbeh1.Font = FFontLV
        olbeh2.Font = FFontLV
        olbeh3.Font = FFontLV
        olbeh4.Font = FFontLV

        olbelv11.Font = FFontLV
        olbelv12.Font = FFontLV
        olbelv13.Font = FFontLV
        olbelv14.Font = FFontLV

        olbelv21.Font = FFontLV
        olbelv22.Font = FFontLV
        olbelv23.Font = FFontLV
        olbelv24.Font = FFontLV



        olbelvt1.Font = FFontLV
        olbelvt2.Font = FFontLV
        olbelvt3.Font = FFontLV
        olbelvt4.Font = FFontLV

        '----E Font LV

        Dim FontHeaderLineSize As Integer = (_P2 * 0.472)
        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)

        olbstarget1.Font = FHeader
        olbstarget2.Font = FHeader

        olbetarget1.Font = FHeader
        olbetarget2.Font = FHeader

        olbsscan1.Font = FHeader
        olbsscan2.Font = FHeader
        olbsprodspeed1.Font = FHeader
        olbsprodspeed2.Font = FHeader

        olbescan1.Font = FHeader
        olbescan2.Font = FHeader
        olbeprodspeed1.Font = FHeader
        olbeprodspeed2.Font = FHeader

        'Dim FontHeaderP2 As Integer = (opnetarget1_1.Height * 0.40816)
        'Dim FontHeaderP3 As Integer = (_P3 * 0.757607)
        'Dim FontHeaderP4 As Integer = (opneheader.Height * 0.456748)

        'Dim FontHeaderP5 As Integer = (opnetarget3_1.Height * 0.887284)
        'Dim FontHeaderP6 As Integer = (opnetarget1_2.Height * 0.4375)
        'Dim FontHeaderP7 As Integer = ((_P4 / 2) * 0.55)

        'Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)
        'Dim FHeaderP2 As New Font("Tahoma", FontHeaderP2, FontStyle.Bold)
        'Dim FHeaderP3 As New Font("Tahoma", FontHeaderP3, FontStyle.Bold)
        'Dim FHeaderP4 As New Font("Tahoma", FontHeaderP4, FontStyle.Bold)
        'Dim FHeaderP5 As New Font("Tahoma", FontHeaderP5, FontStyle.Bold)
        'Dim FHeaderP6 As New Font("Tahoma", FontHeaderP6, FontStyle.Bold)
        'Dim FHeaderP7 As New Font("Tahoma", FontHeaderP7, FontStyle.Bold)


        'olbsline.Font = FHeader
        'olbeline.Font = FHeader

        'opnstarget1_1.Font = FHeaderP2
        'opnstarget1_2.Font = FHeaderP6
        'opnstarget2_1.Font = FHeaderP2
        'opnstarget3_1.Font = FHeaderP5

        'opnetarget1_1.Font = FHeaderP2
        'opnetarget1_2.Font = FHeaderP6
        'opnetarget2_1.Font = FHeaderP2
        'opnetarget3_1.Font = FHeaderP5

        'opnsprod1_1.Font = FHeaderP2
        'opnsprod1_2.Font = FHeaderP6
        'opnsprod2_1.Font = FHeaderP2
        'opnsprod1_2.Font = FHeaderP6

        'opneprod1_1.Font = FHeaderP2
        'opneprod1_2.Font = FHeaderP6
        'opneprod2_1.Font = FHeaderP2
        'opneprod2_2.Font = FHeaderP6

        'opnsper1_1.Font = FHeaderP2
        'opnsper1_2.Font = FHeaderP6
        'opnsper2_1.Font = FHeaderP2
        'opnsper2_2.Font = FHeaderP6

        'opneper1_1.Font = FHeaderP2
        'opneper1_2.Font = FHeaderP6
        'opneper2_1.Font = FHeaderP2
        'opneper2_2.Font = FHeaderP6

        'opnstargetqty1_1.Font = FHeaderP3
        'opnstargetqty2_1.Font = FHeaderP3

        'opnetargetqty1_1.Font = FHeaderP3
        'opnetargetqty2_1.Font = FHeaderP3

        'opnsprodqty1_1.Font = FHeaderP3
        'opnsprodqty2_1.Font = FHeaderP3

        'opneprodqty1_1.Font = FHeaderP3
        'opneprodqty2_1.Font = FHeaderP3

        'opnsperqty1_1.Font = FHeaderP4
        'opnsperqty2_1.Font = FHeaderP4

        'opneperqty1_1.Font = FHeaderP4
        'opneperqty2_1.Font = FHeaderP4

        'FTOrderNo_lbl.Font = FHeaderP7
        'FTOrderNo.Font = FHeaderP7

        'FDShipDate_lbl.Font = FHeaderP7
        'FDShipDate.Font = FHeaderP7

        'FTStyleNo.Font = FHeaderP7
        'FTStyleNo_lbl.Font = FHeaderP7

        'FTPORef.Font = FHeaderP7
        'FTPORef_lbl.Font = FHeaderP7

        'FTColorway.Font = FHeaderP7
        'FTColorway_lbl.Font = FHeaderP7

        'FTSizeCode.Font = FHeaderP7
        'FTSizeCode_lbl.Font = FHeaderP7

        'FNOrderQuantity.Font = FHeaderP7
        'FNOrderQuantity_lbl.Font = FHeaderP7

        'FNProdQty.Font = FHeaderP7
        'FNProdQty_lbl.Font = FHeaderP7


        'FTOrderNo2_lbl.Font = FHeaderP7
        'FTOrderNo2.Font = FHeaderP7

        'FDShipDate2_lbl.Font = FHeaderP7
        'FDShipDate2.Font = FHeaderP7

        'FTStyleNo2.Font = FHeaderP7
        'FTStyleNo2_lbl.Font = FHeaderP7

        'FTPORef2.Font = FHeaderP7
        'FTPORef2_lbl.Font = FHeaderP7

        'FTColorway2.Font = FHeaderP7
        'FTColorway2_lbl.Font = FHeaderP7

        'FTSizeCode2.Font = FHeaderP7
        'FTSizeCode2_lbl.Font = FHeaderP7

        'FNOrderQuantity2.Font = FHeaderP7
        'FNOrderQuantity2_lbl.Font = FHeaderP7

        'FNProdQty2.Font = FHeaderP7
        'FNProdQty2_lbl.Font = FHeaderP7
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

                    _Cmd = "      SELECT  MAX( A.FTOrderNo) AS FTOrderNo , A.FNHSysStyleId"
                    _Cmd &= vbCrLf & "FROM   "
                    _Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                    _Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                    _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                    _Cmd &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                    _Cmd &= vbCrLf & "    UNION "
                    _Cmd &= vbCrLf & " SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , PP.FTOrderNo ,'', B.FTColorway, B.FTSizeBreakDown"
                    _Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                    _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                    _Cmd &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP ON B.FTOrderProdNo = PP.FTOrderProdNo "
                    _Cmd &= vbCrLf & "   ) AS P  RIGHT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                    _Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    _Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                    _Cmd &= vbCrLf & "GROUP BY  A.FNHSysStyleId"

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    _FNSam = 0
                    _PriceCost = 0
                    _TotalCountStyle = 0
                    For Each R As DataRow In _oDt.Rows
                        _TotalCountStyle = _TotalCountStyle + 1

                        _OrderNo = R!FTOrderNo.ToString
                        _FNHSysStyleId = Integer.Parse(Val(R!FNHSysStyleId.ToString))

                        Dim _dtCost As DataTable
                        _Cmd = "   SELECT FNSam, FNCostPerMin, FNPrice"
                        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS P WITH(NOLOCK)"
                        _Cmd &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                        _dtCost = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

                        For Each Rxc As DataRow In _dtCost.Rows
                            _PriceCost = _PriceCost + Val(Rxc!FNPrice.ToString)
                            _FNSam = _FNSam + Val(Rxc!FNSam.ToString)
                            Exit For
                        Next

                        _dtCost.Dispose()

                    Next

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If

                    _TotalCountEmp = Integer.Parse(Val(Me.olbemp1.Text))

                    Dim _dtPrice As DataTable
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

                            Rxp!FNAmountMax = CDbl(Format(_AmtMax / _TotalCountEmp, "0.00"))
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
                                    _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / _TotalCountEmp), "0.00"))
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
                                        olbslv12.Text = Rxp!FNTargetQty.ToString
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
                                        olbslv22.Text = Rxp!FNTargetQty.ToString
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
                                olbslv12.Text = Rxp!FNTargetQty.ToString
                                olbslv13.Text = Rxp!FNActBalQty.ToString
                                olbslv14.Tag = "1"
                                olbslv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                Exit For
                            Next

                        End If

                        For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                            olbslvt1.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                            olbslvt2.Text = Rxp!FNTargetQty.ToString
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
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(A.FNMajorQty)+SUM(A.FNMinorQty))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
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
                                _QAPer = CDbl(Format(((R!FNQAActualQty - R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00"))
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

                    _Cmd = "      SELECT  MAX( A.FTOrderNo) AS FTOrderNo , A.FNHSysStyleId"
                    _Cmd &= vbCrLf & "FROM   "
                    _Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                    _Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                    _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                    _Cmd &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                    _Cmd &= vbCrLf & "    UNION "
                    _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , PP.FTOrderNo ,'', B.FTColorway, B.FTSizeBreakDown"
                    _Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                    _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                    _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP ON B.FTOrderProdNo = PP.FTOrderProdNo "
                    _Cmd &= vbCrLf & "  ) AS P  RIGHT OUTER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                    _Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                    _Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                    _Cmd &= vbCrLf & "GROUP BY  A.FNHSysStyleId"

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    _FNSam = 0
                    _PriceCost = 0
                    _TotalCountStyle = 0
                    For Each R As DataRow In _oDt.Rows
                        _TotalCountStyle = _TotalCountStyle + 1

                        _OrderNo = R!FTOrderNo.ToString
                        _FNHSysStyleId = Integer.Parse(Val(R!FNHSysStyleId.ToString))

                        Dim _dtCost As DataTable
                        _Cmd = "   SELECT FNSam, FNCostPerMin, FNPrice"
                        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS P WITH(NOLOCK)"
                        _Cmd &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                        _dtCost = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

                        For Each Rxc As DataRow In _dtCost.Rows
                            _PriceCost = _PriceCost + Val(Rxc!FNPrice.ToString)
                            _FNSam = _FNSam + Val(Rxc!FNSam.ToString)
                            Exit For
                        Next

                        _dtCost.Dispose()

                    Next

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If

                    _TotalCountEmp = Integer.Parse(Val(Me.olbemp2.Text))

                    Dim _dtPrice As DataTable

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

                            Rxp!FNAmountMax = CDbl(Format(_AmtMax / _TotalCountEmp, "0.00"))
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


                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'"
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
                                    _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / _TotalCountEmp), "0.00"))
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
                                        olbelv12.Text = Rxp!FNTargetQty.ToString
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
                                        olbelv22.Text = Rxp!FNTargetQty.ToString
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
                                olbelv12.Text = Rxp!FNTargetQty.ToString
                                olbelv13.Text = Rxp!FNActBalQty.ToString
                                olbelv14.Tag = "1"
                                olbelv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                Exit For
                            Next

                        End If


                        For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                            olbelvt1.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                            olbelvt2.Text = Rxp!FNTargetQty.ToString
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
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(A.FNMajorQty)+SUM(A.FNMinorQty))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
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
                                _QAPer = CDbl(Format(((R!FNQAActualQty - R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00"))
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
        Try
            Me.olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhour.Text)), "HH:mm:ss")
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
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "09" Then
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
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "09" Then
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

    Private Sub olbqa1_Click(sender As Object, e As EventArgs) Handles olbqa1.Click

    End Sub

    Private Sub olbslv14_Click(sender As Object, e As EventArgs) Handles olbslv14.Click

    End Sub
End Class
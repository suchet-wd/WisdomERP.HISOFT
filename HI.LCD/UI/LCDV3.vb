﻿
Imports System.Drawing
Imports System.Threading

Public Class LCDV3

    Private _StateFTStateDaily As Boolean = False
    Private _TimeSwitchtoSpeed As Integer = 0
    Private _TimeSwitchToHeader As Integer = 1
    Private _TotalEmpFromMasterLine2 As Integer = 0
    Private _TotalEmpHRmorningLine2 As Integer = 0
    Private StateLoad As Boolean = False

    Private _SystemFilePath As Boolean = False
    Property StateWindowsUser As Boolean
        Get
            Return _SystemFilePath
        End Get

        Set(value As Boolean)
            _SystemFilePath = value
        End Set

    End Property


    Public Sub New(Optional DataUnitSectId As Integer = 0, Optional DataLineNo As String = "", Optional DataFormularId As Integer = 0)



        L2LineId = DataUnitSectId
        LineNoL2 = DataLineNo
        L2FormularId = DataFormularId

        'opnget.Dock = System.Windows.Forms.DockStyle.Fill
        'opntalt.Dock = System.Windows.Forms.DockStyle.Fill
        'opntalt.Visible = False

        'L2olbsheader0.Visible = False
        'L2olbslvper11.Visible = False
        'L2olbslvper21.Visible = False
        'L2olbslvtper1.Visible = False

        'L2olbsheader1.Visible = True
        'L2olbslv11.Visible = True
        'L2olbslv21.Visible = True
        'L2olbslvt1.Visible = True

        'L2olbsheader02.Visible = False
        'L2olbslvtarget11.Visible = False
        'L2olbslvtarget21.Visible = False
        'L2olbslvttarget1.Visible = False

        'L2olbsheader2.Visible = True
        'L2olbslv14.Visible = True
        'L2olbslv24.Visible = True
        'L2olbslvt4.Visible = True





    End Sub


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


    Private _ActualdateL2 As String = ""
    ReadOnly Property ActualdateL2 As String
        Get
            Return _ActualdateL2
        End Get
    End Property

    Private _ActualNextDateL2 As String = ""
    ReadOnly Property ActualNextDateL2 As String
        Get
            Return _ActualNextDateL2
        End Get
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

    Private _TransactionDateL2 As String = ""
    Property TransactionDateL2 As String
        Get
            Return _TransactionDateL2
        End Get
        Set(value As String)
            _TransactionDateL2 = value
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

#End Region

#Region "Procedure "

    Private Sub L2GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer = 0
        Dim _Slary As Double = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 5)
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

        _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

        If _Time < Me.Line2CheckTimeINAL2 Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
        End If

        If _Time > Me.Line2CheckTimeINAL2 Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn2,'') <>''	"
        End If

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "
        _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

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

                _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
                _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "
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
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(L2LineId)) & "	"
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
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(L2LineId)) & "	"
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
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & "	"
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
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(L2LineId)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & "	"
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
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

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
        _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute1  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111) And  X1.FTInsDate < Convert(varchar(10),Getdate(),111)  ) As XX1 "
        _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute2  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111) And  X1.FTInsDate = Convert(varchar(10),Getdate(),111)  ) As XX2 "

        _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNAbsent) As FNAbsent  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111)  AND ISNULL(X1.FTIn1,'') ='' ) As XXT2 "

        _Qry &= vbCrLf & "    OUTER APPLY ( "
        _Qry &= vbCrLf & "                  Select TOP 1 FTOtIn, FTOtOut  "
        _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest As X1 With(NOLOCK)  "
        _Qry &= vbCrLf & "                  WHERE     X1.FNHSysEmpID=Emp.FNHSysEmpID   AND  X1.FTDateRequest = Convert(varchar(10),Getdate(),111)  ) As XXOT2 "


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
        _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

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
        _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Getdate()),111) ) "

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

            L2olbslvper11.Text = ""
            L2olbslvtarget11.Text = ""

            L2olbslv11.Text = ""

            L2olbslv13.Text = ""
            L2olbslv14.Text = ""
            L2olbslv21.Text = ""

            L2olbslvper21.Text = ""
            L2olbslvtarget21.Text = ""


            L2olbslv23.Text = ""
            L2olbslv24.Text = ""

            L2olbslvt1.Text = ""
            L2olbslvtper1.Text = ""
            L2olbslvttarget1.Text = ""

            L2olbslvt3.Text = ""
            L2olbslvt4.Text = ""


            L2olbtaktime.Text = ""
            L2lblgrade.Text = ""
        Catch ex As Exception

        End Try


    End Sub

    Private Sub L2ClearLabelLineData()
        L2olbslv11.Text = ""
        L2olbslvper11.Text = ""
        L2olbslvtarget11.Text = ""

        L2olbslv13.Text = ""
        L2olbslv14.Text = ""
        L2olbslv21.Text = ""


        L2olbslvper21.Text = ""
        L2olbslvtarget21.Text = ""

        L2olbslv23.Text = ""
        L2olbslv24.Text = ""

        L2olbslvt1.Text = ""
        L2olbslvtper1.Text = ""
        L2olbslvttarget1.Text = ""

        L2olbslvt3.Text = ""
        L2olbslvt4.Text = ""


    End Sub

    Private Sub L2otmLine2_Tick(sender As Object, e As EventArgs) Handles L2otmline1.Tick
        'Dim _Theard As New Thread(AddressOf CheckStateLine)
        '_Theard.Start()
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

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(L2LineId)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDateL2 & "' AND  FDEDate>='" & Me.TransactionDateL2 & "'  "
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
                        L2olbstarget2.Text = _TotalTargetPerHour.ToString
                    End If


                    Dim _DateNow As String = _TransactionDateL2

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

                    _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(L2LineId)) & ",'" & _DateNow & "' "
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

                    Dim _dtPrice As DataTable


                    If IncenFormulaIdLine2L2 <= 0 Then
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
                        _Qry &= vbCrLf & "   WHERE FNHSysIncenFormulaId=" & IncenFormulaIdLine2L2 & ""
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

                            If Line2EmpCountMoneyL2 > 0 Then
                                Rxp!FNAmountMax = CDbl(Format(_AmtMax / Line2EmpCountMoneyL2, "0.00"))
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


                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDateL2 & "'"
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

                        If _EndTime <> "" And _EndTime <= Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 5) And Val(_Hour) >= 8 Then
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


                                    If Line2EmpCountMoneyL2 > 0 Then
                                        _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / Line2EmpCountMoneyL2), "0.00"))
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

                        L2olbsscan1.Text = _TotalProd.ToString
                        L2olbsscan2.Text = _Prod.ToString

                        Call L2ClearLabelLineData()

                        If _dtPrice.Select("FNActBalQty >0 AND FNLVSeq<>2", "FNLVSeq").Length > 0 Then

                            Dim _RowIdx As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNActBalQty >0 AND FNLVSeq<>2", "FNLVSeq")
                                _RowIdx = _RowIdx + 1
                                If _RowIdx > 2 Then
                                    Exit For
                                End If

                                Select Case _RowIdx
                                    Case 1

                                        If Val(Rxp!FNLVSeq.ToString) <= 1 Then
                                            L2olbslvper11.Text = Format(Val(Rxp!FNEndEff.ToString), "0.00")
                                        Else
                                            L2olbslvper11.Text = Format(Val(Rxp!FNStartEff.ToString), "0.00")
                                        End If


                                        L2olbslvtarget11.Text = Rxp!FNTargetQty.ToString

                                        L2olbslv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                        L2olbslv13.Text = Rxp!FNActBalQty.ToString
                                        L2olbslv14.Tag = "0"

                                        If Val(Rxp!FNAmount.ToString) = 0 Then

                                            L2olbslv14.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"

                                        Else

                                            L2olbslv14.ForeColor = Drawing.Color.Blue
                                            L2olbslv14.Tag = "1"
                                            L2olbslv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString

                                        End If

                                    Case 2


                                        L2olbslvper21.Text = Format(Val(Rxp!FNStartEff.ToString), "0.00")
                                        L2olbslvtarget21.Text = Rxp!FNTargetQty.ToString


                                        L2olbslv21.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                                        L2olbslv23.Text = Rxp!FNActBalQty.ToString
                                        L2olbslv24.Tag = "0"

                                        If Val(Rxp!FNAmount.ToString) = 0 Then
                                            L2olbslv24.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        Else
                                            L2olbslv24.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        End If

                                        'Case 3
                                        '    L2olbslv31.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                                        '    L2olbslv32.Text = Rxp!FNTargetQty.ToString
                                        '    L2olbslv33.Text = Rxp!FNActBalQty.ToString
                                        '    L2olbslv34.Tag = "0"
                                        '    If Val(Rxp!FNAmount.ToString) = 0 Then
                                        '        L2olbslv34.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        '    Else
                                        '        L2olbslv34.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        '    End If

                                End Select

                            Next

                        Else

                            L2olbslv14.Tag = "0"
                            For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")


                                L2olbslvper11.Text = Format(Val(Rxp!FNStartEff.ToString), "0.00")
                                L2olbslvtarget11.Text = Rxp!FNTargetQty.ToString



                                L2olbslv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                L2olbslv13.Text = Rxp!FNActBalQty.ToString
                                L2olbslv14.Tag = "1"
                                L2olbslv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                Exit For
                            Next

                        End If

                        For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")

                            L2olbslvtper1.Text = Format(Val(Rxp!FNStartEff.ToString), "0.00")
                            L2olbslvttarget1.Text = Rxp!FNTargetQty.ToString


                            L2olbslvt1.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                            L2olbslvt3.Text = Rxp!FNActBalQty.ToString
                            L2olbslvt4.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"

                            Exit For

                        Next

                        Me._Line2DataL2 = _dtPrice.Copy
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
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 5)

        If _Time >= TimeInML2 Then

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday


            If _Time >= TimeOutML2 Then
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInML2), CDate(Me.ActualdateL2 & "  " & TimeOutML2))

            Else
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInML2), CDate(Me.ActualdateL2 & "  " & _Time))
            End If


            If _Time > TimeInAL2 Then

                If _Time >= TimeOutAL2 Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInAL2), CDate(Me.ActualdateL2 & "  " & TimeOutAL2))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInAL2), CDate(Me.ActualdateL2 & "  " & _Time))
                End If

            End If

            If _Time > TimeInOTL2 And TimeInOTL2 <> "" And TimeOutOTL2 <> "" Then
                If _Time >= TimeOutOTL2 Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInOTL2), CDate(Me.ActualdateL2 & "  " & TimeOutOTL2))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInOTL2), CDate(Me.ActualdateL2 & "  " & _Time))
                End If
            End If


        End If

        Return SumMinute
    End Function

    Private Delegate Sub DelegateL2LoadTime()
    Private Sub L2CheckLoadTime()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateL2LoadTime(AddressOf L2CheckLoadTime), New Object() {})
        Else
            Try

                Dim _Qry As String
                _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
                Me.L2olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub L2ottime_Tick(sender As Object, e As EventArgs) Handles L2ottime.Tick
        Try
            'Me.L2olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.L2olbhour.Text)), "HH:mm:ss")
            L2CheckLoadTime()
        Catch ex As Exception
        End Try
    End Sub


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

        If Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 2) <> Line2CheckTimeL2 Or (Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 5) = "08:30") Then

            Line2CheckTimeL2 = Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 2)
            Call L2GetEmployeeActualFromHR()

        End If

    End Sub

    Private Sub L2otmLine2checkemp10_Tick(sender As Object, e As EventArgs) Handles L2otmline1checkemp10.Tick
        If Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 2) = "10" Then

            Call L2GetEmployeeActualFromHR()

        End If
    End Sub

    Private Sub L2otmLine2checkemp11_Tick(sender As Object, e As EventArgs) Handles L2otmline1checkemp11.Tick
        If Microsoft.VisualBasic.Left(Me.L2olbhour.Text, 2) = "11" Then

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

                Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")

                If _CurrentTime <> "" And _CurrentTime >= "08:00" Then

                    If _CurrentTime >= TimeInML2 And _CurrentTime <= TimeOutML2 Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInML2), CDate(Me.ActualdateL2 & "  " & _CurrentTime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInML2), CDate(Me.ActualdateL2 & "  " & TimeOutML2))
                    End If

                    If _CurrentTime >= TimeInAL2 Then
                        If _CurrentTime <= TimeOutAL2 Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInAL2), CDate(Me.ActualdateL2 & "  " & _CurrentTime))
                        End If
                    End If

                    If _TotalH > 8 Then

                        If _CurrentTime >= "17:30" Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualdateL2 & "  " & TimeInOTL2), CDate(Me.ActualdateL2 & "  " & _CurrentTime))
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


    Private Sub L2otmcheckswitchtoheader_Tick(sender As Object, e As EventArgs) Handles L2otmcheckswitchtoheader.Tick

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

    Private Sub LCDV1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

        If StateLoad = False Then Exit Sub



    End Sub

    Private Sub L2SwipShowData()

        If StateLoad = False Then Exit Sub

        L2olbsheader0.Visible = Not L2olbsheader0.Visible
        L2olbslvper11.Visible = Not L2olbslvper11.Visible
        L2olbslvper21.Visible = Not L2olbslvper21.Visible
        L2olbslvtper1.Visible = Not L2olbslvtper1.Visible

        L2olbsheader1.Visible = Not L2olbsheader1.Visible
        L2olbslv11.Visible = Not L2olbslv11.Visible
        L2olbslv21.Visible = Not L2olbslv21.Visible
        L2olbslvt1.Visible = Not L2olbslvt1.Visible

        L2olbsheader02.Visible = Not L2olbsheader02.Visible
        L2olbslvtarget11.Visible = Not L2olbslvtarget11.Visible
        L2olbslvtarget21.Visible = Not L2olbslvtarget21.Visible
        L2olbslvttarget1.Visible = Not L2olbslvttarget1.Visible

        L2olbsheader2.Visible = Not L2olbsheader2.Visible
        L2olbslv14.Visible = Not L2olbslv14.Visible
        L2olbslv24.Visible = Not L2olbslv24.Visible
        L2olbslvt4.Visible = Not L2olbslvt4.Visible

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

        L2olbstarget01.Width = L2opcstargetqty1.Width / 2
        L2olbstarget1.Width = L2olbstarget01.Width
        L2olbstarget2.Width = L2olbstarget01.Width
        L2olbsscan02.Width = L2olbstarget01.Width
        L2opnstargetqty1.Width = _MeWidth * 0.57993730407523514

        L2olbprodper.Width = L2opndescprod.Width / 2
        L2olbstarget2Eff.Width = L2olbprodper.Width
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

        L2olbsheader0.Width = _PH0W
        L2olbsheader02.Width = _PH2W
        L2olbsheader1.Width = _PH1W
        L2olbsheader2.Width = _PH3W
        L2olbsheader3.Width = _PH4W

        L2olbslvper11.Width = _PH0W
        L2olbslvtarget11.Width = _PH2W

        L2olbslv11.Width = _PH1W
        L2olbslvper11.Width = _PH1W
        L2olbslvtarget11.Width = _PH1W

        L2olbslv13.Width = _PH3W
        L2olbslv14.Width = _PH4W

        L2olbslvper21.Width = _PH0W
        L2olbslvtarget21.Width = _PH2W
        L2olbslvper21.Width = _PH1W
        L2olbslvtarget21.Width = _PH1W

        L2olbslv21.Width = _PH1W
        L2olbslv23.Width = _PH3W
        L2olbslv24.Width = _PH4W

        L2olbslvtper1.Width = _PH0W
        L2olbslvttarget1.Width = _PH2W
        L2olbslvt1.Width = _PH1W
        L2olbslvt3.Width = _PH3W
        L2olbslvt4.Width = _PH4W

        _CaptionWidth = _LineWidth / 2

        L2olbsheader0.Width = _PH0W
        L2olbslvper11.Width = _PH0W
        L2olbslvper21.Width = _PH0W
        L2olbslvtper1.Width = _PH0W

        L2olbsheader02.Width = _PH2W
        L2olbslvtarget11.Width = _PH2W
        L2olbslvtarget21.Width = _PH2W
        L2olbslvttarget1.Width = _PH2W
        '-----------End Set Line 1

        Dim _ImageW As Integer = (_P11W * 0.42056075)

        'Me.opcstargetqty1.Width = _ImageW


        Me.L2opcsscan1.Width = _ImageW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.45 ' 30 '90
        Dim _FontLineH2 As Integer = _P1 * 0.3 ' 30 '90
        Dim _FontLineH3 As Integer = _P1 * 0.25 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Dim FFontLineH2 As New Font("Tahoma", _FontLineH2, FontStyle.Bold)
        Dim FFontLineH3 As New Font("Tahoma", _FontLineH3, FontStyle.Bold)

        Me.L2olbsline.Font = FFontLineH
        Me.L2olbemp1.Font = FFontLineH2
        Me.L2olbemp1incentive.Font = FFontLineH
        Me.L2olbtime1.Font = FFontLineH

        L2olbprodper.Font = FFontLineH3
        L2olbqrate.Font = FFontLineH3

        Dim _FontLineHQA As Integer = _P1 * 0.46 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.L2olbqa1.Font = FFontLineHQA
        L2olbstarget2Eff.Font = FFontLineHQA

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

        Dim _FontLV001 As Integer = _PLV1 * 0.3635952756 ' 30 '90
        Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        Dim _FontLV002 As Integer = _PLV1 * 0.3635952756 ' 30 '90
        Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        Dim _FontLV003 As Integer = _PLV1 * 0.3635952756 ' 30 '90
        Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)

        L2olbsheader0.Font = FFontLVTile
        L2olbsheader02.Font = FFontLVTile
        L2olbsheader1.Font = FFontLVTile
        L2olbsheader2.Font = FFontLVTile
        L2olbsheader3.Font = FFontLVTile

        L2olbslvper11.Font = FFontLV001
        L2olbslvtarget11.Font = FFontLV001
        L2olbslv11.Font = FFontLV001
        L2olbslv13.Font = FFontLV002
        L2olbslv14.Font = FFontLV003

        L2olbslvper21.Font = FFontLV001
        L2olbslvtarget21.Font = FFontLV001
        L2olbslv21.Font = FFontLV001
        L2olbslv23.Font = FFontLV002
        L2olbslv24.Font = FFontLV003

        L2olbslvtper1.Font = FFontLV001
        L2olbslvttarget1.Font = FFontLV001
        L2olbslvt1.Font = FFontLV001
        L2olbslvt3.Font = FFontLV002
        L2olbslvt4.Font = FFontLV003

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
            Me.L2opnqa1.BackColor = Drawing.Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(L2lblgrade.Text) >= 99.0)

                    Me.L2opnbunus.BackColor = Drawing.Color.FromArgb(0, 192, 0)

                Case (Val(L2olbqa1.Text) < 99.0) And (Val(L2olbqa1.Text) >= 95.0)

                    Me.L2opnbunus.BackColor = Drawing.Color.FromArgb(255, 128, 0)

                Case Else

                    Me.L2opnbunus.BackColor = Drawing.Color.Red

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDV2_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Public Sub SetData()

        Try
            'Call SwipShowData()
            Call L2ClearLabelData()

            Dim _Qry As String = ""

            If LineNoL2 <> "" Then


                _Qry = "SELECT " & HI.UL.ULDate.FormatDateDB & " "
                TransactionDateL2 = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

                _ActualdateL2 = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
                _ActualNextDateL2 = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

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


                _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
                Me.L2olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

                Me.L2ottime.Enabled = True

                Me.L2otmcheckswitchtoheader.Enabled = False
                Me.L2otmcheckswitchtospeed.Enabled = False

                If _TimeSwitchtoSpeed > 0 Then

                    _TimeSwitchtoSpeed = _TimeSwitchtoSpeed * (60 * 1000)

                    L2otmcheckswitchtospeed.Interval = _TimeSwitchtoSpeed

                    If _TimeSwitchToHeader > 0 Then

                        _TimeSwitchToHeader = _TimeSwitchToHeader * (60 * 1000)
                        L2otmcheckswitchtospeed.Interval = _TimeSwitchToHeader

                    End If

                    Me.L2otmcheckswitchtospeed.Enabled = True

                End If

                L2otmcheckswitchtoheader.Enabled = True

                L2olbsline.Text = "L." & Microsoft.VisualBasic.Right(LineNoL2, 2)
                Dim _TotalCountEmp As Integer
                _Qry = "  SELECT Sum(1) AS CountEmp"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ""
                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
                _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"


                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(L2LineId)) & ") "
                _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

                _Qry &= vbCrLf & "	  ) "

                _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
                _TotalEmpFromMasterLine2 = _TotalCountEmp

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

                Dim _Theard1 As New Thread(AddressOf CheckStateLineL2)
                _Theard1.Start()

            End If
            StateLoad = True
        Catch ex As Exception

        End Try

    End Sub
#End Region




End Class

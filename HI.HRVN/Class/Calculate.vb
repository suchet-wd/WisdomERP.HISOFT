Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Threading
Imports System.Globalization.DateTimeFormatInfo
Imports Microsoft.VisualBasic
Imports System.Math
Imports System.Text


Public NotInheritable Class Calculate

    Private Enum eTypeInsuranceVN As Integer
        eSocialInsurance = 0
        eHealthInsurance = 1
        eUnemploymentInsurance = 2
        eUnionInsurance = 3
    End Enum

    Public Enum eTypeRace As Integer
        eThai = 0
        eLaos = 1
        eMyanma = 2
        eCambodia = 3
        eVietnam = 4
    End Enum

#Region "Property"
    Private Shared _ActualDate As String = ""
    Private Shared ReadOnly Property ActualDate() As String
        Get
            If _ActualDate = "" Then
                _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
            End If
            Return _ActualDate
        End Get
    End Property

    Private Shared _ActualNextdate As String = ""
    Private Shared ReadOnly Property ActualNextdate() As String
        Get
            If _ActualNextdate = "" Then
                _ActualNextdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")
            End If
            Return _ActualNextdate
        End Get
    End Property

    Private Shared _ActualBeforedate As String = ""
    Private Shared ReadOnly Property AcBeforedate() As String
        Get
            If _ActualBeforedate = "" Then
                _ActualBeforedate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,-1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")
            End If
            Return _ActualBeforedate
        End Get
    End Property

    Private Shared _OTTimeCtrl As DataTable = Nothing
    Private Shared ReadOnly Property _LoadOTTimeCtrl() As DataTable
        Get
            If _OTTimeCtrl Is Nothing Then
                Dim _Qry As String
                _Qry = " SELECT     FTCfgOTSet,FTCfgOTBegin,FTCfgOTEnd,FNHSysEmpTypeId,FTStatePay  "
                _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound WITH (NOLOCK)"
                _OTTimeCtrl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If
            Return _OTTimeCtrl
        End Get
    End Property

    Private Shared _OTPayOver As DataTable = Nothing
    Private Shared ReadOnly Property LoadOTPayOver() As DataTable
        Get
            If _OTPayOver Is Nothing Then
                Dim _Qry As String
                _Qry = " SELECT     FTStatePayOTOverRequest,FNTimeSacanMin,FNHSysEmpTypeId  "
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTPayOverRequest WITH (NOLOCK)"
                _OTPayOver = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If
            Return _OTPayOver
        End Get
    End Property

    Private Shared _EmpTypeWeekly As DataTable = Nothing
    Private Shared ReadOnly Property LoadEmpTypeWeekly(SDate As String, EDate As String) As DataTable
        Get

            Dim _Qry As String
            _Qry = "SELECt   FNHSysEmpTypeId,FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(SDate) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(EDate) & "' "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Return _EmpTypeWeekly
        End Get
    End Property

    Private Shared _SysHoliday As DataTable = Nothing
    Private Shared ReadOnly Property LoadSysHoliday(_PayYear As String) As DataTable
        Get
            If _SysHoliday Is Nothing Then
                Dim _Qry As String
                _Qry = "SELECt   FDHolidayDate,FNHolidayType AS FTHldType , Isnull(FNSpecialMoney,0) AS FNSpecialMoney , Isnull(FTPayTerm , '') AS FTPayTerm   "
                _Qry &= vbCrLf & " FROM THRMHoliday WITH(NOLOCK) "
                _Qry &= vbCrLf & "Where LEFT(FDHolidayDate,4) ='" & _PayYear & "'"

                _SysHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            End If
            Return _SysHoliday
        End Get
    End Property

    Private Shared _SocailRateKM As DataTable = Nothing
    Private Shared ReadOnly Property LoadSocailRateKM() As DataTable
        Get
            If _SocailRateKM Is Nothing Then
                Dim _Qry As String
                _Qry = "SELECt   FNSocialStartRange, FNSocialEndRange, FNSocialBase, FNSocialAmt  "
                _Qry &= vbCrLf & " FROM   THRMCfgSocailKMRate WITH(NOLOCK) "

                _SocailRateKM = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If
            Return _SocailRateKM
        End Get
    End Property

    Private Shared _WelfareKM As DataTable = Nothing
    Private Shared ReadOnly Property LoadWelfareKM(EmpType As Integer) As DataTable
        Get
            If _WelfareKM Is Nothing Then

                Dim _Qry As String
                _Qry = "SELECt  top 1  FNHSysWelfareId, FNHSysEmpTypeId, FNAttendanceAllowance, FNMealAllowance, FNCarAllowance, FNChildCareAmt, FNChildCareStartAge, FNChildCareEndAge, FNChildCareMaxPeople"
                _Qry &= vbCrLf & " FROM   THRMCfgWelfareVN WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId =" & Integer.Parse(Val(EmpType)) & ""

                _WelfareKM = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            End If
            Return _WelfareKM
        End Get
    End Property

    Private Shared WorkAge_Rate As DataTable = Nothing
    Private Shared ReadOnly Property GetWorkAgeRate() As DataTable
        Get
            If WorkAge_Rate Is Nothing Then
                Dim _Qry As String
                _Qry = "SELECt   FNWorkAgeStart, FNWorkAgeEnd, FNWorkAgeAmt"
                _Qry &= vbCrLf & " FROM   THRMCfgWorkAgeSalary WITH(NOLOCK) "

                WorkAge_Rate = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If

            Return WorkAge_Rate
        End Get
        'Set(value As DataTable)
        '    OT_Round = value
        'End Set
    End Property

    Private Shared _TimeShiftControl As DataTable = Nothing
    Private Shared ReadOnly Property LoadTimeShiftControl As DataTable
        Get
            If _TimeShiftControl Is Nothing Then
                Dim _Qry As String
                Dim _ActualDate As String = ActualDate
                Dim _ActualNextDate As String = ActualNextdate

                _Qry = "   SELECT   FNHSysShiftID, ISNULL(FNScanCtrl,0) AS FNScanCtrl"
                _Qry &= vbCrLf & "   , CASE WHEN ISNULL(FTOverClock,'') <> '' THEN  '" & _ActualDate & "' + '  ' + FTOverClock  ELSE '' END  AS FTOverClock "
                _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTOverClock,'') <>'' THEN '" & _ActualDate & "'  + '  ' + FTOverClock  ELSE '' END As FTOverClock  "

                _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTIn1,'') <> '' THEN  (CASE WHEN (FTIn1 >='00:00' AND  FTIn1 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END)  + '  ' + FTIn1  ELSE '' END AS ChkIn1 "
                _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTOut1,'') <> '' THEN (CASE WHEN (FTOut1 >='00:00' AND  FTOut1 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTOut1 ELSE '' END AS ChkOut1 "
                _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTIn2,'') <> '' THEN (CASE WHEN (FTIn2 >='00:00' AND  FTIn2 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTIn2  ELSE '' END As ChkIn2  "
                _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTOut2,'') <> '' THEN (CASE WHEN (FTOut2 >='00:00' AND  FTOut2 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTOut2  ELSE '' END AS ChkOut2 "


                _Qry &= vbCrLf & ",ISNULL(FCHour,0) As WorkTimePerDay"
                _Qry &= vbCrLf & "   FROM  THRMTimeShift WITH(NOLOCK)"

                _TimeShiftControl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If
            Return _TimeShiftControl
        End Get
    End Property

    Private Shared _ConfigLateTimeDeduct As DataTable = Nothing
    Private Shared ReadOnly Property LoadConfigLateTimeDeduct() As DataTable
        Get
            If _ConfigLateTimeDeduct Is Nothing Then
                Dim _Qry As String

                _Qry = "   Select  FTCfgLateCode, FNRateBegin, FNRateEnd, FTStaDeduct, FNRateDeduct, FTStaActive"
                _Qry &= vbCrLf & "   FROM  THRMConfigLateSet WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE (FTStaActive = '1')"

                _ConfigLateTimeDeduct = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If
            Return _ConfigLateTimeDeduct

        End Get

    End Property

    Private Shared _ConfigWelfareVN As System.Data.DataTable = Nothing '...Config Attendance Allowance, Car Allowance, Meal Allowance
    Private Shared ReadOnly Property LoadConfigWelfareVN() As System.Data.DataTable
        Get
            If _ConfigWelfareVN Is Nothing Then
                Dim TSQL As String
                TSQL = ""
                TSQL = "SELECT A.FNHSysEmpTypeId AS FNHSysEmpTypeId,"
                TSQL &= Environment.NewLine & "        A.FNAttendanceAllowance AS FNAttendanceAllowance,"
                TSQL &= Environment.NewLine & "        A.FNCarAllowance AS FNCarAllowance,"
                TSQL &= Environment.NewLine & "        A.FNMealAllowance AS FNMealAllowance"
                TSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgWelfareVN AS A (NOLOCK)"

                _ConfigWelfareVN = HI.Conn.SQLConn.GetDataTable(TSQL, HI.Conn.DB.DataBaseName.DB_HR)

            End If

            Return _ConfigWelfareVN

        End Get
    End Property

    Private Shared _ConfigAllowanceProbation As System.Data.DataTable = Nothing '...Config Skill Rate, Harmful Rate after pass probation
    Private Shared ReadOnly Property LoadConfigAllowanceProbation() As System.Data.DataTable
        Get
            If _ConfigAllowanceProbation Is Nothing Then
                Dim sSQL As String
                sSQL = ""
                sSQL = "SELECT (SELECT L1.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L1 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L1.FTKeyCode = N'Cfg_HarmfulRate')  AS FNHarmfulRate,"
                sSQL &= Environment.NewLine & "       (SELECT L2.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L2 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L2.FTKeyCode = N'Cfg_SkillRate')  AS FNSkillRate,"
                sSQL &= Environment.NewLine & "       (SELECT L3.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L3 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L3.FTKeyCode = N'Cfg_BasicSalaryMax') AS FNMaximumBasicSalaries,"
                sSQL &= Environment.NewLine & "       (SELECT L4.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L4 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L4.FTKeyCode = N'Cfg_ModPersonRateVN') AS FNModPersonTaxRate,"
                sSQL &= Environment.NewLine & "       (SELECT L5.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L5 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L5.FTKeyCode = N'Cfg_ModChildAllowanceRateVN') AS FNModChildAllowanceTaxRate,"
                sSQL &= Environment.NewLine & "       (SELECT L6.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L6 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L6.FTKeyCode = N'Cfg_ThaiWorkerNoWorkpermitTaxRate') AS FNThaiWorkerNoWorkpermitTaxRate"

                _ConfigAllowanceProbation = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_HR)

            End If

            Return _ConfigAllowanceProbation

        End Get

    End Property

    Public Shared Sub DisposeObject()
        Try
            _ConfigLateTimeDeduct = Nothing
            _TimeShiftControl = Nothing
            _SysHoliday = Nothing
            _OTPayOver = Nothing
            _OTTimeCtrl = Nothing
            _ConfigAllowanceProbation = Nothing
            _ActualBeforedate = ""
            _ActualNextdate = ""
            _ActualNextdate = ""
        Catch ex As Exception

        End Try
        Try
            WorkAge_Rate = Nothing
            _SocailRateKM = Nothing
            _WelfareKM = Nothing
        Catch ex As Exception
        End Try

    End Sub

#End Region

#Region "Function"

    Public Shared Function HelperRoundUpBasicSalary(ByVal pFNNetpayOrg As Double, Optional ByVal pFNAmntCondRoundUp As Double = 100.0) As Double
        Dim FNNetpay As Double = 0.0
        Try
            Dim FNRemainder As Double = 0.0
            FNRemainder = pFNNetpayOrg Mod pFNAmntCondRoundUp

            Select Case True
                Case FNRemainder > 0
                    FNNetpay = pFNNetpayOrg + (pFNAmntCondRoundUp - FNRemainder)
                Case Else
                    FNNetpay = pFNNetpayOrg
            End Select

            Return FNNetpay
        Catch ex As Exception
            Return FNNetpay
        End Try
    End Function

    Public Shared Function HelperRoundUpNetpay(ByVal pFNNetpayOrg As Double, Optional ByVal pFNAmntCondRoundUp As Double = 500.0, Optional ByVal pFNAmntRoundUp As Double = 1000.0) As Double
        Dim FNNetpay As Double = 0.0
        Try
            Dim FNRemainder As Double = 0.0

            FNRemainder = pFNNetpayOrg Mod pFNAmntCondRoundUp

            Select Case True
                Case FNRemainder < pFNAmntCondRoundUp

                    '...CEILING ...pFNAmntCondRoundUp
                    'FNNetpay = pFNNetpayOrg + (pFNAmntCondRoundUp - FNRemainder)
                    FNNetpay = (pFNNetpayOrg - FNRemainder) + pFNAmntCondRoundUp

                Case FNRemainder > pFNAmntCondRoundUp And FNRemainder < pFNAmntRoundUp

                    '...CEILING
                    FNNetpay = (pFNNetpayOrg - FNRemainder) + pFNAmntRoundUp

                Case Else
                    '...nothing
            End Select

        Catch ex As Exception
            FNNetpay = pFNNetpayOrg
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return FNNetpay

    End Function

    Public Shared Function HelperValidtePaymentIncomePayFullMonth(ByVal pFNHsysEmpId As Integer, ByVal pStartDateTr As String, ByVal pEndDateTr As String) As Boolean
        Dim bValidatePayment As Boolean = False
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()
            Dim sSQL As String

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("SELECT CASE WHEN ISDATE(ISNULL((SELECT TOP 1 A.FTDateTrans")
            oStrBuilder.AppendLine("                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans AS A (NOLOCK)")
            oStrBuilder.AppendLine(String.Format("                                WHERE A.FNHSysEmpID = {0}", pFNHsysEmpId))
            oStrBuilder.AppendLine("					                   AND A.FNTimeMin = 0")
            oStrBuilder.AppendLine(String.Format("                                       AND (A.FTDateTrans <= '{0}' AND A.FTDateTrans >= '{1}')", {pEndDateTr, pStartDateTr}))
            oStrBuilder.AppendLine(String.Format("                                       AND A.FNCutAbsent = {0}", 480))
            oStrBuilder.AppendLine("						               AND NOT EXISTS (SELECT 'T'")
            oStrBuilder.AppendLine("                                                       FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS L1 (NOLOCK)")
            oStrBuilder.AppendLine("                                                       WHERE A.FNHSysEmpID = L1.FNHSysEmpID")
            oStrBuilder.AppendLine("		                                                     AND A.FTDateTrans = L1.FTDateTrans)), '')) = 1 THEN 'N' ELSE 'Y' END AS FTChkValidatePayment;")

            sSQL = ""
            sSQL = oStrBuilder.ToString()

            If HI.Conn.SQLConn.GetField(sSQL, HI.Conn.DB.DataBaseName.DB_HR, "N") = "Y" Then
                bValidatePayment = True
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bValidatePayment

    End Function

    Public Shared Function LoadWeekday(ByVal pmFieldDate As String, Optional ByVal pmDBName As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_HR) As Integer
        Try
            Return HI.Conn.SQLConn.GetField("SELECT CASE WHEN ISDATE('" & pmFieldDate & "') = 1 THen  DATEPART (WEEKDAY ,'" & pmFieldDate & "') ELSE -1 END  AS WeekdayAD", pmDBName, "-1")
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Shared Function CheckDateHoliday(ByVal pmDate As String) As Boolean
        Dim _QrySql As String
        Try
            If LoadWeekdayAD(pmDate) = Microsoft.VisualBasic.vbSunday Then
                Return True
            Else
                _QrySql = "SELECT FTDate FROM dbo.THRMCalendar WHERE dbo.THRMCalendar.FTDate = '" & pmDate & "'"

                If HI.Conn.SQLConn.GetField(_QrySql, Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function LoadWeekdayAD(ByVal pmDate As String) As Integer
        Try
            Dim Day1 As String
            Dim DateS2 As String

            DateS2 = CStr(pmDate)
            Day1 = HI.UL.ULDate.ConvertEnDB(pmDate)
            Return Weekday(CDate(pmDate))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function LoadInsuranceVNRate() As Boolean
        Dim bLoad As Boolean = False
        Dim sSQL As String
        Try
            Dim dtInsuranceRate As System.Data.DataTable

            sSQL = ""
            sSQL = "SELECT A.[FNInsuranceVN]"
            sSQL &= Environment.NewLine & "       ,B.[FTNameEN] AS [FTInsuranceDesc]"
            sSQL &= Environment.NewLine & "       ,A.[FNEmployeeRate]"
            sSQL &= Environment.NewLine & "       ,A.[FNEmployerRate]"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMInsuranceVN] AS A (NOLOCK) INNER JOIN (SELECT L1.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..[HSysListData] AS L1 (NOLOCK) WHERE L1.FTListName = N'FNInsuranceVN') AS B ON A.FNInsuranceVN = B.FNListIndex"
            sSQL &= Environment.NewLine & "WHERE A.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "ORDER BY A.FNInsuranceVN ASC;"

            dtInsuranceRate = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_HR)

            If Not DBNull.Value.Equals(dtInsuranceRate) AndAlso dtInsuranceRate.Rows.Count > 0 Then
                Dim nIndex As Integer

                nIndex = 0
                For Each oDataRow As System.Data.DataRow In dtInsuranceRate.Rows

                    ReDim Preserve HCfg.HCfg_InsuranceVNRate(nIndex)

                    HCfg.HCfg_InsuranceVNRate(nIndex).FNInsuranceVN = Val((oDataRow!FNInsuranceVN).ToString())
                    HCfg.HCfg_InsuranceVNRate(nIndex).FTInsuranceDesc = (oDataRow!FTInsuranceDesc).ToString()
                    HCfg.HCfg_InsuranceVNRate(nIndex).FNEmployeeRate = Val((oDataRow!FNEmployeeRate).ToString())
                    HCfg.HCfg_InsuranceVNRate(nIndex).FNEmployerRate = Val((oDataRow!FNEmployerRate).ToString())

                    nIndex = nIndex + 1

                Next

            End If

        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End Try

        Return bLoad

    End Function

    Public Shared Function LoadSocialRate() As Boolean
        Try
            Dim _Qry As String
            Dim _dt As DataTable

            _Qry = " SELECT  TOP 1 FNSocialRate "
            _Qry &= vbCrLf & " , FNSocialMin"
            _Qry &= vbCrLf & " , FNSocialMax"
            _Qry &= vbCrLf & " , FNContributedToTheFund"
            _Qry &= vbCrLf & " ,FNContributedIncomeMax"
            _Qry &= vbCrLf & " , FTContributedDeducIDNo"
            _Qry &= vbCrLf & " , FTContributedDeducCmpCode"
            _Qry &= vbCrLf & " , FTContributedDeducBnkCode"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            HCfg.HSocialRate.SocialIncomeMin = 0
            HCfg.HSocialRate.SocialIncomeMax = 0
            HCfg.HSocialRate.CalSocialRate = 0


            For Each R As DataRow In _dt.Rows
                HCfg.HSocialRate.SocialIncomeMin = Val(R!FNSocialMin.ToString)
                HCfg.HSocialRate.SocialIncomeMax = Val(R!FNSocialMax.ToString)
                HCfg.HSocialRate.CalSocialRate = Val(R!FNSocialRate.ToString)
            Next

            HCfg.HMaxSocialBaht = CDbl(Format((HCfg.HSocialRate.SocialIncomeMax * HCfg.HSocialRate.CalSocialRate) / 100.0, "0"))

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function LoadTaxRate() As Boolean
        Try
            Dim _QrySql As String
            Dim oDBdt As DataTable
            Dim nIndex As Integer

            _QrySql = " SELECT  FCAmtBegin, FCAmtEnd, FCTaxRate "
            _QrySql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate WITH(NOLOCK)"
            _QrySql &= vbCrLf & " WHERE FTStaActive='1' "
            _QrySql &= vbCrLf & " ORDER BY FNSeqNo ASC "
            oDBdt = HI.Conn.SQLConn.GetDataTable(_QrySql, Conn.DB.DataBaseName.DB_HR)
            nIndex = 0
            If oDBdt.Rows.Count > 0 Then
                Dim oDBdr As DataRow
                For Each oDBdr In oDBdt.Rows
                    nIndex = nIndex + 1
                    ReDim Preserve HCfg.HCfg_TaxRate(nIndex)

                    HCfg.HCfg_TaxRate(nIndex).TaxIncomeMin = Val(oDBdr.Item("FCAmtBegin").ToString)
                    HCfg.HCfg_TaxRate(nIndex).TaxIncomeMax = Val(oDBdr.Item("FCAmtEnd").ToString)
                    HCfg.HCfg_TaxRate(nIndex).CalTaxRate = Val(oDBdr.Item("FCTaxRate").ToString)

                Next
            End If
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    Public Shared Function LoadDiscountTax() As Boolean
        Try
            Dim _Qry As String
            Dim _Dt As DataTable

            _Qry = "    SELECT       FTKeyCode, FTKeyValue"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfig"
            _Qry &= vbCrLf & "  WHERE       (FTKeyCode = N'Cfg_ContributedDeducToTheFund') "
            _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ContributedDeducToTheFundBoss') "
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ContributedIncomeMax') "
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ContributedToTheFund') "
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateNotStudied')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateNumberOfChildren')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateStudy')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModDeductibleDonations')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModFatherReduction')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModInsurancePremiums')"
            _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModLendingforHousing')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModLTF')"
            _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModLTFChk')"
            _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModMateFatherReduction')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModMateMotherReduction')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModMotherReduction')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModPersonalExpen')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModPersonalExpenChk')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRateReductionsByMarital')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRateReductionsBySingle')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRMF')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRMFChk')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnly')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlyChk')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlytheExcess')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlytheExcessChk')"
            _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModDeductibleDonationsstudy')"
            'Cfg_ModDeductibleDonationsstudy
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            ' ----------------------------------------- Clear Discount Tax Value -------------------------
            HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied = 0 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
            HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy = 0 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
            HCfg._DiscountTax.Cfg_ModChildAllowanceRateNumberOfChildren = 0 'บุตรศึกษา อัตราลดหย่อนบุตร บุตร กำลังศึกษา คนละ

            '-------------ลดหย่อนบุตร-----------------

            '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
            HCfg._DiscountTax.Cfg_ContributedDeducToTheFund = 0 'ลูกจ้าง
            HCfg._DiscountTax.Cfg_ContributedDeducToTheFundBoss = 0 'นายจ้าง

            '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ

            'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
            HCfg._DiscountTax.Cfg_ContributedToTheFund = 0 ' %
            HCfg._DiscountTax.Cfg_ContributedIncomeMax = 0 'จำนวนเงินสูงสุด

            'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
            HCfg._DiscountTax.Cfg_ModDeductibleDonations = 0 ' % ลดหย่อนเงินบริจาค
            HCfg._DiscountTax.Cfg_ModFatherReduction = 0 'ลดหย่อนบิดา
            HCfg._DiscountTax.Cfg_ModInsurancePremiums = 0 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
            HCfg._DiscountTax.Cfg_ModLendingforHousing = 0 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
            HCfg._DiscountTax.Cfg_ModLTF = 0 '% หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF)
            HCfg._DiscountTax.Cfg_ModLTFChk = 0 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
            HCfg._DiscountTax.Cfg_ModMateFatherReduction = 0 'ลดหย่อนบิดา คู่สมรส
            HCfg._DiscountTax.Cfg_ModMateMotherReduction = 0 'ลดหย่อนมารดา คู่สมรส
            HCfg._DiscountTax.Cfg_ModMotherReduction = 0 'ลดหย่อนมารดา
            HCfg._DiscountTax.Cfg_ModPersonalExpen = 0 '% หัก ค่าใช้จ่ายส่วนบุคคล
            HCfg._DiscountTax.Cfg_ModPersonalExpenChk = 0 ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน
            HCfg._DiscountTax.Cfg_ModRateReductionsByMarital = 0 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
            HCfg._DiscountTax.Cfg_ModRateReductionsBySingle = 0 'อัตราลดหย่อน ตาม สถานภาพ โสด 
            HCfg._DiscountTax.Cfg_ModRMF = 0 ' % หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF)
            HCfg._DiscountTax.Cfg_ModRMFChk = 0 ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
            HCfg._DiscountTax.Cfg_ModSavingsFundOnly = 0 'เปอร์เซนต์ หักเงินสะสมกองทุนสำรองเลี้ยงชีพ ของค่าจ้าง
            HCfg._DiscountTax.Cfg_ModSavingsFundOnlyChk = 0 'หักเงินสะสมกองทุนสำรองเลี้ยงชีพไม่เกิน
            HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcess = 0 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน
            HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcessChk = 0 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน ไม่เกิน
            HCfg._DiscountTax.Cfg_ModDeductDonateStudy = 0
            '----------------------------------------------------
            For Each R As DataRow In _Dt.Rows

                Select Case R!FTKeyCode.ToString.ToUpper
                    '-------------ลดหย่อนบุตร-----------------
                    Case "Cfg_ModChildAllowanceRateNotStudied".ToUpper 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
                        HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModChildAllowanceRateStudy".ToUpper 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
                        HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModChildAllowanceRateNumberOfChildren".ToUpper 'บุตรศึกษา อัตราลดหย่อนบุตร บุตร กำลังศึกษา คนละ
                        HCfg._DiscountTax.Cfg_ModChildAllowanceRateNumberOfChildren = Val(R!FTKeyValue.ToString)
                        '-------------ลดหย่อนบุตร-----------------

                        '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
                    Case "Cfg_ContributedDeducToTheFund".ToUpper 'ลูกจ้าง
                        HCfg._DiscountTax.Cfg_ContributedDeducToTheFund = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ContributedDeducToTheFundBoss".ToUpper 'นายจ้าง
                        HCfg._DiscountTax.Cfg_ContributedDeducToTheFundBoss = Val(R!FTKeyValue.ToString)
                        '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ

                        'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
                    Case "Cfg_ContributedToTheFund".ToUpper ' %
                        HCfg._DiscountTax.Cfg_ContributedToTheFund = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ContributedIncomeMax".ToUpper 'จำนวนเงินสูงสุด
                        HCfg._DiscountTax.Cfg_ContributedIncomeMax = Val(R!FTKeyValue.ToString)
                        'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้

                    Case "Cfg_ModDeductibleDonations".ToUpper ' % ลดหย่อนเงินบริจาค
                        HCfg._DiscountTax.Cfg_ModDeductibleDonations = Val(R!FTKeyValue.ToString)

                    Case "Cfg_ModDeductibleDonationsstudy".ToUpper ' % ลดหย่อนเงินบริจาคเพื่อการศึกษา
                        HCfg._DiscountTax.Cfg_ModDeductDonateStudy = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModFatherReduction".ToUpper 'ลดหย่อนบิดา
                        HCfg._DiscountTax.Cfg_ModFatherReduction = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModInsurancePremiums".ToUpper 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
                        HCfg._DiscountTax.Cfg_ModInsurancePremiums = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModLendingforHousing".ToUpper 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
                        HCfg._DiscountTax.Cfg_ModLendingforHousing = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModLTF".ToUpper '% หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF)
                        HCfg._DiscountTax.Cfg_ModLTF = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModLTFChk".ToUpper 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
                        HCfg._DiscountTax.Cfg_ModLTFChk = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModMateFatherReduction".ToUpper 'ลดหย่อนบิดา คู่สมรส
                        HCfg._DiscountTax.Cfg_ModMateFatherReduction = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModMateMotherReduction".ToUpper 'ลดหย่อนมารดา คู่สมรส
                        HCfg._DiscountTax.Cfg_ModMateMotherReduction = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModMotherReduction".ToUpper 'ลดหย่อนมารดา
                        HCfg._DiscountTax.Cfg_ModMotherReduction = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModPersonalExpen".ToUpper '% หัก ค่าใช้จ่ายส่วนบุคคล
                        HCfg._DiscountTax.Cfg_ModPersonalExpen = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModPersonalExpenChk".ToUpper ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน
                        HCfg._DiscountTax.Cfg_ModPersonalExpenChk = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModRateReductionsByMarital".ToUpper 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
                        HCfg._DiscountTax.Cfg_ModRateReductionsByMarital = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModRateReductionsBySingle".ToUpper 'อัตราลดหย่อน ตาม สถานภาพ โสด 
                        HCfg._DiscountTax.Cfg_ModRateReductionsBySingle = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModRMF".ToUpper ' % หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF)
                        HCfg._DiscountTax.Cfg_ModRMF = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModRMFChk".ToUpper ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
                        HCfg._DiscountTax.Cfg_ModRMFChk = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModSavingsFundOnly".ToUpper 'เปอร์เซนต์ หักเงินสะสมกองทุนสำรองเลี้ยงชีพ ของค่าจ้าง
                        HCfg._DiscountTax.Cfg_ModSavingsFundOnly = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModSavingsFundOnlyChk".ToUpper 'หักเงินสะสมกองทุนสำรองเลี้ยงชีพไม่เกิน
                        HCfg._DiscountTax.Cfg_ModSavingsFundOnlyChk = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModSavingsFundOnlytheExcess".ToUpper 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน
                        HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcess = Val(R!FTKeyValue.ToString)
                    Case "Cfg_ModSavingsFundOnlytheExcessChk".ToUpper 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน ไม่เกิน
                        HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcessChk = Val(R!FTKeyValue.ToString)
                    Case Else

                End Select
            Next
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    Public Shared Function GETnRecalSocial(ByVal pmEmpCode As String, ByVal pnIncome As Double, ByVal pnInstalmentNo As Double, ByVal pmPayYear As String, Optional ByVal pnSocialMonth As Double = 0) As Double
        Try
            Dim nSocialAmount As Double = 0
            Dim nSocialLastWeek As Double = 0
            Dim nSocialRecalLastWeek As Double = 0
            Dim oDBdtSocial As DataTable
            Dim _QrySql As String

            If (pnInstalmentNo Mod 2 = 0) Then

                pnSocialMonth = (HCfg.HSocialRate.SocialIncomeMax * HCfg.HSocialRate.CalSocialRate) / 100
                _QrySql = "SELECT ISNULL(dbo.THRTPayRoll.FCTotalRecalSSO,0) AS FCTotalRecalSSO , ISNULL(dbo.THRTPayRoll.FCSocial,0) AS FCSocial"
                _QrySql &= vbCrLf & "FROM dbo.THRTPayRoll"
                _QrySql &= vbCrLf & "AND dbo.THRTPayRoll.FTPayYear = '" & pmPayYear & "'"
                _QrySql &= vbCrLf & "AND dbo.THRTPayRoll.FTPayTerm = '" & Format(CStr(pnInstalmentNo - 1), "00") & "'"
                _QrySql &= vbCrLf & "AND dbo.THRTPayRoll.FTIdNo = (SELECT dbo.THRMEmployee.FTIdNo FROM dbo.THRMEmployee WHERE FNHSysEmpID = '" & Val(pmEmpCode) & "')"
                oDBdtSocial = HI.Conn.SQLConn.GetDataTable(_QrySql, Conn.DB.DataBaseName.DB_HR)

                If oDBdtSocial.Rows.Count > 0 Then
                    nSocialRecalLastWeek = oDBdtSocial.Rows(0).Item("FCTotalRecalSSO")
                    nSocialLastWeek = oDBdtSocial.Rows(0).Item("FCSocial")
                End If

                Select Case (pnIncome + nSocialRecalLastWeek)
                    Case Is = 0
                        nSocialAmount = 0
                    Case Is <= HCfg.HSocialRate.SocialIncomeMin
                        nSocialAmount = CDbl(Format((HCfg.HSocialRate.SocialIncomeMin * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))
                    Case Is >= HCfg.HSocialRate.SocialIncomeMax
                        nSocialAmount = CDbl(Format((HCfg.HSocialRate.SocialIncomeMax * HCfg.HSocialRate.SocialIncomeMax / 100), "#,##0.00"))
                    Case Else
                        nSocialAmount = CDbl(Format(((pnIncome + nSocialRecalLastWeek) * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))
                End Select

                If nSocialAmount >= pnSocialMonth Then
                    pnSocialMonth = pnSocialMonth
                Else
                    pnSocialMonth = nSocialAmount
                End If

                nSocialAmount = nSocialAmount - nSocialLastWeek
                nSocialAmount = Format(nSocialAmount, "#0.00")

            Else

                pnSocialMonth = CDbl(Format((HCfg.HSocialRate.SocialIncomeMax * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))

                Select Case pnIncome
                    Case Is = 0
                        nSocialAmount = 0
                    Case Is <= HCfg.HSocialRate.SocialIncomeMin
                        nSocialAmount = CDbl(Format((HCfg.HSocialRate.SocialIncomeMin * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))
                    Case Is >= HCfg.HSocialRate.SocialIncomeMax
                        nSocialAmount = CDbl(Format((HCfg.HSocialRate.SocialIncomeMax * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))
                    Case Else
                        nSocialAmount = CDbl(Format((pnIncome * HCfg.HSocialRate.CalSocialRate) / 100, "#,##0.00"))
                End Select

                nSocialAmount = Format("{0:N2", nSocialAmount)
                If (nSocialAmount * 2) >= pnSocialMonth Then
                    pnSocialMonth = pnSocialMonth
                Else
                    pnSocialMonth = nSocialAmount * 2
                End If

            End If

            If nSocialAmount < 0 Then nSocialAmount = 0

            Return nSocialAmount
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function GETnTax(ByVal pnIncome As Double, ByVal OtherAdd As Double, ByRef TaxOther As Double) As Double
        '...pnIncome เงินที่จะนำเอามาคิดภาษี ภายหลัง จากถูกหักลดหย่อนแล้ว
        Try
            Dim nStep As Integer
            Dim nTaxAmount As Double
            Dim nSumTax As Double
            Dim nIncomeRecal As Double
            Dim nIncomeBalance As Double
            nStep = 0 : nTaxAmount = 0 : nSumTax = 0 : nIncomeRecal = 0
            nIncomeBalance = pnIncome ' - OtherAdd

            '--------------ภาษีเงินได้ทั้งหมด รวม Bonus เงินได้อื่นๆ-------------
            Dim nSalaryIncome As Double = pnIncome - OtherAdd
            Dim nSalaryIncomeRecal As Double = nSalaryIncome
            Dim nSlaaryTax As Double = 0

            Do While (nIncomeBalance > 0)
                nStep = nStep + 1

                If HCfg.HCfg_TaxRate(nStep).TaxIncomeMax < pnIncome Then
                    nIncomeRecal = HCfg.HCfg_TaxRate(nStep).TaxIncomeMax - nIncomeRecal '...เงินที่จะเอามาคิดภาษีในลำดับขั้นการคิดภาษี ณ. Step นี้
                    nTaxAmount = ((HCfg.HCfg_TaxRate(nStep).CalTaxRate * nIncomeRecal) / 100)     '...ถูกคิดภาษี ณ. Step นี้เป็นจำนวนเงิน...บาท
                    nIncomeBalance = pnIncome - HCfg.HCfg_TaxRate(nStep).TaxIncomeMax   '...รายได้ที่เหลือ ที่จะนำเอาไปคิดภาษีในลำดับขั้นถัดไป
                    nIncomeRecal = HCfg.HCfg_TaxRate(nStep).TaxIncomeMax                '...Save Max Income
                Else
                    nTaxAmount = (HCfg.HCfg_TaxRate(nStep).CalTaxRate * nIncomeBalance) / 100   '...เงินที่เหลือในช่วงสุดท้ายของลำดับขั้นนี้ที่เอามาคิดภาษี
                    nIncomeBalance = 0  '...Flag เพื่อบอกว่าจบการคิดภาษี ณ. Step นี้
                End If

                nSumTax = nSumTax + nTaxAmount
            Loop

            '--------------ภาษีเงินได้ทั้งหมด รวม Bonus เงินได้อื่นๆ-------------

            '--------------ภาษีเงินได้เฉพาะเงินเดือน-------------

            nSlaaryTax = 0
            nStep = 0 : nTaxAmount = 0 : nIncomeRecal = 0
            nIncomeBalance = nSalaryIncome
            nSalaryIncomeRecal = nSalaryIncome

            Do While (nIncomeBalance > 0)
                nStep = nStep + 1

                If HCfg.HCfg_TaxRate(nStep).TaxIncomeMax < nSalaryIncomeRecal Then
                    nIncomeRecal = HCfg.HCfg_TaxRate(nStep).TaxIncomeMax - nIncomeRecal '...เงินที่จะเอามาคิดภาษีในลำดับขั้นการคิดภาษี ณ. Step นี้
                    nTaxAmount = ((HCfg.HCfg_TaxRate(nStep).CalTaxRate * nIncomeRecal) / 100)     '...ถูกคิดภาษี ณ. Step นี้เป็นจำนวนเงิน...บาท
                    nIncomeBalance = nSalaryIncomeRecal - HCfg.HCfg_TaxRate(nStep).TaxIncomeMax   '...รายได้ที่เหลือ ที่จะนำเอาไปคิดภาษีในลำดับขั้นถัดไป
                    nIncomeRecal = HCfg.HCfg_TaxRate(nStep).TaxIncomeMax                '...Save Max Income
                Else
                    nTaxAmount = (HCfg.HCfg_TaxRate(nStep).CalTaxRate * nIncomeBalance) / 100   '...เงินที่เหลือในช่วงสุดท้ายของลำดับขั้นนี้ที่เอามาคิดภาษี
                    nIncomeBalance = 0  '...Flag เพื่อบอกว่าจบการคิดภาษี ณ. Step นี้
                End If

                nSlaaryTax = nSlaaryTax + nTaxAmount
            Loop

            '--------------ภาษีเงินได้เฉพาะเงินเดือน-------------
            TaxOther = nSumTax - nSlaaryTax

            Return nSumTax - TaxOther
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Shared Function GETnRecalDiscTax(ByVal _Emp As HCfg.EmployeeDiscountTax, ByRef _EmptaxYear As HCfg.EmpTaxYear) As Double
        '...คำนวณเงินลดหย่อน ก่อนนำไปคิดภาษี
        Try
            Dim _TotalIncome As Double = 0
            Dim _TotalDiscount As Double = 0
            Dim _Discount As Double = 0
            Dim _Ind As Integer = 0
            Dim _IndMax As Integer = 0
            With _Emp

                _TotalIncome = .OtherSlary + .BaseSlary + .BeforeIncom

                _EmptaxYear.FTAmt = _TotalIncome  'เงินได้ก่อนหักค่าใช้จ่าย
                _TotalDiscount = .FTSosial

                '---------------- ค่าใช้จ่ายส่วนตัว-----------------------------------------
                If HCfg._DiscountTax.Cfg_ModPersonalExpen > 0 Then
                    _Discount = CDbl(Format((_TotalIncome * HCfg._DiscountTax.Cfg_ModPersonalExpen) / 100, "0.00"))

                    If _Discount > HCfg._DiscountTax.Cfg_ModPersonalExpenChk Then
                        _Discount = HCfg._DiscountTax.Cfg_ModPersonalExpenChk

                    End If

                    _EmptaxYear.FTExpenses = _Discount 'ค่าใช้จ่ายส่วนตัว

                    _TotalDiscount = _TotalDiscount + _Discount
                End If
                '---------------- ค่าใช้จ่ายส่วนตัว-----------------------------------------

                _EmptaxYear.FTNetAmt = _EmptaxYear.FTAmt - _EmptaxYear.FTExpenses ' เงินได้หลังหักค่าใช้จ่าย

                ' ----------------------------ค่าใช้จ่ายบุตร ------------------------------
                _EmptaxYear.FNChildNotLern = .Cfg_ModChildAllowanceRateNotStudied 'จำนวนบุตรไม่ศึกษา
                _EmptaxYear.FNChildLern = .Cfg_ModChildAllowanceRateStudy 'จำนวนบุตรศึกษา

                If .Cfg_ModChildAllowanceRateNotStudied > 0 Or .Cfg_ModChildAllowanceRateStudy > 0 Then
                    _Discount = 0
                    If HCfg._DiscountTax.Cfg_ModChildAllowanceRateNumberOfChildren > 0 Then

                        _IndMax = HCfg._DiscountTax.Cfg_ModChildAllowanceRateNumberOfChildren

                        If HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied > HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy Then
                            For I = 1 To .Cfg_ModChildAllowanceRateNotStudied
                                If _IndMax > 0 Then

                                    _Discount = _Discount + HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied

                                    _EmptaxYear.FTChildNotLern = _EmptaxYear.FTChildNotLern + HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied 'ลดหย่อนบุตรไม่ศึกษา

                                    _IndMax = _IndMax - 1
                                End If
                            Next
                        Else
                            For I = 1 To .Cfg_ModChildAllowanceRateStudy
                                If _IndMax > 0 Then
                                    _Discount = _Discount + HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy
                                    _EmptaxYear.FTChildLern = _EmptaxYear.FTChildLern + HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy 'ลดหย่อนบุตรศึกษา
                                    _IndMax = _IndMax - 1
                                End If
                            Next
                        End If


                        If _IndMax > 0 Then
                            If HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied > HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy Then
                                For I = 1 To .Cfg_ModChildAllowanceRateStudy
                                    If _IndMax > 0 Then

                                        _Discount = _Discount + HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy

                                        _EmptaxYear.FTChildLern = _EmptaxYear.FTChildLern + HCfg._DiscountTax.Cfg_ModChildAllowanceRateStudy 'ลดหย่อนบุตรศึกษา

                                        _IndMax = _IndMax - 1
                                    End If
                                Next
                            Else
                                For I = 1 To .Cfg_ModChildAllowanceRateNotStudied
                                    If _IndMax > 0 Then
                                        _Discount = _Discount + HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied

                                        _EmptaxYear.FTChildNotLern = _EmptaxYear.FTChildNotLern + HCfg._DiscountTax.Cfg_ModChildAllowanceRateNotStudied 'ลดหย่อนบุตรไม่ศึกษา

                                        _IndMax = _IndMax - 1
                                    End If
                                Next
                            End If
                        End If
                    End If

                    _TotalDiscount = _TotalDiscount + _Discount

                End If
                ' ----------------------------ค่าใช้จ่ายบุตร ------------------------------

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModFatherReduction < .Cfg_ModFatherReduction, HCfg._DiscountTax.Cfg_ModFatherReduction, .Cfg_ModFatherReduction) 'ลดหย่อนบิดา
                _EmptaxYear.FTModFather = IIf(HCfg._DiscountTax.Cfg_ModFatherReduction < .Cfg_ModFatherReduction, HCfg._DiscountTax.Cfg_ModFatherReduction, .Cfg_ModFatherReduction) 'ลดหย่อนบิดา

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModInsurancePremiums < .Cfg_ModInsurancePremiums, HCfg._DiscountTax.Cfg_ModInsurancePremiums, .Cfg_ModInsurancePremiums) 'เบี้ยประกันชีวิตส่วนบุคคล
                _EmptaxYear.FTInsurance = IIf(HCfg._DiscountTax.Cfg_ModInsurancePremiums < .Cfg_ModInsurancePremiums, HCfg._DiscountTax.Cfg_ModInsurancePremiums, .Cfg_ModInsurancePremiums) 'เบี้ยประกันชีวิตส่วนบุคคล

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModLendingforHousing < .Cfg_ModLendingforHousing, HCfg._DiscountTax.Cfg_ModLendingforHousing, .Cfg_ModLendingforHousing)  'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
                _EmptaxYear.FTInterest = IIf(HCfg._DiscountTax.Cfg_ModLendingforHousing < .Cfg_ModLendingforHousing, HCfg._DiscountTax.Cfg_ModLendingforHousing, .Cfg_ModLendingforHousing)

                _TotalDiscount = _TotalDiscount + .FCDisabledDependents 'ดูแลบุคลทุพลภาพ
                _EmptaxYear.FTCommutation = _EmptaxYear.FTCommutation + .FCDisabledDependents

                _TotalDiscount = _TotalDiscount + .FCHealthInsurFatherMotherMate 'เบี้ยประกันบิดามารดา
                _EmptaxYear.FTParentsHealthInsurance = .FCHealthInsurFatherMotherMate 'เบี้ยประกันบิดามารดา

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModMateFatherReduction < .Cfg_ModMateFatherReduction, HCfg._DiscountTax.Cfg_ModMateFatherReduction, .Cfg_ModMateFatherReduction)   'ลดหย่อนบิดา คู่สมรส
                _EmptaxYear.FTModFatherMate = IIf(HCfg._DiscountTax.Cfg_ModMateFatherReduction < .Cfg_ModMateFatherReduction, HCfg._DiscountTax.Cfg_ModMateFatherReduction, .Cfg_ModMateFatherReduction)   'ลดหย่อนบิดา คู่สมรส

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModMateMotherReduction < .Cfg_ModMateMotherReduction, HCfg._DiscountTax.Cfg_ModMateMotherReduction, .Cfg_ModMateMotherReduction) 'ลดหย่อนมารดา คู่สมรส
                _EmptaxYear.FTModMotherMate = IIf(HCfg._DiscountTax.Cfg_ModMateMotherReduction < .Cfg_ModMateMotherReduction, HCfg._DiscountTax.Cfg_ModMateMotherReduction, .Cfg_ModMateMotherReduction) 'ลดหย่อนมารดา คู่สมรส

                _TotalDiscount = _TotalDiscount + IIf(HCfg._DiscountTax.Cfg_ModMotherReduction < .Cfg_ModMotherReduction, HCfg._DiscountTax.Cfg_ModMotherReduction, .Cfg_ModMotherReduction) 'ลดหย่อนมารดา
                _EmptaxYear.FTModMother = IIf(HCfg._DiscountTax.Cfg_ModMotherReduction < .Cfg_ModMotherReduction, HCfg._DiscountTax.Cfg_ModMotherReduction, .Cfg_ModMotherReduction) 'ลดหย่อนมารดา

                _TotalDiscount = _TotalDiscount + .FCExceptAgeOver ' 'รายการเงินได้ที่ได้รับยกเว้น ของผู้มีเงินได้ตั้งแต่ 65 ปีขึ้นไป 
                _TotalDiscount = _TotalDiscount + .FCExceptAgeOverMate 'รายการเงินได้ที่ได้รับยกเว้น ของคู่สมรสอายุตั้งแต่ 65 ปีขึ้นไป
                _EmptaxYear.FTPension = .FCExceptAgeOver + .FCExceptAgeOverMate ' บำนาญ

                _TotalDiscount = _TotalDiscount + .FCDeductDividend ' รายการลกหย่อนเงินปันผล
                _EmptaxYear.FTCommutation = _EmptaxYear.FTCommutation + .FCDeductDividend

                If .Cfg_ModRateReductionsByMarital = 1 And .FTMateIncome = False Then _TotalDiscount = _TotalDiscount + HCfg._DiscountTax.Cfg_ModRateReductionsByMarital 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
                If .Cfg_ModRateReductionsByMarital = 1 And .FTMateIncome = False Then _EmptaxYear.FTModMate = HCfg._DiscountTax.Cfg_ModRateReductionsByMarital 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 



                _TotalDiscount = _TotalDiscount + HCfg._DiscountTax.Cfg_ModRateReductionsBySingle 'อัตราลดหย่อน ตาม สถานภาพ โสด 
                _EmptaxYear.FTModEmp = HCfg._DiscountTax.Cfg_ModRateReductionsBySingle 'อัตราลดหย่อน 

                '---------------- LTF -----------------------------------------
                If HCfg._DiscountTax.Cfg_ModLTF > 0 And .Cfg_ModLTFChk > 0 Then


                    _Discount = CDbl(Format((_TotalIncome * HCfg._DiscountTax.Cfg_ModLTF) / 100, "0.00"))

                    If _Discount > HCfg._DiscountTax.Cfg_ModLTFChk Then
                        _Discount = HCfg._DiscountTax.Cfg_ModLTFChk

                    End If

                    _EmptaxYear.FTUnitLTF = _Discount

                    _TotalDiscount = _TotalDiscount + _Discount
                End If
                '---------------- LTF-----------------------------------------

                '---------------- RMF -----------------------------------------
                If HCfg._DiscountTax.Cfg_ModRMF > 0 And .Cfg_ModRMFChk > 0 Then

                    _Discount = CDbl(Format((_TotalIncome * HCfg._DiscountTax.Cfg_ModRMF) / 100, "0.00"))

                    If _Discount > HCfg._DiscountTax.Cfg_ModRMFChk Then
                        _Discount = HCfg._DiscountTax.Cfg_ModRMFChk

                    End If

                    _EmptaxYear.FTUnitRMF = _Discount

                    _TotalDiscount = _TotalDiscount + _Discount

                End If
                '---------------- RMF-----------------------------------------

                If .Cfg_ContributedDeducToTheFund > 0 Then
                    _Discount = .Cfg_ContributedDeducToTheFund

                    If _Discount > HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcessChk Then
                        _EmptaxYear.FTProvidentfund = HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcessChk
                        _EmptaxYear.FTProvidentfundOver = _Discount - HCfg._DiscountTax.Cfg_ModSavingsFundOnlytheExcessChk
                    Else
                        _EmptaxYear.FTProvidentfund = _Discount
                    End If

                    _TotalDiscount = _TotalDiscount + _Discount

                End If
                '----------------------------------------------------

                ' ----------------------------ลดหย่อนบริจาคเพื่อการศึกษา ---------------------
                If .Cfg_ModDeductDonateStudy > 0 Then
                    Dim _tmp As Double = IIf(_TotalIncome <= _TotalDiscount, 0, _TotalIncome - _TotalDiscount)
                    Dim TmpMax As Double = CDbl(Format((_tmp * HCfg._DiscountTax.Cfg_ModDeductDonateStudy) / 100, "0.00"))
                    Dim _TmpDiscount As Double = (.Cfg_ModDeductDonateStudy * 2)

                    If _TmpDiscount > TmpMax Then
                        _TotalDiscount = _TotalDiscount + TmpMax
                        _EmptaxYear.FTDonationLern = TmpMax
                    Else
                        _TotalDiscount = _TotalDiscount + _TmpDiscount
                        _EmptaxYear.FTDonationLern = _TmpDiscount
                    End If

                End If
                ' ----------------------------ลดหย่อนบริจาคเพื่อการศึกษา ---------------------

                ' ----------------------------ลดหย่อนบริจาค ------------------------------
                If .Cfg_ModDeductibleDonations > 0 Then
                    Dim _tmp As Double = IIf(_TotalIncome <= _TotalDiscount, 0, _TotalIncome - _TotalDiscount)
                    Dim TmpMax As Double = CDbl(Format((_tmp * HCfg._DiscountTax.Cfg_ModDeductibleDonations) / 100, "0.00"))

                    If TmpMax > .Cfg_ModDeductibleDonations Then
                        _Discount = .Cfg_ModDeductibleDonations
                    Else
                        _Discount = TmpMax
                    End If

                    _EmptaxYear.FTDonation = _Discount

                    _TotalDiscount = _TotalDiscount + _Discount
                End If
                ' ----------------------------ลดหย่อนบริจาค -----------------------------

            End With

            Return IIf(_TotalIncome <= _TotalDiscount, 0, _TotalIncome - _TotalDiscount)

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function CheckDateClosePeriod(ByVal _DateKey As String, ByVal _EmpTypeKey As String, Optional ByVal _EmpCodeKey As String = "") As Boolean

        Dim _Qry As String
        Dim _MinDate As String

        _Qry = " SELECT        TOP 1 MIN(D.FDCalDateBegin) AS FTdateBegin"
        _Qry &= vbCrLf & " FROM            THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTCmpCode = D.FTCmpCode"
        _Qry &= vbCrLf & "  AND H.FTTypeEmp = D.FTTypeEmp AND H.FTPayTerm = D.FTPayTerm AND "
        _Qry &= vbCrLf & "   H.FTPayYear = D.FTPayYear"
        _Qry &= vbCrLf & "  WHERE   H.FNHSysEmpTypeId ='" & Val(_EmpTypeKey) & "' "
        If _EmpTypeKey = "" And _EmpCodeKey <> "" Then
            _Qry &= vbCrLf & " AND   H.FNHSysEmpTypeId IN  (SELECT TOP 1 FNHSysEmpTypeId "
            _Qry &= vbCrLf & " FROM THRMEmployee AS M WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID='" & Val(_EmpCodeKey) & "'  )  "
        End If
        _MinDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
        If HI.UL.ULDate.ConvertEnDB(_MinDate) > HI.UL.ULDate.ConvertEnDB(_DateKey) Then
            Return True
        Else
            Return False
        End If

    End Function

    'Public Shared Function CalculateWorkTime(ByVal _User As String, ByVal _EmpCode As String, ByVal _StartDate As String, ByVal _EndDate As String, Optional ByVal _EditOt1 As Double = -1, Optional ByVal _EditOt2 As Double = -1, Optional ByVal _EditOt4 As Double = -1, Optional ByVal _EditWT As Double = -1) As Boolean
    '    Try

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
    '        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
    '        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

    '        Dim _Qry As String
    '        Dim _dt As DataTable
    '        Dim _dttran As DataTable

    '        Dim _Err As Integer, _Complete As Integer, _MsgShow As String, _FTMsgShow As String
    '        Dim _Shift As String

    '        Dim _ActualScanMIn As String, _ActualScanMOut As String, _ActualScanAIn As String, _ActualScanAOut As String, _ActualScanOTIn1 As String, _ActualScanOTOut1 As String, _ActualScanOTIn12 As String, _ActualScanOTOut12 As String, _ActIn01 As String, _ActOut01 As String
    '        Dim _CheckTimeMIn As String, _CheckTimeMOut As String, _CheckTimeAIn As String, _CheckTimeAOut As String, _CheckTimeOTIn1 As String, _CheckTimeOTOut1 As String
    '        Dim _CheckTimeOTIn12 As String, _CheckTimeOTOut12 As String

    '        Dim _ActBeforeDate As String
    '        Dim _ActualDate As String, _ActualNextDate As String, _UseBarcode As String, _CalDate As String, _LateCut As String, _ChkCalNextDate As String
    '        Dim _ScanCardOverClock As String, _nTimeCtrl As Double, _LateCompro As Integer
    '        Dim _LeaveSick As Double, _LeaveBusiness As Double, _LeaveVacation As Double, _LeavePregnant As Double
    '        Dim _LeaveOrdain As Double, _LeaveMarry As Double, _LeaveOther As Double
    '        Dim _Absent As Integer, _AbsentSP As Integer, _nOt As Integer, _nOt1 As Integer, _nOt15 As Integer, _nOt2 As Integer, _nOt3 As Integer, _nOt4 As Integer, _nOtH As Integer, _nOtHSP2 As Integer, _nOtHSP4 As Integer
    '        Dim _LateNormal As Integer, _LateNormalNotCut As Integer, _LateOT As Integer, _RetryNormal As Integer, _RetryOT As Integer
    '        Dim _FNLateMMin As Integer, _FNLateAfMin As Integer, _FNRetireMMin As Integer, _FNRetireAfMin As Integer
    '        Dim _nTime As Integer, _nMTime As Integer, _nAfTime As Integer, _nOTTime As Integer
    '        Dim _LateCutN As Integer, _LateCutOT As Integer
    '        Dim _OTRequest As Integer, _SOTRequest As String, _EOTRequest As String
    '        Dim _SOTRequest2 As String, _EOTRequest2 As String

    '        Dim _SOTRequestM1 As String, _EOTRequestM1 As String
    '        Dim _ChkInOTM1 As String, _ChkOutOTM1 As String
    '        Dim _SOTRequestA1 As String, _EOTRequestA1 As String
    '        Dim _ChkInOTA1 As String, _ChkOutOTA1 As String

    '        Dim _FTAppOT As String, _FTWeekDay As String
    '        Dim _WorkTimePerDay As Double
    '        Dim _ScanTimeOverClock As String, _FTTypeEmp As String
    '        Dim _FNAbsentCut As Integer
    '        Dim _CutAbsent As Integer
    '        Dim _StateLate As String = ""
    '        Dim _DetuctLateType As String = ""
    '        Dim _DetuctLateMin As Integer = 0
    '        Dim _SPDateType As Integer = 0
    '        Dim _TimeNormalDiffEdit As Integer = 0
    '        Dim _R As DataRow()
    '        Dim _WeekDayBefore As Integer
    '        Dim _WeekCallDay As Integer
    '        Dim _FTStateAcceptTimeAuto As String = "0"
    '        Dim _DTOTRound As DataTable
    '        Dim _DTLateCfg As DataTable
    '        Dim _DTEmpLeave As DataTable
    '        Dim _DTHoliday As DataTable
    '        Dim _DTTHRMTimeScanCard As DataTable
    '        Dim _DTOTRequest As DataTable
    '        Dim _LoadOTPayOver As DataTable
    '        Dim _FNSpecialTimeMin As Integer = 0
    '        Dim _SpTimeIn As String = ""
    '        Dim _SpTimeOut As String = ""
    '        Dim _StateWorkOffSite As Double
    '        Dim _GetWeekend As DataTable
    '        Dim _GetDateSpecial As DataTable
    '        Dim _GetEmpTypeWeekly As DataTable
    '        Dim _FoundSpecialDay As Boolean = False
    '        Dim _SpecialTime As String = ""
    '        Dim _StatePayOTOverRequest As Boolean = False
    '        Dim _StartLeaveTime As String = ""
    '        Dim _EndLeaveTime As String = ""
    '        _ActualDate = ActualDate
    '        _ActualNextDate = ActualNextdate

    '        _ActBeforeDate = AcBeforedate

    '        '------------------ GetConfig OT Round ----------------------------------
    '        _DTOTRound = _LoadOTTimeCtrl
    '        '------------------ GetConfig OT Round ----------------------------------

    '        _LoadOTPayOver = LoadOTPayOver

    '        '------------------ GetConfig Late ----------------------------------
    '        _DTLateCfg = LoadConfigLateTimeDeduct
    '        '------------------ GetConfig Late ----------------------------------



    '        '------------------ GetConfig Leave ----------------------------------
    '        _Qry = "  SELECT FTDateTrans,SUM(FNTotalMinute) AS Total"
    '        _Qry &= vbCrLf & " ,Max(CASE WHEN ISNULL(FTStaLeaveDay,'') ='' Then '-1'  Else ISNULL(FTStaLeaveDay,'0') END) As FTStaLeaveDay  "
    '        _Qry &= vbCrLf & " , Min(FTLeaveStartTime) As FTLeaveStartTime, Max(FTLeaveEndTime) As FTLeaveEndTime "
    '        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)"
    '        _Qry &= vbCrLf & "  WHERE   (FNHSysEmpID =" & Val(_EmpCode) & ") "
    '        _Qry &= vbCrLf & "  AND (FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "')"
    '        _Qry &= vbCrLf & "  AND (FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "')"
    '        _Qry &= vbCrLf & " GROUP BY FTDateTrans  "
    '        _DTEmpLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    '        '------------------ GetConfig Late ----------------------------------


    '        '------------------ GetConfig WeekEnd ----------------------------------

    '        '------------------- วันหยุด เพิ่มเติม จากกะ ------------------------------------
    '        _GetEmpTypeWeekly = LoadEmpTypeWeekly(_StartDate, _EndDate)
    '        '--------------------------------------------------------------------

    '        '---------------- วันหยุดประจำสัปดาห์ของพนักงานแต่ละคน----------------
    '        _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
    '        _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
    '        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(_EmpCode) & " "
    '        _GetWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        '----------------กรณี พบวันหยุด สัปดาห์ของพนักงานแต่ละคน ไม่ดู  วันหยุด เพิ่มเติม จากกะ ----------------
    '        If _GetWeekend.Rows.Count > 0 Then
    '            _GetEmpTypeWeekly.Clear()
    '        End If
    '        '---------------- วันหยุดประจำสัปดาห์ของพนักงานแต่ละคน----------------

    '        '------------------ GetConfig WeekEnd ----------------------------------

    '        '------------------ GetConfig Holiday ----------------------------------
    '        _DTHoliday = LoadSysHoliday
    '        '------------------ GetConfig Holiday ----------------------------------

    '        '------------------  Work Time ----------------------------------
    '        _DTTHRMTimeScanCard = LoadTimeShiftControl
    '        '------------------  Work Time ----------------------------------

    '        '------------------  OT Request ----------------------------------

    '        _Qry = "    SELECT   FTDateRequest, ISNULL(FNOtTotalTimeMinute ,0) +ISNULL(FNOtTotalTimeMinute2 ,0)   AS OTRequest"
    '        _Qry &= vbCrLf & ",ISNULL(FTOtIn,'') AS SOTRequest "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtOut,'')  AS EOTRequest "
    '        _Qry &= vbCrLf & ",'' AS SOTRequest2 "
    '        _Qry &= vbCrLf & ",''  AS EOTRequest2 "
    '        _Qry &= vbCrLf & ",'' AS SOTRequest1 "
    '        _Qry &= vbCrLf & ", ''  AS EOTRequest1 "

    '        _Qry &= vbCrLf & ",ISNULL(FTOtIn3,'') AS SOTRequest3 "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtOut3,'')  AS EOTRequest3 "

    '        _Qry &= vbCrLf & "  ,CASE WHEN ISNULL(FTApproveState,'') ='1' Then 'Y' Else 'N' END AS FTAppOT "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtIn,'')  AS ChkIn3 "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtOut,'')  AS ChkOut3 "
    '        _Qry &= vbCrLf & ", ''  AS ChkIn32 "
    '        _Qry &= vbCrLf & ",''  AS ChkOut32 "

    '        _Qry &= vbCrLf & ",''  AS ChkIn1 "
    '        _Qry &= vbCrLf & ", ''  AS ChkOut1 "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtIn3,'')  AS ChkIn33 "
    '        _Qry &= vbCrLf & ", ISNULL(FTOtOut3,'')  AS ChkOut33 "

    '        _Qry &= vbCrLf & ", ISNULL(FNRest,0)  AS FNRest "
    '        _Qry &= vbCrLf & ", ISNULL(FNRest2,0)  AS FNRest2 "
    '        _Qry &= vbCrLf & " 	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS O WITH (NOLOCK) "
    '        _Qry &= vbCrLf & "  WHERE (FTDateRequest <> '') "
    '        _Qry &= vbCrLf & "  AND (FNHSysEmpID =" & Val(_EmpCode) & ") "
    '        _Qry &= vbCrLf & "  AND (FTDateRequest >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "')"
    '        _Qry &= vbCrLf & "  AND (FTDateRequest <='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "')"


    '        _DTOTRequest = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    '        '------------------ OT Request ----------------------------------

    '        '------------ Date Special--------------------------------
    '        _Qry = "  SELECT FTDate,FTTimeOut,FTStateStop "
    '        _Qry &= vbCrLf & " 	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS O WITH (NOLOCK) "
    '        _Qry &= vbCrLf & "  WHERE (FTDate <> '') "
    '        _Qry &= vbCrLf & "  AND (FNHSysEmpID =" & Val(_EmpCode) & ") "
    '        _Qry &= vbCrLf & "  AND (FTDate >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "')"
    '        _Qry &= vbCrLf & "  AND (FTDate <='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "')"

    '        _GetDateSpecial = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        '------------ Date Special--------------------------------
    '        _FTMsgShow = ""
    '        _MsgShow = ""
    '        _Complete = 0
    '        _Err = 0

    '        _FTStateAcceptTimeAuto = ""
    '        _Qry = " 	SELECT     T.FTDateTrans,T.FNHSysShiftID AS FTShift"
    '        _Qry &= vbCrLf & "  ,  T.FTIn1"
    '        _Qry &= vbCrLf & "  ,   T.FTOut1"
    '        _Qry &= vbCrLf & "  ,   T.FTIn2"
    '        _Qry &= vbCrLf & "  ,  T.FTOut2"
    '        _Qry &= vbCrLf & "  ,   T.FTIn3"
    '        _Qry &= vbCrLf & "  , T.FTOut3"
    '        _Qry &= vbCrLf & "  , FTIn4,T.FTOut4"
    '        _Qry &= vbCrLf & "  ,M.FNUseBarcode AS FTUseBarcode,M.FNLateCutSta As FTLateCutCond,T.FTWeekDay,M.FNHSysEmpTypeId AS FTTypeEmp"
    '        _Qry &= vbCrLf & ",T.FTScanAOTOut,T.FTScanAOTOut2"
    '        _Qry &= vbCrLf & ",ISNULL(ET.FTStateAcceptTimeAuto,'0') AS FTStateAcceptTimeAuto"
    '        _Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) INNER JOIN THRMEmployee AS  M  WITH(NOLOCK)"
    '        _Qry &= vbCrLf & " 	ON T.FNHSysEmpID = M.FNHSysEmpID"
    '        _Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
    '        _Qry &= vbCrLf & "  WHERE (T.FNHSysEmpID =" & Val(_EmpCode) & " )"
    '        _Qry &= vbCrLf & "   AND (T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "')"
    '        _Qry &= vbCrLf & "   AND (T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "')"

    '        _Qry &= vbCrLf & " ORDER BY T.FTDateTrans"

    '        _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    '        'LoadOTPayOver
    '        For Each R As DataRow In _dttran.Rows
    '            _StateWorkOffSite = 0
    '            _StatePayOTOverRequest = False
    '            _WeekDayBefore = LoadWeekday(HI.UL.ULDate.AddDay(R!FTDateTrans.ToString, -1))
    '            _WeekCallDay = LoadWeekday(R!FTDateTrans.ToString)
    '            _CalDate = R!FTDateTrans.ToString : _Shift = R!FTShift.ToString
    '            _FTStateAcceptTimeAuto = R!FTStateAcceptTimeAuto.ToString

    '            _FNSpecialTimeMin = 0
    '            _ActualScanMIn = R!FTIn1.ToString : _ActualScanMOut = R!FTOut1.ToString
    '            _ActualScanAIn = R!FTIn2.ToString : _ActualScanAOut = R!FTOut2.ToString

    '            _ActualScanOTIn1 = R!FTIn3.ToString
    '            _ActualScanOTOut1 = R!FTOut3.ToString

    '            _ActualScanOTIn12 = R!FTIn4.ToString
    '            _ActualScanOTOut12 = R!FTOut4.ToString

    '            _UseBarcode = R!FTUseBarcode.ToString : _LateCut = R!FTLateCutCond.ToString
    '            _FTWeekDay = R!FTWeekDay.ToString
    '            _FTTypeEmp = R!FTTypeEmp.ToString

    '            _ChkCalNextDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_CalDate, 1))
    '            _SPDateType = 0

    '            '------------ตรวจสอบไป ปฏิบัติงานนอกสถานที่------------------------
    '            _Qry = "SELECT SUM(FNTotalMin) As FNTotalMin FROM  THRTTransWorkOffsite  With(Nolock)  "
    '            _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(_EmpCode) & " "
    '            _Qry &= vbCrLf & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'  "
    '            _StateWorkOffSite = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
    '            '------------ตรวจสอบไป ปฏิบัติงานนอกสถานที่------------------------

    '            '---- กรณีไม่มีการกำหนดวันหยุดประจำสัปดาห์ของพนักงาน ให้ มองที่ กะ
    '            If _GetWeekend.Rows.Count <= 0 Then
    '                _Qry = "   SELECT    FTSunday, FTMonday, FTTuesday, FTWednesday, FTThursday, FTFriday,"
    '                _Qry &= vbCrLf & "    FTSaturday "
    '                _Qry &= vbCrLf & "  FROM            THRMTimeShift  As W WITH(NOLOCK)  "
    '                _Qry &= vbCrLf & " WHERE FNHSysShiftID =" & Val(_Shift) & " "
    '                _GetWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    '            End If
    '            '---- กรณีไม่มีการกำหนดวันหยุดประจำสัปดาห์ของพนักงาน ให้ มองที่ กะ

    '            '------------- ตรวจสอบเวลาลางาน----------------------------------------------------
    '            _R = _DTEmpLeave.Select(" FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' ")

    '            If _R.Length > 0 Then
    '                For Each IR As DataRow In _R

    '                    Select Case IR!FTStaLeaveDay.ToString
    '                        Case "0"
    '                            _ActualScanMIn = ""
    '                            _ActualScanMOut = ""
    '                            _ActualScanAIn = ""
    '                            _ActualScanAOut = ""
    '                        Case "1"
    '                            _ActualScanMIn = ""
    '                            _ActualScanMOut = ""
    '                        Case "2"
    '                            _ActualScanAIn = ""
    '                            _ActualScanAOut = ""
    '                        Case Else
    '                            Dim _StratTimeLeave As String = IR!FTLeaveStartTime.ToString
    '                            Dim _EndTimeLeave As String = IR!FTLeaveEndTime.ToString

    '                            If _StratTimeLeave <= _ActualScanMIn Then

    '                                If _ActualScanMIn <= _EndTimeLeave And _ActualScanMOut > _EndTimeLeave Then
    '                                    _ActualScanMIn = _EndTimeLeave
    '                                Else
    '                                    ' _ActualScanMIn = ""
    '                                End If

    '                            End If

    '                            If _StratTimeLeave <= _ActualScanAIn And _EndTimeLeave > _ActualScanAIn Then
    '                                If _ActualScanAIn < _EndTimeLeave And _ActualScanAOut > _EndTimeLeave Then
    '                                    _ActualScanAIn = _EndTimeLeave
    '                                Else
    '                                    _ActualScanAIn = ""
    '                                End If
    '                            End If

    '                    End Select
    '                    Exit For
    '                Next
    '            End If

    '            If (_ActualScanMOut = "" Or _ActualScanMIn = "") And (_ActualDate <> _CalDate) Then
    '                _ActualScanMOut = ""
    '                _ActualScanMIn = ""
    '            End If

    '            If _ActualScanAOut = "" Or _ActualScanAIn = "" Then
    '                _ActualScanAOut = ""
    '                _ActualScanAIn = ""
    '            End If

    '            _R = _DTHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'  ")
    '            If _R.Length > 0 Then
    '                _SPDateType = 2
    '            Else

    '                '-------------------ตรวจสอบ วันหยุด เพิ่มเติม จากกะ กรณีไม่มีการกำหนด วันหยุด ประจำสัปดาห์ของบุคคล------------------------------------

    '                If _GetWeekend.Rows.Count > 0 Then
    '                    If _GetWeekend.Rows(0).Item(_WeekCallDay - 1).ToString = "1" Then
    '                        _SPDateType = 1
    '                    End If
    '                End If

    '                '-------------------ตรวจสอบ วันหยุด เพิ่มเติม จากกะ กรณีไม่มีการกำหนด วันหยุด ประจำสัปดาห์ของบุคคล------------------------------------

    '            End If

    '            If _SPDateType = 0 Then
    '                If _GetEmpTypeWeekly.Select("FNHSysEmpTypeId=" & Integer.Parse(Val(_FTTypeEmp)) & " AND FDHolidayDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' ").Length > 0 Then
    '                    _SPDateType = 1
    '                End If
    '            End If

    '            _OTRequest = 0 : _SOTRequest = "" : _EOTRequest = "" : _FTAppOT = "" : _CheckTimeOTIn1 = "" : _CheckTimeOTOut1 = "" : _CheckTimeOTIn12 = "" : _CheckTimeOTOut12 = "" : _SOTRequest2 = "" : _EOTRequest2 = ""

    '            _nTimeCtrl = 0
    '            _LateCompro = 0
    '            _AbsentSP = 0 : _LateNormalNotCut = 0

    '            _nTime = 0 : _nMTime = 0 : _nAfTime = 0 : _LateNormal = 0 : _LateOT = 0
    '            _RetryNormal = 0 : _RetryOT = 0 : _FNLateMMin = 0
    '            _FNLateAfMin = 0 : _FNRetireMMin = 0 : _FNRetireAfMin = 0
    '            _nOTTime = 0 : _Absent = 0 : _nOt = 0 : _nOt1 = 0 : _nOt15 = 0 : _nOtH = 0 : _nOtHSP2 = 0 : _nOtHSP4 = 0 : _nOt2 = 0 : _nOt3 = 0 : _nOt4 = 0
    '            _LateCutN = 0 : _LateCutOT = 0 : _nOt = 0 : _nOt1 = 0 : _nOt15 = 0 : _nOt2 = 0 : _nOt3 = 0
    '            _nTimeCtrl = 0 : _CheckTimeMIn = "" : _CheckTimeMOut = "" : _CheckTimeAIn = "" : _CheckTimeAOut = "" : _FNAbsentCut = 0

    '            _ScanCardOverClock = ""
    '            _R = _DTTHRMTimeScanCard.Select(" FNHSysShiftID = " & Val(_Shift) & " ")

    '            If _R.Length > 0 Then
    '                For Each IR As DataRow In _R
    '                    _nTimeCtrl = Val(IR!WorkTimePerDay.ToString)
    '                    _ScanCardOverClock = IR!FTOverClock.ToString
    '                    _CheckTimeMIn = IR!ChkIn1.ToString
    '                    _CheckTimeMOut = IR!ChkOut1.ToString
    '                    _CheckTimeAIn = IR!ChkIn2.ToString
    '                    _CheckTimeAOut = IR!ChkOut2.ToString
    '                    _WorkTimePerDay = Val(IR!WorkTimePerDay.ToString)
    '                    Exit For
    '                Next
    '            End If

    '            _ChkInOTA1 = ""
    '            _ChkOutOTA1 = ""
    '            _ChkInOTM1 = ""
    '            _ChkOutOTM1 = ""

    '            _ScanTimeOverClock = Right(_ScanCardOverClock, 5)

    '            _R = _DTOTRequest.Select(" FTDateRequest = '" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'")
    '            If _R.Length > 0 Then
    '                For Each IR As DataRow In _R
    '                    _OTRequest = Val(IR!OTRequest.ToString)

    '                    _SOTRequest = IR!SOTRequest.ToString
    '                    _EOTRequest = IR!EOTRequest.ToString

    '                    _SOTRequest = IIf(_SOTRequest <> "", IIf(_SOTRequest >= "00:00" And _SOTRequest <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _SOTRequest, "")
    '                    _EOTRequest = IIf(_EOTRequest <> "", IIf(_EOTRequest >= "00:00" And _EOTRequest <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _EOTRequest, "")

    '                    _SOTRequest2 = IR!SOTRequest2.ToString
    '                    _EOTRequest2 = IR!EOTRequest2.ToString

    '                    _SOTRequest2 = IIf(_SOTRequest2 <> "", IIf(_SOTRequest2 >= "00:00" And _SOTRequest2 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _SOTRequest2, "")
    '                    _EOTRequest2 = IIf(_EOTRequest2 <> "", IIf(_EOTRequest2 >= "00:00" And _EOTRequest2 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _EOTRequest2, "")

    '                    If _CheckTimeMIn <= _SOTRequest2 Then
    '                        _SOTRequest2 = ""
    '                        _EOTRequest2 = ""
    '                    End If

    '                    If _SOTRequest < _CheckTimeAOut Then
    '                        _SOTRequest = ""
    '                        _EOTRequest = ""
    '                    End If

    '                    _FTAppOT = IR!FTAppOT.ToString
    '                    _CheckTimeOTIn1 = IR!ChkIn3.ToString
    '                    _CheckTimeOTOut1 = IR!ChkOut3.ToString

    '                    _CheckTimeOTIn1 = IIf(_CheckTimeOTIn1 <> "", IIf(_CheckTimeOTIn1 >= "00:00" And _CheckTimeOTIn1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _CheckTimeOTIn1, "")
    '                    _CheckTimeOTOut1 = IIf(_CheckTimeOTOut1 <> "", IIf(_CheckTimeOTOut1 >= "00:00" And _CheckTimeOTOut1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _CheckTimeOTOut1, "")

    '                    _CheckTimeOTIn12 = IR!ChkIn32.ToString
    '                    _CheckTimeOTOut12 = IR!ChkOut32.ToString

    '                    _CheckTimeOTIn12 = IIf(_CheckTimeOTIn12 <> "", IIf(_CheckTimeOTIn12 >= "00:00" And _CheckTimeOTIn12 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _CheckTimeOTIn12, "")
    '                    _CheckTimeOTOut12 = IIf(_CheckTimeOTOut12 <> "", IIf(_CheckTimeOTOut12 >= "00:00" And _CheckTimeOTOut1 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _CheckTimeOTOut12, "")

    '                    _SOTRequestM1 = IR!SOTRequest1.ToString
    '                    _EOTRequestM1 = IR!EOTRequest1.ToString
    '                    _SOTRequestM1 = IIf(_SOTRequestM1 <> "", IIf(_SOTRequestM1 >= "00:00" And _SOTRequestM1 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _SOTRequestM1, "")
    '                    _EOTRequestM1 = IIf(_EOTRequestM1 <> "", IIf(_EOTRequestM1 >= "00:00" And _EOTRequestM1 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _EOTRequestM1, "")

    '                    _ChkInOTM1 = IR!ChkIn1.ToString
    '                    _ChkOutOTM1 = IR!ChkOut1.ToString
    '                    _ChkInOTM1 = IIf(_ChkInOTM1 <> "", IIf(_ChkInOTM1 >= "00:00" And _ChkInOTM1 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _ChkInOTM1, "")
    '                    _ChkOutOTM1 = IIf(_ChkOutOTM1 <> "", IIf(_ChkOutOTM1 >= "00:00" And _ChkOutOTM1 <= _ScanTimeOverClock, _ActualDate, _ActualDate) & "  " & _ChkOutOTM1, "")

    '                    _SOTRequestA1 = IR!SOTRequest3.ToString
    '                    _EOTRequestA1 = IR!EOTRequest3.ToString
    '                    _SOTRequestA1 = IIf(_SOTRequestA1 <> "", IIf(_SOTRequestA1 >= "00:00" And _SOTRequestA1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _SOTRequestA1, "")
    '                    _EOTRequestA1 = IIf(_EOTRequestA1 <> "", IIf(_EOTRequestA1 >= "00:00" And _EOTRequestA1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _EOTRequestA1, "")

    '                    _ChkInOTA1 = IR!ChkIn33.ToString
    '                    _ChkOutOTA1 = IR!ChkOut33.ToString

    '                    _ChkInOTA1 = IIf(_ChkInOTA1 <> "", IIf(_ChkInOTA1 >= "00:00" And _ChkInOTA1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ChkInOTA1, "")
    '                    _ChkOutOTA1 = IIf(_ChkOutOTA1 <> "", IIf(_ChkOutOTA1 >= "00:00" And _ChkOutOTA1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ChkOutOTA1, "")

    '                    Exit For
    '                Next
    '            End If

    '            If _ActualScanOTIn12 <> "" And _ActualScanOTOut12 = "" Then
    '                _ActualScanOTIn12 = IIf(_ActualScanOTIn12 <> "", IIf(_ActualScanOTIn12 >= "00:00" And _ActualScanOTIn12 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTIn12, "")
    '                If _ActualScanOTIn12 > _ChkInOTA1 Then
    '                    _ActualScanOTOut12 = Right(_ActualScanOTIn12, 5)
    '                    _ActualScanOTIn12 = Right(_ChkInOTA1, 5)
    '                End If
    '            End If

    '            If _ActualScanOTOut12 = "" Then

    '                If _ActualScanOTOut1 <> "" Then
    '                    For Each Rx4 As DataRow In _LoadOTPayOver.Select("FNHSysEmpTypeId=" & Val(_FTTypeEmp) & " AND FTStatePayOTOverRequest='1' ")
    '                        Try
    '                            If DateDiff(DateInterval.Minute, CDate(_EOTRequest), CDate(Microsoft.VisualBasic.Left(_EOTRequest, Len(_EOTRequest) - 5) & R!FTScanAOTOut.ToString)) >= Val(Rx4!FNTimeSacanMin) Then
    '                                _ActualScanOTOut1 = R!FTScanAOTOut.ToString
    '                                _StatePayOTOverRequest = True
    '                            End If
    '                        Catch ex As Exception
    '                        End Try
    '                    Next
    '                End If

    '            Else

    '                If _EOTRequest2 <> "" Then
    '                    For Each Rx4 As DataRow In _LoadOTPayOver.Select("FNHSysEmpTypeId=" & Val(_FTTypeEmp) & " AND FTStatePayOTOverRequest='1' ")
    '                        Try
    '                            If DateDiff(DateInterval.Minute, CDate(_EOTRequest2), CDate(Microsoft.VisualBasic.Left(_EOTRequest2, Len(_EOTRequest2) - 5) & _ActualScanOTOut12)) >= Val(Rx4!FNTimeSacanMin) Then
    '                                _ActualScanOTOut12 = R!FTScanAOTOut2.ToString
    '                                _StatePayOTOverRequest = True
    '                            End If
    '                        Catch ex As Exception

    '                        End Try
    '                    Next
    '                End If

    '            End If

    '            '---------ตรวจสอบ
    '            _ActualScanMIn = IIf(_ActualScanMIn <> "", IIf(_ActualScanMIn >= "00:00" And _ActualScanMIn <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanMIn, "")
    '            _ActualScanMOut = IIf(_ActualScanMOut <> "", IIf(_ActualScanMOut >= "00:00" And _ActualScanMOut <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanMOut, "")
    '            _ActualScanAIn = IIf(_ActualScanAIn <> "", IIf(_ActualScanAIn >= "00:00" And _ActualScanAIn <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanAIn, "")
    '            _ActualScanAOut = IIf(_ActualScanAOut <> "", IIf(_ActualScanAOut >= "00:00" And _ActualScanAOut <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanAOut, "")
    '            _ActualScanOTIn1 = IIf(_ActualScanOTIn1 <> "", IIf(_ActualScanOTIn1 >= "00:00" And _ActualScanOTIn1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTIn1, "")
    '            _ActualScanOTOut1 = IIf(_ActualScanOTOut1 <> "", IIf(_ActualScanOTOut1 >= "00:00" And _ActualScanOTOut1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTOut1, "")
    '            _ActualScanOTIn12 = IIf(_ActualScanOTIn12 <> "", IIf(_ActualScanOTIn12 >= "00:00" And _ActualScanOTIn12 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTIn12, "")
    '            _ActualScanOTOut12 = IIf(_ActualScanOTOut12 <> "", IIf(_ActualScanOTOut12 >= "00:00" And _ActualScanOTOut12 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTOut12, "")

    '            ''-------- รวจสอบวันเลิกงานพิเศษ--------
    '            'For Each Rxd As DataRow In _GetDateSpecial.Select("FTDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'")

    '            '    If Rxd!FTStateStop.ToString = "1" Then
    '            '        '_ActualScanMIn = _CheckTimeMIn
    '            '        '_ActualScanMOut = _CheckTimeMOut
    '            '        '_ActualScanAIn = _CheckTimeAIn
    '            '        '_ActualScanAOut = _CheckTimeAOut

    '            '        _FNSpecialTimeMin = _nTimeCtrl

    '            '    Else
    '            '        Dim TimeOutSP As String = IIf(Rxd!FTTimeOut.ToString <> "", IIf(Rxd!FTTimeOut.ToString >= "00:00" And Rxd!FTTimeOut.ToString <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & Rxd!FTTimeOut.ToString, "")
    '            '        Select Case True
    '            '            Case (TimeOutSP >= _CheckTimeMIn) And (TimeOutSP <= _CheckTimeMOut)

    '            '                If _ActualScanMOut >= TimeOutSP Or _ActualScanAIn >= TimeOutSP Or _ActualScanAOut >= TimeOutSP Then
    '            '                    '_ActualScanMOut = _CheckTimeMOut
    '            '                    '_ActualScanAIn = _CheckTimeAIn
    '            '                    '_ActualScanAOut = _CheckTimeAOut
    '            '                    _FNSpecialTimeMin = _nTimeCtrl -()
    '            '                End If

    '            '            Case (TimeOutSP >= _CheckTimeAIn) And (TimeOutSP <= _CheckTimeAOut)

    '            '                If _ActualScanAIn >= TimeOutSP Or _ActualScanAOut >= TimeOutSP Then
    '            '                    _ActualScanAIn = _CheckTimeAIn
    '            '                    _ActualScanAOut = _CheckTimeAOut
    '            '                End If

    '            '        End Select

    '            '    End If
    '            'Next

    '            ''-------- รวจสอบวันเลิกงานพิเศษ--------
    '            _LeaveSick = 0 : _LeaveBusiness = 0 : _LeaveVacation = 0 : _LeaveOrdain = 0 : _LeaveMarry = 0 : _LeaveOther = 0
    '            _StartLeaveTime = ""
    '            _EndLeaveTime = ""
    '            _R = _DTEmpLeave.Select(" FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' ")
    '            If _R.Length > 0 Then
    '                For Each IR As DataRow In _R
    '                    _LeaveSick = Val(IR!Total.ToString)

    '                    _StartLeaveTime = IIf(IR!FTLeaveStartTime.ToString <> "", IIf(IR!FTLeaveStartTime.ToString >= "00:00" And IR!FTLeaveStartTime.ToString <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & IR!FTLeaveStartTime.ToString, "")
    '                    _EndLeaveTime = IIf(IR!FTLeaveEndTime.ToString <> "", IIf(IR!FTLeaveEndTime.ToString >= "00:00" And IR!FTLeaveEndTime.ToString <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & IR!FTLeaveEndTime.ToString, "")

    '                    Exit For
    '                Next
    '            End If

    '            _nTimeCtrl = ((((_nTimeCtrl * 100) - (_nTimeCtrl * 100) Mod 100) / 100) * 60) + (_nTimeCtrl * 100) Mod 100
    '            _LeaveBusiness = ((((_LeaveBusiness * 100) - (_LeaveBusiness * 100) Mod 100) / 100) * 60) + (_LeaveBusiness * 100) Mod 100
    '            _LeaveVacation = ((((_LeaveVacation * 100) - (_LeaveVacation * 100) Mod 100) / 100) * 60) + (_LeaveVacation * 100) Mod 100
    '            _LeaveOrdain = ((((_LeaveOrdain * 100) - (_LeaveOrdain * 100) Mod 100) / 100) * 60) + (_LeaveOrdain * 100) Mod 100
    '            _LeaveMarry = ((((_LeaveMarry * 100) - (_LeaveMarry * 100) Mod 100) / 100) * 60) + (_LeaveMarry * 100) Mod 100
    '            _LeaveOther = ((((_LeaveOther * 100) - (_LeaveOther * 100) Mod 100) / 100) * 60) + (_LeaveOther * 100) Mod 100
    '            _LeavePregnant = _LeavePregnant * _nTimeCtrl

    '            _TimeNormalDiffEdit = 0
    '            If _UseBarcode = "1" Or _UseBarcode = "0" Then

    '                '------------------ คำนวณเข้างานสาย -------------------------------
    '                If _ActualScanMIn <> "" And _CheckTimeMIn <> "" And _ActualScanMIn > _CheckTimeMIn Then

    '                    _FNLateMMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeMIn), CDate(_ActualScanMIn))

    '                    If _StartLeaveTime <> "" Then
    '                        If _StartLeaveTime < _ActualScanMIn And _EndLeaveTime <= _ActualScanMIn Then
    '                            _FNLateMMin = _FNLateMMin - _LeaveSick
    '                        End If
    '                    End If

    '                ElseIf _ActualScanAIn <> "" And _ActualScanMIn = "" Then

    '                    _FNLateMMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeMIn), CDate(_CheckTimeMOut))
    '                    If _LeaveSick > _FNLateMMin Then
    '                        _FNLateMMin = 0
    '                    End If

    '                End If

    '                If _ActualScanAIn <> "" And _CheckTimeAIn <> "" And _ActualScanAIn > _CheckTimeAIn Then

    '                    If _StartLeaveTime <> "" Then
    '                        If (_StartLeaveTime <= _CheckTimeAIn And _EndLeaveTime >= _ActualScanAIn) Then

    '                        Else
    '                            If (_StartLeaveTime < _CheckTimeAIn) And _EndLeaveTime < _ActualScanAIn Then
    '                                _FNLateAfMin = DateDiff(DateInterval.Minute, CDate(_EndLeaveTime), CDate(_ActualScanAIn))
    '                            Else
    '                                _FNLateAfMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeAIn), CDate(_ActualScanAIn))
    '                            End If
    '                        End If
    '                    Else
    '                        _FNLateAfMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeAIn), CDate(_ActualScanAIn))
    '                    End If


    '                End If

    '                _LateNormal = _FNLateMMin + _FNLateAfMin

    '                '------------------ คำนวณเข้างานสาย -------------------------------

    '                '------------------ คำนวณเข้างาน OT สาย -------------------------------
    '                If _ActualScanOTIn1 <> "" And _CheckTimeOTIn1 <> "" And _ActualScanOTIn1 > _CheckTimeOTIn1 Then
    '                    _LateOT = DateDiff(DateInterval.Minute, CDate(_CheckTimeOTIn1), CDate(_ActualScanOTIn1))
    '                End If

    '                '------------------ คำนวณเข้างาน OT สาย   '-------------------------------

    '                '------------------ คำนวณ ออกก่อนเวลา -------------------------------
    '                If _ActualScanMOut <> "" And _CheckTimeMOut <> "" And _ActualScanMOut < _CheckTimeMOut Then
    '                    _FNRetireMMin = DateDiff(DateInterval.Minute, CDate(_ActualScanMOut), CDate(_CheckTimeMOut))
    '                End If

    '                If _ActualScanAOut <> "" And _CheckTimeAOut <> "" And _ActualScanAOut < _CheckTimeAOut Then
    '                    _FNRetireAfMin = DateDiff(DateInterval.Minute, CDate(_ActualScanAOut), CDate(_CheckTimeAOut))
    '                End If

    '                _RetryNormal = _FNRetireMMin + _FNRetireAfMin
    '                '------------------ คำนวณ ออกก่อนเวลา -------------------------------

    '                '------------------ คำนวณออก OT ก่อนเวลา -------------------------------
    '                If _ActualScanOTOut1 <> "" And _CheckTimeOTOut1 <> "" And _ActualScanOTOut1 < _CheckTimeOTOut1 Then
    '                    _RetryOT = DateDiff(DateInterval.Minute, CDate(_ActualScanOTOut1), CDate(_CheckTimeOTOut1))
    '                End If
    '                '------------------ คำนวณออก OT ก่อนเวลา   '-------------------------------

    '                If _ActualScanMIn <> "" And _ActualScanMOut <> "" And IsDate(_ActualScanMIn) And IsDate(_ActualScanMOut) And _ActualScanMIn < _ActualScanMOut Then
    '                    _nMTime = DateDiff(DateInterval.Minute, CDate(IIf(_CheckTimeMIn > _ActualScanMIn, _CheckTimeMIn, _ActualScanMIn)), CDate(IIf(_CheckTimeMOut > _ActualScanMOut, _ActualScanMOut, _CheckTimeMOut)))
    '                End If

    '                If _ActualScanAIn <> "" And _ActualScanAOut <> "" And IsDate(_ActualScanAIn) And IsDate(_ActualScanAOut) And _ActualScanAIn < _ActualScanAOut Then
    '                    _nAfTime = DateDiff(DateInterval.Minute, CDate(IIf(_CheckTimeAIn > _ActualScanAIn, _CheckTimeAIn, _ActualScanAIn)), CDate(IIf(_CheckTimeAOut > _ActualScanAOut, _ActualScanAOut, _CheckTimeAOut)))
    '                End If

    '                '-------- รวจสอบวันเลิกงานพิเศษ--------
    '                _FNSpecialTimeMin = 0
    '                For Each Rxd As DataRow In _GetDateSpecial.Select("FTDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'")

    '                    If Rxd!FTStateStop.ToString = "1" Then
    '                        _FNSpecialTimeMin = _nTimeCtrl

    '                    Else
    '                        Dim TimeOutSP As String = IIf(Rxd!FTTimeOut.ToString <> "", IIf(Rxd!FTTimeOut.ToString >= "00:00" And Rxd!FTTimeOut.ToString <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & Rxd!FTTimeOut.ToString, "")
    '                        Select Case True
    '                            Case (TimeOutSP >= _CheckTimeMIn) And (TimeOutSP <= _CheckTimeMOut)

    '                                If _ActualScanMOut >= TimeOutSP Or _ActualScanAIn >= TimeOutSP Or _ActualScanAOut >= TimeOutSP Then
    '                                    _FNSpecialTimeMin = _nTimeCtrl - (_nMTime + _FNLateMMin)
    '                                End If

    '                            Case (TimeOutSP >= _CheckTimeAIn) And (TimeOutSP <= _CheckTimeAOut)

    '                                If _ActualScanAIn >= TimeOutSP Or _ActualScanAOut >= TimeOutSP Then
    '                                    _FNSpecialTimeMin = _nTimeCtrl - (_nMTime + _nAfTime + _LateNormal)
    '                                End If

    '                        End Select

    '                    End If
    '                Next

    '                If _FNSpecialTimeMin <= 0 Then
    '                    _FNSpecialTimeMin = 0
    '                End If

    '                '-------- รวจสอบวันเลิกงานพิเศษ--------
    '                '-- --   ตรวจสอบการขอ OT Request หากไม่มีการขอ ไม่คำนวณ OT ให้
    '                _nOtH = 0 : _nOtHSP2 = 0 : _nOtHSP4 = 0
    '                _nOTTime = 0
    '                If _FTAppOT = "Y" Then



    '                    If _ActualScanOTIn1 <> "" And _ActualScanOTOut1 <> "" And IsDate(_ActualScanOTIn1) And IsDate(_ActualScanOTOut1) And _ActualScanOTIn1 < _ActualScanOTOut1 And _CheckTimeOTOut1 <> "" And _CheckTimeOTIn1 <> "" Then

    '                        Dim _TmpIn3 As String = IIf(_CheckTimeOTIn1 > _ActualScanOTIn1, _CheckTimeOTIn1, _ActualScanOTIn1)
    '                        Dim _TmpOut3 As String = IIf(_CheckTimeOTOut1 > _ActualScanOTOut1, _ActualScanOTOut1, _CheckTimeOTOut1)

    '                        If _ActualScanOTOut1 > _CheckTimeOTOut1 And _ActualScanOTIn12 = "" Then
    '                            For Each Rx4 As DataRow In _LoadOTPayOver.Select("FNHSysEmpTypeId=" & Val(_FTTypeEmp) & " AND FTStatePayOTOverRequest='1' ")
    '                                _TmpOut3 = _ActualScanOTOut1
    '                                Exit For
    '                            Next
    '                        End If

    '                        _nOTTime = _nOTTime + (DateDiff(DateInterval.Minute, CDate(_TmpIn3), CDate(_TmpOut3)))

    '                        If Left(_TmpIn3, 10) <> Left(_TmpOut3, 10) Then
    '                            _TmpIn3 = Left(_TmpOut3, 10) & "  " & "00:00"
    '                            _nOtH = DateDiff(DateInterval.Minute, CDate(_TmpIn3), CDate(_TmpOut3))

    '                            _R = _DTHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_ChkCalNextDate) & "'")
    '                            If _R.Length > 0 Then
    '                                _nOtHSP4 = _nOtH
    '                            Else

    '                            End If

    '                        End If

    '                        _nOTTime = (_nOTTime - (_nOtHSP4 + _nOtHSP2))

    '                        If _nOTTime <= 0 Then _nOTTime = 0

    '                    End If

    '                    If _ActualScanOTIn12 <> "" And _ActualScanOTOut12 <> "" And IsDate(_ActualScanOTIn12) And IsDate(_ActualScanOTOut12) And _ActualScanOTIn12 < _ActualScanOTOut12 And _ChkOutOTA1 <> "" And _ChkInOTA1 <> "" Then

    '                        Dim _TmpIn3 As String = IIf(_ChkInOTA1 > _ActualScanOTIn12, _ChkInOTA1, _ActualScanOTIn12)
    '                        Dim _TmpOut3 As String = IIf(_ChkOutOTA1 > _ActualScanOTOut12, _ActualScanOTOut12, _ChkOutOTA1)

    '                        If _ActualScanOTOut12 > _CheckTimeOTOut1 Then
    '                            For Each Rx4 As DataRow In _LoadOTPayOver.Select("FNHSysEmpTypeId=" & Val(_FTTypeEmp) & " AND FTStatePayOTOverRequest='1' ")
    '                                _TmpOut3 = _ActualScanOTOut12
    '                                Exit For
    '                            Next
    '                        End If

    '                        _nOTTime = _nOTTime + (DateDiff(DateInterval.Minute, CDate(_TmpIn3), CDate(_TmpOut3)))

    '                        If Left(_TmpIn3, 10) <> Left(_TmpOut3, 10) Then
    '                            _TmpIn3 = Left(_TmpOut3, 10) & "  " & "00:00"
    '                            _nOtH = DateDiff(DateInterval.Minute, CDate(_TmpIn3), CDate(_TmpOut3))

    '                            _R = _DTHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_ChkCalNextDate) & "'")
    '                            If _R.Length > 0 Then
    '                                _nOtHSP4 = _nOtH
    '                            Else

    '                            End If

    '                        End If

    '                        _nOTTime = (_nOTTime - (_nOtHSP4 + _nOtHSP2))
    '                        If _nOTTime <= 0 Then _nOTTime = 0

    '                    End If
    '                End If

    '                _TimeNormalDiffEdit = 0
    '                _nTime = _nMTime + _nAfTime

    '                If _EditWT <> -1 Then '----- มีการแก้ไข เลาทำงาน จากหน้า Time Attendance

    '                    If _EditWT < _nTime Then
    '                        _TimeNormalDiffEdit = _nTime - _EditWT
    '                    End If

    '                    _nTime = _EditWT
    '                End If

    '                _nOt15 = _nOTTime

    '                If _LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther >= _nTimeCtrl Then

    '                    _nTime = 0
    '                    _LateNormal = 0

    '                Else

    '                    If (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) > 0 Then
    '                        If _nTimeCtrl - (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) < _nTime Then
    '                            _nTime = _nTimeCtrl - _LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther
    '                        End If
    '                    End If

    '                End If

    '                If _nTimeCtrl <= (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) + _nTime Then
    '                    _LateNormal = 0
    '                End If
    '                _AbsentSP = 0

    '                If _ActualDate = _CalDate And _LateNormal = 0 And _nTime = 0 And ((_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) <= 0) And (_ActualScanMIn <> "" And _ActualScanMOut = "" And _ActualScanAIn = "" And _ActualScanAOut = "" And _ActualScanOTIn1 = "" And _ActualScanOTOut1 = "" And _ActualScanOTIn12 = "" And _ActualScanOTOut12 = "") Then
    '                Else
    '                    'If _nTime + _LateNormal + (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) + _LateNormal < _nTimeCtrl Then
    '                    If _nTime + _FNSpecialTimeMin + _LateNormal + (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther) < _nTimeCtrl Then
    '                        _Absent = _nTimeCtrl - (_nTime + IIf(_nTime = 0, 0, _LateNormal) + (_LeaveSick + _LeaveBusiness + _LeaveVacation + _LeavePregnant + _LeaveOrdain + _LeaveMarry + _LeaveOther))

    '                        If _nTimeCtrl > _nTimeCtrl Then
    '                        End If

    '                        If _Absent > _nTimeCtrl Then _Absent = _nTimeCtrl
    '                    End If
    '                End If

    '                If _SPDateType <> 0 And _Absent >= 0 Then
    '                    _Absent = 0
    '                End If

    '                _StateLate = ""
    '                _DetuctLateType = ""
    '                _DetuctLateMin = 0

    '                If _LateNormal > _nTimeCtrl Then
    '                    _LateNormal = _nTimeCtrl
    '                End If

    '                If _LateNormal > 0 Then

    '                    _R = _DTLateCfg.Select(" FNRateBegin <= " & _FNLateMMin & " AND FNRateEnd >=" & _FNLateMMin & " ")
    '                    If _R.Length > 0 Then
    '                        For Each IR As DataRow In _R
    '                            _StateLate = IR!FTCfgLateCode.ToString
    '                            _DetuctLateType = IR!FTStaDeduct.ToString
    '                            _DetuctLateMin = Val(IR!FNRateDeduct.ToString)
    '                            Exit For
    '                        Next
    '                    End If

    '                End If
    '            Else

    '            End If

    '            _LateNormalNotCut = 0

    '            Dim _SumEditTime As Integer = 0

    '            '------------------------------------------------ ตรวจสอบการ Adjust OT  ถ้ามีการ Adjust ยึดค่า Adjust ------------
    '            If (_EditOt1 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _SumEditTime = _SumEditTime + _EditOt1
    '            End If

    '            If (_EditOt2 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _SumEditTime = _SumEditTime + _EditOt2
    '            End If

    '            If (_EditOt4 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _SumEditTime = _SumEditTime + _EditOt4
    '            End If
    '            '------------------------------------------------ ตรวจสอบการ Adjust OT  ถ้ามีการ Adjust ยึดค่า Adjust ------------
    '            Select Case _SPDateType
    '                Case 1, 2

    '            End Select

    '            If _LateCut = "0" And _UseBarcode = "1" Then

    '                'If _StateWorkOffSite > 0 Then 'ไปปฏิบัติงานนอกสถานที่
    '                '    _nTime = _StateWorkOffSite

    '                '    _LateCutN = 0
    '                '    _FNAbsentCut = 0

    '                '    If _nTime >= _nTimeCtrl Then
    '                '        _LateNormalNotCut = 0
    '                '        _LateCutN = 0
    '                '        _FNAbsentCut = 0
    '                '    End If

    '                '    If _nTime >= _nTimeCtrl Then
    '                '        _Absent = 0
    '                '        _AbsentSP = 0
    '                '    Else
    '                '        _Absent = _nTimeCtrl - _StateWorkOffSite
    '                '    End If

    '                '    _LateCutOT = 0

    '                'Else

    '                _LateCutN = 0
    '                _FNAbsentCut = 0

    '                Select Case _DetuctLateType
    '                    Case "1" 'สายหักสายตาม Config
    '                        _LateCutN = _DetuctLateMin
    '                    Case "2"  'สายหักขาดงาน ตาม Config
    '                        _FNAbsentCut = _DetuctLateMin
    '                    Case "3"  'สายหักตามจริงตาม Config
    '                        _LateCutN = _FNLateMMin '_LateNormal
    '                End Select
    '                _LateCutN = _LateCutN + _FNLateAfMin

    '                If _nTime >= _nTimeCtrl Then
    '                    _LateNormalNotCut = _LateNormal
    '                    _LateCutN = 0
    '                    _FNAbsentCut = 0
    '                End If

    '                If _nTime >= _nTimeCtrl Then
    '                    _Absent = 0
    '                    _AbsentSP = 0
    '                End If

    '                _LateCutOT = _LateOT

    '                'End If

    '            Else

    '                If _UseBarcode = "2" Then
    '                    _LateCutN = 0
    '                    _LateCutOT = 0
    '                    _nTime = _nTimeCtrl '_nTime
    '                    '_nTime = _nTime + IIf(_nTime = 0, 0, _LateNormal)
    '                    ' _nOt15 = _nOt15 + _LateOT
    '                    _nOt15 = 0 'nOt15 '+ _LateOT
    '                    _LateNormalNotCut = 0 'IIf(_nTime = 0, 0, _LateNormal)

    '                    _StateLate = ""
    '                    _DetuctLateType = ""
    '                    _DetuctLateMin = 0
    '                Else

    '                    'If _StateWorkOffSite > 0 Then 'ไปปฏิบัติงานนอกสถานที่
    '                    '    _nTime = _StateWorkOffSite

    '                    '    _LateCutN = 0
    '                    '    _FNAbsentCut = 0

    '                    '    If _nTime >= _nTimeCtrl Then
    '                    '        _LateNormalNotCut = 0
    '                    '        _LateCutN = 0
    '                    '        _FNAbsentCut = 0
    '                    '    End If

    '                    '    If _nTime >= _nTimeCtrl Then
    '                    '        _Absent = 0
    '                    '        _AbsentSP = 0
    '                    '    Else
    '                    '        _Absent = _nTimeCtrl - _StateWorkOffSite
    '                    '    End If

    '                    '    _LateCutOT = 0
    '                    'Else
    '                    _LateCutN = 0
    '                    _LateCutOT = 0
    '                    _nTime = _nTime
    '                    '_nTime = _nTime + IIf(_nTime = 0, 0, _LateNormal)
    '                    ' _nOt15 = _nOt15 + _LateOT
    '                    _nOt15 = _nOt15 '+ _LateOT
    '                    _LateNormalNotCut = IIf(_nTime = 0, 0, _LateNormal)

    '                    _StateLate = ""
    '                    _DetuctLateType = ""
    '                    _DetuctLateMin = 0
    '                End If
    '                ' End If

    '            End If

    '            '------------------------------------------------ ตรวจสอบการหักขาดงาน -----------------------------------------
    '            _CutAbsent = _Absent

    '            '------------------------------------------------  ตรวจสอบการหักขาดงาน -----------------------------------------

    '            '------------------------------------------------ ตรวจสอบการปัดเศษ OT -----------------------------------------
    '            Dim _SpareOTMin As Integer = _nOt15 Mod 60
    '            If Not (_StatePayOTOverRequest) Then
    '                _R = _DTOTRound.Select(" FTCfgOTBegin <= " & _SpareOTMin & " AND FTCfgOTEnd >=" & _SpareOTMin & " AND FNHSysEmpTypeId=" & Val(_FTTypeEmp) & " ")
    '                If _R.Length > 0 Then
    '                    For Each IR As DataRow In _R

    '                        If IR!FTStatePay.ToString = "1" Then
    '                            _SpareOTMin = _SpareOTMin
    '                        Else
    '                            _SpareOTMin = Val(IR!FTCfgOTSet)
    '                        End If

    '                        Exit For
    '                    Next
    '                Else
    '                    _SpareOTMin = -1
    '                End If
    '            Else
    '                _SpareOTMin = -1
    '            End If

    '            If _SpareOTMin > -1 Then
    '                _nOt15 = (_nOt15 - (_nOt15 Mod 60)) + _SpareOTMin
    '            End If
    '            '------------------------------------------------ ตรวจสอบการปัดเศษ OT -----------------------------------------

    '            '------------------------------------------------ ตรวจวันทำงานว่าเป็น วันปกติ,วันหยุด หรือ วันนักขัต ---------------------
    '            If _UseBarcode <> "2" Then
    '                Select Case _SPDateType
    '                    Case 0
    '                        _nOt1 = _nOt15
    '                        _nOt15 = 0
    '                        _nOt2 = 0 + _nOtHSP2
    '                        _nOt3 = 0
    '                        _nOt4 = 0 + _nOtHSP4
    '                    Case 1
    '                        _nOt2 = _nOt15 + _nOtHSP2
    '                        _nOt15 = _nTime
    '                        _nOt1 = 0
    '                        _nOt3 = 0
    '                        _nOt4 = 0 + _nOtHSP4

    '                        _nTime = 0
    '                    Case 2
    '                        _nOt2 = 0 + _nOtHSP2
    '                        _nOt1 = 0
    '                        _nOt3 = _nTime
    '                        _nOt4 = _nOt15 + _nOtHSP4
    '                        _nOt15 = 0
    '                        _nTime = 0
    '                End Select
    '            Else
    '                Select Case _SPDateType
    '                    Case 1, 2
    '                        _nOt2 = 0
    '                        _nOt1 = 0
    '                        _nOt3 = 0
    '                        _nOt4 = 0
    '                        _nOt15 = 0
    '                        _nTime = 0
    '                        _LateCutOT = 0
    '                        _nTime = 0
    '                        _nOt15 = 0
    '                        _LateNormalNotCut = 0
    '                        _StateLate = ""
    '                        _DetuctLateType = ""
    '                        _DetuctLateMin = 0
    '                End Select
    '            End If

    '            '------------------------------------------------ ตรวจวันทำงานว่าเป็น วันปกติ,วันหยุด หรือ วันนักขัต ---------------------

    '            '------------------------------------------------ ตรวจสอบการ Adjust OT  ถ้ามีการ Adjust ยึดค่า Adjust ------------
    '            If (_EditOt1 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _nOt1 = _EditOt1
    '            End If

    '            If (_EditOt2 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _nOt2 = _EditOt2
    '            End If

    '            If (_EditOt4 <> -1) Then  '----- มีการแก้ไข OT จากหน้า Time Attendance
    '                _nOt4 = _EditOt4
    '            End If
    '            '------------------------------------------------ ตรวจสอบการ Adjust OT  ถ้ามีการ Adjust ยึดค่า Adjust ------------

    '            _Qry = "  UPDATE dbo.THRTTrans SET  FTUpdUser  ='" & HI.UL.ULF.rpQuoted(_User) & "' "
    '            _Qry &= vbCrLf & ",FTUpdDate = CONVERT(varchar(10),GetDate(),111)"
    '            _Qry &= vbCrLf & ",FTUpdTime = CONVERT(varchar(8),GetDate(),114)"
    '            _Qry &= vbCrLf & ",FNTime =  Convert(numeric(18,2)," & _nTime & " / 60) + Convert(numeric(18,2),((" & _nTime & " %60) /100.00)) "
    '            _Qry &= vbCrLf & ",FNOTRequest =Convert(numeric(18,2)," & _OTRequest & " / 60) + Convert(numeric(18,2),((" & _OTRequest & "  %60) /100.00))  "
    '            _Qry &= vbCrLf & " ,FNOT1 = Convert(numeric(18,2)," & _nOt1 & " / 60) + Convert(numeric(18,2),((" & _nOt1 & " %60) /100.00))  "
    '            _Qry &= vbCrLf & " ,FNOT1_5 = Convert(numeric(18,2)," & _nOt15 & " / 60) + Convert(numeric(18,2),((" & _nOt15 & " %60) /100.00))  "
    '            _Qry &= vbCrLf & " ,FNOT2 = Convert(numeric(18,2)," & _nOt2 & " / 60) + Convert(numeric(18,2),((" & _nOt2 & " %60) /100.00))   "
    '            _Qry &= vbCrLf & " ,FNOT3 = Convert(numeric(18,2), " & _nOt3 & " / 60) + Convert(numeric(18,2),((" & _nOt3 & "  %60) /100.00))    "
    '            _Qry &= vbCrLf & " ,FNOT4 = Convert(numeric(18,2), " & _nOt4 & " / 60) + Convert(numeric(18,2),((" & _nOt4 & "  %60) /100.00))    "
    '            _Qry &= vbCrLf & " ,FNLateNormalMin =" & _LateNormal
    '            _Qry &= vbCrLf & ",FNLateNormalCut=" & _LateCutN
    '            _Qry &= vbCrLf & " ,FNRetireNormalMin =" & _RetryNormal
    '            _Qry &= vbCrLf & ",FNRetireNormalCut =" & _RetryNormal
    '            _Qry &= vbCrLf & ",FNLateOtMin =" & _LateOT
    '            _Qry &= vbCrLf & ",FNLateOtCut=" & _LateCutOT
    '            _Qry &= vbCrLf & ",FNRetireOtMin =" & _RetryOT
    '            _Qry &= vbCrLf & " ,FNRetireOtCut=" & _RetryOT
    '            _Qry &= vbCrLf & ",FNAbsent =" & _Absent
    '            _Qry &= vbCrLf & " ,FNTimeMin=" & _nTime
    '            _Qry &= vbCrLf & " ,FNOT1Min =" & _nOt1
    '            _Qry &= vbCrLf & " ,FNOT1_5Min=" & _nOt15
    '            _Qry &= vbCrLf & " ,FNOT2Min=" & _nOt2
    '            _Qry &= vbCrLf & ",FNOT3Min=" & _nOt3
    '            _Qry &= vbCrLf & ",FNOT4Min=" & _nOt4
    '            _Qry &= vbCrLf & ",FNLateMMin=" & _FNLateMMin
    '            _Qry &= vbCrLf & ",FNLateAfMin=" & _FNLateAfMin
    '            _Qry &= vbCrLf & " ,FNRetireMMin =" & _FNRetireMMin
    '            _Qry &= vbCrLf & " ,FNRetireAfMin=" & _FNRetireAfMin
    '            _Qry &= vbCrLf & " ,FNOTRequestMin =" & _OTRequest
    '            _Qry &= vbCrLf & " ,FNAbsentCut=" & _FNAbsentCut
    '            _Qry &= vbCrLf & " ,FNCutAbsent=" & _CutAbsent
    '            _Qry &= vbCrLf & " ,FTLateCode ='" & HI.UL.ULF.rpQuoted(_StateLate) & "'"
    '            _Qry &= vbCrLf & ",FNHSysEmpTypeId  =" & Val(_FTTypeEmp) & "  "
    '            _Qry &= vbCrLf & " ,FNLateNormalNotCut = " & _LateNormalNotCut
    '            _Qry &= vbCrLf & " ,FNAbsentSP  =" & _AbsentSP
    '            _Qry &= vbCrLf & " ,FNSpecialTimeMin  =" & _FNSpecialTimeMin
    '            _Qry &= vbCrLf & " ,FTStateAccept='" & _FTStateAcceptTimeAuto & "' "
    '            _Qry &= vbCrLf & " ,FTStateRevised=CASE WHEN ISNULL(FTStateAccept,'') ='1' THEN '1' ELSE '0' END "
    '            _Qry &= vbCrLf & " ,FTStateRevisedDate=CASE WHEN ISNULL(FTStateAccept,'') ='1' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE '' END "
    '            _Qry &= vbCrLf & " ,FTStateRevisedTime=CASE WHEN ISNULL(FTStateAccept,'') ='1' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE '' END "
    '            _Qry &= vbCrLf & " ,FTStateRevisedBy=CASE WHEN ISNULL(FTStateAccept,'') ='1' THEN '" & HI.UL.ULF.rpQuoted(_User) & "' ELSE '' END "
    '            _Qry &= vbCrLf & "   WHERE (FNHSysEmpID ='" & Val(_EmpCode) & "' )"
    '            _Qry &= vbCrLf & "   AND FTDateTrans ='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' "

    '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        Next

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function



    Public Shared Function GetDayPerMonth(ByVal _FTPayTerm As String, ByVal _FTPayYear As String, ByVal _FNHSysEmpTypeId As Integer) As Integer
        Try
            Dim _FNWorkDayInWeekBF As Integer = 0
            Dim _FNWorkDayInWeek As Integer = 15
            Dim _FNWorkDayInMonth As Integer = 30
            Dim _dtWKDay As DataTable

            Dim _Qry As String = ""
            _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNMonthWorkDay"
            _Qry &= vbCrLf & " ,B.FNWorkDay AS FNWeekWorkDay"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  INNER Join"
            _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "') "
            _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(_FTPayYear) & "') "
            _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(_FNHSysEmpTypeId) & ")) AS B"
            _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
            _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
            _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
            _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
            _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _dtWKDay.Rows

                If Val(R!FNMonthWorkDay.ToString) > 0 Then
                    _FNWorkDayInMonth = Val(R!FNMonthWorkDay.ToString)

                    If _FNWorkDayInMonth > 26 Then _FNWorkDayInMonth = 26

                End If

                Exit For
            Next
            Return _FNWorkDayInMonth
        Catch ex As Exception
            Return 26
        End Try
    End Function

    Public Shared Sub CalChildCare(ByVal _ChildBirthDay As String, ByVal _DateStartOfMonth As String, ByRef _OfMonthBegin As String, ByRef _OfMonthEnd As String)
        Try
            Dim _Qry As String = ""
            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_Datediff '" & _ChildBirthDay & "','" & _DateStartOfMonth & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Dim _Month As Integer = (Integer.Parse("0" & _oDt.Rows(0).Item("FNYear").ToString) * 12) + Integer.Parse("0" & _oDt.Rows(0).Item("FNMonth").ToString)
            Dim _Day As Integer = Integer.Parse("0" & _oDt.Rows(0).Item("FNDay").ToString)

            _OfMonthBegin = _Month + IIf(_Day > 0, 1, 0)
            _OfMonthEnd = _Month + IIf(_Day > 0, 1, 0)
        Catch ex As Exception
        End Try
    End Sub



    Public Shared Function CalculateWeekEnd_VN(ByVal _User As String, ByVal _EmpCode As String,
      ByVal _EmpType As String, ByVal _StartDate As String, ByVal _EndDate As String, ByVal _PayYear As String,
      ByVal _PayTerm As String, ByVal _PayDate As String, ByVal _CalIns As String, ByVal _EmpCalType As String, ByVal FNBusinessWorkday As Integer,
      Optional ByVal _StateCalRetire As Boolean = False, Optional ByVal _ReturnVacation As Double = 0,
      Optional FTStaDeductAbsent As Integer = 0, Optional FTStaCalPayRoll As Integer = 0, Optional FNStateSalaryType As Integer = 0, Optional _FNExchangeRate As Double = 0, Optional _FNExchangeRateTHB As Double = 0) As Boolean

        '----------------------------------   Variable  ------------------------------------
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        Dim CountDayPerMonth As Integer = FNBusinessWorkday
        Dim FNSocialEmployeeRate As Double = 0
        Dim FNSocialEmployerRate As Double = 0
        Dim FNHealthEmployeeRate As Double = 0
        Dim FNHealthEmployerRate As Double = 0
        Dim FNUnemploymentEmployeeRate As Double = 0
        Dim FNUnemploymentEmployerRate As Double = 0
        Dim FNUnionEmployeeRate As Double = 0
        Dim FNUnionEmployerRate As Double = 0

        Try
            FNSocialEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eSocialInsurance).FNEmployeeRate
            FNSocialEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eSocialInsurance).FNEmployerRate
            FNHealthEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eHealthInsurance).FNEmployeeRate
            FNHealthEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eHealthInsurance).FNEmployerRate
            FNUnemploymentEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnemploymentInsurance).FNEmployeeRate
            FNUnemploymentEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnemploymentInsurance).FNEmployerRate
            FNUnionEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnionInsurance).FNEmployeeRate
            FNUnionEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnionInsurance).FNEmployerRate
        Catch ex As Exception
            'MG.ShowMsg.mInfo("Invalid Config Value...", 1503310001, "", "")
        End Try


        Dim FNSocialInsuranceEmployee As Double = 0.0
        Dim FNSocialInsuranceEmployer As Double = 0.0
        Dim FNHealthInsuranceEmployee As Double = 0.0
        Dim FNHealthInsuranceEmployer As Double = 0.0
        Dim FNUnemploymentInsuranceEmployee As Double = 0.0
        Dim FNUnemploymentInsuranceEmployer As Double = 0.0
        Dim FNUnionInsuranceEmployee As Double = 0.0
        Dim FNUnionInsuranceEmployer As Double = 0.0

        Dim FNSocialInsuranceEmployeeOrg As Double = 0.0
        Dim FNSocialInsuranceEmployerOrg As Double = 0.0
        Dim FNHealthInsuranceEmployeeOrg As Double = 0.0
        Dim FNHealthInsuranceEmployerOrg As Double = 0.0
        Dim FNUnemploymentInsuranceEmployeeOrg As Double = 0.0
        Dim FNUnemploymentInsuranceEmployerOrg As Double = 0.0
        Dim FNUnionInsuranceEmployeeOrg As Double = 0.0
        Dim FNUnionInsuranceEmployerOrg As Double = 0.0

        REM Dim FNMaxWorkday As Integer = 26
        REM Dim FNBusinessWorkday As Integer

        Dim FNCntAddFinCode015 As Integer = 0

        Dim _Qry As String
        Dim _dt As DataTable
        Dim _dttemp As DataTable
        Dim _dttran As DataTable
        Dim _SalaryDevide As Integer = 0
        Dim FNVacationRetMin, FNVacationRetAmt As Double

        Dim _DtFin As New DataTable

        _DtFin.Columns.Add("FTFinCode", GetType(String))
        _DtFin.Columns.Add("FCTotalFinAmt", GetType(String))

        Dim _Err As Integer, _Complete As Integer, _ActualDate As String
        Dim _FCSalary As Double, _FDDateStart As String
        Dim _FDDateEnd As String, FDDateProbation As String
        Dim _FTPaymentCode As String, _FTBankCode As String, _FNChildNotLearn As Double
        Dim _FCReduceDonate As Double, _FCLifeInsurance As Double
        Dim _FCLoanHouse As Double
        Dim _FCShare As Double, _FCReduceFather As Double, _FCReduceMother As Double
        Dim _FCReSpouseFather As Double, _FCReSpouseMother As Double, _FCReduceEducationSupport As Double, _FTMarryIncome As String
        Dim _FTCalSocialSta As String, _FTCalTaxSta As String
        Dim _FTDeptCode As String, _FTSectCode As String, _FTUnitCode As String
        Dim _FTEmpIdNo As String, _FTBranchCode As String, _FTAccNo As String, _FCLifeFeeMoney As Double
        Dim _FCOtherAdd As Double, _FTOtherAddCalculateSocial As String, _FTOtherAddCalculateTax As String
        Dim _FCOtherAddOt As Double, _FTOtherAddOtCalculateSocial As String, _FTOtherAddOtCalculateTax As String, _FCBFShiftMoney As Double
        Dim _FTShiftMoneyCalculateSocial As String, _FTShiftMoneyCalculateTax As String, _FCDiligent As Double
        Dim _FTDiligentCalculateSocial As String, _FTDiligentCalculateTax As String, _FCBonusEndYear As Double
        Dim _FTBonusEndCalculateSocial As String, _FCOtherDeduct As Double, _FTBonusEndCalculateTax As String
        Dim _FCShelter As Double, _FTShelterCalculateSocial As String, _FTShelterCalculateTax As String
        Dim _FCShareFactory As Double, _FTShareFactoryCalculateSocial As String
        Dim FNPayLeaveBusinessBaht, FNPayLeaveSickBaht, FNPayLeaveSpecialBaht, FNParturitionLeave As Double
        Dim FNPayLeaveBusinessBahtMin, FNPayLeaveSickBahtMin, FNPayLeaveSpecialBahtMin, FNParturitionLeaveMin As Double
        Dim GFNPayLeaveBusinessBahtMin, GFNPayLeaveSickBahtMin, GFNPayLeaveSpecialBahtMin, GFNParturitionLeaveMin As Integer
        Dim FTTranStaCode As String
        Dim _FTShift As String
        Dim _FNTime, _FNNotRegis As Double
        Dim _FNOT1 As Double, _FNOT1_5 As Double, _FNOT2 As Double, _FNOT3, _FNOT4 As Double
        Dim _FTAOut, _FTOTIn1, _FTDateTrans As String

        Dim _FNLeaveVacation As Double, _FNLateNormalMin As Double
        Dim _FNLateNormalCut As Double, _FNLateOtMin As Double, _FNLateOtCut As Double
        Dim _FNLateMorning As Double, _FNLateAfternoon As Double, _FNAbsent As Double
        Dim _FNLeavePay, _FNLeaveNotPay As Double, _FNTimeMin As Double, _FNOT1Min As Double
        Dim _FNOT1_5Min As Double, _FNOT2Min As Double, _FNOT3Min As Double, _FNOT4Min As Double, _FNLateMMin As Double
        Dim _FNLateAfMin As Double, _FNRetireMMin As Double, _FNRetireAfMin As Double
        Dim _FNRetireNormalCut As Double, _FNRetireOtMin As Double, _FNRetireOtCut As Double
        Dim _GFNTime, _GFNNotRegis As Double
        Dim _GFNOT1 As Double, _GFNOT1_5 As Double, _GFNOT2 As Double, _GFNOT3 As Double, _GFNOT4 As Double
        Dim _GFNLeaveSick As Double, _GFNLeaveBusiness As Double
        Dim _GFNLeaveVacation As Double, _GFNLeavePregnant As Double, _GFNLeaveOrdain As Double
        Dim _GFNLeaveMarry As Double, _GFNLeaveOther As Double, _GFNLateNormalMin As Double
        Dim _GFNLateNormalCut As Double, _GFNLateOtMin As Double, _GFNLateOtCut As Double
        Dim _GFNLateMorning As Double, _GFNLateAfternoon As Double, _GFNAbsent, _GFNCutAbsent As Double
        Dim _GFNLeavePay As Double, _GFNTimeMin, _GFNOT1Min As Double
        Dim _GFNOT1_5Min As Double, _GFNOT2Min As Double, _GFNOT3Min As Double, _GFNOT4Min As Double, _GFNLateMMin As Double
        Dim _GFNLateAfMin As Double, _GFNRetireMMin As Double, _GFNRetireAfMin As Double
        Dim _GFNRetireNormalCut As Double, _GFNRetireOtMin As Double, _GFNRetireOtCut As Double
        Dim _dtot As DataTable
        Dim _RateOT1, _RateOT15, _RateOT2, _RateOT3, _RateOT4 As Double
        Dim _FCAccumulateIncome As Double, _FCAccumulateSocial As Double, _FCAccumulateTax As Double
        Dim _FTSatrtCalculateDate As String, _FTEndCalculateDate As String, _FNEmpDiligent As Double, _FTStateInDustin As String, _FNDeligentPeriod As Integer
        Dim _FTSatrtCalculateDateIns As String, _FTEndCalculateDateIns As String
        Dim _FNEmpBaht, _nBahtOt1 As Double, _nBahtOt15 As Double, _nBahtOt2 As Double, _nBahtOt3 As Double, _nBahtOt4 As Double, _nBahtAbsent As Double, _nEstimateIncome As Double
        Dim _SocialRate As Double
        Dim _WorkDay As Integer, _TotalWorkDay As Integer, _Holiday As String
        Dim _TotalHoliDay As Integer
        Dim _FNSlaryPerMonth As Double, _FNSlaryPerDay As Double, _FNSlaryPerHour As Double, _FNSlaryPerMin, _FNSlaryOTPerMin As Double, _FNSlaryOTPerHour As Double, _FTEmpState As String
        Dim _FNSlaryOTPerMonth, _FNSlaryOTPerDay As Double

        Dim _Lapaid, _LaNotpaid As Double, _FCPayVacationBaht As Double, _Net As Double, _CalSo As Double, _HBaht As Double, _FCSocial As Double
        Dim _FCTax As Integer, _FCBaht As Double, _ActualNextDate As String
        Dim _SocialMinIncome As Integer, _SocialMaxIncome As Double
        Dim _FTSlary, _FTDivCode, _FTPos As String
        Dim _MSlary As Double, _LateCutAbsent As Double, _LateCutAmt, _LateCutAmtAbsent As Double
        Dim _Dtemp As DataTable
        Dim _SocialBefore, _SocialBeforeAmt, _SocialPayMax As Double
        Dim _FCAdd, _FTAddCalculateSocial, _FTAddCalculateTax, _FCDeduct, _TotalCalSso, _TotalCalTax, _TaxAmt As Double
        Dim _Gtotalleave, _GtotalleavePay, _GtotalleaveNotPay, _GtotalleavePayCalSso, _GtotalleavePayCalSsoAmt As Double
        Dim _dtLeave As DataTable
        Dim _LeaveCode As String = ""
        Dim _dtAddOtherAmt As DataTable
        Dim _dtAddOtherAmtshift As DataTable
        Dim FCModFather, FCModMother, FCModMateFather, FCModMateMother As Double
        Dim FCPremium, FCInterest, FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDisabledDependents, FCDeductDonateStudy As Double
        Dim FCHealthInsurFatherMotherMate, FTHealthInsurIDFather, FTHealthInsurIDMother As Double
        Dim FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate As Double
        Dim FTTotalCalContributedAmt, FTContributedAmt, FTCmpContributedAmt, FTTotalCalContributedAcc As Double
        Dim FTTotalCalWorkmen, FTWorkmenAmt, _FTMaxCalWorkmen, _FTMaxWorkmenRate, FTTotalCalWorkmenAcc As Double
        Dim _FTWorkmenAmtBefore, _FTTotalCalWorkmenBefore As Double
        Dim _ShiftAmt As Double = 0
        Dim _ShiftOTAmt As Double = 0
        Dim _ShiftValue As Double = 0
        Dim _ShiftOTValue As Double = 0
        Dim _WorkingDay As Double = 0
        Dim _THRMContributedFund As DataTable
        Dim _EmpDisTax As New HCfg.EmployeeDiscountTax
        Dim _EmpTaxYear As New HCfg.EmpTaxYear
        Dim _FNNetpayOrg As Double = 0.0
        Dim _FNNetpay As Double = 0.0
        Dim _AmtAddCalOT, _GAmtAddCalOT As Double
        Dim CountTerm As Integer = 0
        Dim _SPDateType, _TotalInstalment, _Instalment As Integer
        Dim _ContributedFundBeginPay As Boolean = False
        Dim _DTHoliday As DataTable
        Dim _ShiftAdv As Double = 0
        Dim _AmtPlus As Double = 0
        Dim _GAmtPlus As Double = 0
        Dim FTHldType As Integer = 0
        Dim _AmtRetire As Double = 0
        Dim _WorkAge As Integer = 0
        Dim _AmtReturnVacation As Double = 0
        Dim _FNIncentiveAmt As Double = 0
        Dim _FTInsurType As Integer = 0
        Dim _DayAdjAdd As Double = 0
        Dim _WageAdjAdd As Double = 0
        Dim _DateStartOfMonth As String = HI.UL.ULDate.ConvertEnDB(Left(_EndDate, 8) & "01")  'วันแรกของเดือน
        Dim _DateEndOfMonth As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", 1), -1)) 'วันแของเดือน
        Dim _FTStatePayHoliday As String = ""
        Dim _FTEmployeeCode As String = ""
        Dim _FNAttandanceAmt As Double = 0
        Dim _FNHealtCareAmt As Double = 0
        Dim _FNTransportAmt As Double = 0
        Dim _FNChildCareAmt As Double = 0
        Dim _FNChildCareStartAge As Double = 0
        Dim _FNChildCareEndAge As Double = 0
        Dim _FNChildCareMaxPeople As Integer = 0
        Dim _FNOTMealAmt As Double = 0
        Dim _FNSocialBase As Double = 0
        Dim _FNWorkAgeSalary As Double = 0
        Dim _FNOTMealAmtUS As Double = 0
        Dim _FNSickLeave As Double = 0
        Dim _LeaveSickPay As Integer = 0
        Dim _FNTotalChildCare As Integer = 0
        Dim _FNNetAttandanceAmt As Double = 0
        Dim _FNNetChildCareAmt As Double = 0
        Dim _FNNetOTMealAmt As Double = 0
        Dim _FNNetSocialBase As Double = 0
        Dim _FNNetOTMealAmtUS As Double = 0
        Dim _FNEmpWorkAge As Integer = 0
        Dim _tmpSocailRateKM As DataTable
        Dim _tmpWelfareKM As DataTable
        Dim _tmpWorkAge As DataTable

        Dim tmpDTConfigAllowancePassProba As System.Data.DataTable
        Dim tmpDTWelfareVN As System.Data.DataTable
        Dim FNAttendanceAllowanceVND As Double = 0.0
        Dim FNCarAllowanceVND As Double = 0.0
        Dim FNMealAllowanceVND As Double = 0.0
        Dim FTProbationSta As String = ""
        Dim FNSkillRate As Double = 0
        Dim FNHarmfulRate As Double = 0
        Dim FNSkillBaht As Double = 0
        Dim FNHarmfulBaht As Double = 0
        Dim FNPayRestOTBaht As Double = 0
        Dim FNPayRestOTMin As Double = 0

        'New cal OT คิดจากเงินเดือนจริง สรุปเวียดนาม 2016/08/30
        Dim _SalaryCalOT As Double = 0

        Dim FNWageScale As Double = 0
        Dim FNHolidaySpecial As Double = 0

        Dim _NewHarmfulBaht As Double = 0
        Dim _NewSkillBaht As Double = 0

        Dim FTChkRaceThaiWorker As String = ""
        Dim FNBasicSalaries As Double = 0
        Dim FNMaximumBasicSalaries As Double = 0
        Dim FNModPersonTaxRate As Double = 0
        Dim FNModChildAllowanceTaxRate As Double = 0
        Dim FNCntEmployeeChild As Integer = 0
        Dim FNThaiWorkerNoWorkpermitTaxRate As Double = 0
        Dim FNNotRecalInsuranceEmpResign As Integer = 16
        'Dim FTStaCalSkillAllowance As String = ""
        Dim _EmpSex As Integer = 0
        Dim _SpecialHoliday As Double = 0

        Dim _DTEmpWorkDay As New DataTable
        Dim _DTTHRMTimeScanCard As DataTable
        Dim _Shift As String
        Dim _ScanTimeOverClock As String, _FTTypeEmp As String
        Dim _ScanCardOverClock As String, _nTimeCtrl As Double, _LateCompro As Integer
        Dim _ActualScanMIn As String, _ActualScanMOut As String, _ActualScanAIn As String, _ActualScanAOut As String, _ActualScanOTIn1 As String, _ActualScanOTOut1 As String, _ActualScanOTIn12 As String, _ActualScanOTOut12 As String, _ActIn01 As String, _ActOut01 As String
        Dim _CheckTimeMIn As String, _CheckTimeMOut As String, _CheckTimeAIn As String, _CheckTimeAOut As String, _CheckTimeOTIn1 As String, _CheckTimeOTOut1 As String, _LateOT As Integer, _RetryOT As Integer
        Dim _PayRate As Double = 0
        Dim RR As DataRow()

        _ActualDate = ActualDate
        _ActualNextDate = ActualNextdate

        _DTEmpWorkDay.Columns.Add("FNSalary", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNDay", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNOT1", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNOT15", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNOT2", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNOT3", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNOT4", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNHoloday", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNLate", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNAbsent", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNLateCutAmtAbsent", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNLeavePay", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNLeaveNotPay", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNBusiness", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNSpecial", GetType(Double))
        _DTEmpWorkDay.Columns.Add("FNParturition", GetType(Double))

        Dim _DTEmpPayLeaveSick As New DataTable
        _DTEmpPayLeaveSick.Columns.Add("FNSalary", GetType(Double))
        _DTEmpPayLeaveSick.Columns.Add("FNDay", GetType(Double))
        _DTEmpPayLeaveSick.Columns.Add("FNPayPer", GetType(Double))

        REM 2014/10/31 vietnam factory
        '...business workday
        '_Qry = ""
        '_Qry = "SELECT [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[FN_BUSINESS_WORKING_DAY](" & Integer.Parse(_PayYear) & ", " & Integer.Parse(_PayTerm) & ", " & FNMaxWorkday & ") AS FNBusinessWorkday;"
        'FNBusinessWorkday = Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR, "26")) '...working per month
        'If System.Diagnostics.Debugger.IsAttached = True Then
        '    MsgBox("FNBusiness Workday : {" & String.Format("{0} days.", FNBusinessWorkday) & "}" & Environment.NewLine & "Payment Year : " & _PayYear & Environment.NewLine & "Payment Term : " & _PayTerm, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Re-Calcualte Month End...")
        'End If

        _Qry = "SELECT TOP 1 FNCalType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
        Dim _TmpCalType As Integer = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

        If FTStaCalPayRoll = 0 Then
            _DateStartOfMonth = _StartDate  'วันแรกของเดือน
            _DateEndOfMonth = _EndDate 'วันสุดท้ายของเดือน
        Else
            _DateStartOfMonth = HI.UL.ULDate.ConvertEnDB(Left(_EndDate, 8) & "01")  'วันแรกของเดือน
            _DateEndOfMonth = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", 1), -1)) 'วันสุดท้ายของเดือน
        End If
        '----------------------------------   Variable  ------------------------------------

        Dim _DateReset As String
        _Qry = "SELECT CASE WHEN RIGHT(FTCurrenDate,5) >= FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
        _Qry &= vbCrLf & "FROM ("
        _Qry &= vbCrLf & "SELECT  TOP 1 CONVERT(VARCHAR(10), GETDATE(), 111) AS FTCurrenDate, CONVERT(VARCHAR(10), DATEADD(YEAR, -1, GETDATE()), 111) AS FTBefore, L.FTLeaveReset"
        _Qry &= vbCrLf & "      FROM  THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK ) ON L.FNHSysEmpTypeId = M.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "      WHERE   M.FNHSysEmpID=" & Val(_EmpCode) & " "
        _Qry &= vbCrLf & "      ) AS T"

        _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        '------------------ รวมวันลาป่วย---------------------------------

        _Qry = "SELECT (SUM(FNTotalPayMinute) / 480) AS FNTotalPayMinute  "
        _Qry &= vbCrLf & " AS TotalLeavePay"
        _Qry &= vbCrLf & "FROM THRTTransLeave "
        _Qry &= vbCrLf & "WHERE (FTLeaveType = '0')"
        _Qry &= vbCrLf & "       AND (FTDateTrans >= N'" & (_DateReset) & "') "
        _Qry &= vbCrLf & "       AND (FTDateTrans < N'" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "') "
        _Qry &= vbCrLf & "       AND (FNHSysEmpID =" & Val(_EmpCode) & ")"

        _LeaveSickPay = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

        '------------------ รวมวันลาป่วย---------------------------------

        With _EmpTaxYear
            .FTAmt = 0 'เงินได้ก่อนหักค่าใช้จ่าย
            .FTExpenses = 0 'ค่าใช้จ่ายส่วนตัว
            .FTNetAmt = 0 'เงินได้หลังหักค่าใช้จ่าย
            .FTModEmp = 0 'ลดหย่อนส่วนตัว
            .FTModMate = 0 'ลดหย่อนคู่สมรส
            .FNChildNotLern = 0 'จำนวนบุตรไม่ศึกษา
            .FNChildLern = 0 'จำนวนบุตรศึกษา
            .FTChildNotLern = 0 'ลดหย่อนบุตรไม่ศึกษา
            .FTChildLern = 0 'ลดหย่อนบุตรศึกษา
            .FTInsurance = 0 'ลดหย่อนเบี้ยประกัน
            .FTProvidentfund = 0 'กองทุนเลียงชีพส่วนที่ไม่เกิน 10000
            .FTInterest = 0 'ดอกเบี้ยเงินกู้
            .FTSocial = 0 'ประกันสังคม
            .FTDonation = 0 'เงินบริจาค
            .FTProvidentfundOver = 0 'กองทุนเลียงชีพส่วนที่เกิน 10000
            .FTGPF = 0 'เงิน กบข.
            .FTSavingsFund = 0 'เงินกองทุนสงเคราะห์
            .FTCommutation = 0 'เงินชดเชยตามกฎหมายแรงงาน
            .FTUnitRMF = 0 'ค่าซื้อหน่วยลงทุน RTF
            .FTModFather = 0 'ลดหย่อนบิดา
            .FTModMother = 0 'ลดหย่อนมารดา
            .FTModFatherMate = 0 'ลดหย่อนบิดาคู่สมรส
            .FTModMotherMate = 0 'ลดหย่อนมารดาคู่สมรส
            .FTUnitLTF = 0 'ค่าซื้อหน่วยลงทุน LTF
            .FTDonationLern = 0 'เงินบริจาคเพื่อสนับสนุนการศึกษา
            .FTParentsHealthInsurance = 0 'เบี้ยประกันสุขภาพบิดามารดา
            .FTSupportSport = 0 'เงินสนับสนุนการกีฬา
            .FTAcquisitionOfProperty = 0 'ค่าซื้ออาคาร
            .FTPension = 0 'บำนาญ
            .FTTravel = 0 'ท่องเที่ยวในประเทศ
            .FTTotalCalTax = 0 'เงินได้สุทธิ
            .FTTotalTax = 0 'ภาษีที่ต้องจ่าย
        End With
        '------------------ GetConfig WeekEnd ----------------------------------

        '------------------ GetConfig หักกองทุนสำรองเลี้ยงชีพ ----------------------------------
        _Qry = "SELECT FNSeqNo, FNAgeBegin, FNAgeEnd, FNEmpPay As FNEmpAmtPer,  FNCmpPay AS FNCmpAmtPer"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
        _THRMContributedFund = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '------------------ GetConfig หักกองทุนสำรองเลี้ยงชีพ ----------------------------------

        '------------------ GetConfig Holiday ----------------------------------
        _DTHoliday = LoadSysHoliday(_PayYear)
        '------------------ GetConfig Holiday ----------------------------------

        '...Cambodia and vn Factory
        _tmpSocailRateKM = LoadSocailRateKM
        _tmpWelfareKM = LoadWelfareKM(Integer.Parse(Val(_EmpType)))
        _tmpWorkAge = GetWorkAgeRate

        '...Cambodia Factory
        '==================================================================================================================
        For Each ZRow In _tmpWelfareKM.Rows

            '_FNAttandanceAmt = Val(ZRow!FNAttandanceAmt.ToString)
            '    _FNHealtCareAmt = Val(ZRow!FNHealtCareAmt.ToString)
            '   _FNTransportAmt = Val(ZRow!FNTransportAmt.ToString)
            _FNChildCareAmt = Double.Parse("0" & ZRow!FNChildCareAmt.ToString)
            _FNChildCareStartAge = Val(ZRow!FNChildCareStartAge.ToString)
            _FNChildCareEndAge = Val(ZRow!FNChildCareEndAge.ToString)
            _FNChildCareMaxPeople = Integer.Parse("0" & ZRow!FNChildCareMaxPeople.ToString)
            '  _FNOTMealAmt = Val(ZRow!FNOTMealAmt.ToString)

            Exit For

        Next
        '==================================================================================================================

        '...Allowance (Rate) Income after pass probation Vietnam Factory
        '===================================================================================================================
        tmpDTConfigAllowancePassProba = LoadConfigAllowanceProbation
        For Each oRow As DataRow In tmpDTConfigAllowancePassProba.Rows
            FNSkillRate = Val(oRow!FNSkillRate.ToString()) '...อัตราเปอร์เซ็นต์ การคิด ค่าทักษะ
            FNHarmfulRate = Val(oRow!FNHarmfulRate.ToString()) '...อัตราการเปอร์เซ็นต์ การคิด ค่าเสี่ยงภัย
            FNMaximumBasicSalaries = Val(oRow!FNMaximumBasicSalaries.ToString()) '...อัตราฐานเงินเดือนสูงสุด
            FNModPersonTaxRate = Val(oRow!FNModPersonTaxRate.ToString()) '...ลดหย่อนตนเอง
            FNModChildAllowanceTaxRate = Val(oRow!FNModChildAllowanceTaxRate.ToString()) '...ลดหย่อนบุตรต่อรายการบุตร 1 คน
            FNThaiWorkerNoWorkpermitTaxRate = Val(oRow!FNThaiWorkerNoWorkpermitTaxRate.ToString()) '...อัตราการคิดภาษีสำหรับ พนง. ที่มีสัญชาติไทย  และมีเลขที่บัตรประชาชน แต่ไม่มี Workpermit

            Exit For

        Next

        'tmpDTWelfareVN = LoadConfigWelfareVN() ' ระบบคำนวณ
        'For Each oDataRow As System.Data.DataRow In tmpDTWelfareVN.Select("FNHSysEmpTypeId = " & Val(_EmpType) & " ")
        '    FNAttendanceAllowanceVND = Val(oDataRow!FNAttendanceAllowance.ToString) '...FinCode : 015
        '    FNCarAllowanceVND = Val(oDataRow!FNCarAllowance.ToString)   '...FinCode : 024
        '    FNMealAllowanceVND = Val(oDataRow!FNMealAllowance.ToString) '...FinCode : 023
        '    Exit For
        'Next
        Dim _Cmd As String = ""
        _Cmd = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
        _Cmd &= vbCrLf & " FROM ("
        _Cmd &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Cmd &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
        Else
            _Cmd &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
        End If

        _Cmd &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,N'')=N'' THEN  0.00 ELSE   FTFinAmt  END AS FTFinAmt"
        _Cmd &= vbCrLf & " FROM "
        _Cmd &= vbCrLf & " ("
        _Cmd &= vbCrLf & " SELECT FTFinCode,FTType "
        _Cmd &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
        _Cmd &= vbCrLf & " WHERE   ISNULL(FTType,N'')=N'2'  "
        _Cmd &= vbCrLf & " AND ISNULL(FTStaActive,N'')=N'1' "
        _Cmd &= vbCrLf & ") THRMFinanceSet"
        _Cmd &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType=N'1'"
        _Cmd &= vbCrLf & " Left JOIN"
        _Cmd &= vbCrLf & " ("
        _Cmd &= vbCrLf & " SELECT FTFinCode,FTFinAmt "
        _Cmd &= vbCrLf & " FROM THRMEmployeeFin  WITH(NOLOCK) "
        _Cmd &= vbCrLf & " WHERE FNHSysEmpID =" & Integer.Parse(_EmpCode)
        _Cmd &= vbCrLf & ") THRMEmployeeFin"
        _Cmd &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
        _Cmd &= vbCrLf & " ) T  "
        _Cmd &= vbCrLf & "  WHERE ISNULL(FTFinDesc,N'') <> '' "
        _Cmd &= vbCrLf & " ORDER BY FNFinSeqNo"
        tmpDTWelfareVN = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
        For Each oDataRow As System.Data.DataRow In tmpDTWelfareVN.Rows
            If oDataRow!FTFinCode.ToString = "015" Then
                FNAttendanceAllowanceVND = Val(oDataRow!FTFinAmt.ToString) '...FinCode : 015
            End If
            If oDataRow!FTFinCode.ToString = "024" Then
                FNCarAllowanceVND = Val(oDataRow!FTFinAmt.ToString) '...FinCode : 024
            End If
            If oDataRow!FTFinCode.ToString = "023" Then
                FNMealAllowanceVND = Val(oDataRow!FTFinAmt.ToString) '...FinCode : 023
            End If
        Next
        '===================================================================================================================

        FNVacationRetMin = 0 : FNVacationRetAmt = 0
        _FTSatrtCalculateDate = _StartDate
        _FTEndCalculateDate = _EndDate
        _FTSatrtCalculateDateIns = _StartDate
        _FTEndCalculateDateIns = _EndDate
        _FNEmpDiligent = 0 : _TotalWorkDay = 0 : _WorkDay = 0 : _TotalHoliDay = 0
        _FTStateInDustin = "" : _FNSlaryPerMonth = 0
        _FNSlaryPerDay = 0 : _FNSlaryPerHour = 0 : _FNSlaryPerMin = 0
        _FTEmpState = "" : _FNEmpBaht = 0 : _nBahtOt1 = 0
        _nBahtOt15 = 0 : _nBahtOt2 = 0 : _nBahtOt3 = 0
        _nBahtAbsent = 0 : _nEstimateIncome = 0 : _Lapaid = 0 : _LaNotpaid = 0 : _Net = 0
        _FCPayVacationBaht = 0 : _CalSo = 0 : _HBaht = 0 : _FCSocial = 0
        _FCTax = 0 : _FCBaht = 0 : _SocialRate = 0
        _SocialMinIncome = 0 : _SocialMaxIncome = 0
        _Complete = 0 : _Err = 0 : _FCSalary = -99
        CountTerm = 0
        _TotalInstalment = 0 : _Instalment = 0

        FNPayLeaveBusinessBahtMin = 0 : FNPayLeaveSickBahtMin = 0 : FNPayLeaveSpecialBahtMin = 0 : FNParturitionLeaveMin = 0
        GFNPayLeaveBusinessBahtMin = 0 : GFNPayLeaveSickBahtMin = 0 : GFNPayLeaveSpecialBahtMin = 0 : GFNParturitionLeaveMin = 0
        FNPayLeaveBusinessBaht = 0 : FNPayLeaveSickBaht = 0 : FNPayLeaveSpecialBaht = 0 : FNParturitionLeave = 0

        _Qry = "SELECT  CONVERT(VARCHAR(10), GETDATE(), 111)"
        _ActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        _Qry = "SELECT  CONVERT(varchar(10), DATEADD(DAY, 1, GETDATE()), 111)"
        _ActualNextDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        '...employee detail master profile and current achivement status
        _Qry = ""
        '_Qry = "SELECT  TOP 1  M.FNHSysCmpId As FTCmpCode, M.FNHSysEmpID AS FTEmpCode,M.FTEmpCode AS FTEmployeeCode, M.FDDateStart, M.FDDateEnd, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus AS FTStatus, M.FNHSysEmpTypeId AS FTTypeEmp"
        _Qry = "SELECT  TOP 1  M.FNHSysCmpId As FTCmpCode, M.FNHSysEmpID AS FTEmpCode,M.FTEmpCode AS FTEmployeeCode, M.FDDateStart, M.FDDateEnd, M.FDDateProbation, CASE WHEN ISDATE(M.FDDateProbation) = 1 THEN '1' ELSE '0' END AS FTProbationSta, M.FNEmpStatus AS FTStatus, M.FNHSysEmpTypeId AS FTTypeEmp"
        _Qry &= vbCrLf & "          ,M.FNHSysDeptId AS FTDeptCode "
        _Qry &= vbCrLf & "          ,M.FNHSysDivisonId AS FTDivCode, M.FNHSysSectId AS FTSectCode,  M.FNHSysUnitSectId AS FTUnitSecCode"
        _Qry &= vbCrLf & "          ,M.FNHSysPositId AS FTPositCode,'' as FTJobGrade,'' AS FTCostCNCode,M.FNLateCutSta AS FTLateCutSta"
        _Qry &= vbCrLf & "          ,M.FNPaidOTSta AS FTPaidOTSta, M.FTEmpIdNo, M.FTSocialNo, M.FTTaxNo, M.FNCalSocialSta AS FTCalSocialSta, M.FNCalTaxSta AS FTCalTaxSta, M.FNHSysPayRollPayId AS FTPayCode"
        _Qry &= vbCrLf & "          ,M.FTAccNo, M.FNHSysBankId AS FTBnkCode, M.FNHSysBankBranchId AS FTBnkBchCode,M.FNSalary AS FTSalary"
        _Qry &= vbCrLf & "          ,M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather"
        _Qry &= vbCrLf & "          ,ET.FNCalType AS FTCalType, ET.FNInsurType AS FTInsurType,M.FNMaritalStatus AS FTMaritalCode,M.FDFundBegin, M.FDFundEnd"
        _Qry &= vbCrLf & "          ,M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother"
        _Qry &= vbCrLf & "          ,M.FCPremium, M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDisabledDependents,M.FCDeductDonateStudy"
        _Qry &= vbCrLf & "          ,M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,M.FTHealthInsurIDMother"
        _Qry &= vbCrLf & "          ,M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,M.FTMateIncome,M.FCExceptAgeOver,M.FCExceptAgeOverMate,M.FCDeductDividend"
        _Qry &= vbCrLf & "          ,CASE WHEN ISDATE(M.FdDateStart) = 1 AND ISDATE(M.FDRetire) = 1 THEN  DATEDIFF(MONTH,M.FdDateStart,M.FDRetire) ELSE 0 END AS FNWorkAge"
        _Qry &= vbCrLf & "          ,CASE WHEN ISDATE(M.FdDateStart) = 1 AND ISDATE(M.FDRetire) = 1 THEN  DATEDIFF(MONTH,M.FdDateStart,M.FDRetire) ELSE DATEDIFF(MONTH,M.FdDateStart, DATEADD(DAY, 1, CONVERT(DATETIME,'" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "'))) END AS FNEmpWorkAge"
        _Qry &= vbCrLf & "          ,ISNULL(ET.FNSalaryDivide,0) AS FNSalaryDivide"
        _Qry &= vbCrLf & "          ,ISNULL(ET.FTStatePayHoliday,'') AS FTStatePayHoliday"
        _Qry &= vbCrLf & "          ,dbo.FN_Get_Emp_WorkAge(M.FdDateStart,M.FdDateEnd) AS FNEmpWorkAgeNew"
        _Qry &= vbCrLf & "          ,Isnull(M.FTStateWorkpermit,'0') AS FTChkRaceThaiWorker"
        _Qry &= vbCrLf & "          ,ISNULL((SELECT COUNT(L1.FNSeqNo)"
        _Qry &= vbCrLf & "                   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMEmployeeChild] AS L1 (NOLOCK)"
        _Qry &= vbCrLf & "                   WHERE L1.FNHSysEmpID = M.FNHSysEmpId"
        _Qry &= vbCrLf & "                   GROUP BY L1.FNHSysEmpId), 0) AS FNCntEmployeeChild"
        _Qry &= vbCrLf & "          ,ISNULL(Posit.FTStaCalSkillAllowance, '0') AS FTStaCalSkillAllowance  , M.FNEmpSex "
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId  LEFT JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS Posit WITH(NOLOCK) ON M.FNHSysPositId = Posit.FNHSysPositId"
        _Qry &= vbCrLf & "WHERE (M.FNHSysEmpID = " & Val(_EmpCode) & ")"

        _Dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _Dtemp.Rows
            '...add new / update employee finance (welfare vietnam fixed rate payment)
            '=====================================================================================================================================================
            _EmpSex = Integer.Parse("0" & R!FNEmpSex.ToString)
            Dim oArrWelfare() As String = {"015", "023", "024", "027", "028"}
            Dim oStrBuilder As New System.Text.StringBuilder()
            For Each sWelfareFinCode As String In oArrWelfare
                oStrBuilder.Remove(0, oStrBuilder.Length)
                oStrBuilder.AppendLine("DECLARE @FNHSysEmpID AS INT;")
                oStrBuilder.AppendLine("DECLARE @FTFinCode AS NVARCHAR(30);")
                oStrBuilder.AppendLine("DECLARE @FTFinAmt AS NUMERIC(18,2);")
                Select Case sWelfareFinCode
                    Case "015"
                        oStrBuilder.AppendLine(String.Format("SET @FTFinAmt = {0};", FNAttendanceAllowanceVND))
                    Case "023"
                        oStrBuilder.AppendLine(String.Format("SET @FTFinAmt = {0};", FNMealAllowanceVND))
                    Case "024"
                        oStrBuilder.AppendLine(String.Format("SET @FTFinAmt = {0};", FNCarAllowanceVND))
                End Select
                oStrBuilder.AppendLine(String.Format("SET @FNHSysEmpID = {0};", Val(_EmpCode)))
                oStrBuilder.AppendLine(String.Format("SET @FTFinCode = N'{0}';", sWelfareFinCode))
                oStrBuilder.AppendLine("IF NOT EXISTS (SELECT TOP 1 A.FTFinCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMEmployeeFin] AS A (NOLOCK) WHERE A.FNHSysEmpID = @FNHSysEmpID AND A.FTFinCode = @FTFinCode)")
                oStrBuilder.AppendLine("   BEGIN")
                oStrBuilder.AppendLine("      INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] ([FTInsUser], [FDInsDate], [FTInsTime], [FTUpdUser], [FDUpdDate],[FTUpdTime],[FNHSysEmpID],[FTFinCode],[FTFinAmt])")
                oStrBuilder.AppendLine("      SELECT NULL, CONVERT(VARCHAR(10), GETDATE(), 111), CONVERT(VARCHAR(8), GETDATE(), 114), NULL, NULL, NULL, @FNHSysEmpID, @FTFinCode, @FTFinAmt")
                oStrBuilder.AppendLine("   END")
                oStrBuilder.AppendLine("ELSE")
                oStrBuilder.AppendLine("   BEGIN")
                oStrBuilder.AppendLine("      UPDATE A")
                oStrBuilder.AppendLine("      SET A.[FTUpdUser] = NULL")
                oStrBuilder.AppendLine("         ,A.[FDUpdDate] = CONVERT(VARCHAR(10), GETDATE(), 111)")
                oStrBuilder.AppendLine("         ,A.[FTUpdTime] = CONVERT(VARCHAR(8), GETDATE(), 114)")
                oStrBuilder.AppendLine("         ,A.[FTFinAmt] = @FTFinAmt")
                oStrBuilder.AppendLine("      FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] AS A")
                oStrBuilder.AppendLine("      WHERE A.FNHSysEmpID = @FNHSysEmpID")
                oStrBuilder.AppendLine("            AND A.FTFinCode = @FTFinCode")
                oStrBuilder.AppendLine("   END;")
                _Qry = oStrBuilder.ToString()
                HI.Conn.SQLConn.ExecuteOnly(_Qry, HI.Conn.DB.DataBaseName.DB_HR)
            Next
            '=====================================================================================================================================================

            _DtFin.Rows.Clear()

            _AmtAddCalOT = 0

            _SalaryDevide = Val(R!FNSalaryDivide.ToString)
            _FTStatePayHoliday = R!FTStatePayHoliday.ToString
            _FNEmpWorkAge = Integer.Parse(Val(R!FNEmpWorkAge.ToString))
            _FTInsurType = Val(R!FTInsurType.ToString)

            If _SalaryDevide <= 0 Then
                _SalaryDevide = 1
            End If

            _FTSlary = R!FTSalary.ToString : _FDDateStart = R!FDDateStart.ToString : _FDDateEnd = R!FDDateEnd.ToString : FDDateProbation = R!FDDateProbation.ToString
            _FTPaymentCode = R!FTPayCode.ToString : _FTBankCode = R!FTBnkCode.ToString

            FTProbationSta = R!FTProbationSta.ToString

            '...คิด หรือ ไม่คิด ปกส. / คิด หรือ ไม่คิด ภาษี
            _FTCalSocialSta = R!FTCalSocialSta.ToString : _FTCalTaxSta = R!FTCalTaxSta.ToString

            '...ฝ่าย แผนก สังกัด ส่วน ตำแหน่ง
            _FTDeptCode = R!FTDeptCode.ToString : _FTDivCode = R!FTDivCode.ToString
            _FTSectCode = R!FTSectCode.ToString
            _FTUnitCode = R!FTUnitSecCode.ToString : _FTPos = R!FTPositCode.ToString

            _FTEmpState = R!FTCalType.ToString
            _FTEmpIdNo = R!FTTaxNo.ToString 'R!FTEmpIdNo.ToString
            _FTBranchCode = R!FTBnkBchCode.ToString
            _FTAccNo = R!FTAccNo.ToString

            FTChkRaceThaiWorker = R!FTChkRaceThaiWorker.ToString '...พนง.คนไทย ที่ไปทำงานที่โรงงานเวียดนาม (เป็นคนต่างด้าว มีสัญชาติไทย มีเลขที่บัตรประชาชน และมีใบอนุญาตทำงาน)
            FNCntEmployeeChild = Integer.Parse(R!FNCntEmployeeChild.ToString) '...จำนวนบุตร ตามรายการ รหัสพนักงาน ที่ใช้ในการลดหย่อนภาษี

            'FTStaCalSkillAllowance = R!FTStaCalSkillAllowance.ToString '...พนง รหัสนี้ตำแหน่ง นี้ คำนวณ Skill Allowance : '1'  คำนวณ / '0' : 'ไม่คำนวณ'

            '---------------------------------------- ลดหย่อน----------------------------------------------------------------------------------------
            _FNChildNotLearn = 0 : _FCReduceDonate = 0 : _FCLifeInsurance = 0 : _FCLoanHouse = 0 : _FCReduceEducationSupport = 0
            _FCShare = 0 : _FCReduceFather = 0 : _FCReduceMother = 0 : _FCReSpouseFather = 0 : _FCReSpouseMother = 0 : _FTMarryIncome = 0
            _FCLifeFeeMoney = 0

            FCModFather = 0 : FCModMother = 0 : FCModMateFather = 0 : FCModMateMother = 0
            FCPremium = 0 : FCInterest = 0 : FCUnitRMF = 0 : FCUnitLTF = 0 : FCDeductDonate = 0 : FCDisabledDependents = 0 : FCDeductDonateStudy = 0
            FCHealthInsurFatherMotherMate = 0 : FTHealthInsurIDFather = 0 : FTHealthInsurIDMother = 0
            FTHealthInsurIDFatherMate = 0 : FTHealthInsurIDMotherMate = 0

            FTTotalCalContributedAmt = 0 : FTContributedAmt = 0 : FTCmpContributedAmt = 0
            FTTotalCalWorkmen = 0 : FTWorkmenAmt = 0 : _FTMaxCalWorkmen = 0 : _FTMaxWorkmenRate = 0 : FTTotalCalWorkmenAcc = 0
            _FNIncentiveAmt = 0

            '--------- อายุงาน  เดือน
            _WorkAge = Val(R!FNEmpWorkAgeNew.ToString)

            '----------- Calculate Seniority Bonus For KKN---------------
            _FNWorkAgeSalary = 0
            If _FTEmpState <> "2" And _FTEmpState <> "3" Then

                If (_StartDate <= Left(_StartDate, 8) & "24" And _EndDate >= Left(_StartDate, 8) & "24") Then
                    For Each ZRow In _tmpWorkAge.Select(" FNWorkAgeStart <= " & _WorkAge & " AND  FNWorkAgeEnd >=" & _WorkAge & " ")
                        _FNWorkAgeSalary = Val(ZRow!FNWorkAgeAmt.ToString)

                        Exit For

                    Next

                End If

            End If

            '----------- Calculate Seniority Bonus For KKN---------------
            If _FTEmpState = "2" Then
                _TotalInstalment = 12
            Else
                _TotalInstalment = 24
            End If

            REM _TotalInstalment = 12 '...Vietnam factory

            _ContributedFundBeginPay = False
            If R!FDFundBegin.ToString <> "" Then
                If R!FDFundBegin.ToString < _FTEndCalculateDate Then
                    If R!FDFundEnd.ToString <> "" Then
                        If R!FDFundEnd.ToString > _FTEndCalculateDate Then
                            _ContributedFundBeginPay = True
                        End If
                    Else
                        _ContributedFundBeginPay = True
                    End If
                End If
            End If

            _Instalment = Val(_PayTerm) '...งวดการคำนวณสิ้นงวด
            _FNIncentiveAmt = 0

            '-------------คำนวณ Incentive-------------------------------------------
            Select Case _FTInsurType
                Case 1

                    '---------ประกันเป็นวัน
                    _Qry = "SELECT SUM ( CASE WHEN ISNULL(FNNetProAmt,0) > ISNULL(FNNetAmt,0) THEN  (ISNULL(FNNetProAmt,0) - ISNULL(FNNetAmt,0))  ELSE 0 END  ) AS FNIncentiveAmt "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE  (FNHSysEmpID = " & Val(_EmpCode) & ")"
                    _Qry &= vbCrLf & " 	AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "' "
                    _Qry &= vbCrLf & " 	AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "' "

                    _FNIncentiveAmt = CDbl(Format(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")), "0.00"))

                Case 2
                    '---------ประกันเป็นเป็นงวด-------------------------------------------

                    _Qry = "SELECT  ( CASE WHEN ISNULL(FNNetProAmt,0) > ISNULL(FNNetAmt,0) THEN  (ISNULL(FNNetProAmt,0) - ISNULL(FNNetAmt,0))  ELSE 0 END  ) AS FNIncentiveAmt "
                    _Qry &= vbCrLf & " FROM ( SELECT SUM(ISNULL(FNNetAmt,0) ) AS FNNetAmt , SUM(ISNULL(FNNetProAmt,0) ) AS FNNetProAmt"
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE  (FNHSysEmpID = " & Val(_EmpCode) & ")"
                    _Qry &= vbCrLf & " 	AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "' "
                    _Qry &= vbCrLf & " 	AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "' "
                    _Qry &= vbCrLf & " ) AS M"

                    _FNIncentiveAmt = CDbl(Format(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")), "0.00"))

                Case Else

            End Select
            '-------------------------------------

            '---------------------------------------- ลดหย่อน------------------------------------


            _Qry = " SELECT  FNHSysEmpID, FTChildSex, FTStudySta ,FDChildBirthDate"
            _Qry &= vbCrLf & "        ,convert(int , (dbo.FN_Get_Emp_WorkAge(FDChildBirthDate,'" & _DateStartOfMonth & "')))  / 12 AS FNChildAge"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeChild WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE (FNHSysEmpID = " & Val(_EmpCode) & ")"
            _dttemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _ChildNotStudy As Integer = 0
            Dim _ChildStudy As Integer = 0

            _FNTotalChildCare = 0

            For Each _Drow As DataRow In _dttemp.Rows
                '***********ChildCare
                Dim _MonthBegin As Integer = 0 : Dim _MonthEnd As Integer = 0
                Call CalChildCare(_Drow!FDChildBirthDate.ToString, _DateStartOfMonth, _MonthBegin, _MonthEnd)
                If _MonthBegin >= _FNChildCareStartAge And _MonthEnd <= _FNChildCareEndAge Then
                    _FNTotalChildCare = _FNTotalChildCare + 1
                End If
                '***********ChildCare

                '--------  Add Child Care For KKN--------------
                If _Drow!FTStudySta.ToString = "1" Then
                    _ChildStudy = _ChildStudy + 1
                Else
                    _ChildNotStudy = _ChildNotStudy + 1
                End If

            Next
            If _FNTotalChildCare > _FNChildCareMaxPeople Then
                _FNTotalChildCare = _FNChildCareMaxPeople
            End If

            _FNNetChildCareAmt = Format(_FNTotalChildCare * _FNChildCareAmt, "0.00")
            If _EmpSex <> 1 Then _FNNetChildCareAmt = 0

            _FCAccumulateIncome = 0 : _FCAccumulateSocial = 0 : _FCAccumulateTax = 0
            FTTotalCalContributedAcc = 0 : FTTotalCalWorkmenAcc = 0

            '----------- Get Summary ------------------
            LoadIncomeTax(_FTEmpIdNo, _PayYear, _PayTerm, _FCAccumulateIncome, _FCAccumulateTax, _FCAccumulateSocial, CountTerm, FTTotalCalContributedAcc, FTTotalCalWorkmenAcc, Integer.Parse(Val(_EmpCode)))
            '----------- Get Summary ------------------

            With _EmpDisTax

                .BaseSlary = 0
                .OtherSlary = 0
                .BeforeIncom = _FCAccumulateIncome
                .BeforeTax = _FCAccumulateTax
                .FTMateIncome = (R!FTMateIncome.ToString = "0")
                ' ----------------------------------------- Clear Discount Tax Value -------------------------
                .Cfg_ModChildAllowanceRateNotStudied = _ChildNotStudy 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
                .Cfg_ModChildAllowanceRateStudy = _ChildStudy 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
                '-------------ลดหย่อนบุตร-----------------

                '--- หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
                .Cfg_ContributedDeducToTheFund = FTTotalCalContributedAcc 'ลูกจ้าง
                '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ

                .Cfg_ModDeductibleDonations = CDbl(Val(R!FCDeductDonate.ToString)) ' ' % ลดหย่อนเงินบริจาค
                .Cfg_ModDeductDonateStudy = CDbl(Val(R!FCDeductDonateStudy.ToString))
                .Cfg_ModFatherReduction = CDbl(Val(R!FCModFather.ToString)) '  'ลดหย่อนบิดา
                .Cfg_ModInsurancePremiums = CDbl(Val(R!FCPremium.ToString)) '  'ค่าเบี้ยประกันชีวิตส่วนบุคคล
                .Cfg_ModLendingforHousing = CDbl(Val(R!FCInterest.ToString)) ' 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย

                .Cfg_ModLTFChk = CDbl(Val(R!FCUnitLTF.ToString)) 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
                .Cfg_ModMateFatherReduction = CDbl(Val(R!FCModMateFather.ToString)) ' 'ลดหย่อนบิดา คู่สมรส
                .Cfg_ModMateMotherReduction = CDbl(Val(R!FCModMateMother.ToString)) '  'ลดหย่อนมารดา คู่สมรส
                .Cfg_ModMotherReduction = CDbl(Val(R!FCModMother.ToString)) ' 'ลดหย่อนมารดา

                .Cfg_ModPersonalExpenChk = 0 ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน

                .Cfg_ModRateReductionsByMarital = (IIf(R!FTMaritalCode.ToString = "1", 1, 0)) 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
                .Cfg_ModRateReductionsBySingle = (IIf(R!FTMaritalCode.ToString <> "1", 1, 0)) 'อัตราลดหย่อน ตาม สถานภาพ โสด 

                .Cfg_ModRMFChk = CDbl(Val(R!FCUnitRMF.ToString)) '  ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
                .FCDisabledDependents = CDbl(Val(R!FCDisabledDependents.ToString)) '  'ค่าอุปการะเลี้ยงดูคนพิการหรือทุพพลภาพ
                .FCHealthInsurFatherMotherMate = CDbl(Val(R!FCHealthInsurFatherMotherMate.ToString)) '   'เบี้ยประกันสุขภาพบิดามารดาของผู้มีเงินได้และคู่สมสร

                .FCExceptAgeOver = CDbl(Val(R!FCExceptAgeOver.ToString)) ' ' 'รายการเงินได้ที่ได้รับยกเว้น ของผู้มีเงินได้ตั้งแต่ 65 ปีขึ้นไป 
                .FCExceptAgeOverMate = CDbl(Val(R!FCExceptAgeOverMate.ToString)) ' 'รายการเงินได้ที่ได้รับยกเว้น ของคู่สมรสอายุตั้งแต่ 65 ปีขึ้นไป
                '----------------------------------------------------
            End With
            '---------------------------------------- ลดหย่อน------------------------------------

            _FCOtherAdd = 0 : _FTOtherAddCalculateSocial = "0" : _FTOtherAddCalculateTax = "0" : _FCOtherAddOt = 0
            _FTOtherAddOtCalculateSocial = "0" : _FTOtherAddOtCalculateTax = "0" : _FCBFShiftMoney = 0 : _FTShiftMoneyCalculateSocial = "0"
            _FTShiftMoneyCalculateTax = "0" : _FCDiligent = 0 : _FTDiligentCalculateSocial = "0" : _FTDiligentCalculateTax = "0"
            _FCBonusEndYear = 0 : _FTBonusEndCalculateSocial = "0" : _FTBonusEndCalculateTax = "0" : _FCShelter = 0
            _FTShelterCalculateSocial = "0" : _FTShelterCalculateTax = "0" : _FCShareFactory = 0 : _FTShareFactoryCalculateSocial = "0"
            _FNNetpayOrg = 0.0
            _FNNetpay = 0.0
            _FCSalary = -99
            _FTSlary = _GetNewSalary(Val(_EmpCode))
            If _FTSlary = 0 Then
                _FTSlary = (R!FTSalary.ToString)
            End If

            _Qry = "SELECT TOP 1 Isnull(FTStatePayHarmful,'0') AS FTStatePayHarmful FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
            Dim _EmpTypePayHarmful As String = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

            _Qry = "SELECT TOP 1 Isnull(FTStatePaySkill,'0') AS FTStatePaySkill  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
            Dim _EmpTypePaySkill As String = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))


            'FTStaCalSkillAllowance = _EmpTypePaySkill

            If IsNumeric(_FTSlary) Then
                _MSlary = _FTSlary 'CInt(CInt(_FTSlary / CountDayPerMonth) * CountDayPerMonth) 
                _FCSalary = CDbl(_FTSlary)

                If FDDateProbation <= _StartDate Then
                    If _EmpTypePayHarmful = "1" Then
                        FNHarmfulBaht = (_FCSalary * FNHarmfulRate) / 100
                    End If
                    If _EmpTypePaySkill = "1" Then
                        FNSkillBaht = ((_FCSalary + FNHarmfulBaht) * FNSkillRate) / 100
                    End If
                    _FCSalary = _FCSalary + FNHarmfulBaht + FNSkillBaht
                End If

                '027 Wage Scale*********
                FNWageScale = _GetWageScale(Double.Parse("0" & _EmpCode), _EmpType, _StartDate, _FCSalary)
                _FCSalary += FNWageScale

                '_SalaryCalOT = _FCSalary
                _FCSalary = Calculate.HelperRoundUpBasicSalary(_FCSalary)
                _SalaryCalOT = _FCSalary

                If FNWageScale > 0 Then
                    _DtFin.Rows.Add("027", FNWageScale)
                End If
                '...Vietnam Factory Skip
                '------------------ คำนวณคืนพักร้อน ----------------------------
                If _ReturnVacation > 0 Then
                    _Qry = "SELECT TOP 1  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_CALCULATE_MONTH_TO_CURRENT(FDDateStart) AS FTMonth"
                    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK)"
                    _Qry &= vbCrLf & "WHERE   M.FNHSysEmpID='" & HI.UL.ULF.rpQuoted(_EmpCode) & "' "

                    '_Qry = "SELECT TOP 1  FDDateStart "
                    '_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK)"
                    '_Qry &= vbCrLf & "WHERE   M.FNHSysEmpID='" & HI.UL.ULF.rpQuoted(_EmpCode) & "' "
                    'Dim _SDate As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
                    '_Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].SP_Datediff '" & _SDate & "',N''"
                    'Dim _oDtR As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                    'Dim _Y As Integer = 0 : Dim _M As Integer = 0
                    'For Each z As DataRow In _oDtR.Rows
                    '    _Y = Integer.Parse("0" & z!FNYear.ToString)
                    '    _M = Integer.Parse("0" & z!FNMonth.ToString)
                    '    Exit For
                    'Next
                    '_M = _M + (_Y * 12)

                    Dim _Month As Integer = 0
                    Dim _Leave As Double = 0
                    Dim _SumLeaveVacation As Double = 0
                    Dim _ResetDate As String = ""

                    '   _Month = _M
                    _Month = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "-1"))

                    If _Month >= 0 Then

                        _Qry = "SELECT TOP 1  VC.FNLeaveRight, VC.FNAgeBegin, VC.FNAgeEnd --, VC.FTCmpCode, VC.FTTypeEmp"
                        _Qry &= vbCrLf & ",ISNULL((SELECT TOP 1 FTLeaveReset FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave WITH(NOLOCK) WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  AND FTLeaveCode = '98'  ),'') AS FTReset"
                        _Qry &= vbCrLf & "FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation AS VC WITH(NOLOCK)"
                        _Qry &= vbCrLf & "WHERE  VC.FNHSysEmpTypeId='" & Val(_EmpType) & "' "
                        _Qry &= vbCrLf & "  AND  VC.FNAgeBegin<=" & _Month & " "
                        _Qry &= vbCrLf & "  AND  VC.FNAgeEnd>=" & _Month & " "

                        _dttemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                        For Each R3 As DataRow In _dttemp.Rows
                            _Leave = Val(R3!FNLeaveRight.ToString)
                            _ResetDate = R3!FTReset.ToString
                            Exit For
                        Next

                        If _Leave > 0 And _ResetDate <> "" Then

                            _Qry = "SELECT (SUM(CASE WHEN FNTotalMinute >= 480 THEN 480  ELSE FNTotalMinute END)/480.00) AS FNTotalMinute"
                            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)"
                            _Qry &= vbCrLf & "WHERE (FTLeaveType = '98')"
                            _Qry &= vbCrLf & "	     AND FNHSysEmpID =" & Val(_EmpCode) & " "
                            _Qry &= vbCrLf & " 	     AND  FTDateTrans >= '" & (_DateReset) & "'"
                            _Qry &= vbCrLf & " 	     AND  FTDateTrans <=Convert(varchar(10),DateAdd(year,1,Convert(Datetime,'" & (_DateReset) & "')),111)"

                            _SumLeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

                            'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ
                            Select Case FNStateSalaryType
                                Case 0 'ต่อเดือน
                                    _FNSlaryPerDay = CDbl(Format(CDbl((_FCSalary) / CountDayPerMonth), "0.00"))
                                '_FNSlaryPerDay = CDbl(Format((_FCSalary + FNHarmfulBaht + FNSkillBaht) / CountDayPerMonth, "0.000000000000"))
                                '_FNSlaryPerDay = CInt(_FNSlaryPerDay)
                                Case 1 'ต่อวัน

                                    '_FNSlaryPerDay = CDbl(Format((_FCSalary), "0.00"))
                                    _FNSlaryPerDay = CDbl(Format(CDbl((_FCSalary) / CountDayPerMonth), "0.00"))
                            End Select
                            'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ


                            If _Leave > _SumLeaveVacation Then
                                _AmtReturnVacation = CDbl(Format(((_Leave - _SumLeaveVacation) * (_ReturnVacation * _FNSlaryPerDay)), "0.00"))
                                FNVacationRetAmt = _AmtReturnVacation
                                FNVacationRetMin = Integer.Parse((_Leave - _SumLeaveVacation) * 480)

                            End If

                        End If

                    End If

                End If
                '------------------ คำนวณคืนพักร้อน ----------------------------

                _SocialMinIncome = HCfg.HSocialRate.SocialIncomeMin
                _SocialMaxIncome = HCfg.HSocialRate.SocialIncomeMax
                _SocialRate = HCfg.HSocialRate.CalSocialRate

                _RateOT1 = 0 : _RateOT15 = 0 : _RateOT2 = 0 : _RateOT3 = 0 : _RateOT4 = 0
                _AmtPlus = 0

                _Qry = " SELECT FTCfgOTCode,FCCfgOTValue,ISNULL(FCCfgOTAmtPlus,0) AS FCCfgOTAmtPlus  "
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTSet WITH (NOLOCK) "
                _Qry &= vbCrLf & "WHERE  (FNCalType  = " & Val(_EmpType) & ")"
                _dtot = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R3 As DataRow In _dtot.Rows
                    Select Case R3!FTCfgOTCode.ToString.ToUpper
                        Case "01"
                            _RateOT1 = Val(R3!FCCfgOTValue.ToString)
                        Case "02"
                            _RateOT15 = Val(R3!FCCfgOTValue.ToString)
                        Case "03"
                            _RateOT2 = Val(R3!FCCfgOTValue.ToString)
                        Case "04"
                            _RateOT3 = Val(R3!FCCfgOTValue.ToString)
                            _AmtPlus = Val(R3!FCCfgOTAmtPlus.ToString)
                        Case "05"
                            _RateOT4 = Val(R3!FCCfgOTValue.ToString)
                    End Select

                Next

                '---------รายได้รายหัก อื่นๆ-------------------------
                _Qry = "SELECT  FN.FTStaTax, FN.FTStaSocial,  (ISNULL(BF.FTFinAmt,0)) AS FCFinAmt,  FM.FTFinType"
                _Qry &= vbCrLf & "       , FN.FTCalType, FN.FTPayType, FN.FTStaCalOT, FN.FTStaLate, FN.FTStaAbsent, FN.FTStaLeave"
                _Qry &= vbCrLf & "       , FN.FTStaVacation, FN.FTStaRetire, FN.FTStaHoliday, FN.FNOTTimeM"
                _Qry &= vbCrLf & "       , FN.FTOTTime,FN.FTFinCode "
                _Qry &= vbCrLf & "       , FN.FTStaCheckLate, FN.FTLateMin"
                _Qry &= vbCrLf & "       , FN.FTStaCheckLeave, FN.FTLeaveMin, FN.FTStaCheckWorkTime, FN.FTCheckWorkTimeMin, FN.FTStaMaternityleaveNotpay"
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin AS BF WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet AS FN WITH (NOLOCK) ON BF.FTFinCode = FN.FTFinCode  INNER JOIN"
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinance AS FM WITH (NOLOCK) ON FN.FTFinCode = FM.FTFinCode"
                _Qry &= vbCrLf & "WHERE  (BF.FNHSysEmpID = " & Val(_EmpCode) & ")"
                _Qry &= vbCrLf & "       AND (FM.FTFinType = '1' OR FM.FTFinType = '2')"
                _Qry &= vbCrLf & "       AND FTPayType <> '" & IIf(_EmpCalType = "0" Or (Val(_PayTerm) Mod 2 = 0), "", "1") & "' "

                _dtAddOtherAmt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " SELECT  FTFinCode, FTType, FTCalType, FTPayType, FTStaTax, "
                _Qry &= vbCrLf & "FTStaSocial, FTStaCalOT, FTStaLate, FTStaAbsent, FTStaLeave, FTStaVacation, FTStaRetire, FTStaHoliday, FNOTTimeM, FTOTTime, FTStaCheckLate, FTLateMin,"
                _Qry &= vbCrLf & "FTStaCheckLeave, FTLeaveMin, FTStaCheckWorkTime, FTCheckWorkTimeMin, FTStaMaternityleaveNotpay, FTStaActive"
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet"
                _Qry &= vbCrLf & "WHERE  (FTFinCode = N'001') OR (FTFinCode = N'007')"

                _dtAddOtherAmtshift = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                REM 2014/10/31
                ''...vietnam factory
                ''================= welfare vienam fixed rate payment allowance =================================================================================================================
                'If Not DBNull.Value.Equals(_dtAddOtherAmt) AndAlso _dtAddOtherAmt.Rows.Count > 0 Then
                '    '...update finance amount from master file
                '    For Each oDataRow As System.Data.DataRow In _dtAddOtherAmt.Rows
                '        Select Case oDataRow!FTFinCode.ToString
                '            Case "015"  '...Attendance Allowance
                '                oDataRow!FCFinAmt = FNAttendanceAllowanceVND
                '            Case "023"  '...Meal Allowance
                '                oDataRow!FCFinAmt = FNMealAllowanceVND
                '            Case "024"  '...Car Allowance
                '                oDataRow!FCFinAmt = FNCarAllowanceVND
                '        End Select
                '    Next
                '    _dtAddOtherAmt.AcceptChanges()
                'End If
                ''===============================================================================================================================================================================

                'If FNWageScale > 0 Then
                '    For Each R2 As DataRow In _dtAddOtherAmt.Select("FTFinCode = '027'")
                '        R2!FCFinAmt = FNWageScale
                '    Next
                'End If


                _GAmtAddCalOT = 0
                For Each R2 As DataRow In _dtAddOtherAmt.Select("FTCalType <> '0' AND FTFinType= '1' AND FTStaCalOT = '1' AND FTPayType = '0'")
                    _GAmtAddCalOT = _GAmtAddCalOT + Val(R2!FCFinAmt.ToString)
                Next

                _FCOtherAdd = 0 : _FTOtherAddCalculateSocial = 0 : _FTOtherAddCalculateTax = 0 : _FCOtherDeduct = 0

                '---------------- Adjust Before Calculate------------------------------------
                _Qry = ""
                _Qry = " SELECT  FN.FTStaTax, FN.FTStaSocial, (ISNULL(BF.FCFinAmt,0))  AS FCFinAmt, FM.FTFinType,ISNULL(BF.FNDay,-1) As FNDay,BF.FTFinCode "
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage AS BF WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet AS FN WITH (NOLOCK) ON BF.FTFinCode = FN.FTFinCode INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinance AS FM WITH (NOLOCK) ON FN.FTFinCode = FM.FTFinCode"
                _Qry &= vbCrLf & " WHERE  (BF.FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "')"
                _Qry &= vbCrLf & "        AND (BF.FTPayTerm = '" & HI.UL.ULF.rpQuoted(_PayTerm) & "')"
                _Qry &= vbCrLf & "        AND (BF.FNHSysEmpID = " & Val(_EmpCode) & ")"
                _Qry &= vbCrLf & "        AND (FM.FTFinType = '1' OR FM.FTFinType = '2')"

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R2 As DataRow In _dt.Select("FNDay <= 0")
                    Select Case R2!FTFinType.ToString
                        Case "1"
                            _FCOtherAdd = _FCOtherAdd + Val((R2!FCFinAmt.ToString))

                            If R2!FTStaTax.ToString = "1" Then _FTOtherAddCalculateTax = _FTOtherAddCalculateTax + Val((R2!FCFinAmt.ToString))
                            If R2!FTStaSocial.ToString = "1" Then _FTOtherAddCalculateSocial = _FTOtherAddCalculateSocial + Val((R2!FCFinAmt.ToString))

                        Case "2"
                            _FCOtherDeduct = _FCOtherDeduct + Val((R2!FCFinAmt.ToString))
                    End Select

                Next

                '------------------------------------------------------------------------------

                _DayAdjAdd = 0
                _WageAdjAdd = 0

                For Each R2 As DataRow In _dt.Select("FNDay > 0")
                    _DayAdjAdd = _DayAdjAdd + Val((R2!FNDay.ToString))
                    _WageAdjAdd = _WageAdjAdd + Val((R2!FCFinAmt.ToString))
                Next

                _Qry = "SELECT  FTLeaveType AS LFTLeaveCode, Case WHEN FTLeaveType = '98' Then 1 Else CASE WHEN FTLeaveType = '97' THEN 2 ELSE 0 END  END AS LeaveType"
                _Qry &= vbCrLf & "     , SUM(CASE WHEN ISNULL(FNTotalMinute,0) >= 480 THEN 480   ELSE  ISNULL(FNTotalMinute,0)   END) AS FNTotalMinute"
                _Qry &= vbCrLf & "     , SUM(CASE WHEN ISNULL(FNTotalPayMinute,0) >= 480 THEN 480   ELSE ISNULL(FNTotalPayMinute,0)   END ) AS FNTotalPayMinute"
                _Qry &= vbCrLf & "     , SUM(CASE WHEN ISNULL(FNTotalNotPayMinute,0) >= 480 THEN 480 ELSE ISNULL(FNTotalNotPayMinute,0)   END ) AS FNTotalNotPayMinute"
                _Qry &= vbCrLf & "     , FTDateTrans"
                _Qry &= vbCrLf & "     , ISNULL(FTStaCalSSO,'N') AS FTStaCalSSO "
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)"
                _Qry &= vbCrLf & "WHERE (FNHSysEmpID =" & Val(_EmpCode) & " )"

                If _FTEmpState = "2" Or _FTEmpState = "3" Then
                    _Qry &= vbCrLf & " 	AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_DateStartOfMonth) & "' "
                    _Qry &= vbCrLf & " 	AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_DateEndOfMonth) & "' "
                Else
                    _Qry &= vbCrLf & " 	AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "' "
                    _Qry &= vbCrLf & " 	AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "' "
                End If

                _Qry &= vbCrLf & " GROUP BY FTDateTrans, Case WHEN FTLeaveType = '98' THEN 1 ELSE 0 END, ISNULL(FTStaCalSSO,'N'),FTLeaveType"

                _dtLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = ""
                _Qry = "SELECT  ISNULL(T.FNHSysShiftID, 0) AS FTShift"
                _Qry &= vbCrLf & "            , (ISNULL(T.FNTimeMin, 0) + ISNULL(T.FNSpecialTimeMin, 0) + ISNULL(T.FNLateNormalMin, 0))- (ISNULL(T.FNLateNormalCut, 0) + ISNULL(T.FNAbsentCut, 0)) AS FNTime"
                _Qry &= vbCrLf & " 	          , ISNULL(T.FNNotRegis, 0) As FNNotRegis, ISNULL(FNOT1, 0) AS FNOT1"
                _Qry &= vbCrLf & " 	          , ISNULL(T.FNOT1_5, 0) AS FNOT1_5"
                _Qry &= vbCrLf & " 	          , ISNULL(T.FNOT2, 0) AS FNOT2  , ISNULL(T.FNOT3, 0) AS FNOT3, ISNULL(T.FNOT4, 0) AS FNOT4"
                _Qry &= vbCrLf & " 	          , ISNULL(T.FNLateNormalMin, 0) AS FNLateNormalMin, ISNULL(T.FNLateNormalCut, 0) AS FNLateNormalCut"
                _Qry &= vbCrLf & "            , ISNULL(T.FNLateOtMin, 0) AS FNLateOtMin, ISNULL(T.FNLateOtCut, 0) As FNLateOtCut"
                _Qry &= vbCrLf & "            , ISNULL(T.FNLateMMin, 0) AS FNLateMorning"
                _Qry &= vbCrLf & "  	      , ISNULL(T.FNLateAfMin, 0) AS FNLateAfternoon, ISNULL(T.FNAbsentCut, 0) AS FNAbsentCut"  '...T.FNAbsentCut : สาย หัก ขาดงาน
                _Qry &= vbCrLf & " 	          , (CASE WHEN ISNULL(T.FNAbsentSP, 0) = ISNULL(T.FNAbsent, 0) THEN 0 ELSE  ISNULL(T.FNAbsent, 0)  END ) AS FNAbsent_Cut" '...T.FNAbsent : จำนวนเวลาที่ขาดงาน 
                _Qry &= vbCrLf & " 	          , ISNULL(T.FNCutAbsent, 0) AS FNAbsent " '...T.FNCutAbsent : จำนวนเวลาที่หักขาดงาน
                _Qry &= vbCrLf & "            , (ISNULL(T.FNTimeMin, 0) + ISNULL(T.FNSpecialTimeMin, 0) + ISNULL(T.FNLateNormalMin, 0))- (ISNULL(T.FNLateNormalCut, 0) + ISNULL(T.FNAbsentCut, 0) ) AS FNTimeMin"
                _Qry &= vbCrLf & "            , (ISNULL(T.FNTimeMin, 0) + ISNULL(T.FNSpecialTimeMin, 0)) AS FNTimeMinOrg"
                _Qry &= vbCrLf & "            , ISNULL(T.FNOT1Min, 0) AS FNOT1Min"
                _Qry &= vbCrLf & "            , ISNULL(T.FNOT1_5Min, 0) AS FNOT1_5Min"
                _Qry &= vbCrLf & "            , ISNULL(T.FNOT2Min, 0) AS FNOT2Min"
                _Qry &= vbCrLf & "            , ISNULL(T.FNOT3Min, 0) AS FNOT3Min, ISNULL(FNOT4Min,0) AS FNOT4Min"
                _Qry &= vbCrLf & "            , ISNULL(T.FNLateMMin, 0) AS FNLateMMin"
                _Qry &= vbCrLf & "            , ISNULL(T.FNLateAfMin, 0) AS FNLateAfMin"
                _Qry &= vbCrLf & "            , ISNULL(T.FNRetireMMin, 0) AS FNRetireMMin"
                _Qry &= vbCrLf & "            , ISNULL(T.FNRetireAfMin, 0)  AS FNRetireAfMin"
                _Qry &= vbCrLf & "            , ISNULL(T.FNRetireNormalCut, 0) AS FNRetireNormalCut"
                _Qry &= vbCrLf & "            , ISNULL(T.FNRetireOtMin, 0) AS FNRetireOtMin"
                _Qry &= vbCrLf & "            , ISNULL(T.FNRetireOtCut, 0) AS FNRetireOtCut,FTDateTrans"
                _Qry &= vbCrLf & "            , ISNULL(T.FTIn1, '') AS FTIn1"
                _Qry &= vbCrLf & "            , ISNULL(T.FTOut1, '') AS FTOut1"
                _Qry &= vbCrLf & "            , ISNULL(T.FTIn2, '') AS FTIn2"
                _Qry &= vbCrLf & "            , ISNULL(T.FTOut2, '') AS FTOut2"
                _Qry &= vbCrLf & "            , ISNULL(T.FTIn3, '') AS FTIn3"
                _Qry &= vbCrLf & "            , ISNULL(T.FTOut3, '') AS FTOut3"
                _Qry &= vbCrLf & "            , P.FTOverClock,T.FTWeekDay"
                _Qry &= vbCrLf & "            , CASE WHEN T.FTWeekDay = 1 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTSunday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTSunday, '0') = '1') THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 2 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTMonday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTMonday, '0') = '1') THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 3 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTTuesday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTTuesday, '0') = '1')  THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 4 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTWednesday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTWednesday, '0') = '1')  THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 5 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTThursday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTThursday, '0') = '1')  THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 6 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTFriday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTFriday, '0') = '1')  THEN '1'"
                _Qry &= vbCrLf & "                   WHEN T.FTWeekDay = 7 AND ((EHL.FNHSysEmpID  IS NULL  AND (ISNULL(SH.FTSaturday, '0') = '1' OR ISNULL(ETHL.FDHolidayDate, '') <> ''))  OR ISNULL(EHL.FTSaturday, '0') = '1')  THEN '1'"
                _Qry &= vbCrLf & "                                                                                                                                                                                                ELSE '0' END AS FTWeekly, ISNULL(FTStateAccept, '') AS FTStateAccept"
                _Qry &= vbCrLf & "            , ISNULL(T.FNHSysTranStaId, 0) AS FNHSysTranStaId"
                _Qry &= vbCrLf & "            , (SELECT ISNULL((SELECT TOP 1 L1.FTTranStaCode AS FTTranStaCode"
                _Qry &= vbCrLf & "                              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..THRMTranStatus AS L1 (NOLOCK)"
                _Qry &= vbCrLf & "                              WHERE (L1.FNHSysTranStaId = T.FNHSysTranStaId)), '')) FTTranStaCode"
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) LEFT OUTER JOIN   THRMTimeShift AS P WITH(NOLOCK) ON T.FNHSysShiftID  = P.FNHSysShiftID"
                _Qry &= vbCrLf & "                                                                                                          INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID  =  M.FNHSysEmpID"
                _Qry &= vbCrLf & "                                                                                                          LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID = EHL.FNHSysEmpID"
                _Qry &= vbCrLf & "                                                                                                          LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId = ETHL.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "                                                                                                          LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID"

                _Qry &= vbCrLf & "WHERE (T.FNHSysEmpID = " & Val(_EmpCode) & ")"
                _Qry &= vbCrLf & " 	    AND T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "' "
                _Qry &= vbCrLf & " 	    AND T.FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "'  "

                If _FDDateEnd <> "" Then
                    _Qry &= vbCrLf & " 	AND T.FTDateTrans < '" & HI.UL.ULDate.ConvertEnDB(_FDDateEnd) & "'  "
                End If
                If _FTEmpState <> "2" And _FTEmpState <> "3" Then ' คำนวณค่าแรงพนักงานรายเดือน
                    _Qry &= vbCrLf & " 	AND T.FTStateAccept = '1' "
                End If

                _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                '---------------Get Trans Daily--------------------------------------
                _GFNTime = 0 : _GFNNotRegis = 0 : _GFNOT1 = 0 : _GFNOT1_5 = 0
                _GFNOT2 = 0 : _GFNOT3 = 0 : _GFNOT4 = 0 : _GFNLeaveSick = 0 : _GFNLeaveBusiness = 0
                _GFNLeaveVacation = 0 : _GFNLeavePregnant = 0 : _GFNLeaveOrdain = 0 : _GFNLeaveMarry = 0
                _GFNLeaveOther = 0 : _GFNLateNormalMin = 0 : _GFNLateNormalCut = 0 : _GFNLateOtMin = 0
                _GFNLateOtCut = 0 : _GFNLateMorning = 0 : _GFNLateAfternoon = 0
                _GFNAbsent = 0 : _GFNCutAbsent = 0 : _GFNLeavePay = 0 : _GFNTimeMin = 0 : _GFNOT1Min = 0 : _GFNOT1_5Min = 0
                _GFNOT2Min = 0 : _GFNOT3Min = 0 : _GFNLateMMin = 0 : _GFNLateAfMin = 0 : _GFNRetireMMin = 0
                _GFNRetireAfMin = 0 : _GFNRetireNormalCut = 0 : _GFNRetireOtMin = 0 : _GFNRetireOtCut = 0
                _LateCutAbsent = 0 : _LateCutAmt = 0 : _LateCutAmtAbsent = 0
                _Gtotalleave = 0 : _GtotalleavePay = 0 : _GtotalleaveNotPay = 0 : _GtotalleavePayCalSso = 0 : _GtotalleavePayCalSsoAmt = 0
                _TotalHoliDay = 0

                '------------------- เริ่มการคำนวณรายวัน
                Dim _oHoliday As Integer = 0

                _TotalWorkDay = 0
                _ShiftAmt = 0
                _ShiftValue = 0
                _ShiftOTValue = 0
                _ShiftOTAmt = 0
                _FCAdd = 0 : _FTAddCalculateSocial = 0 : _FTAddCalculateTax = 0 : _FCDeduct = 0
                _GAmtPlus = 0

                If _FDDateStart > _FTSatrtCalculateDate Then _FTSatrtCalculateDate = _FDDateStart '/*เริ่มงานระหว่างงวดการคำนวณ ให้วันที่เริ่มการคำนวณสิ้นงวดเท่ากับวันที่เริ่มงานจริง คือ กลางงวดการคำนวณเงินเดือน*/
                '...1
                Do While _FTSatrtCalculateDate <= _FTEndCalculateDate And (_FDDateEnd = "" Or _FTSatrtCalculateDate <= _FDDateEnd)
                    _oHoliday = 0
                    FTHldType = 0

                    Dim _NewSlary As String

                    _Qry = "SELECT TOP 1  FNCurrentSlary  AS AMT"
                    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChangeSlary WITH(NOLOCK)"
                    _Qry &= vbCrLf & "WHERE (FTEffectiveDate > N'" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "') "
                    _Qry &= vbCrLf & "      AND  (FNHSysEmpID = " & Val(_EmpCode) & ")"
                    _Qry &= vbCrLf & "ORDER BY FTEffectiveDate ASC"

                    _NewSlary = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
                    If IsNumeric(_NewSlary) Then
                        If FDDateProbation <= _FTSatrtCalculateDate Then
                            If _EmpTypePayHarmful = "1" Then
                                _NewHarmfulBaht = (_NewSlary * FNHarmfulRate) / 100
                            End If
                            If _EmpTypePaySkill = "1" Then
                                _NewSkillBaht = (_NewSlary * FNSkillRate) / 100
                            End If
                            _NewSlary = _NewSlary + _NewHarmfulBaht + _NewSkillBaht
                        Else

                            If _EmpTypePayHarmful = "1" Then
                                _NewHarmfulBaht = (_NewSlary * FNHarmfulRate) / 100
                            End If
                            _NewSlary = _NewSlary + _NewHarmfulBaht

                        End If
                    Else
                        _MSlary = _FTSlary 'CInt(CInt(_FTSlary / CountDayPerMonth) * CountDayPerMonth) 
                        _FCSalary = CDbl(_FTSlary)

                        If FDDateProbation <= _FTSatrtCalculateDate Then
                            If _EmpTypePayHarmful = "1" Then
                                FNHarmfulBaht = (_FCSalary * FNHarmfulRate) / 100
                            End If
                            If _EmpTypePaySkill = "1" Then
                                FNSkillBaht = ((_FCSalary + FNHarmfulBaht) * FNSkillRate) / 100
                            End If
                            _FCSalary = _FCSalary + FNHarmfulBaht + FNSkillBaht
                        Else
                            If _EmpTypePayHarmful = "1" Then
                                FNHarmfulBaht = (_FCSalary * FNHarmfulRate) / 100
                            End If

                            _FCSalary = _FCSalary + FNHarmfulBaht
                        End If

                    End If


                    '027 Wage Scale*********

                    FNWageScale = _GetWageScale(Double.Parse("0" & _EmpCode), _EmpType, _StartDate, _FCSalary)
                    _FCSalary += FNWageScale

                    '_MSlary = _FNSlaryPerDay * CountDayPerMonth
                    ' _FCSalary = _MSlary
                    If IsNumeric(_NewSlary) Then _FCSalary = CDbl(_NewSlary) '...ปรับเงินเดือนระหว่างงวดการคำนวนสิ้นงวด
                    '_SalaryCalOT = _FCSalary
                    _FCSalary = Calculate.HelperRoundUpBasicSalary(_FCSalary)
                    _SalaryCalOT = _FCSalary

                    _Holiday = ""
                    _SpecialHoliday = 0
                    '...วันที่ใน trans daily ตรงกับวันหยุดนักขัตฤ์ใช่หรือไม่ (match case public holiday)
                    '----------cal holiday hold edit 2016/08/29
                    For Each IR As DataRow In _DTHoliday.Select("FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "'")
                        _Holiday = "H"
                        FTHldType = Val(IR!FTHldType.ToString)
                        '     _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)


                        '  _FDDateEnd
                        '_PayTerm
                        '**New concept form hr VN 20160825 
                        'and term in master holiday 

                        Exit For

                    Next




                    'If _FDDateEnd = "" Then
                    '    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "'")
                    '        _Holiday = "H"
                    '        _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)
                    '        Exit For
                    '    Next
                    'Else
                    '    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "' and (FDHolidayDate <='" & HI.UL.ULDate.ConvertEnDB(_FDDateEnd) & "' )")
                    '        _Holiday = "H"
                    '        _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)
                    '        Exit For
                    '    Next
                    'End If



                    _FTShift = ""

                    _FNTime = 0
                    _FNNotRegis = 0
                    _FNOT1 = 0 : _FNOT1_5 = 0 : _FNOT2 = 0
                    _FNOT3 = 0 : _FNOT4 = 0
                    _FNLateNormalMin = 0 : _FNLateNormalCut = 0
                    _FNLateOtMin = 0 : _FNLateOtCut = 0
                    _FNLateMorning = 0 : _FNLateAfternoon = 0
                    _LateCutAbsent = 0 : _FNAbsent = 0
                    _FNTimeMin = 0 : _FNOT1Min = 0
                    _FNOT1_5Min = 0 : _FNOT2Min = 0
                    _FNOT3Min = 0 : _FNOT4Min = 0
                    _FNLateMMin = 0 : _FNLateAfMin = 0
                    _FNRetireMMin = 0 : _FNRetireAfMin = 0
                    _FNRetireNormalCut = 0 : _FNRetireNormalCut = 0
                    _FNRetireOtMin = 0 : _FNRetireOtMin = 0
                    _FNRetireOtCut = 0
                    _FNLeavePay = 0 : _FNLeaveVacation = 0
                    _FNLeaveNotPay = 0
                    _AmtAddCalOT = 0
                    _GtotalleavePayCalSso = 0
                    _LeaveCode = ""
                    FTTranStaCode = ""
                    _FTAOut = "" : _FTOTIn1 = "" : _FTDateTrans = ""

                    Dim _InOT As String = "" : Dim _OutOT As String = "" : Dim _Over As String = ""

                    Dim _R() As DataRow = _dttran.Select("FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "'")
                    For Each R2 In _R
                        FTTranStaCode = R2!FTTranStaCode.ToString

                        _FTShift = R2!FTShift.ToString
                        _FNTime = IIf(Val(R2!FNTime.ToString) < 0, 0, Val(R2!FNTime.ToString))
                        _FNTimeMin = IIf(Val(R2!FNTimeMin.ToString) < 0, 0, Val(R2!FNTimeMin.ToString))
                        _FNNotRegis = Val(R2!FNNotRegis.ToString)
                        _FNOT1 = Val(R2!FNOT1.ToString) : _FNOT1_5 = Val(R2!FNOT1_5.ToString) : _FNOT2 = Val(R2!FNOT2.ToString)
                        _FNOT3 = Val(R2!FNOT3.ToString) : _FNOT4 = Val(R2!FNOT3.ToString)
                        _FTAOut = R2!FTOut2.ToString : _FTOTIn1 = R2!FTIn3.ToString : _FTDateTrans = R2!FTDateTrans.ToString  ' ****************************ช่วงเวลาออก2 - เข้าโอที3 
                        _FNLateNormalMin = Val(R2!FNLateNormalMin.ToString) : _FNLateNormalCut = Val(R2!FNLateNormalCut.ToString)
                        _FNLateOtMin = Val(R2!FNLateOtMin.ToString) : _FNLateOtCut = Val(R2!FNLateOtCut.ToString)
                        _FNLateMorning = Val(R2!FNLateMorning.ToString) : _FNLateAfternoon = (Val(R2!FNLateAfternoon.ToString))
                        _LateCutAbsent = Val(R2!FNAbsentCut.ToString) : _FNAbsent = Val(R2!FNAbsent_Cut.ToString)
                        _FNOT1Min = Val(R2!FNOT1Min.ToString)
                        _FNOT1_5Min = Val(R2!FNOT1_5Min.ToString) : _FNOT2Min = Val(R2!FNOT2Min.ToString)
                        _FNOT3Min = Val(R2!FNOT3Min.ToString) : _FNOT4Min = Val(R2!FNOT4Min.ToString)
                        _FNLateMMin = Val(R2!FNLateMMin.ToString) : _FNLateAfMin = Val(R2!FNLateAfMin.ToString)
                        _FNRetireMMin = Val(R2!FNRetireMMin.ToString) : _FNRetireAfMin = Val(R2!FNRetireAfMin.ToString)
                        _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString) : _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString)
                        _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString) : _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString)
                        _FNRetireOtCut = Val(R2!FNRetireOtCut.ToString)
                        _InOT = R2!FTIn3.ToString
                        _OutOT = R2!FTOut3.ToString

                        _Over = R2!FTOverClock.ToString

                        If _FTShift <> "" And (_FNTime + _FNOT1Min + _FNOT1_5Min + _FNOT2Min + _FNOT3Min + _FNOT4Min > 0) Then
                            _ShiftValue = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FCShiftAmt FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift WHERE FNHSysShiftID = " & Val(_FTShift) & " ", Conn.DB.DataBaseName.DB_HR, "0"))

                            _TotalWorkDay = _TotalWorkDay + 1

                            _ShiftOTValue = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FCShiftOTAmt FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift WHERE FNHSysShiftID = " & Val(_FTShift) & " ", Conn.DB.DataBaseName.DB_HR, "0"))

                            '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------
                            If _FTShift <> "" And (_FNTimeMin + _FNOT1_5Min + _FNOT3Min + _FNOT1Min + _FNOT2Min + _FNOT4Min) > 0 Then

                                If _FNOT1Min + _FNOT2Min + _FNOT4Min + _FNOT1_5Min >= 2 Then
                                    _FNNetOTMealAmt = _FNNetOTMealAmt + _FNOTMealAmt
                                End If

                                _SPDateType = 0

                                _Holiday = ""
                                _SpecialHoliday = 0
                                'cal holiday hold 2016/08/29
                                For Each IR As DataRow In _DTHoliday.Select("FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "'")
                                    _Holiday = "H"
                                    '   _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)
                                    Exit For

                                Next

                                'If _FDDateEnd = "" Then
                                '    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "'")
                                '        _Holiday = "H"
                                '        _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)
                                '        Exit For
                                '    Next
                                'Else
                                '    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "' and (FDHolidayDate <='" & HI.UL.ULDate.ConvertEnDB(_FDDateEnd) & "' )")
                                '        _Holiday = "H"
                                '        _SpecialHoliday = Double.Parse(IR!FNSpecialMoney.ToString)
                                '        Exit For
                                '    Next
                                'End If





                                If _Holiday <> "" Then _SPDateType = 2

                                Dim _StateLeaveOther As Boolean = False
                                Dim _StateLeavacation As Boolean = False
                                Dim _StateFTStaMaternityleaveNotpay As Boolean = False
                                Dim _SumLeave As Integer = 0

                                For Each sR As DataRow In _dtLeave.Select("FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "'")

                                    'If System.Diagnostics.Debugger.IsAttached = True Then
                                    '    If sR!FTDateTrans.ToString = "2014/10/01" Then
                                    '        If Val(sR!FNTotalMinute.ToString) >= 480 Then
                                    '            MsgBox("FTDateTrans : " & sR!FTDateTrans.ToString & " , FNTotalMinute : " & sR!FNTotalMinute.ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                                    '        End If
                                    '    End If
                                    'End If

                                    _SumLeave = _SumLeave + Val(sR!FNTotalMinute)

                                    If Val(sR!LeaveType) = 1 Then
                                        _StateLeavacation = True
                                    Else
                                        _StateLeaveOther = True
                                    End If

                                    If Val(sR!LeaveType) = 2 Then
                                        _StateFTStaMaternityleaveNotpay = True
                                    End If

                                Next

                                '--------------------------- ค่ากะ -------------------------------------
                                For Each RFin As DataRow In _dtAddOtherAmtshift.Select("FTFinCode = '001'")
                                    Dim _StatePass As Boolean = True

                                    If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                    If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                    If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                    If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                    If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)

                                    If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                        _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                    End If

                                    If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                    If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                    If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                        Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                        Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                        If _STime.Length = _ETime.Length Then
                                            If IsDate(_STime) And IsDate(_ETime) Then
                                                If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If
                                        Else
                                            _StatePass = False
                                        End If

                                    End If

                                    If RFin!FNOTTimeM.ToString <> "" And _StatePass Then
                                        If Val(RFin!FNOTTimeM.ToString) > 0 Then

                                            If _FNOT1 + _FNOT2 + _FNOT4 > 0 Then
                                                If (_FNOT1 + _FNOT2 + _FNOT4) < Val(RFin!FNOTTimeM.ToString) Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If

                                        End If
                                    End If


                                    If RFin!FTStaVacation.ToString = "1" Then _StatePass = Not (_StateLeavacation)

                                    If _StatePass Then
                                        _ShiftAmt = _ShiftAmt + _ShiftValue

                                        If RFin!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + _ShiftValue
                                        If RFin!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + _ShiftValue
                                        If RFin!FTStaCalOT.ToString = "1" Then _AmtAddCalOT = _AmtAddCalOT + _ShiftValue
                                    End If

                                Next
                                '--------------------------- ค่ากะ -------------------------------------

                                '--------------------------- ค่ากะ OT ----------------------------------
                                For Each RFin As DataRow In _dtAddOtherAmtshift.Select("FTFinCode = '007'")
                                    Dim _StatePass As Boolean = True

                                    If _OutOT <> "" Then
                                        Beep()
                                    End If

                                    If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                    If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                    If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                    If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                    If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)
                                    If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                        _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                    End If

                                    If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                    If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                    If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                        Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                        Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                        If _STime.Length = _ETime.Length Then
                                            If IsDate(_STime) And IsDate(_ETime) Then
                                                If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If
                                        Else
                                            _StatePass = False
                                        End If

                                    End If

                                    If RFin!FNOTTimeM.ToString <> "" And _StatePass Then
                                        If Val(RFin!FNOTTimeM.ToString) > 0 Then

                                            If _FNOT1 + _FNOT2 + _FNOT4 > 0 Then
                                                If (_FNOT1 + _FNOT2 + _FNOT4) < Val(RFin!FNOTTimeM.ToString) Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If

                                        End If
                                    End If

                                    If RFin!FTStaVacation.ToString = "1" Then _StatePass = Not (_StateLeavacation)

                                    If _StatePass Then

                                        _ShiftOTAmt = _ShiftOTAmt + _ShiftOTValue

                                        If RFin!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + _ShiftOTValue
                                        If RFin!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + _ShiftOTValue
                                        If RFin!FTStaCalOT.ToString = "1" Then _AmtAddCalOT = _AmtAddCalOT + _ShiftOTValue

                                    End If

                                Next
                                '--------------------------- ค่ากะ OT ----------------------------------

                                For Each RFin As DataRow In _dtAddOtherAmt.Select("FTCalType = '0' AND FTFinType = '1' AND FTPayType = '0'")
                                    Dim _StatePass As Boolean = True

                                    If _OutOT <> "" Then
                                        Beep()
                                    End If

                                    '...THRMTranStatus
                                    Select Case True
                                        '...Attendance Allowance
                                        Case (RFin!FTFinCode.ToString = "015")
                                            If FTTranStaCode = "001" Then _StatePass = False '...พนักงานไม่รูดบัตร
                                            '...Meal Allowance
                                        Case (RFin!FTFinCode.ToString = "023")
                                            If FTTranStaCode = "004" Then _StatePass = False '...พนักงานลืมบัตร
                                            '...Car Allowance
                                        Case (RFin!FTFinCode.ToString = "024")
                                            If FTTranStaCode = "004" Then _StatePass = False '...พนักงานลืมบัตร
                                        Case Else
                                            '...Nothing 
                                    End Select

                                    'If System.Diagnostics.Debugger.IsAttached = True Then
                                    '    If (RFin!FTFinCode.ToString = "015") And (_StatePass = True) Then
                                    '        FNCntAddFinCode015 = FNCntAddFinCode015 + 1
                                    '    End If
                                    'End If

                                    '...Linear If...Statement validate
                                    If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                    If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                    If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                    If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                    If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)
                                    If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                        _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                    End If

                                    If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                    If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                    If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                        Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                        Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                        If _STime.Length = _ETime.Length Then
                                            If IsDate(_STime) And IsDate(_ETime) Then
                                                If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If
                                        Else
                                            _StatePass = False
                                        End If

                                    End If

                                    If RFin!FNOTTimeM.ToString <> "" And _StatePass Then
                                        If Val(RFin!FNOTTimeM.ToString) > 0 Then

                                            If _FNOT1 + _FNOT2 + _FNOT4 > 0 Then
                                                If (_FNOT1 + _FNOT2 + _FNOT4) < Val(RFin!FNOTTimeM.ToString) Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If

                                        End If

                                    End If

                                    REM 2014/10/30 Vietnam factory If RFin!FTStaVacation.ToString = "1" Then _StatePass = Not (_StateLeavacation)

                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)

                                    If _StatePass Then
                                        _FCAdd = _FCAdd + Val(RFin!FCFinAmt.ToString)

                                        '...Vietnam Factory
                                        If System.Diagnostics.Debugger.IsAttached = True Then
                                            If RFin!FTFinCode.ToString = "015" Then
                                                FNCntAddFinCode015 = FNCntAddFinCode015 + 1
                                                Beep()
                                                'MsgBox("FinCode : " & RFin!FTFinCode.ToString() & " : Attendance Allowance !!!" & Environment.NewLine & "FTDateTrans : " & _FTSatrtCalculateDate, MsgBoxStyle.OkOnly, "Finance Code")
                                            End If

                                        End If

                                        If RFin!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + Val(RFin!FCFinAmt.ToString)
                                        If RFin!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + Val(RFin!FCFinAmt.ToString)
                                        If RFin!FTStaCalOT.ToString = "1" Then _AmtAddCalOT = _AmtAddCalOT + Val(RFin!FCFinAmt.ToString)

                                        If _DtFin.Select("FTFinCode = '" & RFin!FTFinCode.ToString & "'").Length <= 0 Then
                                            '...ถ้ายังไม่มีรายการ Finance Code นี้ ให้ทำการเพิ่มรายการนี้ ใน DataTable พร้อมด้วยค่า รหัสรายการ และ จำนวนเงิน
                                            _DtFin.Rows.Add(RFin!FTFinCode.ToString, Val(RFin!FCFinAmt.ToString))
                                        Else
                                            '...แต่ถ้ามีการรหัส Finance Code นี้อยู่แล้ว ใน DataTable ให้ทำการ update รายการช่องจำนวนเงิน ตามรหัสนี้
                                            For Each xRow As DataRow In _DtFin.Select("FTFinCode = '" & RFin!FTFinCode.ToString & "'")
                                                xRow!FCTotalFinAmt = Val(xRow!FCTotalFinAmt) + Val(RFin!FCFinAmt.ToString)
                                            Next

                                        End If

                                    End If

                                Next

                            End If

                            '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                        End If

                    Next

                    'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ
                    Select Case FNStateSalaryType
                        Case 0 'ต่อเดือน
                            _FNSlaryPerMonth = CDbl(Format((_FCSalary), "0.00")) '+ FNHarmfulBaht + FNSkillBaht + FNWageScale
                            _FNSlaryPerDay = CDbl((_FNSlaryPerMonth) / CountDayPerMonth)
                            '  _FNSlaryPerDay = CInt(_FNSlaryPerDay)

                            _FNSlaryOTPerMonth = CDbl(Format((_SalaryCalOT), "0.00"))
                            _FNSlaryOTPerDay = CDbl((_FNSlaryOTPerMonth) / CountDayPerMonth)
                        Case 1 'ต่อวัน
                            '_FNSlaryPerMonth = CDbl(Format((_FCSalary * CountDayPerMonth), "0.00"))
                            '_FNSlaryPerDay = CDbl(Format((_FCSalary), "0.00"))
                            _FNSlaryPerMonth = CDbl(Format((_FCSalary), "0.00")) '+ FNHarmfulBaht + FNSkillBaht + FNWageScale
                            _FNSlaryPerDay = CDbl((_FNSlaryPerMonth) / CountDayPerMonth)

                            _FNSlaryOTPerMonth = CDbl(Format((_SalaryCalOT), "0.00"))
                            _FNSlaryOTPerDay = CDbl((_FNSlaryOTPerMonth) / CountDayPerMonth)

                    End Select
                    'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ

                    _FNSlaryPerHour = CDbl(Format(_FNSlaryPerDay / 8, "0.00000000000"))
                    _FNSlaryPerMin = CDbl(Format(_FNSlaryPerHour / 60, "0.00000000000"))

                    'cal hold 2016/08/30 factory vn.
                    '  _FNSlaryOTPerMin = CDbl(Format(CDbl(Format((_FNSlaryPerDay + _AmtAddCalOT + _GAmtAddCalOT) / 8, "0.00000000000")) / 60, "0.00000000000"))
                    _FNSlaryOTPerMin = CDbl(Format(CDbl(Format((_FNSlaryOTPerDay + _AmtAddCalOT + _GAmtAddCalOT) / 8, "0.00000000000")) / 60, "0.00000000000"))
                    _FNSlaryOTPerHour = CDbl(Format((_FNSlaryOTPerDay + _AmtAddCalOT + _GAmtAddCalOT) / 8, "0.00"))

                    If _FTShift = "" Then
                        If _Holiday <> "" Then
                            _oHoliday += 1
                            _TotalHoliDay = _TotalHoliDay + 1
                        End If
                    Else

                        'If _Holiday <> "" And (_FNTime + _FNOT1Min + _FNOT1_5Min + _FNOT2Min + _FNOT3Min + _FNOT4Min <= 0) Then
                        If _Holiday <> "" Then
                            _oHoliday += 1
                            _TotalHoliDay = _TotalHoliDay + 1
                        End If

                        If (_FNTime + _FNOT1Min + _FNOT1_5Min + _FNOT2Min + _FNOT3Min + _FNOT4Min > 0) Then
                            _WorkDay = _WorkDay + 1
                        End If

                        _GFNLateNormalMin = _GFNLateNormalMin + _FNLateNormalMin
                        _GFNLateNormalCut = _GFNLateNormalCut + _FNLateNormalCut
                        _GFNLateOtMin = _GFNLateOtMin + _FNLateOtMin
                        _GFNLateOtCut = _GFNLateOtCut + _FNLateOtCut
                        _GFNLateMorning = _GFNLateMorning + _FNLateMorning
                        _GFNLateAfternoon = _GFNLateAfternoon + _FNLateAfternoon
                        _GFNAbsent = _GFNAbsent + _FNAbsent
                        _GFNCutAbsent = _GFNCutAbsent + _LateCutAbsent
                        _GFNTimeMin = _GFNTimeMin + _FNTimeMin
                        _GFNOT1Min = _GFNOT1Min + _FNOT1Min
                        _GFNOT1_5Min = _GFNOT1_5Min + _FNOT1_5Min
                        _GFNOT2Min = _GFNOT2Min + _FNOT2Min
                        _GFNOT3Min = _GFNOT3Min + _FNOT3Min
                        _GFNOT4Min = _GFNOT4Min + _FNOT4Min
                        _GFNLateMMin = _GFNLateMMin + _FNLateMMin
                        _GFNLateAfMin = _GFNLateAfMin + _FNLateAfMin
                        _GFNRetireOtMin = _GFNRetireOtMin + _FNRetireOtMin
                        _GFNRetireOtCut = _GFNRetireOtCut + _FNRetireOtCut
                        _GFNRetireMMin = _GFNRetireMMin + _FNRetireMMin
                        _GFNRetireAfMin = _GFNRetireAfMin + _FNRetireAfMin
                        _GFNRetireNormalCut = _GFNRetireNormalCut + _FNRetireNormalCut

                    End If

                    _FNLeavePay = 0 : _FNLeaveVacation = 0
                    _FNLeaveNotPay = 0
                    _LeaveCode = ""

                    For Each sR As DataRow In _dtLeave.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDate) & "'")
                        _LeaveCode = sR!LFTLeaveCode.ToString
                        _FNLeavePay = Val(sR!FNTotalPayMinute.ToString)

                        _Gtotalleave = _Gtotalleave + Val(sR!FNTotalMinute.ToString)
                        _GtotalleavePay = _GtotalleavePay + Val(sR!FNTotalPayMinute.ToString)
                        _GtotalleaveNotPay = _GtotalleaveNotPay + Val(sR!FNTotalNotPayMinute.ToString)

                        If sR!FTStaCalSSO.ToString = "1" Then
                            _GtotalleavePayCalSso = Val(sR!FNTotalPayMinute.ToString)
                        End If

                        _FNLeaveNotPay = Val(sR!FNTotalNotPayMinute.ToString)

                        Select Case sR!LFTLeaveCode.ToString.ToUpper '""Val(sR!LeaveType)
                            Case "98"
                                _FNLeaveVacation = Val(sR!FNTotalPayMinute.ToString)
                                _GFNLeaveVacation = _GFNLeaveVacation + Val(sR!FNTotalMinute.ToString)
                            Case "1"
                                FNPayLeaveBusinessBahtMin = Val(sR!FNTotalPayMinute.ToString)
                                GFNPayLeaveBusinessBahtMin = GFNPayLeaveBusinessBahtMin + FNPayLeaveBusinessBahtMin

                                _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)
                            Case "0"

                                FNPayLeaveSickBahtMin = Val(sR!FNTotalPayMinute.ToString)
                                GFNPayLeaveSickBahtMin = GFNPayLeaveSickBahtMin + FNPayLeaveSickBahtMin
                                _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)

                                If FNPayLeaveSickBahtMin > 0 Then
                                    _LeaveSickPay = _LeaveSickPay + 1

                                    Dim _PayPer As Double = 0
                                    Select Case _LeaveSickPay
                                        Case Is <= 30
                                            _PayPer = 100
                                        Case Is <= 60
                                            _PayPer = 60
                                        Case Is <= 180
                                            _PayPer = 40
                                    End Select

                                    If _PayPer = 0 Then
                                        FNPayLeaveSickBahtMin = 0
                                        GFNPayLeaveSickBahtMin = GFNPayLeaveSickBahtMin - FNPayLeaveSickBahtMin
                                        _GFNLeaveOther = _GFNLeaveOther - FNPayLeaveSickBahtMin
                                    End If

                                    If _DTEmpPayLeaveSick.Select("FNSalary=" & _FNSlaryPerDay & " AND FNPayPer = " & _PayPer & " ").Length > 0 Then
                                        For Each Rx As DataRow In _DTEmpPayLeaveSick.Select("FNSalary = " & _FNSlaryPerDay & "  AND FNPayPer = " & _PayPer & " ")
                                            Rx!FNDay = Val(Rx!FNDay) + FNPayLeaveSickBahtMin
                                            Exit For
                                        Next
                                    Else
                                        _DTEmpPayLeaveSick.Rows.Add(_FNSlaryPerDay, FNPayLeaveSickBahtMin, _PayPer)
                                    End If


                                End If

                            Case "2"
                                FNPayLeaveSpecialBahtMin = Val(sR!FNTotalPayMinute.ToString)
                                _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)
                            Case "97"
                                FNParturitionLeaveMin = Val(sR!FNTotalPayMinute.ToString)
                                GFNParturitionLeaveMin = GFNParturitionLeaveMin + FNParturitionLeaveMin

                                _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)
                            Case Else
                                _FNLeavePay = Val(sR!FNTotalPayMinute.ToString)
                                _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)
                        End Select

                    Next

                    _GFNLeavePay = _GFNLeavePay + _FNLeavePay
                    _SocialBefore = 0
                    _SocialBeforeAmt = 0

                    Dim _WageAmtPerDay As Double = 0
                    Dim _WageOTAmtPerDay As Double = 0
                    Dim _TimeOTMdr As Integer = 0

                    If _FTEmpState = "2" Or _FTEmpState = "3" Then
                    Else
                        _WageAmtPerDay = CDbl(Format((_FNTimeMin) * _FNSlaryPerMin, "0.00"))
                        _FNEmpBaht = _FNEmpBaht + _WageAmtPerDay ' CDbl(Format((_FNTimeMin) * _FNSlaryPerMin, "0.00"))
                    End If


                    If _FNOT1Min >= 90 Then
                        ' DateDiff(DateInterval.Minute, Convert.ToDateTime(_FTDateTrans & " "&_FTAOut),Convert.ToDateTime(_FTDateTrans & " " & _FTOTIn1))
                        Dim _RestOverTimeMin As Integer = Integer.Parse(DateDiff(DateInterval.Minute, Convert.ToDateTime(_FTDateTrans & " " & _FTAOut), Convert.ToDateTime(_FTDateTrans & " " & _FTOTIn1)))
                        FNPayRestOTMin += +_RestOverTimeMin
                        FNPayRestOTBaht = FNPayRestOTBaht + CDbl(Format((_RestOverTimeMin) * ((_FNSlaryOTPerMin) * _RateOT1), "0.000000"))
                    End If
                    _nBahtOt1 = _nBahtOt1 + CDbl((_FNOT1Min) * ((_FNSlaryOTPerMin) * _RateOT1))


                    If FTHldType = 1 And _FNOT3Min > 0 Then
                        _GAmtPlus = _GAmtPlus + _AmtPlus
                    End If

                    _nBahtOt15 = _nBahtOt15 + CDbl(Format((_FNOT1_5Min) * ((_FNSlaryOTPerMin) * _RateOT15), "0.00000"))
                    _nBahtOt2 = _nBahtOt2 + CDbl(Format((_FNOT2Min) * ((_FNSlaryOTPerMin) * _RateOT2), "0.00000"))
                    _nBahtOt3 = _nBahtOt3 + CDbl(Format((_FNOT3Min) * ((_FNSlaryOTPerMin) * _RateOT3), "0.00000"))
                    _nBahtOt4 = _nBahtOt4 + CDbl(Format((_FNOT4Min) * ((_FNSlaryOTPerMin) * _RateOT4), "0.00000"))

                    _nBahtAbsent = _nBahtAbsent + CDbl(Format(_FNAbsent * _FNSlaryPerMin, "0.00"))
                    _LateCutAmt = _LateCutAmt + CDbl(Format((_FNLateNormalCut) * _FNSlaryPerMin, "0.00"))
                    _LateCutAmtAbsent = _LateCutAmtAbsent + CDbl(Format((_LateCutAbsent) * _FNSlaryPerMin, "0.00"))

                    _LaNotpaid = _LaNotpaid + CDbl(Format(_FNLeaveNotPay * _FNSlaryPerMin, "0.00"))

                    Dim _TmpLapaidAmt As Double = CDbl(Format(_FNLeavePay * _FNSlaryPerMin, "0.00"))
                    _Lapaid = _Lapaid + _TmpLapaidAmt
                    _GtotalleavePayCalSsoAmt = _GtotalleavePayCalSsoAmt + CDbl(Format(_GtotalleavePayCalSso * _FNSlaryPerMin, "0.00"))  'เงินลาจ่ายที่นำไปคิดประกันสังคม

                    If _LeaveCode <> "" And _FNLeaveVacation > 0 Then
                        _FCPayVacationBaht = _FCPayVacationBaht + CDbl(Format(_FNLeaveVacation * _FNSlaryPerMin, "0.00"))
                    Else
                        _FCPayVacationBaht = _FCPayVacationBaht + CDbl(Format(_FNLeaveVacation * _FNSlaryPerMin, "0.00"))
                    End If

                    REM 2014/10/21 Vietnam factory
                    If _FTStatePayHoliday <> "1" Then '--------- ไม่ได้ค่าจ้างวันหยุด---------------
                        _oHoliday = 0
                    Else

                        'Or (_FNLeaveNotPay > 0 And _LeaveCode = "97")
                        If (_FNLeaveNotPay <= 0) Then
                            If _TmpLapaidAmt <= 0 Then ' กรณีไม่มีลาจ่ายในวันนักขัต 
                                _HBaht = _HBaht + CDbl(Format(_oHoliday * _FNSlaryPerDay, "0.00"))
                                'If FTProbationSta = "1" Then
                                _HBaht += +_SpecialHoliday
                                'End If

                            Else ' กรณีมีลาจ่ายในวันนักขัต  ไม่ได้นักขัต ได้ลาจ่าย

                                _TotalHoliDay = _TotalHoliDay - _oHoliday
                                _oHoliday = 0
                            End If
                        Else ' กรณีมีลาไม่จ่ายในวันนักขัต  ไม่ได้นักขัต

                            _TotalHoliDay = _TotalHoliDay - _oHoliday
                            _oHoliday = 0
                        End If

                    End If



                    If _DTEmpWorkDay.Select("FNSalary = " & _FNSlaryPerDay & "").Length > 0 Then
                        '...ถ้ามีรายการนี้แล้วให้ทำการ Update รายการ นาที สะสม ของรายการต่างๆ
                        For Each Rx As DataRow In _DTEmpWorkDay.Select("FNSalary=" & _FNSlaryPerDay & "")
                            Rx!FNDay = Val(Rx!FNDay) + _FNTimeMin
                            Rx!FNOT1 = Val(Rx!FNOT1) + _FNOT1Min
                            Rx!FNOT15 = Val(Rx!FNOT15) + _FNOT1_5Min
                            Rx!FNOT2 = Val(Rx!FNOT2) + _FNOT2Min
                            Rx!FNOT3 = Val(Rx!FNOT3) + _FNOT3Min
                            Rx!FNOT4 = Val(Rx!FNOT4) + _FNOT4Min
                            Rx!FNHoloday = Val(Rx!FNHoloday) + (_oHoliday)
                            Rx!FNLate = Val(Rx!FNLate) + _FNLateNormalCut
                            Rx!FNAbsent = Val(Rx!FNAbsent) + _FNAbsent
                            Rx!FNLateCutAmtAbsent = Val(Rx!FNLateCutAmtAbsent) + _LateCutAbsent

                            Rx!FNLeavePay = Val(Rx!FNLeavePay) + _FNLeavePay
                            Rx!FNLeaveNotPay = Val(Rx!FNLeaveNotPay) + (_FNLeaveNotPay)
                            Rx!FNBusiness = Val(Rx!FNBusiness) + FNPayLeaveBusinessBahtMin
                            Rx!FNSpecial = Val(Rx!FNSpecial) + FNPayLeaveSpecialBahtMin
                            Rx!FNParturition = Val(Rx!FNParturition) + FNParturitionLeaveMin

                            Exit For

                        Next

                    Else
                        '...รายการเงินได้ประจำวัน ถ้ายังไม่มีรายการนี้ใน DataTable ให้ทำการเพิ่มรายการใน DataTable
                        _DTEmpWorkDay.Rows.Add(_FNSlaryPerDay, _FNTimeMin, _FNOT1Min, _FNOT1_5Min,
                                               _FNOT2Min, _FNOT3Min, _FNOT4Min, (_oHoliday),
                                               _FNLateNormalCut, _FNAbsent, _LateCutAbsent, _FNLeavePay,
                                                _FNLeaveNotPay, FNPayLeaveBusinessBahtMin, FNPayLeaveSpecialBahtMin, FNParturitionLeaveMin)
                    End If

                    '...vietnam factory leave not payment
                    'If System.Diagnostics.Debugger.IsAttached = True Then
                    '    If _FNLeaveNotPay > 0 Then
                    '        MsgBox("_FNLeaveNotPay : " & Environment.NewLine & "Date Trans : " & _FTSatrtCalculateDate & Environment.NewLine & "Minute Not Payment : " & _FNLeaveNotPay, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                    '    End If
                    'End If



                    _FTSatrtCalculateDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_FTSatrtCalculateDate, 1)) '...increment date re-calculate payroll 

                Loop

                _FNEmpBaht = 0
                _FNEmpBaht = 0
                _nBahtOt1 = 0
                _nBahtOt15 = 0
                _nBahtOt2 = 0
                _nBahtOt3 = 0
                _nBahtOt4 = 0
                _nBahtAbsent = 0
                _LateCutAmt = 0
                _LateCutAmtAbsent = 0
                '  _HBaht = 0
                _LaNotpaid = 0
                _Lapaid = 0

                FNPayLeaveBusinessBaht = 0 : FNPayLeaveSickBaht = 0 : FNPayLeaveSpecialBaht = 0 : FNParturitionLeave = 0

                For Each Rx As DataRow In _DTEmpWorkDay.Rows
                    _FNSlaryPerMin = CDbl(Format(CDbl(Rx!FNSalary) / 480, "0.00000000000"))
                    ' คิดโอที เรท เดี๋ยวกับค่า แรง ปัดเศษ 2018/0519 NOH
                    _FNSlaryOTPerMin = _FNSlaryPerMin
                    _FNEmpBaht = _FNEmpBaht + CDbl(Format(Val(Rx!FNDay) * _FNSlaryPerMin, "0.00"))
                    _nBahtOt1 = _nBahtOt1 + CDbl(Val(Rx!FNOT1) * ((_FNSlaryOTPerMin) * _RateOT1))
                    _nBahtOt15 = _nBahtOt15 + CDbl(Format(Val(Rx!FNOT15) * ((_FNSlaryOTPerMin) * _RateOT15), "0.00"))
                    _nBahtOt2 = _nBahtOt2 + CDbl(Format(Val(Rx!FNOT2) * ((_FNSlaryOTPerMin) * _RateOT2), "0.00"))
                    _nBahtOt3 = _nBahtOt3 + CDbl(Format(Val(Rx!FNOT3) * ((_FNSlaryOTPerMin) * _RateOT3), "0.00"))
                    _nBahtOt4 = _nBahtOt4 + CDbl(Format(Val(Rx!FNOT4) * ((_FNSlaryOTPerMin) * _RateOT4), "0.00"))
                    _nBahtAbsent = _nBahtAbsent + CDbl(Format(Val(Rx!FNAbsent) * _FNSlaryPerMin, "0.00"))
                    _LateCutAmt = _LateCutAmt + CDbl(Format((Val(Rx!FNLate)) * _FNSlaryPerMin, "0.00"))
                    _LateCutAmtAbsent = _LateCutAmtAbsent + CDbl(Format((Val(Rx!FNLateCutAmtAbsent)) * _FNSlaryPerMin, "0.00"))


                    '   _HBaht = _HBaht + CDbl(Format(Val(Rx!FNHoloday) * CDbl(Rx!FNSalary), "0.00"))

                    _LaNotpaid = _LaNotpaid + CDbl(Format(Val(Rx!FNLeaveNotPay) * _FNSlaryPerMin, "0.00"))
                    _Lapaid = _Lapaid + CDbl(Format(Val(Rx!FNLeavePay) * _FNSlaryPerMin, "0.00"))
                    FNPayLeaveBusinessBaht = FNPayLeaveBusinessBaht + CDbl(Format(Val(Rx!FNBusiness) * _FNSlaryPerMin, "0.00"))
                    FNPayLeaveSpecialBaht = FNPayLeaveSpecialBaht + CDbl(Format(Val(Rx!FNSpecial) * _FNSlaryPerMin, "0.00"))
                    FNParturitionLeave = FNParturitionLeave + CDbl(Format(Val(Rx!FNParturition) * _FNSlaryPerMin, "0.00"))

                Next

                FNPayLeaveSickBaht = 0
                For Each Rx As DataRow In _DTEmpPayLeaveSick.Rows
                    _FNSlaryPerMin = (CDbl(Format(CDbl(Rx!FNSalary) / 480, "0.00000000000")) * CDbl(Rx!FNPayPer)) / 100.0
                    FNPayLeaveSickBaht = FNPayLeaveSickBaht + CDbl(Format(Val(Rx!FNDay) * _FNSlaryPerMin, "0.00"))
                Next

                If _FNNetOTMealAmt > 0 And _FNExchangeRate > 0 Then
                    _FNNetOTMealAmtUS = Format(_FNNetOTMealAmt / _FNExchangeRate, "0.00")
                End If


                If _FTEmpState = "2" Or _FTEmpState = "3" Then ' คำนวณค่าแรงพนักงานรายเดือน

                    FNPayLeaveBusinessBahtMin = 0 : FNPayLeaveSickBahtMin = 0 : FNPayLeaveSpecialBahtMin = 0 : FNParturitionLeaveMin = 0
                    GFNPayLeaveBusinessBahtMin = 0 : GFNPayLeaveSickBahtMin = 0 : GFNPayLeaveSpecialBahtMin = 0 : GFNParturitionLeaveMin = 0
                    FNPayLeaveBusinessBaht = 0 : FNPayLeaveSickBaht = 0 : FNPayLeaveSpecialBaht = 0 : FNParturitionLeave = 0

                    If _dttran.Select("FTStateAccept <> '1' AND FTWeekly <> '1'").Length > 0 Or _dttran.Rows.Count <= 0 Then
                        Return False
                    End If

                    _FCSalary = _MSlary
                    'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ
                    Select Case FNStateSalaryType
                        Case 0 'ต่อเดือน
                            _FNSlaryPerMonth = CDbl(Format((_FCSalary + FNHarmfulBaht + FNSkillBaht + FNWageScale), "0.00"))
                            _FNSlaryPerDay = CDbl((_FCSalary + FNHarmfulBaht + FNSkillBaht + FNWageScale) / CountDayPerMonth)
                            '_FNSlaryPerDay = CDbl(Format((_FCSalary) / CountDayPerMonth, "0.0000000"))
                        Case 1 'ต่อวัน
                            '_FNSlaryPerMonth = CDbl(Format((_FCSalary * CountDayPerMonth), "0.00"))
                            '_FNSlaryPerDay = CDbl(Format((_FCSalary), "0.00"))
                            _FNSlaryPerMonth = CDbl(Format((_FCSalary + FNHarmfulBaht + FNSkillBaht + FNWageScale), "0.00"))
                            _FNSlaryPerDay = CDbl((_FCSalary + FNHarmfulBaht + FNSkillBaht + FNWageScale) / CountDayPerMonth)
                    End Select
                    'อัตราค่าแรงบันทึก ต่อเดือน หรือ ต่อวัน ฯลฯ

                    _Gtotalleave = 0
                    _GtotalleavePay = 0
                    _GtotalleaveNotPay = 0
                    _GFNLeaveOther = 0
                    _GFNLeavePay = 0
                    _GtotalleavePayCalSso = 0
                    _LaNotpaid = 0

                    Dim _NewSlary As String = ""

                    For Each sR As DataRow In _dtLeave.Rows

                        _Gtotalleave = _Gtotalleave + Val(sR!FNTotalMinute.ToString)
                        _GtotalleavePay = _GtotalleavePay + Val(sR!FNTotalPayMinute.ToString)
                        _GtotalleaveNotPay = _GtotalleaveNotPay + Val(sR!FNTotalNotPayMinute.ToString)

                        If sR!FTStaCalSSO.ToString = "1" Then
                            _GtotalleavePayCalSso = Val(sR!FNTotalPayMinute.ToString)
                        End If

                        _FNLeaveNotPay = Val(sR!FNTotalNotPayMinute.ToString)

                        If Val(sR!LeaveType) = 1 Then
                            _FNLeaveVacation = Val(sR!FNTotalPayMinute.ToString)
                            _GFNLeaveVacation = _GFNLeaveVacation + Val(sR!FNTotalMinute.ToString)
                        Else
                            _GFNLeavePay = _GFNLeavePay + Val(sR!FNTotalPayMinute.ToString)
                            _GFNLeaveOther = _GFNLeaveOther + Val(sR!FNTotalMinute.ToString)
                        End If

                    Next

                    _LaNotpaid = CDbl(Format(_GtotalleaveNotPay * _FNSlaryPerMin, "0.00"))

                    If _LaNotpaid > _FNSlaryPerMonth Then
                        _LaNotpaid = _FNSlaryPerMonth
                    End If

                    _WorkingDay = Abs(DateDiff(DateInterval.Day, CDate(_DateStartOfMonth), CDate(_DateEndOfMonth))) + 1

                    If _FDDateStart <= _StartDate And (_FDDateEnd >= _DateEndOfMonth Or _FDDateEnd = "") Then

                        Dim _SmDate As String = _DateStartOfMonth
                        Dim _EmDate As String = _DateEndOfMonth
                        Dim _dtSalary As New DataTable

                        _dtSalary.Columns.Add("FNCurrentSlary", GetType(Double))
                        _dtSalary.Columns.Add("FNDay", GetType(Integer))
                        '...2
                        Do While _SmDate <= _EmDate

                            _Qry = "SELECT TOP 1  FNCurrentSlary AS AMT"
                            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChangeSlary WITH(NOLOCK)"
                            _Qry &= vbCrLf & "WHERE (FTEffectiveDate > N'" & HI.UL.ULDate.ConvertEnDB(_SmDate) & "') "
                            _Qry &= vbCrLf & "      AND  (FNHSysEmpID = " & Val(_EmpCode) & ")"
                            _Qry &= vbCrLf & "ORDER BY FTEffectiveDate ASC "

                            _NewSlary = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
                            If IsNumeric(_NewSlary) Then
                                _NewHarmfulBaht = (_NewSlary * FNHarmfulRate) / 100
                                If _EmpTypePaySkill = "1" Then
                                    _NewSkillBaht = (_NewSlary * FNSkillRate) / 100
                                End If
                            End If
                            _FCSalary = _NewSlary + FNHarmfulBaht + FNSkillBaht
                            _FCSalary = CDbl(_FTSlary)
                            '027 Wage Scale*********
                            FNWageScale = _GetWageScale(Double.Parse("0" & _EmpCode), _EmpType, _StartDate, _FCSalary)
                            _FCSalary += FNWageScale
                            _FCSalary = Calculate.HelperRoundUpBasicSalary(_FCSalary)
                            '_SalaryCalOT = _FCSalary
                            '_FCSalary = _FNSlaryPerMonth

                            If IsNumeric(_NewSlary) Then _FCSalary = CDbl(_NewSlary) + _NewHarmfulBaht + _NewSkillBaht

                            If _dtSalary.Select("FNCurrentSlary=" & _FCSalary & "").Length > 0 Then

                                For Each Rxx As DataRow In _dtSalary.Select("FNCurrentSlary = " & _FCSalary & "")
                                    Rxx!FNDay = Integer.Parse(Val(Rxx!FNDay)) + 1
                                    Exit For
                                Next

                            Else
                                _dtSalary.Rows.Add(_FCSalary, 1)
                            End If

                            _SmDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SmDate, 1))

                        Loop

                        Dim _TotalDay As Integer = 0

                        _FNEmpBaht = 0
                        If _dtSalary.Select("FNCurrentSlary <> " & _FNSlaryPerMonth & "").Length > 0 Then
                            For Each Rxx As DataRow In _dtSalary.Select("FNCurrentSlary <> " & _FNSlaryPerMonth & "")
                                _TotalDay = _TotalDay + Integer.Parse(Val(Rxx!FNDay))

                                _FNEmpBaht = _FNEmpBaht + Double.Parse(Format((Integer.Parse(Val(Rxx!FNDay)) * Double.Parse(Val(Rxx!FNCurrentSlary))), "0.00"))
                            Next

                            _FNEmpBaht = _FNEmpBaht + (_FNSlaryPerMonth - CDbl(Format(_TotalDay * _FNSlaryPerDay, "0.00")))

                        Else
                            _FNEmpBaht = _FNSlaryPerMonth
                        End If

                        _dtSalary.Dispose()

                    Else

                        Dim _TotalDay As Integer = 0
                        If _FDDateStart > _StartDate And _FDDateEnd < _EndDate And _FDDateEnd <> "" Then
                            _TotalDay = Abs(DateDiff(DateInterval.Day, CDate(_FDDateStart), CDate(_FDDateEnd))) + 1

                        ElseIf _FDDateStart > _StartDate Then
                            _TotalDay = Abs(DateDiff(DateInterval.Day, CDate(_DateStartOfMonth), CDate(_FDDateStart)))
                        ElseIf _FDDateEnd < _EndDate And _FDDateEnd <> "" Then
                            If _FDDateEnd < _DateStartOfMonth Then
                                _TotalDay = _WorkingDay
                            Else
                                _TotalDay = Abs(DateDiff(DateInterval.Day, CDate(_FDDateEnd), CDate(_DateEndOfMonth))) + 1
                            End If
                        End If

                        If _TotalDay >= _WorkingDay Then
                            _FNEmpBaht = 0
                            _WorkingDay = 0
                        Else
                            _FNEmpBaht = _FNSlaryPerMonth - CDbl(Format(_TotalDay * _FNSlaryPerDay, "0.00"))
                            If _WorkingDay > 30 Then _WorkingDay = 30
                            _WorkingDay = _WorkingDay - _TotalDay
                        End If

                    End If

                    If _WorkingDay > 30 Then _WorkingDay = 30

                    _WorkingDay = CDbl(Format(((_WorkingDay * 480) - (_Gtotalleave)) / 480, "0.00"))
                    _WorkingDay = _WorkingDay - (_GFNAbsent / 480)
                    If _WorkingDay < 0 Then
                        _WorkingDay = 0
                    End If

                Else
                    _WorkingDay = CDbl(Format(_GFNTimeMin / 480, "0.00"))   '...จำนวนวันทำงานจริง
                End If

                '-----------calculate Other Add For KKN ------------------ 
                Dim _ChkLeave As Integer = 0
                For Each sR As DataRow In _dtLeave.Select("LFTLeaveCode='0' OR LFTLeaveCode='1' OR LFTLeaveCode='2' OR LFTLeaveCode='3' ")
                    _ChkLeave = _ChkLeave + Val(sR!FNTotalMinute.ToString)
                Next

                _FNNetAttandanceAmt = 0
                If _FTEmpState = "2" Or (_FTEmpState <> "2" And _StartDate <= Left(_StartDate, 8) & "24" And _EndDate >= Left(_StartDate, 8) & "24") Then
                    If _GFNAbsent <= 0 And _GFNLateNormalMin <= 15 And _GFNLeaveOther <= 0 Then

                        Dim _FNWorkingDayBF As Integer = 0
                        _Qry = "SELECT TOP 1 SUM(ISNULL(P.FNWorkingDay,0)) AS FNWorkingDay"
                        _Qry &= vbCrLf & "FROM dbo.THRTPayRoll AS P WITH (NOLOCK), (SELECT FTPayYear ,FTPayTerm,FNMonth"
                        _Qry &= vbCrLf & "                                          FROM THRMCfgPayDT  WITH (NOLOCK) WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ") ) AS PD"
                        _Qry &= vbCrLf & "                                          WHERE P.FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                        _Qry &= vbCrLf & "                                                AND ISNULL(P.FNTotalRecalSSO,0) > 0 "
                        _Qry &= vbCrLf & "                                                AND P.FTEmpIdNo = '" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "'"
                        _Qry &= vbCrLf & "                                                AND PD.FTPayTerm < '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                        _Qry &= vbCrLf & "                                                AND P.FTPayYear = PD.FTPayYear"
                        _Qry &= vbCrLf & "                                                AND P.FTPayTerm = PD.FTPayTerm"
                        _Qry &= vbCrLf & "                                                AND PD.FNMonth IN ("
                        _Qry &= "SELECT  FNMonth"
                        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH (NOLOCK) "
                        _Qry &= vbCrLf & "WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ")"
                        _Qry &= vbCrLf & "       AND  FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                        _Qry &= vbCrLf & "       AND  FTPayTerm = '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                        _Qry &= ")"

                        _FNWorkingDayBF = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

                        _FNNetAttandanceAmt = _FNAttandanceAmt

                        If _FDDateEnd <> "" And _FDDateEnd > _DateStartOfMonth And _FDDateEnd <= _DateEndOfMonth Then
                            If (_WorkingDay + _FNWorkingDayBF) < 26 And _EndDate <> "" Then
                                _FNNetAttandanceAmt = Format((_FNNetAttandanceAmt / 26) * (_WorkingDay + _FNWorkingDayBF), "0.00")
                            End If
                            'ElseIf _FDDateStart > _StartDate And _FDDateStart <= _EndDate Then
                        ElseIf _FDDateStart > _DateStartOfMonth And _FDDateStart <= _DateEndOfMonth Then
                            If (_WorkingDay + _FNWorkingDayBF) < 26 Then
                                _FNNetAttandanceAmt = Format((_FNNetAttandanceAmt / 26) * (_WorkingDay + _FNWorkingDayBF), "0.00")
                            End If
                        End If

                    End If

                End If

                '-----------calculate Other Add For KKN ------------------
                _WorkingDay = _WorkingDay + _DayAdjAdd

                _GFNTimeMin = _GFNTimeMin + (_DayAdjAdd * 480)

                _FNEmpBaht = _FNEmpBaht + _WageAdjAdd

                _nBahtOt1 = CDbl(Format(_nBahtOt1, "0.00"))
                _nBahtOt15 = CDbl(Format(_nBahtOt15, "0.00"))
                _nBahtOt2 = CDbl(Format(_nBahtOt2, "0.00"))
                _nBahtOt3 = CDbl(Format(_nBahtOt3, "0.00")) + _GAmtPlus  'ได้เงินพิเศาช่วงเทศกาลเพิ่ม
                _nBahtOt4 = CDbl(Format(_nBahtOt4, "0.00"))

                Dim _TmpPe As String = ""

                If _FTEmpState = "2" Then
                Else
                    _TmpPe = IIf(Val(_PayTerm) - 1 Mod 2 = 1, (Val(_PayTerm) - 1).ToString("00"), "")
                End If

                '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน ประเภทจ่ายเป็นเดือน ของงวดก่อนหน้า ---------------------
                'If _FTStatePayHoliday <> "1" Then '--------- รายเดือนไม่ได้ค่าจ้างวันหยุด---------------
                'Else
                If _dtAddOtherAmt.Select("FTCalType = '0' AND FTFinType = '1' AND FTPayType = '1' ").Length > 0 Then

                    Dim _BFSDate As String = ""
                    Dim _BFEDate As String = ""

                    _Qry = " SELECT TOP 1 FDCalDateBegin, FDCalDateEnd"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH (NOLOCK)"
                    _Qry &= vbCrLf & "WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ")"
                    _Qry &= vbCrLf & "       AND FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                    _Qry &= vbCrLf & "       AND FTPayTerm < '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                    _Qry &= vbCrLf & "       AND FTPayMonth IN (   "
                    _Qry &= "SELECT FTPayMonth"
                    _Qry &= vbCrLf & "                    FROM THRMCfgPayDT WITH (NOLOCK) "
                    _Qry &= vbCrLf & "                    WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ")"
                    _Qry &= vbCrLf & "                           AND  FTPayYear ='" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                    _Qry &= vbCrLf & "                           AND  FTPayTerm ='" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                    _Qry &= vbCrLf & ")"

                    _dttemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each Row As DataRow In _dttemp.Rows
                        _BFSDate = Row!FDCalDateBegin.ToString
                        _BFEDate = Row!FDCalDateEnd.ToString
                    Next

                    If _BFSDate <> "" And _BFEDate <> "" Then

                        _Qry = "SELECT  ISNULL(T.FNHSysShiftID, 0) AS FTShift, (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) + ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTime"
                        _Qry &= vbCrLf & ", ISNULL(T.FNNotRegis, 0) AS FNNotRegis, ISNULL(FNOT1, 0) AS FNOT1"
                        _Qry &= vbCrLf & ", ISNULL(FNOT1_5, 0) AS FNOT1_5"
                        _Qry &= vbCrLf & ", ISNULL(FNOT2, 0 ) AS FNOT2  , ISNULL(FNOT3, 0) AS FNOT3, ISNULL(FNOT4, 0) AS FNOT4"
                        _Qry &= vbCrLf & ", ISNULL(FNLateNormalMin, 0) AS FNLateNormalMin, ISNULL(FNLateNormalCut, 0 ) + ISNULL(FNAbsentCut, 0 ) AS FNLateNormalCut"
                        _Qry &= vbCrLf & ", ISNULL(FNLateOtMin, 0) AS FNLateOtMin,ISNULL(FNLateOtCut, 0) As FNLateOtCut"
                        _Qry &= vbCrLf & ", ISNULL(FNLateMMin, 0) AS FNLateMorning"
                        _Qry &= vbCrLf & ", ISNULL(FNLateAfMin, 0) AS FNLateAfternoon,Isnull(FNAbsentCut, 0) AS FNAbsentCut "
                        _Qry &= vbCrLf & ", ISNULL(FNAbsent, 0) AS FNAbsent "
                        _Qry &= vbCrLf & ", (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) + ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTimeMin"
                        _Qry &= vbCrLf & ", ISNULL(FNTimeMin, 0)  + ISNULL(FNSpecialTimeMin, 0) AS FNTimeMinOrg"
                        _Qry &= vbCrLf & ", ISNULL(FNOT1Min, 0) AS FNOT1Min"
                        _Qry &= vbCrLf & ", ISNULL(FNOT1_5Min, 0) AS FNOT1_5Min "
                        _Qry &= vbCrLf & ", ISNULL(FNOT2Min, 0) AS FNOT2Min "
                        _Qry &= vbCrLf & ", ISNULL(FNOT3Min, 0) As FNOT3Min, ISNULL(FNOT4Min,0) As FNOT4Min"
                        _Qry &= vbCrLf & ", ISNULL( FNLateMMin, 0) AS FNLateMMin "
                        _Qry &= vbCrLf & ", ISNULL(FNLateAfMin, 0) AS FNLateAfMin"
                        _Qry &= vbCrLf & ", ISNULL(FNRetireMMin, 0) AS FNRetireMMin"
                        _Qry &= vbCrLf & ", ISNULL(FNRetireAfMin, 0 ) AS FNRetireAfMin"
                        _Qry &= vbCrLf & ", ISNULL(FNRetireNormalCut, 0) AS FNRetireNormalCut"
                        _Qry &= vbCrLf & ", ISNULL(FNRetireOtMin, 0) AS FNRetireOtMin"
                        _Qry &= vbCrLf & ", ISNULL(FNRetireOtCut, 0) AS FNRetireOtCut,FTDateTrans"
                        _Qry &= vbCrLf & ", ISNULL(T.FTIn1, '') AS FTIn1"
                        _Qry &= vbCrLf & ", ISNULL(T.FTOut1, '') AS FTOut1"
                        _Qry &= vbCrLf & ", ISNULL(T.FTIn2, '') AS FTIn2"
                        _Qry &= vbCrLf & ", ISNULL(T.FTOut2, '') AS FTOut2"
                        _Qry &= vbCrLf & ", ISNULL(T.FTIn3, '') AS FTIn3"
                        _Qry &= vbCrLf & ", ISNULL(T.FTOut3, '') AS FTOut3"
                        _Qry &= vbCrLf & ", P.FTOverClock, P.FTWeekDay"
                        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) LEFT OUTER JOIN  THRMTimeShift AS P WITH(NOLOCK) ON T.FNHSysShiftID = P.FNHSysShiftID"
                        _Qry &= vbCrLf & "WHERE (T.FNHSysEmpID =" & Val(_EmpCode) & " )"
                        _Qry &= vbCrLf & " 	    AND T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "' "
                        _Qry &= vbCrLf & " 	    AND T.FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_BFEDate) & "' "

                        _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                        '...3
                        Do While _BFSDate <= _BFEDate
                            _FTShift = ""
                            Dim _InOT As String = "" : Dim _OutOT As String = "" : Dim _Over As String = ""

                            Dim _R() As DataRow = _dttran.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "'")
                            For Each R2 In _R

                                _FTShift = R2!FTShift.ToString
                                _FNTime = IIf(Val(R2!FNTime.ToString) < 0, 0, Val(R2!FNTime.ToString))
                                _FNTimeMin = IIf(Val(R2!FNTimeMin.ToString) < 0, 0, Val(R2!FNTimeMin.ToString))
                                _FNNotRegis = Val(R2!FNNotRegis.ToString)
                                _FNOT1 = Val(R2!FNOT1.ToString) : _FNOT1_5 = Val(R2!FNOT1_5.ToString) : _FNOT2 = Val(R2!FNOT2.ToString)
                                _FNOT3 = Val(R2!FNOT3.ToString) : _FNOT4 = Val(R2!FNOT3.ToString)
                                _FNLateNormalMin = Val(R2!FNLateNormalMin.ToString) : _FNLateNormalCut = Val(R2!FNLateNormalCut.ToString)
                                _FNLateOtMin = Val(R2!FNLateOtMin.ToString) : _FNLateOtCut = Val(R2!FNLateOtCut.ToString)
                                _FNLateMorning = Val(R2!FNLateMorning.ToString) : _FNLateAfternoon = (Val(R2!FNLateAfternoon.ToString))
                                _LateCutAbsent = Val(R2!FNAbsentCut.ToString) : _FNAbsent = Val(R2!FNAbsent.ToString)
                                _FNOT1Min = Val(R2!FNOT1Min.ToString)
                                _FNOT1_5Min = Val(R2!FNOT1_5Min.ToString) : _FNOT2Min = Val(R2!FNOT2Min.ToString)
                                _FNOT3Min = Val(R2!FNOT3Min.ToString) : _FNOT4Min = Val(R2!FNOT4Min.ToString)
                                _FNLateMMin = Val(R2!FNLateMMin.ToString) : _FNLateAfMin = Val(R2!FNLateAfMin.ToString)
                                _FNRetireMMin = Val(R2!FNRetireMMin.ToString) : _FNRetireAfMin = Val(R2!FNRetireAfMin.ToString)
                                _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString) : _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString)
                                _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString) : _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString)
                                _FNRetireOtCut = Val(R2!FNRetireOtCut.ToString)

                                _InOT = R2!FTIn3.ToString
                                _OutOT = R2!FTOut3.ToString

                                _Over = R2!FTOverClock.ToString

                                If _FTShift <> "" And (_FNTimeMin + _FNOT1_5Min + _FNOT3Min + _FNOT1Min + _FNOT2Min + _FNOT4Min) > 0 Then

                                    '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                                    If _FTShift <> "" Then

                                        _SPDateType = 0

                                        _Holiday = ""

                                        _Qry = "SELECT TOP 1 'H' AS FTHoliday "
                                        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK)"
                                        _Qry &= vbCrLf & "WHERE FDHolidayDate ='" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "' "
                                        _Holiday = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                                        If _Holiday <> "" Then _SPDateType = 2

                                        Dim _StateLeaveOther As Boolean = False
                                        Dim _StateLeavacation As Boolean = False


                                        Dim _StateFTStaMaternityleaveNotpay As Boolean = False
                                        Dim _SumLeave As Integer = 0

                                        For Each sR As DataRow In _dtLeave.Select("FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "'")
                                            _SumLeave = _SumLeave + Val(sR!FNTotalMinute)

                                            If Val(sR!LeaveType) = 1 Then
                                                _StateLeavacation = True
                                            Else
                                                _StateLeaveOther = True
                                            End If

                                            If Val(sR!LeaveType) = 2 Then
                                                _StateFTStaMaternityleaveNotpay = True
                                            End If

                                        Next

                                        For Each RFin As DataRow In _dtAddOtherAmt.Select("FTCalType = '0' AND FTFinType = '1'  AND FTPayType = '1'")
                                            Dim _StatePass As Boolean = True

                                            If _OutOT <> "" Then
                                                Beep()
                                            End If

                                            If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                            If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                            If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                            If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                            If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                            If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)
                                            If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                                _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                            End If

                                            If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                            If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                            If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                                Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                                Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                                If _STime.Length = _ETime.Length Then
                                                    If IsDate(_STime) And IsDate(_ETime) Then
                                                        If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                            _StatePass = False
                                                        End If
                                                    Else
                                                        _StatePass = False
                                                    End If
                                                Else
                                                    _StatePass = False
                                                End If

                                            End If

                                            If _StatePass Then
                                                _FCAdd = _FCAdd + Val(RFin!FCFinAmt.ToString)

                                                If RFin!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + Val(RFin!FCFinAmt.ToString)
                                                If RFin!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + Val(RFin!FCFinAmt.ToString)

                                                If _DtFin.Select("FTFinCode = '" & RFin!FTFinCode.ToString & "'").Length <= 0 Then
                                                    _DtFin.Rows.Add(RFin!FTFinCode.ToString, Val(RFin!FCFinAmt.ToString)) '...ถ้ายังไม่มีรายการ FinCode นี้ให้ทำการเพิ่มรายการนี้ใน DataTable
                                                Else
                                                    For Each xRow As DataRow In _DtFin.Select("FTFinCode = '" & RFin!FTFinCode.ToString & "'")
                                                        xRow!FCTotalFinAmt = Val(xRow!FCTotalFinAmt) + Val(RFin!FCFinAmt.ToString) '...ถ้ามีรายการ FinCode นี้แล้วให้ทำาการ update จำนวน amount ของรายการ FinCode นี้
                                                    Next

                                                End If

                                            End If
                                        Next
                                    End If
                                    '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------
                                End If
                            Next

                            _BFSDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_BFSDate, 1))

                        Loop

                    End If

                End If
                ' End If
                '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                '-------- รายได้อื่นๆจ่ายงวดกลางเดือน--------------------
                If _FTEmpState <> "2" And Val(_PayTerm) Mod 2 = 1 Then
                    If _dtAddOtherAmt.Select("FTCalType = '0' AND FTFinType = '1' AND FTPayType = '2'").Length > 0 Then

                        Dim _BFSDate As String = ""
                        Dim _BFEDate As String = ""

                        _BFSDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", -1))  'วันแรกของเดือน
                        _BFEDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Left(_BFSDate, 8) & "01", 1), -1)) 'วันสุดท้ายของเดือน

                        If _BFSDate <> "" And _BFEDate <> "" Then

                            _Qry = "SELECT  ISNULL(T.FNHSysShiftID, 0) AS FTShift, (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) +ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTime"
                            _Qry &= vbCrLf & ", ISNULL(T.FNNotRegis, 0) AS FNNotRegis, ISNULL(FNOT1,0) AS FNOT1"
                            _Qry &= vbCrLf & ", ISNULL(FNOT1_5, 0) AS FNOT1_5"
                            _Qry &= vbCrLf & ", ISNULL(FNOT2, 0) AS FNOT2, ISNULL(FNOT3, 0) AS FNOT3, ISNULL(FNOT4, 0) AS FNOT4"
                            _Qry &= vbCrLf & ", ISNULL(FNLateNormalMin, 0) AS FNLateNormalMin, ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0) AS FNLateNormalCut"
                            _Qry &= vbCrLf & ", ISNULL(FNLateOtMin, 0) AS FNLateOtMin, ISNULL(FNLateOtCut, 0) AS FNLateOtCut"
                            _Qry &= vbCrLf & ", ISNULL(FNLateMMin, 0) AS FNLateMorning"
                            _Qry &= vbCrLf & ", ISNULL(FNLateAfMin, 0) AS FNLateAfternoon, ISNULL(FNAbsentCut, 0) AS FNAbsentCut"
                            _Qry &= vbCrLf & ", ISNULL(FNAbsent, 0) AS FNAbsent"
                            _Qry &= vbCrLf & ", (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) + ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTimeMin"
                            _Qry &= vbCrLf & ", ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) AS FNTimeMinOrg"
                            _Qry &= vbCrLf & ", ISNULL(FNOT1Min, 0) AS FNOT1Min"
                            _Qry &= vbCrLf & ", ISNULL(FNOT1_5Min, 0) AS FNOT1_5Min"
                            _Qry &= vbCrLf & ", ISNULL(FNOT2Min, 0) AS FNOT2Min"
                            _Qry &= vbCrLf & ", ISNULL(FNOT3Min, 0) AS FNOT3Min, ISNULL(FNOT4Min,0) AS FNOT4Min "
                            _Qry &= vbCrLf & ", ISNULL(FNLateMMin, 0) AS FNLateMMin "
                            _Qry &= vbCrLf & ", ISNULL(FNLateAfMin, 0) AS FNLateAfMin"
                            _Qry &= vbCrLf & ", ISNULL(FNRetireMMin, 0) AS FNRetireMMin"
                            _Qry &= vbCrLf & ", ISNULL(FNRetireAfMin, 0) AS FNRetireAfMin"
                            _Qry &= vbCrLf & ", ISNULL(FNRetireNormalCut, 0) AS FNRetireNormalCut"
                            _Qry &= vbCrLf & ", ISNULL(FNRetireOtMin, 0) AS FNRetireOtMin"
                            _Qry &= vbCrLf & ", ISNULL(FNRetireOtCut, 0) AS FNRetireOtCut,FTDateTrans"
                            _Qry &= vbCrLf & ", ISNULL(T.FTIn1, '') AS FTIn1"
                            _Qry &= vbCrLf & ", ISNULL(T.FTOut1, '') AS FTOut1"
                            _Qry &= vbCrLf & ", ISNULL(T.FTIn2,'') AS FTIn2"
                            _Qry &= vbCrLf & ", ISNULL(T.FTOut2,'') AS FTOut2"
                            _Qry &= vbCrLf & ", ISNULL(T.FTIn3,'') AS FTIn3"
                            _Qry &= vbCrLf & ", ISNULL(T.FTOut3,'') AS FTOut3"
                            _Qry &= vbCrLf & ", P.FTOverClock,P.FTWeekDay"
                            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) LEFT OUTER JOIN  THRMTimeShift AS P WITH(NOLOCK) ON T.FNHSysShiftID = P.FNHSysShiftID"
                            _Qry &= vbCrLf & "WHERE (T.FNHSysEmpID =" & Val(_EmpCode) & " )"
                            _Qry &= vbCrLf & " 	    AND T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "' "
                            _Qry &= vbCrLf & " 	    AND T.FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_BFEDate) & "' "

                            _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                            '...4
                            Do While _BFSDate <= _BFEDate
                                _FTShift = ""
                                Dim _InOT As String = "" : Dim _OutOT As String = "" : Dim _Over As String = ""
                                Dim _R() As DataRow = _dttran.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "'")
                                For Each R2 In _R

                                    _FTShift = R2!FTShift.ToString
                                    _FNTime = IIf(Val(R2!FNTime.ToString) < 0, 0, Val(R2!FNTime.ToString))
                                    _FNTimeMin = IIf(Val(R2!FNTimeMin.ToString) < 0, 0, Val(R2!FNTimeMin.ToString))
                                    _FNNotRegis = Val(R2!FNNotRegis.ToString)
                                    _FNOT1 = Val(R2!FNOT1.ToString) : _FNOT1_5 = Val(R2!FNOT1_5.ToString) : _FNOT2 = Val(R2!FNOT2.ToString)
                                    _FNOT3 = Val(R2!FNOT3.ToString) : _FNOT4 = Val(R2!FNOT3.ToString)
                                    _FNLateNormalMin = Val(R2!FNLateNormalMin.ToString) : _FNLateNormalCut = Val(R2!FNLateNormalCut.ToString)
                                    _FNLateOtMin = Val(R2!FNLateOtMin.ToString) : _FNLateOtCut = Val(R2!FNLateOtCut.ToString)
                                    _FNLateMorning = Val(R2!FNLateMorning.ToString) : _FNLateAfternoon = (Val(R2!FNLateAfternoon.ToString))
                                    _LateCutAbsent = Val(R2!FNAbsentCut.ToString) : _FNAbsent = Val(R2!FNAbsent.ToString)
                                    _FNOT1Min = Val(R2!FNOT1Min.ToString)
                                    _FNOT1_5Min = Val(R2!FNOT1_5Min.ToString) : _FNOT2Min = Val(R2!FNOT2Min.ToString)
                                    _FNOT3Min = Val(R2!FNOT3Min.ToString) : _FNOT4Min = Val(R2!FNOT4Min.ToString)
                                    _FNLateMMin = Val(R2!FNLateMMin.ToString) : _FNLateAfMin = Val(R2!FNLateAfMin.ToString)
                                    _FNRetireMMin = Val(R2!FNRetireMMin.ToString) : _FNRetireAfMin = Val(R2!FNRetireAfMin.ToString)
                                    _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString) : _FNRetireNormalCut = Val(R2!FNRetireNormalCut.ToString)
                                    _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString) : _FNRetireOtMin = Val(R2!FNRetireOtMin.ToString)
                                    _FNRetireOtCut = Val(R2!FNRetireOtCut.ToString)
                                    _InOT = R2!FTIn3.ToString
                                    _OutOT = R2!FTOut3.ToString
                                    _Over = R2!FTOverClock.ToString

                                    If _FTShift <> "" And (_FNTimeMin + _FNOT1_5Min + _FNOT3Min + _FNOT1Min + _FNOT2Min + _FNOT4Min) > 0 Then

                                        '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                                        If _FTShift <> "" Then

                                            _SPDateType = 0

                                            _Holiday = ""

                                            _Qry = "SELECT TOP 1 'H' AS FTHoliday "
                                            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK)"
                                            _Qry &= vbCrLf & "WHERE  FDHolidayDate ='" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "' "
                                            _Holiday = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                                            If _Holiday <> "" Then _SPDateType = 2

                                            Dim _StateLeaveOther As Boolean = False
                                            Dim _StateLeavacation As Boolean = False


                                            Dim _StateFTStaMaternityleaveNotpay As Boolean = False
                                            Dim _SumLeave As Integer = 0

                                            For Each sR As DataRow In _dtLeave.Select("FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_BFSDate) & "'")
                                                _SumLeave = _SumLeave + Val(sR!FNTotalMinute)

                                                If Val(sR!LeaveType) = 1 Then
                                                    _StateLeavacation = True
                                                Else
                                                    _StateLeaveOther = True
                                                End If

                                                If Val(sR!LeaveType) = 2 Then
                                                    _StateFTStaMaternityleaveNotpay = True
                                                End If

                                            Next

                                            For Each RFin As DataRow In _dtAddOtherAmt.Select("FTCalType = '0' AND FTFinType = '1'  AND FTPayType = '2'")
                                                Dim _StatePass As Boolean = True

                                                If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                                If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                                If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                                If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                                If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                                If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)

                                                If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                                    _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                                End If

                                                If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                                If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                                If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                                    Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                                    Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                                    If _STime.Length = _ETime.Length Then
                                                        If IsDate(_STime) And IsDate(_ETime) Then
                                                            If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                                _StatePass = False
                                                            End If
                                                        Else
                                                            _StatePass = False
                                                        End If
                                                    Else
                                                        _StatePass = False
                                                    End If

                                                End If

                                                If _StatePass Then
                                                    _FCAdd = _FCAdd + Val(RFin!FCFinAmt.ToString)

                                                    If RFin!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + Val(RFin!FCFinAmt.ToString)
                                                    If RFin!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + Val(RFin!FCFinAmt.ToString)

                                                    If _DtFin.Select("FTFinCode='" & RFin!FTFinCode.ToString & "'").Length <= 0 Then
                                                        _DtFin.Rows.Add(RFin!FTFinCode.ToString, Val(RFin!FCFinAmt.ToString))
                                                    Else
                                                        For Each xRow As DataRow In _DtFin.Select("FTFinCode='" & RFin!FTFinCode.ToString & "'")
                                                            xRow!FCTotalFinAmt = Val(xRow!FCTotalFinAmt) + Val(RFin!FCFinAmt.ToString)
                                                        Next
                                                    End If

                                                End If
                                            Next

                                        End If
                                        '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                                    End If

                                Next

                                _BFSDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_BFSDate, 1))

                            Loop

                        End If

                    End If

                End If
                '-------------------------------------------------------------------------------------------


                For Each R2 As DataRow In _dtAddOtherAmt.Select("FTCalType <> '0' AND FTFinType = '1' AND FTPayType = '1'")
                    Dim _StatePass As Boolean = True

                    If R2!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_GFNLateNormalMin <= 0)
                    If R2!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_GFNAbsent <= 0)
                    If R2!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveOther <= 0)
                    If R2!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveVacation <= 0)

                    If _StatePass Then

                        _FCAdd = _FCAdd + Val(R2!FCFinAmt.ToString)

                        If R2!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + Val(R2!FCFinAmt.ToString)
                        If R2!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + Val(R2!FCFinAmt.ToString)

                        If _DtFin.Select("FTFinCode='" & R2!FTFinCode.ToString & "'").Length <= 0 Then
                            _DtFin.Rows.Add(R2!FTFinCode.ToString, Val(R2!FCFinAmt.ToString))
                        Else

                            For Each xRow As DataRow In _DtFin.Select("FTFinCode='" & R2!FTFinCode.ToString & "'")
                                xRow!FCTotalFinAmt = Val(xRow!FCTotalFinAmt) + Val(R2!FCFinAmt.ToString)
                            Next

                        End If

                    End If

                Next


                For Each R2 As DataRow In _dtAddOtherAmt.Select("FTCalType <> '0' AND FTFinType = '1' AND FTPayType = '0'")
                    Dim _StatePass As Boolean = True

                    If R2!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_GFNLateNormalMin <= 0)
                    If R2!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_GFNAbsent <= 0)
                    If R2!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveOther <= 0)
                    If R2!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveVacation <= 0)

                    If _StatePass Then
                        _FCAdd = _FCAdd + Val(R2!FCFinAmt.ToString)

                        If R2!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + Val(R2!FCFinAmt.ToString)
                        If R2!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + Val(R2!FCFinAmt.ToString)

                        If _DtFin.Select("FTFinCode = '" & R2!FTFinCode.ToString & "'").Length <= 0 Then
                            '...Vietnam Factory
                            REM 2014/10/30 _DtFin.Rows.Add(R2!FTFinCode.ToString, Val(R2!FCFinAmt.ToString))
                            If R2!FTFinCode.ToString = "013" Or R2!FTFinCode.ToString = "018" Then
                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox("Finance Code : " & R2!FTFinCode.ToString() & " " & IIf(R2!FTFinCode.ToString = "013", "Competency Allowance", "Quality Allowance"), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Finance Code XXX")
                                'End If

                                REM 2014/11/07 Vietnam factory
                                'Dim FNTotalMinuteLeave As Double = 0.0
                                'Dim FNLeaveOverMinute As Double = 1440.0 '...remark 480 minute like 1 days, so 1440 like 3 days.
                                'For Each oDataRow As System.Data.DataRow In _dtLeave.Rows
                                '    FNTotalMinuteLeave = FNTotalMinuteLeave + Val(oDataRow!FNTotalMinute.ToString)
                                'Next

                                'If FNTotalMinuteLeave > FNLeaveOverMinute Then
                                '    '...Nothing
                                'Else
                                '    If Calculate.HelperValidtePaymentIncomePayFullMonth(Integer.Parse(_EmpCode), _StartDate, _EndDate) = True Then
                                '        _DtFin.Rows.Add(R2!FTFinCode.ToString, Val(R2!FCFinAmt.ToString))
                                '    End If

                                'End If

                                If Calculate.HelperValidtePaymentIncomePayFullMonth(Integer.Parse(_EmpCode), _StartDate, _EndDate) = True Then
                                    Dim FNTotalMinuteLeave As Double = 0.0
                                    Dim FNLeaveOverMinute As Double = 1440.0 '...remark 480 minute like 1 days, so 1440 like 3 days.
                                    For Each oDataRow As System.Data.DataRow In _dtLeave.Rows
                                        FNTotalMinuteLeave = FNTotalMinuteLeave + Val(oDataRow!FNTotalMinute.ToString)
                                    Next

                                    If FNTotalMinuteLeave > FNLeaveOverMinute Then   '...ลางานต่อเดือนได้ไม่เกิน 3 วัน (1440 นาที)
                                        '...Nothing
                                    Else
                                        _DtFin.Rows.Add(R2!FTFinCode.ToString, Val(R2!FCFinAmt.ToString))
                                    End If

                                End If

                            Else
                                _DtFin.Rows.Add(R2!FTFinCode.ToString, Val(R2!FCFinAmt.ToString))
                            End If

                        Else

                            For Each xRow As DataRow In _DtFin.Select("FTFinCode = '" & R2!FTFinCode.ToString & "'")
                                xRow!FCTotalFinAmt = Val(xRow!FCTotalFinAmt) + Val(R2!FCFinAmt.ToString)
                            Next

                        End If

                    End If

                Next

                For Each R2 As DataRow In _dtAddOtherAmt.Select("FTFinType = '2'")
                    _FCDeduct = _FCDeduct + Val(R2!FCFinAmt.ToString)
                Next
                '---------รายได้รายหัก อื่นๆ-------------------------

                '------------------- สิ้นสุดการคำนวณรายวัน/Terminate Re-Calculate Month End Employee type daily
                _FTWorkmenAmtBefore = 0
                _FTTotalCalWorkmenBefore = 0

                _Qry = "SELECT  TOP 1 SUM(ISNULL(P.FNTotalRecalSSO,0)) AS FCSocial"
                _Qry &= vbCrLf & "         , SUM(ISNULL(P.FNSocial,0)) AS FCSocialAmt"
                _Qry &= vbCrLf & "FROM dbo.THRTPayRoll AS P WITH (NOLOCK), (SELECT FTPayYear, FTPayTerm, FNMonth FROM THRMCfgPayDT  WITH (NOLOCK) WHERE (FNHSysEmpTypeId = " & Val(_EmpType) & ") ) AS PD"
                _Qry &= vbCrLf & "WHERE P.FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                _Qry &= vbCrLf & "      AND ISNULL(P.FNTotalRecalSSO,0) > 0 "
                _Qry &= vbCrLf & "      AND P.FTEmpIdNo = '" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "' "
                _Qry &= vbCrLf & "      AND PD.FTPayTerm < '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                _Qry &= vbCrLf & "      AND P.FTPayYear = PD.FTPayYear"
                _Qry &= vbCrLf & "      AND P.FTPayTerm = PD.FTPayTerm"
                _Qry &= vbCrLf & "      AND PD.FNMonth IN (SELECT FNMonth"
                _Qry &= vbCrLf & "                         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH (NOLOCK)"
                _Qry &= vbCrLf & "                         WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ")"
                _Qry &= vbCrLf & "                                 AND  FTPayYear  = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                _Qry &= vbCrLf & "                                 AND FTPayTerm  = '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"
                _Qry &= vbCrLf & "                         )"

                Dim _DtSso As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                If _DtSso.Rows.Count > 0 Then
                    _SocialBefore = Val(_DtSso.Rows(0)!FCSocial.ToString)
                    _SocialBeforeAmt = Val(_DtSso.Rows(0)!FCSocialAmt.ToString)
                End If

                If _FTEmpState = "2" Or _FTEmpState = "3" Then

                    If FTStaDeductAbsent = 0 Then
                        _FNEmpBaht = _FNEmpBaht - (_LaNotpaid + _LateCutAmt + _LateCutAmtAbsent + _nBahtAbsent)
                    Else
                        _nBahtAbsent = 0
                        _FNEmpBaht = _FNEmpBaht - (_LaNotpaid + _LateCutAmt + _LateCutAmtAbsent)
                    End If


                    If _FNEmpBaht < 0 Then _FNEmpBaht = 0

                End If

                '_oHoliday = 0
                '_TotalHoliDay = 0
                '_HBaht = 0


                If _FDDateEnd = "" Then
                    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "'")
                        FNHolidaySpecial += +Double.Parse(IR!FNSpecialMoney.ToString)
                        '_oHoliday += 1
                        '_TotalHoliDay = _TotalHoliDay + 1
                    Next
                Else
                    For Each IR As DataRow In _DTHoliday.Select("FTPayTerm  = '" & (_PayTerm) & "' and (FDHolidayDate <='" & HI.UL.ULDate.ConvertEnDB(_FDDateEnd) & "' )")
                        FNHolidaySpecial += +Double.Parse(IR!FNSpecialMoney.ToString)
                        '_oHoliday += 1
                        ' _TotalHoliDay = _TotalHoliDay + 1
                    Next
                End If
                '    _HBaht += +CDbl(Format(_oHoliday * _FNSlaryPerDay, "0.00"))
                If FNHolidaySpecial > 0 Then
                    _DtFin.Rows.Add("028", FNHolidaySpecial)
                    _FCAdd = _FCAdd + FNHolidaySpecial
                End If

                For Each R2 As DataRow In _dtAddOtherAmt.Select("FTCalType <> '0' AND FTFinType = '1' AND FTPayType = '0' and FTFinCode = '028'")
                    Dim _StatePass As Boolean = True

                    If R2!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_GFNLateNormalMin <= 0)
                    If R2!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_GFNAbsent <= 0)
                    If R2!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveOther <= 0)
                    If R2!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = (_GFNLeaveVacation <= 0)
                    If _StatePass Then
                        If R2!FTStaTax.ToString = "1" Then _FTAddCalculateTax = _FTAddCalculateTax + FNHolidaySpecial
                        If R2!FTStaSocial.ToString = "1" Then _FTAddCalculateSocial = _FTAddCalculateSocial + FNHolidaySpecial
                    End If

                Next

                _TotalCalTax = 0 : _TaxAmt = 0

                _TotalCalSso = _FNEmpBaht + _HBaht + _FTOtherAddCalculateSocial + _FTAddCalculateSocial + _GtotalleavePayCalSsoAmt
                _TotalCalSso = _TotalCalSso + _FNNetAttandanceAmt + _FNHealtCareAmt + _FNTransportAmt + _FNNetChildCareAmt + _FNWorkAgeSalary
                _TotalCalTax = _FNEmpBaht + _HBaht + _Lapaid + _FCPayVacationBaht + (FNPayLeaveBusinessBaht + FNPayLeaveSickBaht + FNPayLeaveSpecialBaht + FNParturitionLeave) + _FNNetAttandanceAmt + _FNHealtCareAmt + _FNTransportAmt + _FNNetChildCareAmt + _FNWorkAgeSalary

                '-----------------หักเงินเข้า กองทุนสำรองเลี้ยงชีพ-------------------------------
                If _ContributedFundBeginPay Then
                    Dim _EMpWorkAge As Integer = Val(R!FNEmpWorkAge.ToString)

                    For Each sR As DataRow In _THRMContributedFund.Select("FNAgeBegin <= " & _EMpWorkAge & " AND FNAgeEnd >=" & _EMpWorkAge & " ")

                        FTTotalCalContributedAmt = _TotalCalSso

                        FTContributedAmt = CDbl(Format(((FTTotalCalContributedAmt * Val(sR!FNEmpAmtPer.ToString)) / 100.0), "0"))
                        FTCmpContributedAmt = CDbl(Format(((FTTotalCalContributedAmt * Val(sR!FNCmpAmtPer.ToString)) / 100.0), "0"))

                        Exit For

                    Next

                End If
                '-----------------หักเงินเข้า กองทุนสำรองเลี้ยงชีพ----------------------------
                '-----------------หักเงินเข้า กองทุนทดแทน---------------------------------
                FTTotalCalWorkmen = _TotalCalSso

                If _FTMaxCalWorkmen > 0 Then

                    _SocialPayMax = CDbl(Format(((_FTMaxCalWorkmen * _FTMaxWorkmenRate) / 100.0), "0"))

                    If (_TotalCalSso + _FTTotalCalWorkmenBefore) > _FTMaxCalWorkmen Then
                        FTTotalCalWorkmen = _FTMaxCalWorkmen
                    ElseIf FTTotalCalWorkmen > 0 Then
                        FTTotalCalWorkmen = FTTotalCalWorkmen
                    Else
                        FTTotalCalWorkmen = 0
                    End If

                    If _FTTotalCalWorkmenBefore > 0 Then
                        FTWorkmenAmt = CDbl(Format((((_CalSo + _FTTotalCalWorkmenBefore) * _FTMaxWorkmenRate) / 100.0), "0"))
                        FTWorkmenAmt = FTWorkmenAmt - _FTWorkmenAmtBefore
                    Else
                        FTWorkmenAmt = CDbl(Format(((FTTotalCalWorkmen * _FTMaxWorkmenRate) / 100.0), "0"))
                    End If
                End If

                '-----------------หักเงินเข้า กองทุนทดแทน-------------------------------

                '--------- คิดประกันสังคม-----------
                _SocialPayMax = HCfg.HMaxSocialBaht
                _CalSo = 0
                _FCSocial = 0
                _TotalCalSso = (_TotalCalSso * _FNExchangeRate) + _FNNetOTMealAmt
                _FNSocialBase = 0

                If _FTCalSocialSta <> "1" Then ' 1 ไม่่คิดประกันสังคม
                    '...คำนวณ ป.ก.ส.
                    If _FTEmpState = "2" Or (_FTEmpState <> "2" And _StartDate <= Left(_StartDate, 8) & "24" And _EndDate >= Left(_StartDate, 8) & "24") Then
                        For Each Rsso As DataRow In _tmpSocailRateKM.Select("FNSocialStartRange<=" & _TotalCalSso & " AND  FNSocialEndRange>=" & _TotalCalSso & "")

                            _FNSocialBase = Val(Rsso!FNSocialBase.ToString)
                            _FCSocial = Val(Rsso!FNSocialAmt.ToString)

                            Exit For
                        Next
                    Else
                        _FCSocial = 0
                    End If


                Else
                    _TotalCalSso = 0
                End If

                _FNEmpDiligent = 0
                _FTStateInDustin = ""
                _FNDeligentPeriod = 0
                _ScanCardOverClock = ""
                _ActualScanMIn = ""
                _DTTHRMTimeScanCard = LoadTimeShiftControl

                '_Qry = " 	SELECT     T.FTDateTrans,T.FNHSysShiftID AS FTShift"
                '_Qry &= vbCrLf & "  ,  T.FTIn1"
                '_Qry &= vbCrLf & "  ,   T.FTOut1"
                '_Qry &= vbCrLf & "  ,   T.FTIn2"
                '_Qry &= vbCrLf & "  ,  T.FTOut2"
                '_Qry &= vbCrLf & "  ,   T.FTIn3"
                '_Qry &= vbCrLf & "  , T.FTOut3"
                '_Qry &= vbCrLf & "  , FTIn4,T.FTOut4"
                '_Qry &= vbCrLf & "  ,M.FNUseBarcode AS FTUseBarcode,M.FNLateCutSta As FTLateCutCond,T.FTWeekDay,M.FNHSysEmpTypeId AS FTTypeEmp"
                '_Qry &= vbCrLf & ",T.FTScanAOTOut,T.FTScanAOTOut2"
                '_Qry &= vbCrLf & ",ISNULL(ET.FTStateAcceptTimeAuto,'0') AS FTStateAcceptTimeAuto"
                '_Qry &= vbCrLf & "  ,T.FTOtMIn"
                '_Qry &= vbCrLf & "  ,T.FTOtMOut"
                '_Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) INNER JOIN THRMEmployee AS  M  WITH(NOLOCK)"
                '_Qry &= vbCrLf & " 	ON T.FNHSysEmpID = M.FNHSysEmpID"
                '_Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
                '_Qry &= vbCrLf & "  WHERE (T.FNHSysEmpID =" & Val(_EmpCode) & " )"
                '_Qry &= vbCrLf & "   AND (T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "')"
                '_Qry &= vbCrLf & "   AND (T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "')"

                '_Qry &= vbCrLf & " ORDER BY T.FTDateTrans"

                '_dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                ''LoadOTPayOver
                'For Each Row As DataRow In _dttran.Rows
                '    '_StateWorkOffSite = 0
                '    '_StatePayOTOverRequest = False
                '    '_WeekDayBefore = LoadWeekday(HI.UL.ULDate.AddDay(Row!FTDateTrans.ToString, -1))
                '    '_WeekCallDay = LoadWeekday(Row!FTDateTrans.ToString)
                '    '_CalDate = Row!FTDateTrans.ToString
                '    _Shift = Row!FTShift.ToString
                '    '_FTStateAcceptTimeAuto = R!FTStateAcceptTimeAuto.ToString

                '    '_FNSpecialTimeMin = 0
                '    _ActualScanMIn = Row!FTIn1.ToString : _ActualScanMOut = Row!FTOut1.ToString
                '    _ActualScanAIn = Row!FTIn2.ToString : _ActualScanAOut = Row!FTOut2.ToString

                '    _ActualScanOTIn1 = Row!FTIn3.ToString
                '    _ActualScanOTOut1 = Row!FTOut3.ToString

                '    '_ActualScanOTIn12 = Row!FTIn4.ToString
                '    '_ActualScanOTOut12 = Row!FTOut4.ToString

                '    '_FTOtMIn = R!FTOtMIn.ToString
                '    '_FTOtMOut = R!FTOtMOut.ToString
                'Next
                'RR = _DTTHRMTimeScanCard.Select(" FNHSysShiftID = " & Val(_Shift) & " ")

                'If RR.Length > 0 Then
                '    For Each IR As DataRow In RR
                '        _nTimeCtrl = Val(IR!WorkTimePerDay.ToString)
                '        _ScanCardOverClock = IR!FTOverClock.ToString
                '        _CheckTimeMIn = IR!ChkIn1.ToString
                '        _CheckTimeMOut = IR!ChkOut1.ToString
                '        _CheckTimeAIn = IR!ChkIn2.ToString
                '        _CheckTimeAOut = IR!ChkOut2.ToString

                '        '_WorkTimePerDay = Val(IR!WorkTimePerDay.ToString)
                '        Exit For
                '    Next
                'End If

                _ScanTimeOverClock = Right(_ScanCardOverClock, 5)

                '_ActualScanMIn = IIf(_ActualScanMIn <> "", IIf(_ActualScanMIn >= "00:00" And _ActualScanMIn <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanMIn, "")

                If _CalIns <> "" Then
                    Dim Time_NotScan As Integer = 0, Time_Late As Integer = 0, Time_LateMore15 As Integer = 0, Time_Absent As Integer = 0, _WorkD As Integer = 0
                    Dim dtdl As DataTable

                    Dim _BFSDate As String = ""
                    Dim _BFEDate As String = ""

                    _Qry = " SELECT TOP 1 FDCalDateBegin, FDCalDateEnd"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH (NOLOCK)"
                    _Qry &= vbCrLf & "WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ")"
                    _Qry &= vbCrLf & "       AND FTPayYear = '" & HI.UL.ULF.rpQuoted(_PayYear) & "'"
                    _Qry &= vbCrLf & "       AND FTPayTerm = '" & HI.UL.ULF.rpQuoted(_PayTerm) & "'"

                    _dttemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each Row As DataRow In _dttemp.Rows
                        _BFSDate = Row!FDCalDateBegin.ToString
                        _BFEDate = Row!FDCalDateEnd.ToString
                    Next

                    'If _BFSDate <> "" And _BFEDate <> "" Then

                    _Qry = "SELECT  ISNULL(T.FNHSysShiftID, 0) AS FTShift, (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) + ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTime"
                    _Qry &= vbCrLf & ",FTDateTrans , ISNULL(FNLateMMin, 0) AS FNLateMMin "
                    _Qry &= vbCrLf & ", ISNULL(FNLateAfMin, 0) AS FNLateAfMin"
                    _Qry &= vbCrLf & ", ISNULL(FNRetireMMin, 0) AS FNRetireMMin"
                    _Qry &= vbCrLf & ", ISNULL(FNRetireAfMin, 0 ) AS FNRetireAfMin"
                    _Qry &= vbCrLf & ", ISNULL(FNAbsent, 0) AS FNAbsent "
                    _Qry &= vbCrLf & ", FNSpecialTime, FNSpecialTimeMin, ISNULL(FNTimeMin, 0)  + ISNULL(FNSpecialTimeMin, 0) AS TimeSpecial "
                    _Qry &= vbCrLf & ", ISNULL(T.FNNotRegis, 0) AS FNNotRegis, ISNULL(FNOT1, 0) AS FNOT1"
                    _Qry &= vbCrLf & ", ISNULL(FNOT1_5, 0) AS FNOT1_5"
                    _Qry &= vbCrLf & ", ISNULL(FNOT2, 0 ) AS FNOT2  , ISNULL(FNOT3, 0) AS FNOT3, ISNULL(FNOT4, 0) AS FNOT4"
                    _Qry &= vbCrLf & ", ISNULL(FNLateNormalMin, 0) AS FNLateNormalMin, ISNULL(FNLateNormalCut, 0 ) + ISNULL(FNAbsentCut, 0 ) AS FNLateNormalCut"
                    _Qry &= vbCrLf & ", ISNULL(FNLateOtMin, 0) AS FNLateOtMin,ISNULL(FNLateOtCut, 0) As FNLateOtCut"
                    _Qry &= vbCrLf & ", (ISNULL(FNTimeMin, 0) + ISNULL(FNSpecialTimeMin, 0) + ISNULL(FNLateNormalMin, 0)) - (ISNULL(FNLateNormalCut, 0) + ISNULL(FNAbsentCut, 0)) AS FNTimeMin"
                    _Qry &= vbCrLf & ", ISNULL(FNTimeMin, 0)  + ISNULL(FNSpecialTimeMin, 0) AS FNTimeMinOrg"
                    _Qry &= vbCrLf & ", ISNULL(FNOT1Min, 0) AS FNOT1Min"
                    _Qry &= vbCrLf & ", ISNULL(FNOT1_5Min, 0) AS FNOT1_5Min "
                    _Qry &= vbCrLf & ", ISNULL(FNOT2Min, 0) AS FNOT2Min "
                    _Qry &= vbCrLf & ", ISNULL(FNOT3Min, 0) As FNOT3Min, ISNULL(FNOT4Min,0) As FNOT4Min"
                    _Qry &= vbCrLf & ", ISNULL(FNRetireNormalCut, 0) AS FNRetireNormalCut"
                    _Qry &= vbCrLf & ", ISNULL(FNRetireOtMin, 0) AS FNRetireOtMin"
                    _Qry &= vbCrLf & ", ISNULL(FNRetireOtCut, 0) AS FNRetireOtCut"

                    _Qry &= vbCrLf & "  , OT.FTOtMIn AS FTCheckOTMIn"
                    _Qry &= vbCrLf & "  , OT.FTOtMOut AS FTCheckOTMOut"
                    _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
                    _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
                    _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
                    _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2"

                    '_Qry &= vbCrLf & ",CASE WHEN ISNULL(OT.FTOtMIn,'') <> '' THEN  (CASE WHEN (FTIn1 >='00:00' AND  FTIn1 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END)  + '  ' + FTIn1  ELSE '' END AS ChkIn1 "
                    '_Qry &= vbCrLf & ",CASE WHEN ISNULL(OT.FTOtMOut,'') <> '' THEN (CASE WHEN (FTOut1 >='00:00' AND  FTOut1 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTOut1 ELSE '' END AS ChkOut1 "
                    '_Qry &= vbCrLf & ",CASE WHEN ISNULL(OT.FTOtIn,'') <> '' THEN (CASE WHEN (OT.FTOtIn >='00:00' AND  OT.FTOtIn < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + OT.FTOtIn  ELSE '' END As ChkIn2  "
                    '_Qry &= vbCrLf & ",CASE WHEN ISNULL(FTOut2,'') <> '' THEN (CASE WHEN (FTOut2 >='00:00' AND  FTOut2 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTOut2  ELSE '' END AS ChkOut2 "

                    _Qry &= vbCrLf & ", ISNULL(T.FTIn1, '') AS FTIn1"
                    _Qry &= vbCrLf & ", ISNULL(T.FTOut1, '') AS FTOut1"
                    _Qry &= vbCrLf & ", ISNULL(T.FTIn2, '') AS FTIn2"
                    _Qry &= vbCrLf & ", ISNULL(T.FTOut2, '') AS FTOut2"
                    _Qry &= vbCrLf & ", ISNULL(T.FTIn3, '') AS FTIn3"
                    _Qry &= vbCrLf & ", ISNULL(T.FTOut3, '') AS FTOut3"
                    _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTIn3,'') <> '' THEN (CASE WHEN (FTIn3 >='00:00' AND  FTIn3 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTIn3  ELSE '' END As ChkIn3  "
                    _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTOut3,'') <> '' THEN (CASE WHEN (FTOut3 >='00:00' AND  FTOut3 < ISNULL(FTOverClock,'')  ) THEN  '" & _ActualNextDate & "' ELSE '" & _ActualDate & "'  END) + '  ' + FTOut3  ELSE '' END AS ChkOut3 "
                    _Qry &= vbCrLf & ",T.FNHSysTranStaId,TM.FTTranStaCode, P.FTOverClock, FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn"
                    _Qry &= vbCrLf & ", FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2,FTScanOtMInM,FTScanOtMOutM,FTScanOtMIn,FTScanOtMOut"
                    _Qry &= vbCrLf & " ,T.FNHSysTranStaId,TM.FTTranStaCode"
                    _Qry &= vbCrLf & " ,EM.FDDateStart"
                    _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FTLeaveType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)  WHERE FNHSysEmpID=T.FNHSysEmpID AND FTDateTrans=T.FTDateTrans   ),'') AS FTLeaveCode "
                    _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) LEFT OUTER JOIN  THRMTimeShift AS P WITH(NOLOCK) ON T.FNHSysShiftID = P.FNHSysShiftID"
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS EM ON T.FNHSysEmpID = EM.FNHSysEmpID "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
                    _Qry &= vbCrLf & "WHERE (T.FNHSysEmpID =" & Val(_EmpCode) & " )"
                    _Qry &= vbCrLf & " 	    AND T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "' "
                    _Qry &= vbCrLf & " 	    AND T.FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "' "

                    _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                    'End If

                    For Each Row As DataRow In _dttran.Rows
                        Dim _Date As String = Row!FTDateTrans.ToString
                        Dim _WorkStartDate As String = Row!FDDateStart.ToString
                        Dim LateM As Integer = 0, LateAf As Integer = 0, RetireM As Integer = 0, RetireAf As Integer = 0
                        Dim LeaveCode As String = Row!FTLeaveCode.ToString

                        If (Row!FNTime > 0) Then
                            _WorkD = _WorkD + 1
                        End If

                        LateM = Row!FNLateMMin
                        LateAf = Row!FNLateAfMin

                        If (Row!TimeSpecial >= 480) Then
                            RetireM = 0
                            RetireAf = 0
                        Else
                            RetireM = Row!FNRetireMMin
                            RetireAf = Row!FNRetireAfMin
                        End If


                        'If (_WorkStartDate > _EndDate) Then
                        '    MessageBox.Show(_WorkStartDate & " < " & _EndDate)
                        'Else
                        '    MessageBox.Show("false")
                        'End If

                        _Shift = Row!FTShift.ToString
                        '_FTStateAcceptTimeAuto = R!FTStateAcceptTimeAuto.ToString

                        '_FNSpecialTimeMin = 0
                        _ActualScanMIn = Row!FTIn1.ToString : _ActualScanMOut = Row!FTOut1.ToString
                        _ActualScanAIn = Row!FTIn2.ToString : _ActualScanAOut = Row!FTOut2.ToString

                        _ActualScanOTIn1 = Row!FTIn3.ToString
                        _ActualScanOTOut1 = Row!FTOut3.ToString

                        '_ActualScanOTIn12 = Row!FTIn4.ToString
                        '_ActualScanOTOut12 = Row!FTOut4.ToString

                        RR = _DTTHRMTimeScanCard.Select(" FNHSysShiftID = " & Val(_Shift) & " ")

                        'If RR.Length > 0 Then
                        '    For Each IR As DataRow In RR
                        '        _nTimeCtrl = Val(IR!WorkTimePerDay.ToString)
                        '        _ScanCardOverClock = IR!FTOverClock.ToString
                        '        _CheckTimeMIn = IR!ChkIn1.ToString
                        '        _CheckTimeMOut = IR!ChkOut1.ToString
                        '        _CheckTimeAIn = IR!ChkIn2.ToString
                        '        _CheckTimeAOut = IR!ChkOut2.ToString

                        '        '_WorkTimePerDay = Val(IR!WorkTimePerDay.ToString)
                        '        Exit For
                        '    Next
                        'End If

                        'If (Row!ChkIn3.ToString <> "") Then
                        '    _CheckTimeOTIn1 = Row!FTCheckOTAIn1.ToString
                        '    _CheckTimeOTOut1 = Row!FTCheckOTAOut1.ToString
                        'End If

                        _ScanTimeOverClock = Right(_ScanCardOverClock, 5)

                        _ActualScanMIn = IIf(_ActualScanMIn <> "", IIf(_ActualScanMIn >= "00:00" And _ActualScanMIn <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanMIn, "")
                        '_ActualScanMOut = IIf(_ActualScanMOut <> "", IIf(_ActualScanMOut >= "00:00" And _ActualScanMOut <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanMOut, "")
                        '_ActualScanAIn = IIf(_ActualScanAIn <> "", IIf(_ActualScanAIn >= "00:00" And _ActualScanAIn <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanAIn, "")
                        '_ActualScanAOut = IIf(_ActualScanAOut <> "", IIf(_ActualScanAOut >= "00:00" And _ActualScanAOut <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanAOut, "")
                        '_ActualScanOTIn1 = IIf(_ActualScanOTIn1 <> "", IIf(_ActualScanOTIn1 >= "00:00" And _ActualScanOTIn1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTIn1, "")
                        '_ActualScanOTOut1 = IIf(_ActualScanOTOut1 <> "", IIf(_ActualScanOTOut1 >= "00:00" And _ActualScanOTOut1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTOut1, "")
                        '_ActualScanOTIn12 = IIf(_ActualScanOTIn12 <> "", IIf(_ActualScanOTIn12 >= "00:00" And _ActualScanOTIn12 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTIn12, "")
                        '_ActualScanOTOut12 = IIf(_ActualScanOTOut12 <> "", IIf(_ActualScanOTOut12 >= "00:00" And _ActualScanOTOut12 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _ActualScanOTOut12, "")

                        '_CheckTimeOTIn1 = IIf(_CheckTimeOTIn1 <> "", IIf(_CheckTimeOTIn1 >= "00:00" And _CheckTimeOTIn1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _CheckTimeOTIn1, "")
                        '_CheckTimeOTOut1 = IIf(_CheckTimeOTOut1 <> "", IIf(_CheckTimeOTOut1 >= "00:00" And _CheckTimeOTOut1 <= _ScanTimeOverClock, _ActualNextDate, _ActualDate) & "  " & _CheckTimeOTOut1, "")

                        'If (_ActualScanMIn <> "") Then
                        '_FNLateMMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeMIn), CDate(_ActualScanMIn))
                        '_FNLateAfMin = DateDiff(DateInterval.Minute, CDate(_CheckTimeAIn), CDate(_ActualScanAIn))

                        '_FNRetireMMin = DateDiff(DateInterval.Minute, CDate(_ActualScanMOut), CDate(_CheckTimeMOut))
                        '_FNRetireAfMin = DateDiff(DateInterval.Minute, CDate(_ActualScanAOut), CDate(_CheckTimeAOut))

                        'If _ActualScanOTIn1 <> "" And _CheckTimeOTIn1 <> "" And _ActualScanOTIn1 > _CheckTimeOTIn1 Then
                        '    _LateOT = DateDiff(DateInterval.Minute, CDate(_CheckTimeOTIn1), CDate(_ActualScanOTIn1))
                        '    _RetryOT = DateDiff(DateInterval.Minute, CDate(_ActualScanOTOut1), CDate(_CheckTimeOTOut1))
                        'Else
                        '    _LateOT = 0
                        '    _RetryOT = 0
                        'End If

                        'check reason for edit time
                        Select Case Row!FTTranStaCode.ToString
                            Case "NS001"
                                Time_NotScan = Time_NotScan + 1
                            Case "NS002"
                                Time_NotScan = Time_NotScan + 2
                            Case "NS003"
                                Time_NotScan = Time_NotScan + 3
                            Case "NS004"
                                Time_NotScan = Time_NotScan + 4
                            Case "NS005"
                                Time_NotScan = Time_NotScan + 5
                            Case "NS006"
                                Time_NotScan = Time_NotScan + 6
                        End Select

                        'check late morning, afternoon, ot
                        If (LateM > 0) And (LateM <= 15) Then
                            Time_Late = Time_Late + 1
                        ElseIf (LateM > 15) Then
                            Time_LateMore15 = Time_LateMore15 + 1
                        End If

                        If (LateAf > 0) And (LateAf <= 15) Then
                            Time_Late = Time_Late + 1
                        ElseIf (LateAf > 15) Then
                            Time_LateMore15 = Time_LateMore15 + 1
                        End If

                        'If (_LateOT > 0) And (_LateOT <= 15) Then
                        '    Time_Late = Time_Late + 1
                        'ElseIf (_LateOT > 15) Then
                        '    Time_LateMore15 = Time_LateMore15 + 1
                        'End If
                        'End If

                        If (RetireM > 0) And (RetireM <= 15) Then
                            Time_Late = Time_Late + 1
                        ElseIf (RetireM > 15) Then
                            Time_LateMore15 = Time_LateMore15 + 1
                        End If

                        If (RetireAf > 0) And (RetireAf <= 15) Then
                            Time_Late = Time_Late + 1
                        ElseIf (RetireAf > 15) Then
                            Time_LateMore15 = Time_LateMore15 + 1
                        End If

                        'If (_RetryOT > 0) And (_RetryOT <= 15) Then
                        '    Time_Late = Time_Late + 1
                        'ElseIf (_RetryOT > 15) Then
                        '    Time_LateMore15 = Time_LateMore15 + 1
                        'End If
                        'End If

                        'check absent
                        If ((Row!FNAbsent > 0)) Or (LeaveCode = "1") Or (LeaveCode = "0") Then
                            If (_Date > _WorkStartDate) Then
                                Time_Absent = Time_Absent + 1
                            End If
                        End If
                    Next

                    Dim _NotScanCut As Double = 0, _LateCut As Double = 0, _LateMore15Cut As Double = 0, _AbsentCut As Double = 0
                    Dim _FNDili As Integer = 0, _FNDiliPe As Integer = 0
                    Dim _head As DataTable

                    _PayRate = 0

                    _Qry = "SELECT  FNDeligent, FNDeligentPeriod, FNPayDeligent, FTResetOpt, FTStateRightNow, FTAbsentOpt, FTLateOpt, FTLeaveOpt, FTVacationOpt, FTStateScanCard, FTResetNewYearOpt, FNStartDiligent, FNWageRate"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD AS HD WITH(NOLOCK) "
                    _Qry &= vbCrLf & "WHERE HD.FTDeligentCode ='" & _CalIns & "'"

                    _head = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                    For Each _HRow As DataRow In _head.Rows
                        _FNDili = _HRow!FNDeligent
                        _PayRate = _HRow!FNStartDiligent
                    Next

                    Dim _StateCalIns As Boolean = False
                    _Qry = "SELECT TOP 1 FNPayDeligent FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(_CalIns) & "' "
                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "1" Then
                        If Val(_PayTerm) Mod 2 = 1 Then
                            _FTSatrtCalculateDateIns = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", -1))  'วันแรกของเดือน
                            _FTEndCalculateDateIns = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddMonth(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", 1), -1), -1)) 'วันแของเดือน
                            _StateCalIns = True
                        End If
                    Else

                        _StateCalIns = True
                    End If

                    If _StateCalIns Then
                        _Qry = " SELECT   ISNULL(PayIndus,0) As PayIndus , ISNULL(StateIndus,'') AS StateIndus,ISnuLL(FNDeligentPeriod,0) AS FNDeligentPeriod"
                        _Qry &= vbCrLf & "	FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_CALCULATE_INDUST(" & Val(_EmpCode) & ",'" & HI.UL.ULDate.ConvertEnDB(_FTSatrtCalculateDateIns) & "','" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDateIns) & "','" & HI.UL.ULF.rpQuoted(_CalIns) & "')"

                        Dim _DtIns As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                        If _DtIns.Rows.Count > 0 Then
                            _FNEmpDiligent = Val(_DtIns.Rows(0)!PayIndus.ToString)
                            _FTStateInDustin = _DtIns.Rows(0)!StateIndus.ToString
                            _FNDeligentPeriod = Val(_DtIns.Rows(0)!FNDeligentPeriod.ToString)
                        End If
                    End If

                    Dim _PayPerDay As Double = 0

                    _Qry = "SELECT  FTPayTerm, FTPayYear, FNHSysEmpTypeId, FNMonth, FDCalDateBegin, FDCalDateEnd, FDPayDate, FNWorkDay"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH(NOLOCK) "
                    _Qry &= vbCrLf & "WHERE FTPayYear ='" & _PayYear & "' AND FTPayTerm = '" & _PayTerm & "' AND FNHSysEmpTypeId =" & _EmpType & ""

                    dtdl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each _Row As DataRow In dtdl.Rows
                        _PayPerDay = _PayRate / _Row!FNWorkDay
                    Next

                    '_PayRate = _PayPerDay * _WorkD

                    'check condition
                    _Qry = "SELECT  FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct"
                    _Qry &= vbCrLf & " WHERE FTDeligentCode ='" & _CalIns & "' AND FNDeligenDeductType= 1"

                    dtdl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each _Row As DataRow In dtdl.Rows
                        'calculate cut diligent
                        If (Time_NotScan >= _Row!FNStartMinute) And (Time_NotScan <= _Row!FNToMinute) Then
                            If (_FNDili = 0) Then
                                _NotScanCut = _PayRate * _Row!FNDeductPer / 100
                            Else
                                _NotScanCut = _FNEmpDiligent * _Row!FNDeductPer / 100
                            End If
                        End If
                    Next

                    _Qry = "SELECT  FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct"
                    _Qry &= vbCrLf & " WHERE FTDeligentCode ='" & _CalIns & "' AND FNDeligenDeductType= 2"

                    dtdl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each _Row As DataRow In dtdl.Rows
                        'calculate cut diligent
                        If (Time_Late >= _Row!FNStartMinute) And (Time_Late <= _Row!FNToMinute) Then
                            If (_FNDili = 0) Then
                                _LateCut = _PayRate * _Row!FNDeductPer / 100
                            Else
                                _LateCut = _FNEmpDiligent * _Row!FNDeductPer / 100
                            End If
                        End If
                    Next

                    _Qry = "SELECT  FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct"
                    _Qry &= vbCrLf & " WHERE FTDeligentCode ='" & _CalIns & "' AND FNDeligenDeductType= 3"

                    dtdl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each _Row As DataRow In dtdl.Rows
                        'calculate cut diligent
                        If (Time_LateMore15 >= _Row!FNStartMinute) And (Time_LateMore15 <= _Row!FNToMinute) Then
                            If (_FNDili = 0) Then
                                _LateMore15Cut = _PayRate * _Row!FNDeductPer / 100
                            Else
                                _LateMore15Cut = _FNEmpDiligent * _Row!FNDeductPer / 100
                            End If
                        End If
                    Next

                    _Qry = "SELECT  FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct"
                    _Qry &= vbCrLf & " WHERE FTDeligentCode ='" & _CalIns & "' AND FNDeligenDeductType= 4"

                    dtdl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each _Row As DataRow In dtdl.Rows
                        'calculate cut diligent
                        If (Time_Absent >= _Row!FNStartMinute) And (Time_Absent <= _Row!FNToMinute) Then
                            If (_FNDili = 0) Then
                                _AbsentCut = _PayRate * _Row!FNDeductPer / 100
                            Else
                                _AbsentCut = _FNEmpDiligent * _Row!FNDeductPer / 100
                            End If
                        End If
                    Next

                    'calculate pay rate
                    If (_FNDili = 0) Then
                        _PayRate = _PayRate - _NotScanCut - _LateCut - _LateMore15Cut - _AbsentCut
                        _FNEmpDiligent = _PayRate
                    Else
                        _FNEmpDiligent = _FNEmpDiligent - _NotScanCut - _LateCut - _LateMore15Cut - _AbsentCut
                        _PayRate = _FNEmpDiligent
                        '_PayRate = _PayRate - _NotScanCut - _LateCut - _LateMore15Cut - _AbsentCut
                    End If

                    'If (_PayRate > 0) Then

                    '    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt,FCTotalFinAmt)"
                    '    _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '015'," & _PayRate & "," & _PayRate & ""

                    '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                    '    'End If
                    'End If
                End If

                _FTAddCalculateTax = _FTAddCalculateTax + _ShiftAmt + _ShiftOTAmt
                _FCAdd = _FCAdd + _ShiftAmt + _ShiftOTAmt + _PayRate


                If _FTCalTaxSta <> "1" Then
                    '...คำนวณภาษี
                    _EmpDisTax.BeforeIncom = 0
                    _EmpDisTax.BeforeTax = 0
                    With _EmpDisTax
                        .FTSosial = 0 '_FCAccumulateSocial + _FCSocial + (_FCSocial * (_TotalInstalment - _Instalment))

                        'If .FTSosial > (((_SocialMaxIncome * _SocialRate) / 100.0) * 12) Then
                        '    .FTSosial = CDbl(Format((((_SocialMaxIncome * _SocialRate) / 100.0) * 12), "0.00"))
                        'End If

                        .BaseSlary = CDbl(Format((_TotalCalTax * _FNExchangeRate), "0.00")) '(_TotalCalTax * (_TotalInstalment - _Instalment)) + _TotalCalTax
                        .OtherSlary = _FTOtherAddCalculateTax + _FTAddCalculateTax + _PayRate + _nBahtOt1 + _nBahtOt15 + _nBahtOt2 + _nBahtOt3 + _nBahtOt4 + _FNNetAttandanceAmt + _FNHealtCareAmt + _FNTransportAmt + _FNNetChildCareAmt + _FNWorkAgeSalary  '_FNEmpDiligent
                        .OtherSlary = Format(.OtherSlary * _FNExchangeRate, "0.00") + _FNNetOTMealAmt

                        .Cfg_ContributedDeducToTheFund = .Cfg_ContributedDeducToTheFund + FTContributedAmt + (FTContributedAmt * (_TotalInstalment - _Instalment))

                    End With

                    _TotalCalTax = _EmpDisTax.BaseSlary + _EmpDisTax.OtherSlary

                    Dim _TaxOther As Double = _EmpDisTax.OtherSlary
                    Dim _TaxOtherAmt As Double = 0

                    Dim _Total As Double = GETnRecalDiscTax(_EmpDisTax, _EmpTaxYear)

                    _EmpTaxYear.FTSocial = 0 '_EmpDisTax.FTSosial '   _FCAccumulateSocial + _FCSocial  'ประกันสังคม

                    _EmpTaxYear.FTTotalCalTax = _Total


                    Dim _TotalTax As Double = GETnTax(_Total, _TaxOther, _TaxOtherAmt)

                    _EmpTaxYear.FTTotalTax = _TotalTax 'ภาษีที่ต้องจ่าย

                    _TotalTax = CDbl(Format(_TotalTax - _EmpDisTax.BeforeTax, "0.00"))

                    If _TotalTax > 0 Then
                        _TaxAmt = CDbl(Format((_TotalTax / ((_TotalInstalment - _Instalment) + 1)), "0.00"))
                        _TaxAmt = _TaxAmt + _TaxOtherAmt
                    Else
                        _TaxAmt = 0
                    End If

                    _EmpTaxYear.FTTotalTaxPay = _FCAccumulateTax + _TaxAmt

                Else
                    _TotalCalTax = 0
                    _TaxAmt = 0
                End If

                '========================================================Re-Calculate Basic Salaries (Vietnam factory)==================================================================
                Dim FNHarmful As Double = 0.0
                Dim FNSkill As Double = 0.0
                Dim FNworkdayAchivement As Double = 0.0

                FNworkdayAchivement = ((_GFNTimeMin + (_TotalHoliDay * 480.0) + _GtotalleavePay) / 480.0)

                '...Basic Salaries case match pass probation/ not pass probation
                If FTProbationSta = "1" Then
                    '...pass probation
                    'If Microsoft.VisualBasic.DateDiff(DateInterval.Day, CDate(_EndDate), CDate(FDDateProbation)) > 0 Then
                    '    '...pass probation status but probation date is greater than re-calculate end date
                    'Else
                    '    '...Harmful wages =  (((5 % * ( Minimum Salaries - หักเงินสาย - หักเงินลาไม่จ่าย - หักเงินขาดงาน) ) / Working days Per Month) * Working days
                    '    '...Skill wages = (((7 % * ( Minimum Salaries - หักเงินสาย - หักเงินลาไม่จ่าย - หักเงินขาดงาน) ) / Working days Per Month) * Working days

                    '    FNHarmful = (((FNHarmfulRate / 100.0) * _FCSalary) / FNBusinessWorkday) * FNworkdayAchivement   '...Add New To List FinCode Harmful
                    '    If FTStaCalSkillAllowance = "1" Then '...calculate skill allowance except position maid แม่บ้าน
                    '        FNSkill = (((FNSkillRate / 100.0) * _FCSalary) / FNBusinessWorkday) * FNworkdayAchivement   '...Add New To List FinCode Skill
                    '    End If

                    'End If

                Else
                    '...not pass probation
                End If

                'If _EmpCalType = "2" Then
                '    _FCSalary = _FCBaht
                '    _FTSlary = _FCBaht

                'End If


                FNBasicSalaries = ((_FCSalary) / FNBusinessWorkday) * FNworkdayAchivement '+ FNHarmfulBaht + FNSkillBaht
                If _EmpCalType = "2" Then
                    If _FNEmpBaht <= 0 Then
                        FNBasicSalaries = FNBasicSalaries - _LaNotpaid
                        If FNBasicSalaries < 0 Then
                            FNBasicSalaries = 0
                        End If
                        _FCSalary = _FCSalary - _LaNotpaid
                        If _FCSalary < 0 Then
                            _FCSalary = 0
                        End If
                    End If
                ElseIf _EmpCalType = "0" Then
                    FNBasicSalaries = _FNEmpBaht
                End If

                '...Re-Calculate Insurance Employee/Employer
                FNSocialInsuranceEmployee = 0 : FNSocialInsuranceEmployer = 0
                FNHealthInsuranceEmployee = 0 : FNHealthInsuranceEmployer = 0
                FNUnemploymentInsuranceEmployee = 0 : FNUnemploymentInsuranceEmployer = 0
                FNUnionInsuranceEmployee = 0 : FNUnionInsuranceEmployer = 0

                Dim FNFixedSalary As Double = _FCSalary

                If FDDateProbation <= _StartDate Then ' **คิดประกันสังคม และ ผ่านทดลองงาน ..."1" ไม่คิดประกันสังคม
                    Dim FNBasicSalariesRecalInsurance As Double = 0.0
                    'Chang 20150403 hold คิดแบบเมืองไทย คือเอาจำนวนเงินได้ตามจริง
                    'FNBasicSalariesRecalInsurance = IIf(FNBasicSalaries > FNMaximumBasicSalaries, FNMaximumBasicSalaries, FNBasicSalaries)
                    'new คิดจากฐานเงินเดือน

                    'hold 2010831 roundup to 100
                    ' FNBasicSalariesRecalInsurance = IIf(_FTSlary > FNMaximumBasicSalaries, FNMaximumBasicSalaries, _FTSlary + FNHarmfulBaht + FNSkillBaht)
                    FNBasicSalariesRecalInsurance = IIf(_FTSlary > FNMaximumBasicSalaries, FNMaximumBasicSalaries, _FCSalary)

                    FNBasicSalariesRecalInsurance = FNBasicSalariesRecalInsurance + _FTAddCalculateSocial
                    If _EmpCalType = "2" Then
                        If _FNEmpBaht <= 0 Then
                            FNBasicSalariesRecalInsurance = FNBasicSalariesRecalInsurance - _LaNotpaid
                            If FNBasicSalariesRecalInsurance < 0 Then
                                FNBasicSalariesRecalInsurance = 0
                            End If
                        End If
                    End If


                    If _FTCalSocialSta = "0" Then
                        FNSocialInsuranceEmployeeOrg = FNBasicSalariesRecalInsurance * (FNSocialEmployeeRate / 100.0)
                        FNSocialInsuranceEmployerOrg = FNBasicSalariesRecalInsurance * (FNSocialEmployerRate / 100.0)
                    End If


                    FNHealthInsuranceEmployeeOrg = FNBasicSalariesRecalInsurance * (FNHealthEmployeeRate / 100.0)
                    FNHealthInsuranceEmployerOrg = FNBasicSalariesRecalInsurance * (FNHealthEmployerRate / 100.0)

                    FNUnemploymentInsuranceEmployeeOrg = FNBasicSalariesRecalInsurance * (FNUnemploymentEmployeeRate / 100.0)
                    FNUnemploymentInsuranceEmployerOrg = FNBasicSalariesRecalInsurance * (FNUnemploymentEmployerRate / 100.0)

                    FNUnionInsuranceEmployeeOrg = FNBasicSalariesRecalInsurance * (FNUnionEmployeeRate / 100.0)
                    FNUnionInsuranceEmployerOrg = FNBasicSalariesRecalInsurance * (FNUnionEmployerRate / 100.0)


                    FNSocialInsuranceEmployee = Math.Ceiling(FNSocialInsuranceEmployeeOrg) : FNSocialInsuranceEmployer = Math.Ceiling(FNSocialInsuranceEmployerOrg)
                    FNHealthInsuranceEmployee = Math.Ceiling(FNHealthInsuranceEmployeeOrg) : FNHealthInsuranceEmployer = Math.Ceiling(FNHealthInsuranceEmployerOrg)
                    FNUnemploymentInsuranceEmployee = Math.Ceiling(FNUnemploymentInsuranceEmployeeOrg) : FNUnemploymentInsuranceEmployer = Math.Ceiling(FNUnemploymentInsuranceEmployerOrg)
                    FNUnionInsuranceEmployee = Math.Ceiling(FNUnionInsuranceEmployeeOrg) : FNUnionInsuranceEmployer = Math.Ceiling(FNUnionInsuranceEmployerOrg)


                    If _FDDateEnd <> "" Then
                        'Dim x As String = Convert.ToDateTime(_FDDateEnd).Month()
                        'Dim mEndDateCal As String = Convert.ToDateTime(_EndDate).Month
                        If Microsoft.VisualBasic.Left(_FDDateEnd, 7).ToString = Microsoft.VisualBasic.Left(_EndDate, 7).ToString Then
                            '...ถ้าพนักงานลาออกจากงานในช่วงวันที่ 1-15 ไม่ต้องจ่าย แต่ถ้าลาออกในช่วงวันที่ 16-30 ต้องจ่าย
                            If Integer.Parse(Microsoft.VisualBasic.Strings.Right(_FDDateEnd, 2)) < FNNotRecalInsuranceEmpResign Then
                                FNSocialInsuranceEmployee = 0 : FNSocialInsuranceEmployer = 0
                                FNHealthInsuranceEmployee = 0 : FNHealthInsuranceEmployer = 0
                                FNUnemploymentInsuranceEmployee = 0 : FNUnemploymentInsuranceEmployer = 0
                                FNUnionInsuranceEmployee = 0 : FNUnionInsuranceEmployer = 0



                            End If
                        End If

                    Else
                        '...Nothing
                    End If

                    'change by Noh 20150303 
                    'Hold()
                    _TotalCalSso = FNSocialInsuranceEmployee + FNHealthInsuranceEmployee + FNUnemploymentInsuranceEmployee + FNUnionInsuranceEmployee
                    _FCSocial = _TotalCalSso
                    'new
                    _FCSocial = Math.Truncate(FNSocialInsuranceEmployee)

                    'ค่าประกันสุขภาพ
                    _FNHealtCareAmt = Math.Truncate(FNHealthInsuranceEmployee)



                Else
                    _TotalCalSso = 0
                    _FCSocial = 0
                End If






                Dim FNAttendanceAllowance As Double = 0.0
                Dim FNMealAllowance As Double = 0.0
                Dim FNCarAllowance As Double = 0.0
                Dim FNCompetencyAllowance As Double = 0.0
                Dim FNQualityaAllowance As Double = 0.0

                '--013 : Competency Allowance/ค่าความสามารถ
                '--015 : Attendance Allowance/ค่ารูดบัตร
                '--018 : Quality Allowance/ค่าคุณภาพ
                '--023 : Meal Allowance/ค่าอาหาร
                '--024 : Car Allowance/ค่ารถ
                For Each oDataRow As System.Data.DataRow In _DtFin.Rows
                    Select Case oDataRow!FTFinCode.ToString()
                        Case "013" '...Competency Allowance
                            FNCompetencyAllowance = FNCompetencyAllowance + Val(oDataRow!FCTotalFinAmt.ToString)
                        Case "015" '...Attendance Allowance
                            'If System.Diagnostics.Debugger.IsAttached = True Then
                            '    MsgBox("Count Cost Attendance No days : " & FNCntAddFinCode015 & Environment.NewLine & oDataRow!FCTotalFinAmt, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                            'End If
                            FNAttendanceAllowance = FNAttendanceAllowance + Val(oDataRow!FCTotalFinAmt.ToString)
                        Case "018" '...Quality Allowance
                            FNQualityaAllowance = FNQualityaAllowance + Val(oDataRow!FCTotalFinAmt.ToString)
                        Case "023" '...Meal Allowance
                            FNMealAllowance = FNMealAllowance + Val(oDataRow!FCTotalFinAmt.ToString)
                        Case "024" '...Car Allowance
                            FNCarAllowance = FNCarAllowance + Val(oDataRow!FCTotalFinAmt.ToString)
                    End Select

                Next

                Dim FNOvertimeAmnt As Double = 0.0

                FNOvertimeAmnt = FNOvertimeAmnt + _nBahtOt1 + _nBahtOt15 + _nBahtOt2 + _nBahtOt3 + _nBahtOt4

                '...Total Income (Basic Salaries รายเดือนใช้ Fixed Rate = FCSalary ตาม Profile)
                REM Type Daily Total Income = Basic Salaries + Attendance + Meal + Car + Competency + Quality + Incentive + Overtime + Other Receive – Social – Health - Unemployment – Union - Other  Discount - Tax
                REM Type Month Total Income = Basic Salaries + Attendance + Meal + Car + Competency + Quality + Incentive + Overtime + Other Receive – Social – Health - Unemployment – Union - Other  Discount - Tax
                Dim FNTotalIncome As Double = 0.0

                _Qry = ""
                _Qry = "SELECT CASE WHEN (A.FTEmpTypeCode = N'M' OR A.FTEmpTypeNameEN LIKE N'%Monthly%' OR A.FTEmpTypeNameTH LIKE '%รายเดือน%') THEN '1' ELSE '0' END AS FTChkEmpTypeMonthly"
                _Qry &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[THRMEmpType] AS A (NOLOCK)"
                _Qry &= Environment.NewLine & "WHERE A.FNHSysEmpTypeId = " & Val(_EmpType) & ";"

                '...employee type monthly
                Dim FTChkEmpTypeMonthly As String
                FTChkEmpTypeMonthly = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MASTER, "0")

                REM 2014/11/10 Vietnam factory
                'If FTChkEmpTypeMonthly = "1" Then
                '    FNTotalIncome = _FCSalary + FNAttendanceAllowance + FNMealAllowance + FNCarAllowance + FNCompetencyAllowance + FNQualityaAllowance + FNOvertimeAmnt + _FNIncentiveAmt + _FCOtherAdd - FNSocialInsuranceEmployee - FNHealthInsuranceEmployee - FNUnemploymentInsuranceEmployee - FNUnionInsuranceEmployee - _FCOtherDeduct
                'Else
                '    FNTotalIncome = FNBasicSalaries + FNAttendanceAllowance + FNMealAllowance + FNCarAllowance + FNCompetencyAllowance + FNQualityaAllowance + FNOvertimeAmnt + _FNIncentiveAmt + _FCOtherAdd - FNSocialInsuranceEmployee - FNHealthInsuranceEmployee - FNUnemploymentInsuranceEmployee - FNUnionInsuranceEmployee - _FCOtherDeduct
                'End If
                ' _FCAdd = _FCAdd - FNWageScale
                _FCOtherAdd += +_AmtReturnVacation


                If FTChkEmpTypeMonthly = "1" Then
                    'FNTotalIncome = _FCSalary + FNCompetencyAllowance + FNQualityaAllowance + FNOvertimeAmnt + _FNIncentiveAmt + _FCOtherAdd + FNPayRestOTBaht + _FCAdd
                    ' FNTotalIncome = Math.Round(FNBasicSalaries) + FNCompetencyAllowance + FNQualityaAllowance + Math.Round(FNOvertimeAmnt) + _FNIncentiveAmt + _FCOtherAdd + FNPayRestOTBaht + _FCAdd  '+ FNHarmfulBaht + FNSkillBaht
                    FNTotalIncome = Math.Round(FNBasicSalaries) + Math.Round(FNOvertimeAmnt) + _FNIncentiveAmt + _FCOtherAdd + FNPayRestOTBaht + _FCAdd + (_Lapaid + _HBaht) '+ _FNEmpDiligent   '+ _AmtReturnVacation '+ FNHarmfulBaht + FNSkillBaht
                Else
                    FNTotalIncome = Math.Round(FNBasicSalaries) + Math.Round(FNOvertimeAmnt) + _FNIncentiveAmt + _FCOtherAdd + FNPayRestOTBaht + _FCAdd + (_Lapaid + _HBaht)  '+ _FNEmpDiligent '+ _AmtReturnVacation  '+ FNHarmfulBaht + FNSkillBaht
                End If
                FNTotalIncome = Math.Round(FNTotalIncome)
                ' FNAttendanceAllowance + FNMealAllowance + FNCarAllowance +FNCompetencyAllowance+ FNQualityaAllowance



                If _FTCalTaxSta = "0" Then  '..."1" ไม่คิดภาาษี
                    Dim FNRecalTax As Double = 0
                    Dim FNTaxOtherAmnt As Double

                    '...รายการเงินที่จะต้องนำมาคิดภาษี
                    If FTChkRaceThaiWorker = "1" Then
                        '...บุคคลคนต่างด้าว (พนง คนไทยที่ไปทำงานในเวียดนาม รายการเงินที่จะต้องนำมาคิดภาษีดังนี้)
                        Dim FTChkWorkPermit As String = "1"

                        If FTChkWorkPermit = "1" Then
                            _TotalCalTax = (FNBasicSalaries - FNHealthInsuranceEmployee - FNModPersonTaxRate - (FNCntEmployeeChild * FNModChildAllowanceTaxRate)) + _FTAddCalculateTax

                            FNRecalTax = GETnTax(_TotalCalTax, 0, FNTaxOtherAmnt)
                            '...จำนวนเงินภาษีที่ต้องนำส่ง
                            If FNRecalTax > 0 Then
                                _TaxAmt = FNRecalTax
                            Else
                                _TaxAmt = 0
                            End If

                        Else
                            _TotalCalTax = FNTotalIncome
                            FNRecalTax = _TotalCalTax * (FNThaiWorkerNoWorkpermitTaxRate / 100.0)
                        End If

                    Else

                        _TotalCalTax = FNTotalIncome - FNSocialInsuranceEmployee - FNHealthInsuranceEmployee - FNUnemploymentInsuranceEmployee - FNModPersonTaxRate - (FNCntEmployeeChild * FNModChildAllowanceTaxRate) - _FCAdd + _FTAddCalculateTax
                        _TotalCalTax = IIf(_TotalCalTax <= 0, 0, _TotalCalTax)
                        If _TotalCalTax > 0 Then
                            FNRecalTax = GETnTax(_TotalCalTax, 0, FNTaxOtherAmnt)
                        Else
                            FNRecalTax = 0
                        End If

                    End If

                    '...จำนวนเงินภาษีที่ต้องนำส่ง
                    If FNRecalTax > 0 Then
                        _TaxAmt = FNRecalTax
                    Else
                        _TaxAmt = 0
                    End If

                Else
                    _TotalCalTax = 0
                    _TaxAmt = 0
                End If

                ' _FCPayVacationBaht = 0 'IIf(HI.Conn.SQLConn.GetField("select Top 1 From  THRMFinanceSet With(nolock) where ",Conn.DB.DataBaseName.DB_HR,"") <> 3 , _FCPayVacationBaht , 0)
                _Lapaid += -_FCPayVacationBaht
                '=======================================================================================================================================================================

                REM 2014/10/22 Vietnam Factory
                '_Net = (_FNEmpBaht + _HBaht + _nBahtOt1 + _FNEmpDiligent + _Lapaid + (FNPayLeaveBusinessBaht + FNPayLeaveSickBaht + FNPayLeaveSpecialBaht + FNParturitionLeave) + _nBahtOt15 + _nBahtOt2 + _nBahtOt3 + _nBahtOt4 + _FCPayVacationBaht + _FCOtherAdd + _FCAdd + _FNIncentiveAmt + _FNNetAttandanceAmt + _FNHealtCareAmt + _FNTransportAmt + _FNNetChildCareAmt + _FNWorkAgeSalary + _FNNetOTMealAmtUS) - (_FCOtherDeduct + _FCDeduct)
                '_FCBaht = _FNEmpBaht + _nBahtOt1 + _nBahtOt15 + _nBahtOt2 + _nBahtOt3 + _nBahtOt4

                '_FNNetpay = _Net - (FTContributedAmt)
                '_FNNetpayOrg = _FNNetpay

                If FNTotalIncome <= 0 Then
                    If FNSkillBaht > 0 Then FNSkillBaht = 0
                    If FNHarmfulBaht > 0 Then FNHarmfulBaht = 0
                End If

                If FNTotalIncome < ((_TaxAmt) + Math.Truncate(FNSocialInsuranceEmployee) + Math.Truncate(FNHealthInsuranceEmployee) + Math.Truncate(FNUnemploymentInsuranceEmployee) + Math.Truncate(FNUnionInsuranceEmployee) + _FCOtherDeduct) Then

                    If _FCSocial > 0 Then _FCSocial = 0
                    If FNSocialInsuranceEmployee > 0 Then FNSocialInsuranceEmployee = 0

                    If FNUnemploymentInsuranceEmployee > 0 Then FNUnemploymentInsuranceEmployee = 0
                    If FNUnionInsuranceEmployee > 0 Then FNUnionInsuranceEmployee = 0

                    If FNSocialInsuranceEmployer > 0 Then FNSocialInsuranceEmployer = 0
                    If FNUnemploymentInsuranceEmployer > 0 Then FNUnemploymentInsuranceEmployer = 0
                    If FNUnionInsuranceEmployer > 0 Then FNUnionInsuranceEmployer = 0

                    If _FNHealtCareAmt > 0 Then _FNHealtCareAmt = 0
                    If _FNTransportAmt > 0 Then _FNTransportAmt = 0

                    If FNTotalIncome < FNHealthInsuranceEmployee Then
                        If FNHealthInsuranceEmployee > 0 Then FNHealthInsuranceEmployee = 0
                        If FNHealthInsuranceEmployer > 0 Then FNHealthInsuranceEmployer = 0
                    End If

                End If

                If FTChkRaceThaiWorker = "1" Then

                    FNSocialInsuranceEmployee = 0
                    FNUnemploymentInsuranceEmployee = 0
                    FNUnionInsuranceEmployee = 0
                    'copany
                    FNSocialInsuranceEmployer = 0
                    FNUnemploymentInsuranceEmployer = 0
                    FNUnionInsuranceEmployer = 0

                End If

                _Net = Math.Ceiling(FNTotalIncome) - (_TaxAmt) - Math.Truncate(FNSocialInsuranceEmployee) - Math.Truncate(FNHealthInsuranceEmployee) - Math.Truncate(FNUnemploymentInsuranceEmployee) - Math.Truncate(FNUnionInsuranceEmployee) - _FCOtherDeduct

                REM 2014/11/03 vietnam factory 
                '_FNNetpay = _Net
                '_FNNetpayOrg = _FNNetpay

                '...เมื่อคำนวณค่าแรงประจำเดือนแล้ว กรณีเศษเงิน < 500 VND ให้ปัดเศษเงินเป็น 500 VND และกรณีเศษเงิน > 500 VND  < 1,000 VND ให้ปัดเศษเงินเป็น 1,000 VND
                _FNNetpayOrg = _Net
                'ไม่มีการปัดเศษแล้ว 03/04/2015
                '_FNNetpay = Calculate.HelperRoundUpNetpay(_FNNetpayOrg, 500.0, 1000.0)
                _FNNetpay = _FNNetpayOrg ' Calculate.HelperRoundUpNetpay(_FNNetpayOrg, 500.0, 1000.0)

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll"
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_EmpCode) & " "
                _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
                _Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll (FTInsUser, FTInsDate, FTInsTime"
                _Qry &= vbCrLf & ", FTPayYear, FTPayTerm, FNHSysEmpID, FTEmpIdNo"
                _Qry &= vbCrLf & ", FNHSysEmpTypeId, FTPayDate"
                _Qry &= vbCrLf & ", FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId,  FNHSysPayRollPayId"
                _Qry &= vbCrLf & ", FNHSysBankId, FNHSysBankBranchId, FTAccNo, FNHoliday"
                _Qry &= vbCrLf & ", FNSalary, FNWorkingHour"
                _Qry &= vbCrLf & ", FNOt1, FNOt15, FNOt2, FNOt3,FNOt4"
                _Qry &= vbCrLf & ", FNTotalLeavePay, FNTotalLeaveNotPay, FNTotalLeave"
                _Qry &= vbCrLf & ", FNTotalWKNMin,  FNOt1Min, FNOt15Min, FNOt2Min"
                _Qry &= vbCrLf & ", FNOt3Min, FNOt4Min, FNTotalLateMin, FNLateCutMin, FNLateCutAbsentMin"
                _Qry &= vbCrLf & ", FNAbsentMin, FNTotalWKMin, FNTotalLeavePayMin, FNTotalLeaveNotPayMin, FNTotalLeaveMin"
                _Qry &= vbCrLf & ", FCBaht, FCOt1_Baht"
                _Qry &= vbCrLf & ", FCOt15_Baht, FCOt2_Baht, FCOt3_Baht,FCOt4_Baht,FCNetBaht"
                _Qry &= vbCrLf & ", FNDiligentBaht, FNPayLeaveVacationBaht, FNPayLeaveOtherBaht"
                _Qry &= vbCrLf & ", FNLateCutAmt, FNLateCutAbsentAmt,FNAbsentAmt, FNTotalRecalSSO, FNTotalRecalTAX"
                _Qry &= vbCrLf & ", FNTotalAdd,FNTotalAddOther, FNTotalExpense, FNTotalExpenseOther, FNTotalIncome"
                _Qry &= vbCrLf & ", FNSocial, FNTax, FHolidayBaht, FNNetpay, FNAccumulateIncomeYear"
                _Qry &= vbCrLf & ", FNAccumulateSocialYear, FNAccumulateTax, FTStateInDustin"
                _Qry &= vbCrLf & ", FNTotalCalContributedAmt,FNContributedAmt,FNCmpContributedAmt,FNTotalCalWorkmen,FNWorkmenAmt ,FNAmtRetire"
                _Qry &= vbCrLf & ", FNPayLeaveSSo,FNWorkingDay,FNAdjBeforeCal,FNIncentiveAmt,FNNetpayOrg"
                _Qry &= vbCrLf & ", FNAttandanceAmt, FNHealtCareAmt"
                _Qry &= vbCrLf & ", FNTransportAmt, FNChildCareAmt, FNOTMealAmt, FNSocialBase, FNWorkAgeSalary, FNOTMealAmtUS, FNExchangeRate, FNSickLeaveBaht, FNSickLeaveMin, FNBusinessLeaveBaht, FNBusinessLeaveMin"
                _Qry &= vbCrLf & ", FNSpecialLeaveBaht, FNSpecialLeaveMin, FNParturitionLeaveBaht, FNParturitionLeaveMin, FNVacationRetMin, FNVacationRetAmt,FNExchangeRateTHB"
                _Qry &= vbCrLf & ", FNSkillRate, FNHarmfulRate, FNBasicSalaries, FNSocialInsuranceEmployee, FNSocialInsuranceEmployer, FNHealthInsuranceEmployee, FNHealthInsuranceEmployer, FNUnemploymentInsuranceEmployee, FNUnemploymentInsuranceEmployer, FNUnionInsuranceEmployee, FNUnionInsuranceEmployer, FNBusinessWorkday"
                _Qry &= vbCrLf & " , FNSkillBaht, FNHarmfulBaht, FNPayRestOTBaht, FNPayRestOTMin,FTStateCalSocial,FTStateCalTax"
                '_Qry &= vbCrLf & ",FNSocialInsuranceEmployeeOrg, FNSocialInsuranceEmployerOrg, FNHealthInsuranceEmployeeOrg, FNHealthInsuranceEmployerOrg, FNUnemploymentInsuranceEmployeeOrg, "
                '_Qry &= vbCrLf & "  FNUnemploymentInsuranceEmployerOrg, FNUnionInsuranceEmployeeOrg, FNUnionInsuranceEmployerOrg"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(_User) & "',CONVERT(VARCHAR(10), GETDATE(), 111), CONVERT(VARCHAR(8), GETDATE(), 114)"
                _Qry &= vbCrLf & " 	,'" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ""
                _Qry &= vbCrLf & "  ,N'" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "','" & _EmpType & "','" & HI.UL.ULDate.ConvertEnDB(_PayDate) & "'"
                _Qry &= vbCrLf & " 	," & Val(_FTDeptCode) & ""
                _Qry &= vbCrLf & " 	," & Val(_FTDivCode) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTSectCode) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTUnitCode) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTPos) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTPaymentCode) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTBankCode) & " "
                _Qry &= vbCrLf & " 	," & Val(_FTBranchCode) & " "
                _Qry &= vbCrLf & " 	,N'" & HI.UL.ULF.rpQuoted(_FTAccNo) & "'," & _TotalHoliDay & ""
                _Qry &= vbCrLf & " 	," & _FTSlary & "" '_FCSalary
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2), " & _GFNTimeMin & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNTimeMin & " % 60) / 100.00)) "
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2), " & _GFNOT1Min & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNOT1Min & "  % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2), " & _GFNOT1_5Min & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNOT1_5Min & "  % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2), " & _GFNOT2Min & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNOT2Min & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2)," & _GFNOT3Min & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNOT3Min & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2),  " & _GFNOT4Min & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GFNOT4Min & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2),  " & _GtotalleavePay & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GtotalleavePay & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2),  " & _GtotalleaveNotPay & " / 60) + CONVERT(NUMERIC(18,2),(( " & _GtotalleaveNotPay & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	,CONVERT(NUMERIC(18,2),  " & _Gtotalleave & " / 60) + CONVERT(NUMERIC(18,2),(( " & _Gtotalleave & " % 60) / 100.00))"
                _Qry &= vbCrLf & " 	, " & _GFNTimeMin
                _Qry &= vbCrLf & " 	, " & _GFNOT1Min
                _Qry &= vbCrLf & " 	, " & _GFNOT1_5Min
                _Qry &= vbCrLf & "  , " & _GFNOT2Min
                _Qry &= vbCrLf & "  , " & _GFNOT3Min
                _Qry &= vbCrLf & "  , " & _GFNOT4Min
                _Qry &= vbCrLf & "  , " & _GFNLateNormalMin
                _Qry &= vbCrLf & "  , " & _GFNLateNormalCut
                _Qry &= vbCrLf & "  , " & _GFNCutAbsent
                _Qry &= vbCrLf & "  , " & _GFNAbsent & "," & (_GFNTimeMin + _GFNOT1Min + _GFNOT1_5Min + _GFNOT2Min + _GFNOT3Min + _GFNOT4Min)
                _Qry &= vbCrLf & "  , " & _GtotalleavePay
                _Qry &= vbCrLf & "  , " & _GtotalleaveNotPay
                _Qry &= vbCrLf & "  , " & _Gtotalleave
                _Qry &= vbCrLf & "  , " & Math.Round(_FNEmpBaht) & " "
                _Qry &= vbCrLf & "  , " & Math.Round(_nBahtOt1) & " "
                _Qry &= vbCrLf & "  , " & Math.Round(_nBahtOt15) & " "
                _Qry &= vbCrLf & "  , " & Math.Round(_nBahtOt2) & " "
                _Qry &= vbCrLf & "  , " & Math.Round(_nBahtOt3) & " "
                _Qry &= vbCrLf & "  , " & Math.Round(_nBahtOt4) & " "
                _Qry &= vbCrLf & "  , " & (Math.Round(_FNEmpBaht) + Math.Round(_nBahtOt1) + Math.Round(_nBahtOt15) + Math.Round(_nBahtOt2) + Math.Round(_nBahtOt3) + Math.Round(_nBahtOt4) + _FNIncentiveAmt) & " "
                _Qry &= vbCrLf & "  , " & _FNEmpDiligent & " "
                _Qry &= vbCrLf & "  , " & _FCPayVacationBaht & " "
                _Qry &= vbCrLf & "  , " & _Lapaid & " "
                _Qry &= vbCrLf & "  , " & _LateCutAmt & " "
                _Qry &= vbCrLf & "  , " & _LateCutAmtAbsent & " "
                _Qry &= vbCrLf & "  , " & _nBahtAbsent & " "
                _Qry &= vbCrLf & "  , " & _TotalCalSso & " "
                _Qry &= vbCrLf & "  , " & _TotalCalTax & " "
                _Qry &= vbCrLf & "  , " & _FCAdd & " "
                _Qry &= vbCrLf & "  , " & _FCOtherAdd & " "
                _Qry &= vbCrLf & "  , " & _FCDeduct & " "
                _Qry &= vbCrLf & "  , " & _FCOtherDeduct & " "
                REM 2014/11/10 Vietnam factory _Qry &= vbCrLf & "  , " & _Net & " "
                _Qry &= vbCrLf & "  , " & FNTotalIncome & " "
                _Qry &= vbCrLf & "  , " & _FCSocial & " "
                _Qry &= vbCrLf & "  , " & _TaxAmt & " "
                _Qry &= vbCrLf & "  , " & _HBaht & " "
                _Qry &= vbCrLf & "  , " & _FNNetpay & ""
                _Qry &= vbCrLf & "  , " & (_Net + _FCAccumulateIncome) & " "
                _Qry &= vbCrLf & "  , " & (_FCSocial + _FCAccumulateSocial) & " "
                _Qry &= vbCrLf & "  , " & (_TaxAmt + _FCAccumulateTax) & " "
                _Qry &= vbCrLf & "  ,'" & _FTStateInDustin & "' "
                _Qry &= vbCrLf & "  , " & (FTTotalCalContributedAmt) & " "
                _Qry &= vbCrLf & "  , " & (FTContributedAmt) & " "
                _Qry &= vbCrLf & "  , " & (FTCmpContributedAmt) & " "
                _Qry &= vbCrLf & "  , " & (FTTotalCalWorkmen) & " "
                _Qry &= vbCrLf & "  , " & (FTWorkmenAmt) & " "
                _Qry &= vbCrLf & "  , " & _AmtRetire & " "
                _Qry &= vbCrLf & "  , " & _GtotalleavePayCalSsoAmt & " "
                _Qry &= vbCrLf & "  , " & _WorkingDay & " " ' Format((_GFNTimeMin / 480), "0.00") 
                _Qry &= vbCrLf & "  , " & _WageAdjAdd & " "
                _Qry &= vbCrLf & "  , " & _FNIncentiveAmt & " "
                _Qry &= vbCrLf & "  , " & _FNNetpayOrg & " "
                _Qry &= vbCrLf & "  , " & _FNNetAttandanceAmt & " "
                _Qry &= vbCrLf & "  , " & _FNHealtCareAmt & " "
                _Qry &= vbCrLf & "  , " & _FNTransportAmt & " "
                _Qry &= vbCrLf & "  , " & _FNNetChildCareAmt & " "
                _Qry &= vbCrLf & "  , " & _FNNetOTMealAmt & " "
                _Qry &= vbCrLf & "  , " & _FNSocialBase & " "
                _Qry &= vbCrLf & "  , " & _FNWorkAgeSalary & " "
                _Qry &= vbCrLf & "  , " & _FNNetOTMealAmtUS & " "
                _Qry &= vbCrLf & "  , " & _FNExchangeRate & " "
                _Qry &= vbCrLf & "  , " & FNPayLeaveSickBaht & " "
                _Qry &= vbCrLf & "  , " & GFNPayLeaveSickBahtMin & " "
                _Qry &= vbCrLf & "  , " & FNPayLeaveBusinessBaht & " "
                _Qry &= vbCrLf & "  , " & GFNPayLeaveBusinessBahtMin & " "
                _Qry &= vbCrLf & "  , " & FNPayLeaveSpecialBaht & " "
                _Qry &= vbCrLf & "  , " & GFNPayLeaveSpecialBahtMin & " "
                _Qry &= vbCrLf & "  , " & FNParturitionLeave & " "
                _Qry &= vbCrLf & "  , " & GFNParturitionLeaveMin & " "
                _Qry &= vbCrLf & "  , " & FNVacationRetMin & " "
                _Qry &= vbCrLf & "  , " & FNVacationRetAmt & " "
                _Qry &= vbCrLf & "  , " & _FNExchangeRateTHB & " "

                _Qry &= vbCrLf & "  , " & FNSkillRate & " "
                _Qry &= vbCrLf & "  , " & FNHarmfulRate & " "
                _Qry &= vbCrLf & "  , " & FNBasicSalaries & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNSocialInsuranceEmployee) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNSocialInsuranceEmployer) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNHealthInsuranceEmployee) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNHealthInsuranceEmployer) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNUnemploymentInsuranceEmployee) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNUnemploymentInsuranceEmployer) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNUnionInsuranceEmployee) & " "
                _Qry &= vbCrLf & "  , " & Math.Truncate(FNUnionInsuranceEmployer) & " "
                _Qry &= vbCrLf & "  , " & FNBusinessWorkday & " "
                '----****************************************new In 20150408
                _Qry &= vbCrLf & " , " & FNSkillBaht
                _Qry &= vbCrLf & " , " & FNHarmfulBaht
                _Qry &= vbCrLf & " , " & FNPayRestOTBaht
                _Qry &= vbCrLf & " , " & FNPayRestOTMin
                '----************************************** State Cal Socail
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTCalSocialSta) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTCalTaxSta) & "'"
                '_Qry &= vbCrLf & "," & FNSocialInsuranceEmployeeOrg
                '_Qry &= vbCrLf & "," & FNSocialInsuranceEmployerOrg
                '_Qry &= vbCrLf & "," & FNHealthInsuranceEmployeeOrg
                '_Qry &= vbCrLf & "," & FNHealthInsuranceEmployerOrg
                '_Qry &= vbCrLf & "," & FNUnemploymentInsuranceEmployeeOrg
                '_Qry &= vbCrLf & "," & FNUnemploymentInsuranceEmployerOrg
                '_Qry &= vbCrLf & "," & FNUnionInsuranceEmployeeOrg
                '_Qry &= vbCrLf & "," & FNUnionInsuranceEmployerOrg


                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                If _FTCalTaxSta <> "1" And _FTEmpIdNo <> "" Then
                    '-----------------------------ภาษี -----------------------------------------------------
                    _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTaxYear "
                    _Qry &= vbCrLf & "WHERE FTYear = '" & _PayYear & "' "
                    _Qry &= vbCrLf & "      AND  FTEmpIdNo = '" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                    _Qry &= vbCrLf & "INSERT INTO THRTTaxYear (FNHSysCmpId,FTYear, FTEmpIdNo, FNAmt, FNExpenses, FNNetAmt, "
                    _Qry &= vbCrLf & "  FNModEmp, FNModMate, FNChildNotLern, FNChildLern, FNChildNotLernAmt, FNChildLernAmt, FNInsurance, FNProvidentfund, FNInterest, FNSocial, FNDonation, "
                    _Qry &= vbCrLf & "  FNProvidentfundOver, FNGPF, FNSavingsFund, FNCommutation, FNUnitRMF, FNModFather, FNModMother, FNModFatherMate, FNModMotherMate, FNUnitLTF, "
                    _Qry &= vbCrLf & "  FNDonationLern, FNParentsHealthInsurance, FNSupportSport, FNAcquisitionOfProperty, FNPension, FNTravel, FNTotalCalTax, FNTotalTax, FNTotalTaxPay)"
                    _Qry &= vbCrLf & "SELECT " & HI.ST.SysInfo.CmpID & ",'" & _PayYear & "','" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "' "

                    With _EmpTaxYear

                        _Qry &= vbCrLf & "," & .FTAmt & " "
                        _Qry &= vbCrLf & "," & .FTExpenses & ""
                        _Qry &= vbCrLf & "," & .FTNetAmt & ""
                        _Qry &= vbCrLf & "," & .FTModEmp & ""
                        _Qry &= vbCrLf & "," & .FTModMate & ""
                        _Qry &= vbCrLf & "," & .FNChildNotLern & ""
                        _Qry &= vbCrLf & "," & .FNChildLern & " "
                        _Qry &= vbCrLf & "," & .FTChildNotLern & ""
                        _Qry &= vbCrLf & "," & .FTChildLern & ""
                        _Qry &= vbCrLf & "," & .FTInsurance & ""
                        _Qry &= vbCrLf & "," & .FTProvidentfund & ""
                        _Qry &= vbCrLf & "," & .FTInterest & ""
                        _Qry &= vbCrLf & "," & .FTSocial & ""
                        _Qry &= vbCrLf & "," & .FTDonation & ""
                        _Qry &= vbCrLf & "," & .FTProvidentfundOver & ""
                        _Qry &= vbCrLf & "," & .FTGPF & ""
                        _Qry &= vbCrLf & "," & .FTSavingsFund & ""
                        _Qry &= vbCrLf & "," & .FTCommutation & ""
                        _Qry &= vbCrLf & "," & .FTUnitRMF & ""
                        _Qry &= vbCrLf & "," & .FTModFather & ""
                        _Qry &= vbCrLf & "," & .FTModMother & ""
                        _Qry &= vbCrLf & "," & .FTModFatherMate & ""
                        _Qry &= vbCrLf & "," & .FTModMotherMate & ""
                        _Qry &= vbCrLf & "," & .FTUnitLTF & ""
                        _Qry &= vbCrLf & "," & .FTDonationLern & ""
                        _Qry &= vbCrLf & "," & .FTParentsHealthInsurance & ""
                        _Qry &= vbCrLf & "," & .FTSupportSport & ""
                        _Qry &= vbCrLf & "," & .FTAcquisitionOfProperty & ""
                        _Qry &= vbCrLf & "," & .FTPension & ""
                        _Qry &= vbCrLf & "," & .FTTravel & ""
                        _Qry &= vbCrLf & "," & .FTTotalCalTax & ""
                        _Qry &= vbCrLf & "," & .FTTotalTax & ""
                        _Qry &= vbCrLf & "," & .FTTotalTaxPay & ""

                    End With

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '----------------------------- ภาษี -----------------------------------------------------
                End If

                '-----------------------------รายได้อื่นๆ -----------------------------------------------------

                _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin"
                _Qry &= vbCrLf & "WHERE  FNHSysEmpID = " & Val(_EmpCode) & " "
                _Qry &= vbCrLf & " AND FTPayYear = '" & _PayYear & "' "
                _Qry &= vbCrLf & " AND  FTPayTerm = '" & _PayTerm & "' "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                Dim _AllFincode As String = ""

                For Each _R As DataRow In _DtFin.Select("FCTotalFinAmt <> '0'")
                    _AllFincode = _R!FTFincode.ToString


                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,  FTFinCode, FCFin, FCFinAmt, FCFinAmtOther, FCTotalFinAmt)"
                    _Qry &= vbCrLf & "SELECT  '" & _PayYear & "', '" & _PayTerm & "', FNHSysEmpID,  FTFinCode, FTFinAmt, " & _R!FCTotalFinAmt.ToString & ", 0, " & _R!FCTotalFinAmt.ToString & ""
                    _Qry &= vbCrLf & "FROM  THRMEmployeeFin "
                    _Qry &= vbCrLf & "WHERE  FNHSysEmpID = " & Val(_EmpCode) & " "
                    _Qry &= vbCrLf & "       AND  FTFinCode = ('" & _AllFincode & "') "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

                For Each _R As DataRow In _dt.Select("FCFinAmt <> 0")
                    _AllFincode = _R!FTFincode.ToString

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin"
                    _Qry &= vbCrLf & "SET   FCTotalFinAmt = FCTotalFinAmt + " & Val(_R!FCFinAmt.ToString) & " "
                    _Qry &= vbCrLf & "    , FCFinAmtOther = " & _R!FCFinAmt.ToString & " "
                    _Qry &= vbCrLf & "    , FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "    , FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "    , FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "WHERE FTPayYear = '" & _PayYear & "' "
                    _Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "
                    _Qry &= vbCrLf & "      AND FNHSysEmpID = " & Val(_EmpCode) & " "
                    _Qry &= vbCrLf & "      AND FTFinCode = '" & _AllFincode & "' "

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollFin] (FTPayYear, FTPayTerm, FNHSysEmpID,  FTFinCode, FCFin, FCFinAmt, FCFinAmtOther, FCTotalFinAmt)"
                        _Qry &= vbCrLf & "SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ",'" & _AllFincode & "',0, 0," & _R!FCFinAmt.ToString & "," & _R!FCFinAmt.ToString & ""

                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If

                Next

                If _ShiftAmt > 0 Then
                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt, FCTotalFinAmt)"
                    _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '001'," & _ShiftValue.ToString & "," & _ShiftAmt.ToString & ""

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                End If

                If _ShiftOTAmt > 0 Then
                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt, FCTotalFinAmt)"
                    _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '007'," & _ShiftOTValue.ToString & "," & _ShiftOTAmt.ToString & ""

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                End If



                If (_PayRate > 0) Then
                    If _FNDeligentPeriod = 0 Then
                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt,FCTotalFinAmt)"
                        _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '008'," & _PayRate & "," & _PayRate & ""
                    Else
                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt,FCTotalFinAmt)"
                        _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '009'," & _PayRate & "," & _PayRate & ""
                    End If

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                    'End If
                End If



                '_Qry = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin"
                '_Qry &= vbCrLf & "WHERE FTPayYear = '" & _PayYear & "' "
                '_Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "
                '_Qry &= vbCrLf & "      AND FNHSysEmpID = " & Val(_EmpCode) & " "
                '_Qry &= vbCrLf & "      AND FTFinCode = '015' "
                'HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                'If _FNEmpDiligent > 0 Then


                '    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,   FTFinCode, FCFinAmt,FCTotalFinAmt)"
                '    _Qry &= vbCrLf & " SELECT  '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '015'," & _FNEmpDiligent.ToString & "," & _FNEmpDiligent.ToString & ""

                '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                'End If




                If _AmtReturnVacation > 0 Then

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin"
                    _Qry &= vbCrLf & "SET   FCTotalFinAmt = " & Val(_AmtReturnVacation.ToString) & " "
                    _Qry &= vbCrLf & "    , FCFinAmt = " & _ShiftValue.ToString & " "
                    _Qry &= vbCrLf & "    , FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "    , FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "    , FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "WHERE FTPayYear = '" & _PayYear & "' "
                    _Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "
                    _Qry &= vbCrLf & "      AND FNHSysEmpID = " & Val(_EmpCode) & " "
                    _Qry &= vbCrLf & "      AND FTFinCode = '025' "
                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTPayYear, FTPayTerm, FNHSysEmpID,  FTFinCode, FCFinAmt,FCTotalFinAmt)"
                        _Qry &= vbCrLf & "SELECT '" & _PayYear & "','" & _PayTerm & "'," & Val(_EmpCode) & ", '025', " & _ShiftValue.ToString & "," & _AmtReturnVacation.ToString & ""

                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                    End If

                End If
                '----------- เงินคืนพักร้อน-------------------------------

                '...Vietnam Factory Skill:019/Harmful:010 payment condition pass probation only
                '========================================================================================================================================================================================
                If FNSkill > 0 Then
                    '...Add FinCode : 019/Skill to list finance for payment year/term
                End If

                If FNHarmful > 0 Then
                    '...Add FinCode : 010/Harmful to list finance for payment year/term
                End If

                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollFin] "
                _Qry &= vbCrLf & "Set FCFinAmt=" & FNSkill.ToString
                _Qry &= vbCrLf & ",FCTotalFinAmt=" & FNSkill.ToString
                _Qry &= vbCrLf & "where FTPayYear='" & _PayYear & "'"
                _Qry &= vbCrLf & "AND FTPayTerm='" & _PayTerm & "'"
                _Qry &= vbCrLf & "AND FNHSysEmpID=" & Val(_EmpCode)
                _Qry &= vbCrLf & "AND FTFinCode='019'"
                If HI.Conn.SQLConn.ExecuteOnly(_Qry, HI.Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollFin] (FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode, FCFin, FCFinAmt, FCFinAmtOther, FCTotalFinAmt)"
                    _Qry &= Environment.NewLine & "SELECT '" & _PayYear & "', '" & _PayTerm & "', " & Val(_EmpCode) & ", '019', 0, " & FNSkill.ToString & ",  0, " & FNSkill.ToString & ""
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, HI.Conn.DB.DataBaseName.DB_HR)
                End If

                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollFin] "
                _Qry &= vbCrLf & "Set FCFinAmt=" & FNHarmful.ToString
                _Qry &= vbCrLf & ",FCTotalFinAmt=" & FNHarmful.ToString
                _Qry &= vbCrLf & "where FTPayYear='" & _PayYear & "'"
                _Qry &= vbCrLf & "AND FTPayTerm='" & _PayTerm & "'"
                _Qry &= vbCrLf & "AND FNHSysEmpID=" & Val(_EmpCode)
                _Qry &= vbCrLf & "AND FTFinCode='010'"
                If HI.Conn.SQLConn.ExecuteOnly(_Qry, HI.Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollFin] (FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode, FCFin, FCFinAmt, FCFinAmtOther, FCTotalFinAmt)"
                    _Qry &= Environment.NewLine & "SELECT '" & _PayYear & "', '" & _PayTerm & "', " & Val(_EmpCode) & ", '010', 0, " & FNHarmful.ToString & ",  0, " & FNHarmful.ToString & ""
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, HI.Conn.DB.DataBaseName.DB_HR)
                End If

                '========================================================================================================================================================================================

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave "
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_EmpCode) & " "
                _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
                _Qry &= vbCrLf & "      AND  FTPayTerm = '" & _PayTerm & "' "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave (FTPayYear, FTPayTerm, FNHSysEmpID, FTLeaveType, FNTotalHour, FNTotalMinute, FNTotalPayHour, FNTotalPayMinute, FNTotalNotPayHour, FNTotalNotPayMinute)"
                _Qry &= vbCrLf & "SELECT '" & _PayYear & "', '" & _PayTerm & "', FNHSysEmpID,  FTLeaveType,Convert(numeric(18,2), SUM(FNTotalMinute) / 60) + Convert(numeric(18,2),((Sum(FNTotalMinute) %60) /100.00)) , "
                _Qry &= vbCrLf & "  SUM(FNTotalMinute), CONVERT(NUMERIC(18,2), SUM(FNTotalPayMinute) / 60) + CONVERT(NUMERIC(18,2),((SUM(FNTotalPayMinute) % 60) / 100.00)), SUM(FNTotalPayMinute),  CONVERT(NUMERIC(18,2),SUM(FNTotalNotPayMinute) / 60) + Convert(NUMERIC(18,2),((SUM(FNTotalNotPayMinute) % 60) /100.00)), SUM(FNTotalNotPayMinute)"
                _Qry &= vbCrLf & "FROM THRTTransLeave"
                _Qry &= vbCrLf & "WHERE  (FNHSysEmpID = '" & Val(_EmpCode) & "')"
                _Qry &= vbCrLf & " 	    AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "' "
                _Qry &= vbCrLf & " 	    AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "' "
                _Qry &= vbCrLf & "GROUP BY   FNHSysEmpID, FTLeaveType"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            End If

            _Qry = "DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollCalculate"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
            _Qry &= vbCrLf & "      AND  FTPayTerm = '" & _PayTerm & "' "
            _Qry &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[THRTPayRollCalculate] (FTInsUser, FTInsDate, FTInsTime"
            _Qry &= vbCrLf & ", FTPayYear, FTPayTerm, FNHSysEmpID, FTEmpIdNo"
            _Qry &= vbCrLf & ", FNHSysEmpTypeId, FTPayDate"
            _Qry &= vbCrLf & ", FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysPayRollPayId"
            _Qry &= vbCrLf & ", FNHSysBankId, FNHSysBankBranchId, FTAccNo, FNHoliday"
            _Qry &= vbCrLf & ", FNSalary, FNWorkingHour"
            _Qry &= vbCrLf & ", FNOt1, FNOt15, FNOt2, FNOt3,FNOt4"
            _Qry &= vbCrLf & ", FNTotalLeavePay, FNTotalLeaveNotPay, FNTotalLeave"
            _Qry &= vbCrLf & ", FNTotalWKNMin, FNOt1Min, FNOt15Min, FNOt2Min"
            _Qry &= vbCrLf & ", FNOt3Min, FNOt4Min, FNTotalLateMin, FNLateCutMin, FNLateCutAbsentMin"
            _Qry &= vbCrLf & ", FNAbsentMin, FNTotalWKMin, FNTotalLeavePayMin, FNTotalLeaveNotPayMin, FNTotalLeaveMin"
            _Qry &= vbCrLf & ", FCBaht, FCOt1_Baht"
            _Qry &= vbCrLf & ", FCOt15_Baht, FCOt2_Baht, FCOt3_Baht,FCOt4_Baht,FCNetBaht"
            _Qry &= vbCrLf & ", FNDiligentBaht, FNPayLeaveVacationBaht, FNPayLeaveOtherBaht "
            _Qry &= vbCrLf & ", FNLateCutAmt, FNLateCutAbsentAmt,FNAbsentAmt, FNTotalRecalSSO, FNTotalRecalTAX"
            _Qry &= vbCrLf & ", FNTotalAdd,FNTotalAddOther, FNTotalExpense, FNTotalExpenseOther, FNTotalIncome"
            _Qry &= vbCrLf & ", FNSocial, FNTax, FHolidayBaht, FNNetpay, FNAccumulateIncomeYear"
            _Qry &= vbCrLf & ", FNAccumulateSocialYear, FNAccumulateTax, FTStateInDustin"
            _Qry &= vbCrLf & ", FNTotalCalContributedAmt,FNContributedAmt,FNCmpContributedAmt,FNTotalCalWorkmen,FNWorkmenAmt ,FNAmtRetire"
            _Qry &= vbCrLf & ", FNPayLeaveSSo,FNWorkingDay,FNAdjBeforeCal,FNIncentiveAmt,FNNetpayOrg"
            _Qry &= vbCrLf & ", FNAttandanceAmt, FNHealtCareAmt"
            _Qry &= vbCrLf & ", FNTransportAmt, FNChildCareAmt, FNOTMealAmt, FNSocialBase, FNWorkAgeSalary, FNOTMealAmtUS, FNExchangeRate, FNSickLeaveBaht, FNSickLeaveMin, FNBusinessLeaveBaht, FNBusinessLeaveMin"
            _Qry &= vbCrLf & ", FNSpecialLeaveBaht, FNSpecialLeaveMin, FNParturitionLeaveBaht, FNParturitionLeaveMin , FNVacationRetMin, FNVacationRetAmt,FNExchangeRateTHB"

            '************New 
            '_Qry &= vbCrLf & ", FNSkillRate, FNHarmfulRate, FNBasicSalaries, FNSocialInsuranceEmployee, FNSocialInsuranceEmployer, FNHealthInsuranceEmployee, FNHealthInsuranceEmployer, FNUnemploymentInsuranceEmployee, FNUnemploymentInsuranceEmployer, FNUnionInsuranceEmployee, FNUnionInsuranceEmployer, FNBusinessWorkday"
            '_Qry &= vbCrLf & " , FNSkillBaht, FNHarmfulBaht, FNPayRestOTBaht, FNPayRestOTMin,FTStateCalSocial,FTStateCalTax"
            '************New 

            '_Qry &= vbCrLf & ",FNSocialInsuranceEmployeeOrg, FNSocialInsuranceEmployerOrg, FNHealthInsuranceEmployeeOrg, FNHealthInsuranceEmployerOrg, FNUnemploymentInsuranceEmployeeOrg, "
            '_Qry &= vbCrLf & "  FNUnemploymentInsuranceEmployerOrg, FNUnionInsuranceEmployeeOrg, FNUnionInsuranceEmployerOrg"

            _Qry &= vbCrLf & ")"
            _Qry &= vbCrLf & " SELECT TOP 1  FTInsUser, FTInsDate, FTInsTime"
            _Qry &= vbCrLf & ", FTPayYear, FTPayTerm, FNHSysEmpID, FTEmpIdNo"
            _Qry &= vbCrLf & ", FNHSysEmpTypeId, FTPayDate"
            _Qry &= vbCrLf & ", FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysPayRollPayId"
            _Qry &= vbCrLf & ", FNHSysBankId, FNHSysBankBranchId, FTAccNo, FNHoliday"
            _Qry &= vbCrLf & ", FNSalary, FNWorkingHour"
            _Qry &= vbCrLf & ", FNOt1, FNOt15, FNOt2, FNOt3,FNOt4"
            _Qry &= vbCrLf & ", FNTotalLeavePay, FNTotalLeaveNotPay, FNTotalLeave"
            _Qry &= vbCrLf & ", FNTotalWKNMin, FNOt1Min, FNOt15Min, FNOt2Min"
            _Qry &= vbCrLf & ", FNOt3Min, FNOt4Min, FNTotalLateMin, FNLateCutMin, FNLateCutAbsentMin"
            _Qry &= vbCrLf & ", FNAbsentMin, FNTotalWKMin, FNTotalLeavePayMin, FNTotalLeaveNotPayMin, FNTotalLeaveMin"
            _Qry &= vbCrLf & ", FCBaht, FCOt1_Baht"
            _Qry &= vbCrLf & ", FCOt15_Baht, FCOt2_Baht, FCOt3_Baht,FCOt4_Baht,FCNetBaht"
            _Qry &= vbCrLf & ", FNDiligentBaht, FNPayLeaveVacationBaht, FNPayLeaveOtherBaht "
            _Qry &= vbCrLf & ", FNLateCutAmt, FNLateCutAbsentAmt,FNAbsentAmt, FNTotalRecalSSO, FNTotalRecalTAX"
            _Qry &= vbCrLf & ", FNTotalAdd,FNTotalAddOther, FNTotalExpense, FNTotalExpenseOther, FNTotalIncome "
            _Qry &= vbCrLf & ", FNSocial, FNTax, FHolidayBaht, FNNetpay, FNAccumulateIncomeYear"
            _Qry &= vbCrLf & ", FNAccumulateSocialYear, FNAccumulateTax, FTStateInDustin"
            _Qry &= vbCrLf & ", FNTotalCalContributedAmt,FNContributedAmt,FNCmpContributedAmt,FNTotalCalWorkmen,FNWorkmenAmt ,FNAmtRetire"
            _Qry &= vbCrLf & ", FNPayLeaveSSo,FNWorkingDay,FNAdjBeforeCal,FNIncentiveAmt,FNNetpayOrg"
            _Qry &= vbCrLf & ", FNAttandanceAmt, FNHealtCareAmt"
            _Qry &= vbCrLf & ", FNTransportAmt, FNChildCareAmt, FNOTMealAmt, FNSocialBase, FNWorkAgeSalary, FNOTMealAmtUS, FNExchangeRate, FNSickLeaveBaht, FNSickLeaveMin, FNBusinessLeaveBaht, FNBusinessLeaveMin"
            _Qry &= vbCrLf & ", FNSpecialLeaveBaht, FNSpecialLeaveMin, FNParturitionLeaveBaht, FNParturitionLeaveMin , FNVacationRetMin, FNVacationRetAmt,FNExchangeRateTHB"

            '************New
            '_Qry &= vbCrLf & ", FNSkillRate, FNHarmfulRate, FNBasicSalaries, FNSocialInsuranceEmployee, FNSocialInsuranceEmployer, FNHealthInsuranceEmployee, FNHealthInsuranceEmployer, FNUnemploymentInsuranceEmployee, FNUnemploymentInsuranceEmployer, FNUnionInsuranceEmployee, FNUnionInsuranceEmployer, FNBusinessWorkday"
            '_Qry &= vbCrLf & " , FNSkillBaht, FNHarmfulBaht, FNPayRestOTBaht, FNPayRestOTMin,FTStateCalSocial,FTStateCalTax"
            '************New
            '_Qry &= vbCrLf & ",FNSocialInsuranceEmployeeOrg, FNSocialInsuranceEmployerOrg, FNHealthInsuranceEmployeeOrg, FNHealthInsuranceEmployerOrg, FNUnemploymentInsuranceEmployeeOrg, "
            '_Qry &= vbCrLf & "  FNUnemploymentInsuranceEmployerOrg, FNUnionInsuranceEmployeeOrg, FNUnionInsuranceEmployerOrg"


            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "'"
            _Qry &= vbCrLf & "      AND  FTPayTerm = '" & _PayTerm & "'"

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND  FTPayYear = '" & _PayYear & "' "
            _Qry &= vbCrLf & "      AND  FTPayTerm = '" & _PayTerm & "' "
            _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate (FTPayYear, FTPayTerm, FNHSysEmpID,  FTFinCode,FCFin, FCFinAmt,FCFinAmtOther,FCTotalFinAmt)"
            _Qry &= vbCrLf & "SELECT FTPayYear, FTPayTerm, FNHSysEmpID,  FTFinCode,FCFin, FCFinAmt,FCFinAmtOther,FCTotalFinAmt "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
            _Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
            _Qry &= vbCrLf & "      AND  FTPayTerm = '" & _PayTerm & "' "
            _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate ( FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode, FCFinAmt, FNDay)"
            _Qry &= vbCrLf & "SELECT  FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode, FCFinAmt, FNDay "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(_EmpCode) & " "
            _Qry &= vbCrLf & "      AND FTPayYear = '" & _PayYear & "' "
            _Qry &= vbCrLf & "      AND FTPayTerm = '" & _PayTerm & "' "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

        Next

        Return True

    End Function

    Public Shared Function HolidayPaySpecialMoney() As Boolean
        Try
            Dim _Cmd As String = ""


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function CalculateWageDaily_VN(ByVal _User As String, ByVal _FTEmpCode As String,
      ByVal _EmpType As String, ByVal _StartDate As String, ByVal _EndDate As String) As Boolean

        '----------------------------------   Variable  ------------------------------------
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
        Dim _Qry As String
        _Qry = "SELECT TOP 1 Isnull(FTStatePayHarmful,'0') AS FTStatePayHarmful FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
        Dim _EmpTypePayHarmful As String = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

        _Qry = "SELECT TOP 1 Isnull(FTStatePaySkill,'0') AS FTStatePaySkill  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
        Dim _EmpTypePaySkill As String = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

        Dim _FDDateProbation As String

        Dim FNSocialEmployeeRate As Double = 0
        Dim FNSocialEmployerRate As Double = 0
        Dim FNHealthEmployeeRate As Double = 0
        Dim FNHealthEmployerRate As Double = 0
        Dim FNUnemploymentEmployeeRate As Double = 0
        Dim FNUnemploymentEmployerRate As Double = 0
        Dim FNUnionEmployeeRate As Double = 0
        Dim FNUnionEmployerRate As Double = 0

        Try
            FNSocialEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eSocialInsurance).FNEmployeeRate
            FNSocialEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eSocialInsurance).FNEmployerRate
            FNHealthEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eHealthInsurance).FNEmployeeRate
            FNHealthEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eHealthInsurance).FNEmployerRate
            FNUnemploymentEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnemploymentInsurance).FNEmployeeRate
            FNUnemploymentEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnemploymentInsurance).FNEmployerRate
            FNUnionEmployeeRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnionInsurance).FNEmployeeRate
            FNUnionEmployerRate = HCfg.HCfg_InsuranceVNRate(eTypeInsuranceVN.eUnionInsurance).FNEmployerRate
        Catch ex As Exception
            'MG.ShowMsg.mInfo("Invalid Config Value...", 1503310001, "", "")
        End Try


        Dim _dt As DataTable
        Dim _dttran As DataTable
        Dim _Err As Integer, _Complete As Integer, _ActualDate As String
        Dim _FCSalary As Double, _FDDateStart As String
        Dim FNHarmfulBaht As Double
        Dim FNSkillBaht As Double
        Dim _FDDateEnd As String
        Dim _FTDeptCode As String, _FTSectCode As String, _FTUnitCode As String
        Dim _FCOtherAdd As Double
        Dim _FCOtherDeduct As Double
        Dim _FTShift As String
        Dim _FNTime, _FNNotRegis As Double
        Dim _FNOT1 As Double, _FNOT1_5 As Double, _FNOT2 As Double, _FNOT3, _FNOT4 As Double
        Dim _FNLeaveVacation As Double, _FNLateNormalMin As Double
        Dim _FNLateNormalCut As Double, _FNLateOtMin As Double, _FNLateOtCut As Double
        Dim _FNLateMorning As Double, _FNLateAfternoon As Double, _FNAbsent As Double
        Dim _FNLeavePay, _FNLeaveNotPay As Double, _FNTimeMin, _FNOT1Min As Double
        Dim _FNOT1_5Min As Double, _FNOT2Min As Double, _FNOT3Min As Double, _FNOT4Min As Double, _FNLateMMin As Double
        Dim _FNLateAfMin As Double, _FNRetireMMin As Double, _FNRetireAfMin As Double
        Dim _FNRetireNormalCut As Double, _FNRetireOtMin As Double, _FNRetireOtCut As Double
        Dim _dtot As DataTable
        Dim _RateOT1, _RateOT15, _RateOT2, _RateOT3, _RateOT4 As Double

        Dim _FTStartCalculateDate As String, _FTEndCalculateDate As String
        Dim _FNEmpBaht, _FNEmpBahtOT1 As Double, _FNEmpBahtOT15 As Double, _FNEmpBahtOT2 As Double, _FNEmpBahtOT3 As Double, _FNEmpBahtOT4 As Double, _nBahtAbsent As Double, _nEstimateIncome As Double
        Dim _SocialRate As Double
        Dim _WorkDay As Integer, _TotalWorkDay As Integer, _Holiday As String
        Dim _TotalHoliDay As Integer
        Dim _FNSlaryPerMonth As Double, _FNSlaryPerDay As Double, _FNSlaryPerHour As Double, _FNSlaryPerMin, _FNSlaryOTPerMin As Double, _FNSlaryOTPerHour As Double, _FTEmpState As String
        Dim _Lapaid, _LaNotpaid As Double, _FCPayVacationBaht As Double, _Net As Double, _CalSo As Double, _HBaht As Double, _FCSocial As Double
        Dim _FCTax As Integer, _FCBaht As Double, _ActualNextDate As String
        Dim _SocialMinIncome As Integer, _SocialMaxIncome As Double
        Dim _FTSlary, _FTDivCode, _FTPos As String
        Dim _MSlary As Double, _LateCutAbsent As Double, _LateCutAmt, _LateCutAmtAbsent As Double
        Dim _Dtemp As DataTable
        Dim _FCAdd, _FCDeduct As Double

        Dim _dtLeave As DataTable
        Dim _LeaveCode As String = ""
        Dim _dtAddOtherAmtshift As DataTable

        Dim _ShiftAmt As Double = 0
        Dim _ShiftOTAmt As Double = 0
        Dim _ShiftValue As Double = 0
        Dim _ShiftOTValue As Double = 0

        Dim _ContributedFundBeginPay As Boolean = False
        Dim _DTHoliday As DataTable
        Dim _ShiftAdv As Double = 0
        Dim _AmtPlus As Double = 0
        Dim _GAmtPlus As Double = 0
        Dim FTHldType As Integer = 0
        Dim _SPDateType As Integer = 0
        Dim _ReturnVacationAmount As Double = 0
        Dim FDDateProbation As String = ""
        Dim FNWageScale As Double = 0

        Dim _DateStartOfMonth As String = HI.UL.ULDate.ConvertEnDB(Left(_EndDate, 8) & "01")  'วันแรกของเดือน
        Dim _DateEndOfMonth As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Left(_EndDate, 8) & "01", 1), -1)) 'วันแของเดือน
        Dim _FTStatePayHoliday As String = ""
        Dim tmpDTConfigAllowancePassProba As DataTable
        Dim FNSkillRate, FNHarmfulRate, FNMaximumBasicSalaries, FNModPersonTaxRate, FNModChildAllowanceTaxRate, FNThaiWorkerNoWorkpermitTaxRate As Integer
        tmpDTConfigAllowancePassProba = LoadConfigAllowanceProbation
        For Each oRow As DataRow In tmpDTConfigAllowancePassProba.Rows
            FNSkillRate = Val(oRow!FNSkillRate.ToString()) '...อัตราเปอร์เซ็นต์ การคิด ค่าทักษะ
            FNHarmfulRate = Val(oRow!FNHarmfulRate.ToString()) '...อัตราการเปอร์เซ็นต์ การคิด ค่าเสี่ยงภัย
            FNMaximumBasicSalaries = Val(oRow!FNMaximumBasicSalaries.ToString()) '...อัตราฐานเงินเดือนสูงสุด
            FNModPersonTaxRate = Val(oRow!FNModPersonTaxRate.ToString()) '...ลดหย่อนตนเอง
            FNModChildAllowanceTaxRate = Val(oRow!FNModChildAllowanceTaxRate.ToString()) '...ลดหย่อนบุตรต่อรายการบุตร 1 คน
            FNThaiWorkerNoWorkpermitTaxRate = Val(oRow!FNThaiWorkerNoWorkpermitTaxRate.ToString()) '...อัตราการคิดภาษีสำหรับ พนง. ที่มีสัญชาติไทย  และมีเลขที่บัตรประชาชน แต่ไม่มี Workpermit

            Exit For

        Next


        '------------------ GetConfig Holiday ----------------------------------


        _DTHoliday = LoadSysHoliday(Microsoft.VisualBasic.Left(_StartDate, 4))
        '------------------ GetConfig Holiday ----------------------------------

        _FTStartCalculateDate = _StartDate
        _FTEndCalculateDate = _EndDate
        _TotalWorkDay = 0 : _WorkDay = 0 : _TotalHoliDay = 0
        : _FNSlaryPerMonth = 0
        _FNSlaryPerDay = 0 : _FNSlaryPerHour = 0 : _FNSlaryPerMin = 0
        _FTEmpState = "" : _FNEmpBaht = 0 : _FNEmpBahtOT1 = 0
        _FNEmpBahtOT15 = 0 : _FNEmpBahtOT2 = 0 : _FNEmpBahtOT3 = 0
        _nBahtAbsent = 0 : _nEstimateIncome = 0 : _Lapaid = 0 : _LaNotpaid = 0 : _Net = 0
        _FCPayVacationBaht = 0 : _CalSo = 0 : _HBaht = 0 : _FCSocial = 0
        _FCTax = 0 : _FCBaht = 0 : _SocialRate = 0
        _SocialMinIncome = 0 : _SocialMaxIncome = 0
        _Complete = 0 : _Err = 0 : _FCSalary = -99

        _Qry = "SELECT  CONVERT(varchar(10),GETDATE(),111)"
        _ActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        _Qry = "SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111) "
        _ActualNextDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


        _Qry = "   SELECT  TOP 1  M.FNHSysCmpId As FTCmpCode, M.FNHSysEmpID AS FTEmpCode, M.FDDateStart, M.FDDateEnd, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus AS FTStatus, M.FNHSysEmpTypeId AS FTTypeEmp"
        _Qry &= vbCrLf & " ,M.FNHSysDeptId AS FTDeptCode "
        _Qry &= vbCrLf & "  ,M.FNHSysDivisonId AS FTDivCode, M.FNHSysSectId AS FTSectCode,  M.FNHSysUnitSectId AS FTUnitSecCode"
        _Qry &= vbCrLf & " , M.FNHSysPositId AS FTPositCode,'' as FTJobGrade,'' AS FTCostCNCode,M.FNLateCutSta AS FTLateCutSta"
        _Qry &= vbCrLf & "  , M.FNPaidOTSta AS FTPaidOTSta, M.FTEmpIdNo, M.FTSocialNo, M.FTTaxNo, M.FNCalSocialSta AS FTCalSocialSta, M.FNCalTaxSta AS FTCalTaxSta, M.FNHSysPayRollPayId AS FTPayCode"
        _Qry &= vbCrLf & " , M.FTAccNo, M.FNHSysBankId AS FTBnkCode, M.FNHSysBankBranchId AS FTBnkBchCode,M.FNSalary AS FTSalary, "
        _Qry &= vbCrLf & "  M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,"
        _Qry &= vbCrLf & "   ET.FNCalType AS FTCalType, ET.FNInsurType AS FTInsurType,M.FNMaritalStatus AS FTMaritalCode,M.FDFundBegin, M.FDFundEnd,"
        _Qry &= vbCrLf & " M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, "
        _Qry &= vbCrLf & " M.FCPremium, M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDisabledDependents,M.FCDeductDonateStudy, "
        _Qry &= vbCrLf & "  M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,M.FTHealthInsurIDMother,"
        _Qry &= vbCrLf & " M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,M.FTMateIncome,M.FCExceptAgeOver,M.FCExceptAgeOverMate,M.FCDeductDividend "
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(M.FdDateStart) = 1 AND ISDATE(M.FDRetire) = 1 THEN  Datediff(month,M.FdDateStart,M.FDRetire) ELSE 0 END AS FNWorkAge"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(M.FdDateStart) = 1 AND ISDATE(M.FDRetire) = 1 THEN  Datediff(month,M.FdDateStart,M.FDRetire) ELSE Datediff(month,M.FdDateStart,DateAdd(day,1,CONVERT(Datetime,'" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "'))) END AS FNEmpWorkAge"
        _Qry &= vbCrLf & " ,ISNULL(ET.FNSalaryDivide,0) AS FNSalaryDivide"
        _Qry &= vbCrLf & ",ISNULL(ET.FTStatePayHoliday,'') AS FTStatePayHoliday "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "	WHERE     (M.FNHSysEmpID =" & Val(_FTEmpCode) & " ) "

        _Dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _Dtemp.Rows

            _FTStatePayHoliday = R!FTStatePayHoliday.ToString
            _FTSlary = R!FTSalary.ToString : _FDDateStart = R!FDDateStart.ToString : _FDDateEnd = R!FDDateEnd.ToString
            _FTDeptCode = R!FTDeptCode.ToString : _FTDivCode = R!FTDivCode.ToString
            _FTSectCode = R!FTSectCode.ToString
            _FTUnitCode = R!FTUnitSecCode.ToString : _FTPos = R!FTPositCode.ToString
            _FTEmpState = R!FTCalType.ToString
            _FDDateProbation = R!FDDateProbation.ToString
            FDDateProbation = R!FDDateProbation.ToString

            _FCSalary = -99
            _FTSlary = (_FTSlary)

            If IsNumeric(_FTSlary) Then
                _MSlary = _FTSlary
                _FCSalary = CDbl(_FTSlary)



                If FDDateProbation <= _StartDate Then
                    If _EmpTypePayHarmful = "1" Then
                        FNHarmfulBaht = (_FCSalary * FNHarmfulRate) / 100
                    End If
                    If _EmpTypePaySkill = "1" Then
                        FNSkillBaht = ((_FCSalary + FNHarmfulBaht) * FNSkillRate) / 100
                    End If
                    _FCSalary = _FCSalary + FNHarmfulBaht + FNSkillBaht
                End If

                '027 Wage Scale*********
                FNWageScale = _GetWageScale(Double.Parse("0" & _FTEmpCode), _EmpType, _StartDate, _FCSalary)
                _FCSalary += FNWageScale

                '_SalaryCalOT = _FCSalary
                _FCSalary = Calculate.HelperRoundUpBasicSalary(_FCSalary)




                _RateOT1 = 0 : _RateOT15 = 0 : _RateOT2 = 0 : _RateOT3 = 0 : _RateOT4 = 0
                _AmtPlus = 0

                _Qry = " SELECT FTCfgOTCode,FCCfgOTValue,ISNULL(FCCfgOTAmtPlus,0) AS FCCfgOTAmtPlus  "
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTSet WITH (NOLOCK) "
                _Qry &= vbCrLf & "  WHERE  (FNCalType  = " & Val(_EmpType) & ")"
                _dtot = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                For Each R3 As DataRow In _dtot.Rows
                    Select Case R3!FTCfgOTCode.ToString.ToUpper
                        Case "01"
                            _RateOT1 = Val(R3!FCCfgOTValue.ToString)
                        Case "02"
                            _RateOT15 = Val(R3!FCCfgOTValue.ToString)
                        Case "03"
                            _RateOT2 = Val(R3!FCCfgOTValue.ToString)
                        Case "04"
                            _RateOT3 = Val(R3!FCCfgOTValue.ToString)
                            _AmtPlus = Val(R3!FCCfgOTAmtPlus.ToString)
                        Case "05"
                            _RateOT4 = Val(R3!FCCfgOTValue.ToString)
                    End Select

                Next



                _Qry = " SELECT     FTFinCode, FTType, FTCalType, FTPayType, FTStaTax, "
                _Qry &= vbCrLf & "   FTStaSocial, FTStaCalOT, FTStaLate, FTStaAbsent, FTStaLeave, FTStaVacation, FTStaRetire, FTStaHoliday, FNOTTimeM, FTOTTime, FTStaCheckLate, FTLateMin,"
                _Qry &= vbCrLf & "    FTStaCheckLeave, FTLeaveMin, FTStaCheckWorkTime, FTCheckWorkTimeMin, FTStaMaternityleaveNotpay, FTStaActive"
                _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMFinanceSet"
                _Qry &= vbCrLf & "  WHERE        (FTFinCode = N'001') OR  (FTFinCode = N'007') "
                _dtAddOtherAmtshift = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                _FCOtherAdd = 0 : _FCOtherDeduct = 0

                _Qry = "  SELECT   FTLeaveType AS LFTLeaveCode,Case WHEN FTLeaveType='98' Then 1 Else CASE WHEN FTLeaveType='97' THEN 2 ELSE 0 END  END AS LeaveType"
                _Qry &= vbCrLf & " ,     SUM(CASE WHEN ISNULL(FNTotalMinute,0) >= 480 THEN 480   ELSE  ISNULL(FNTotalMinute,0)   END) AS FNTotalMinute"
                _Qry &= vbCrLf & " , SUM( CASE WHEN ISNULL(FNTotalPayMinute,0) >= 480 THEN 480   ELSE ISNULL(FNTotalPayMinute,0)   END ) AS FNTotalPayMinute"
                _Qry &= vbCrLf & " , SUM( CASE WHEN ISNULL(FNTotalNotPayMinute,0) >= 480 THEN 480 ELSE ISNULL(FNTotalNotPayMinute,0)   END ) AS FNTotalNotPayMinute"
                _Qry &= vbCrLf & " , FTDateTrans"
                _Qry &= vbCrLf & " ,ISNULL(FTStaCalSSO,'N') AS FTStaCalSSO "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)"
                _Qry &= vbCrLf & "    WHERE (FNHSysEmpID =" & Val(_FTEmpCode) & " )"


                _Qry &= vbCrLf & " 	AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "' "
                _Qry &= vbCrLf & " 	AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "' "

                _Qry &= vbCrLf & " GROUP BY FTDateTrans,Case WHEN FTLeaveType='98' Then 1 Else 0 END,ISNULL(FTStaCalSSO,'N'),FTLeaveType"

                _dtLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                _Qry = " 	SELECT  ISNULL(T.FNHSysShiftID,0) AS FTShift	"
                _Qry &= vbCrLf & " ,(ISNULL(FNTimeMin,0) + ISNULL(FNLateNormalMin,0) )- (ISNULL(FNLateNormalCut,0) + ISNULL(FNAbsentCut,0) )     AS FNTime"
                _Qry &= vbCrLf & " 	, ISNULL(T.FNNotRegis,0) As FNNotRegis 	, ISNULL(FNOT1,0) AS FNOT1"
                _Qry &= vbCrLf & " 	, ISNULL(FNOT1_5,0) AS FNOT1_5"
                _Qry &= vbCrLf & " 	, ISNULL(FNOT2,0 ) AS FNOT2  , ISNULL(FNOT3,0) AS FNOT3, ISNULL(FNOT4,0) AS FNOT4"
                _Qry &= vbCrLf & " 	, ISNULL(FNLateNormalMin,0) AS FNLateNormalMin, ISNULL(FNLateNormalCut,0 )   AS FNLateNormalCut"
                _Qry &= vbCrLf & " , ISNULL(FNLateOtMin,0) As FNLateOtMin,ISNULL(FNLateOtCut,0) As FNLateOtCut"
                _Qry &= vbCrLf & " , ISNULL(FNLateMMin,0) As FNLateMorning"
                _Qry &= vbCrLf & " 	, ISNULL(FNLateAfMin,0) AS FNLateAfternoon,Isnull(FNAbsentCut,0) AS FNAbsentCut "
                _Qry &= vbCrLf & " 	, (CASE WHEN ISNULL(FNAbsentSP,0) = ISNULL(FNAbsent,0) THEN 0 ELSE  ISNULL(FNAbsent,0)  END ) AS FNAbsent_Cut "
                _Qry &= vbCrLf & " 	, ISNULL(FNCutAbsent,0) AS FNAbsent "
                '_Qry &= vbCrLf & " ,(ISNULL(FNTimeMin,0) + ISNULL(FNLateNormalMin,0) )- (ISNULL(FNLateNormalCut,0) + ISNULL(FNAbsentCut,0) )   AS FNTimeMin"
                '_Qry &= vbCrLf & " ,ISNULL(FNTimeMin,0) As FNTimeMinOrg"
                _Qry &= vbCrLf & " ,(ISNULL(T.FNTimeMin,0) + ISNULL(T.FNSpecialTimeMin,0) + ISNULL(T.FNLateNormalMin,0) )- (ISNULL(T.FNLateNormalCut,0) + ISNULL(T.FNAbsentCut,0) )   AS FNTimeMin"
                _Qry &= vbCrLf & " ,(ISNULL(T.FNTimeMin,0) + ISNULL(T.FNSpecialTimeMin,0)) As FNTimeMinOrg"
                _Qry &= vbCrLf & " , ISNULL(FNOT1Min,0) As FNOT1Min  "
                _Qry &= vbCrLf & " , ISNULL(FNOT1_5Min,0) As FNOT1_5Min "
                _Qry &= vbCrLf & " ,ISNULL(FNOT2Min,0) As FNOT2Min "
                _Qry &= vbCrLf & " , ISNULL(FNOT3Min,0) As FNOT3Min, ISNULL(FNOT4Min,0) As FNOT4Min "
                _Qry &= vbCrLf & " ,ISNULL( FNLateMMin,0) AS FNLateMMin "
                _Qry &= vbCrLf & " , ISNULL(FNLateAfMin,0) AS FNLateAfMin"
                _Qry &= vbCrLf & " , ISNULL(FNRetireMMin,0) AS FNRetireMMin "
                _Qry &= vbCrLf & " ,ISNULL(FNRetireAfMin,0 )  as FNRetireAfMin"
                _Qry &= vbCrLf & " , ISNULL(FNRetireNormalCut,0) As FNRetireNormalCut "
                _Qry &= vbCrLf & " , ISNULL(FNRetireOtMin,0) AS FNRetireOtMin"
                _Qry &= vbCrLf & " ,ISNULL(FNRetireOtCut,0) AS FNRetireOtCut,FTDateTrans"
                _Qry &= vbCrLf & " ,ISNULL(T.FTIn1,'') AS FTIn1"
                _Qry &= vbCrLf & " ,ISNULL(T.FTOut1,'') AS FTOut1"
                _Qry &= vbCrLf & " ,ISNULL(T.FTIn2,'') AS FTIn2"
                _Qry &= vbCrLf & " ,ISNULL(T.FTOut2,'') AS FTOut2"
                _Qry &= vbCrLf & " ,ISNULL(T.FTIn3,'') AS FTIn3"
                _Qry &= vbCrLf & " ,ISNULL(T.FTOut3,'') AS FTOut3"
                _Qry &= vbCrLf & " ,P.FTOverClock,T.FTWeekDay"

                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) LEFT OUTER JOIN   THRMTimeShift AS P WITH(NOLOCK) ON T.FNHSysShiftID =P.FNHSysShiftID  "
                _Qry &= vbCrLf & "  WHERE(T.FNHSysEmpID =" & Val(_FTEmpCode) & " )"
                _Qry &= vbCrLf & " 	AND T.FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "' "
                _Qry &= vbCrLf & " 	AND T.FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(_FTEndCalculateDate) & "' "

                _dttran = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                _LateCutAbsent = 0 : _LateCutAmt = 0 : _LateCutAmtAbsent = 0
                _TotalHoliDay = 0

                Dim _oHoliday As Integer = 0

                _TotalWorkDay = 0

                _FCAdd = 0 : _FCDeduct = 0


                If _FDDateStart > _FTStartCalculateDate Then _FTStartCalculateDate = _FDDateStart

                Do While _FTStartCalculateDate <= _FTEndCalculateDate And (_FDDateEnd = "" Or _FTStartCalculateDate <= _FDDateEnd)

                    _ShiftAmt = 0
                    _ShiftValue = 0
                    _ShiftOTValue = 0
                    _ShiftOTAmt = 0
                    _oHoliday = 0
                    FTHldType = 0

                    Dim _WorkDayPerMonth As Integer = _GetTermYearPay(_FTStartCalculateDate, _FTEndCalculateDate, Integer.Parse(_EmpType))
                    '_FCSalary = _MSlary
                    'If _FDDateProbation <= _FTStartCalculateDate Then
                    '    If _EmpTypePayHarmful = "1" Then
                    '        FNHarmfulBaht = (_FCSalary * FNHarmfulRate) / 100
                    '    End If
                    '    If _EmpTypePaySkill = "1" Then
                    '        FNSkillBaht = ((_FCSalary + FNHarmfulBaht) * FNSkillRate) / 100
                    '    End If
                    '    _FCSalary = _FCSalary + FNHarmfulBaht + FNSkillBaht
                    'End If

                    Dim _NewSlary As String

                    _Qry = "  SELECT      TOP 1   FNCurrentSlary  AS AMT"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChangeSlary"
                    _Qry &= vbCrLf & "  WHERE        (FTEffectiveDate > N'" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "') "
                    _Qry &= vbCrLf & "  AND  (FNHSysEmpID = " & Val(_FTEmpCode) & ")"
                    _Qry &= vbCrLf & "  ORDER BY FTEffectiveDate ASC "
                    _NewSlary = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


                    If IsNumeric(_NewSlary) Then _FCSalary = CDbl(_NewSlary)

                    _Holiday = ""

                    For Each IR As DataRow In _DTHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'")
                        _Holiday = "H"
                        FTHldType = Val(IR!FTHldType.ToString)
                        Exit For
                    Next

                    _FTShift = ""

                    _FNTime = 0
                    _FNNotRegis = 0
                    _FNOT1 = 0 : _FNOT1_5 = 0 : _FNOT2 = 0
                    _FNOT3 = 0 : _FNOT4 = 0
                    _FNLateNormalMin = 0 : _FNLateNormalCut = 0
                    _FNLateOtMin = 0 : _FNLateOtCut = 0
                    _FNLateMorning = 0 : _FNLateAfternoon = 0
                    _LateCutAbsent = 0 : _FNAbsent = 0
                    _FNTimeMin = 0 : _FNOT1Min = 0

                    _FNOT1_5Min = 0 : _FNOT2Min = 0
                    _FNOT3Min = 0 : _FNOT4Min = 0
                    _FNLateMMin = 0 : _FNLateAfMin = 0
                    _FNRetireMMin = 0 : _FNRetireAfMin = 0
                    _FNRetireNormalCut = 0 : _FNRetireNormalCut = 0
                    _FNRetireOtMin = 0 : _FNRetireOtMin = 0
                    _FNRetireOtCut = 0
                    _FNLeavePay = 0 : _FNLeaveVacation = 0
                    _FNLeaveNotPay = 0
                    _LeaveCode = ""

                    Dim _InOT As String = "" : Dim _OutOT As String = "" : Dim _Over As String = ""
                    Dim _R() As DataRow = _dttran.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'")
                    For Each R2 In _R

                        _FTShift = R2!FTShift.ToString
                        _FNTime = IIf(Val(R2!FNTime.ToString) < 0, 0, Val(R2!FNTime.ToString))
                        _FNTimeMin = IIf(Val(R2!FNTimeMin.ToString) < 0, 0, Val(R2!FNTimeMin.ToString))
                        _FNNotRegis = Val(R2!FNNotRegis.ToString)
                        _FNOT1 = Val(R2!FNOT1.ToString) : _FNOT1_5 = Val(R2!FNOT1_5.ToString) : _FNOT2 = Val(R2!FNOT2.ToString)
                        _FNOT3 = Val(R2!FNOT3.ToString) : _FNOT4 = Val(R2!FNOT3.ToString)
                        _FNOT1Min = Val(R2!FNOT1Min.ToString)
                        _FNOT1_5Min = Val(R2!FNOT1_5Min.ToString) : _FNOT2Min = Val(R2!FNOT2Min.ToString)
                        _FNOT3Min = Val(R2!FNOT3Min.ToString) : _FNOT4Min = Val(R2!FNOT4Min.ToString)


                        _InOT = R2!FTIn3.ToString
                        _OutOT = R2!FTOut3.ToString

                        _Over = R2!FTOverClock.ToString

                        If _FTShift <> "" And (_FNTime + _FNOT1Min + _FNOT1_5Min + _FNOT2Min + _FNOT3Min + _FNOT4Min > 0) Then
                            _ShiftValue = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FCShiftAmt FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift WHERE FNHSysShiftID=" & Val(_FTShift) & " ", Conn.DB.DataBaseName.DB_HR, "0"))


                            _TotalWorkDay = _TotalWorkDay + 1

                            _ShiftOTValue = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FCShiftOTAmt FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift WHERE FNHSysShiftID=" & Val(_FTShift) & " ", Conn.DB.DataBaseName.DB_HR, "0"))

                            '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------
                            If _FTShift <> "" And (_FNTimeMin + _FNOT1_5Min + _FNOT3Min + _FNOT1Min + _FNOT2Min + _FNOT4Min) > 0 Then

                                _SPDateType = 0

                                _Holiday = ""

                                For Each IR As DataRow In _DTHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'")
                                    _Holiday = "H"
                                    Exit For
                                Next

                                If _Holiday <> "" Then _SPDateType = 2

                                Dim _StateLeaveOther As Boolean = False
                                Dim _StateLeavacation As Boolean = False
                                Dim _StateFTStaMaternityleaveNotpay As Boolean = False
                                Dim _SumLeave As Integer = 0

                                For Each sR As DataRow In _dtLeave.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'")
                                    _SumLeave = _SumLeave + Val(sR!FNTotalMinute)

                                    If Val(sR!LeaveType) = 1 Then
                                        _StateLeavacation = True
                                    Else
                                        _StateLeaveOther = True
                                    End If

                                    If Val(sR!LeaveType) = 2 Then
                                        _StateFTStaMaternityleaveNotpay = True
                                    End If
                                Next

                                '--------------------------- ค่ากะ -------------------------------------
                                For Each RFin As DataRow In _dtAddOtherAmtshift.Select("FTFinCode='001' ")
                                    Dim _StatePass As Boolean = True

                                    If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                    If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                    If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                    If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                    If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)
                                    If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                        _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                    End If

                                    If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                    If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                    If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                        Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                        Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                        If _STime.Length = _ETime.Length Then
                                            If IsDate(_STime) And IsDate(_ETime) Then
                                                If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If
                                        Else
                                            _StatePass = False
                                        End If

                                    End If

                                    If RFin!FNOTTimeM.ToString <> "" And _StatePass Then
                                        If Val(RFin!FNOTTimeM.ToString) > 0 Then

                                            If _FNOT1 + _FNOT2 + _FNOT4 > 0 Then
                                                If (_FNOT1 + _FNOT2 + _FNOT4) < Val(RFin!FNOTTimeM.ToString) Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If

                                        End If
                                    End If

                                    If RFin!FTStaVacation.ToString = "1" Then _StatePass = Not (_StateLeavacation)

                                    If _StatePass Then
                                        _ShiftAmt = _ShiftValue
                                    End If
                                Next
                                '--------------------------- ค่ากะ -------------------------------------

                                '--------------------------- ค่ากะ OT ----------------------------------
                                For Each RFin As DataRow In _dtAddOtherAmtshift.Select("FTFinCode='007' ")
                                    Dim _StatePass As Boolean = True

                                    If _OutOT <> "" Then
                                        Beep()
                                    End If
                                    If RFin!FTStaLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= 0)
                                    If RFin!FTStaCheckLate.ToString = "1" And _StatePass Then _StatePass = (_FNLateNormalMin <= Val(RFin!FTLateMin.ToString))
                                    If RFin!FTStaAbsent.ToString = "1" And _StatePass Then _StatePass = (_FNAbsent <= 0)
                                    If RFin!FTStaLeave.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeaveOther)
                                    If RFin!FTStaVacation.ToString = "1" And _StatePass Then _StatePass = Not (_StateLeavacation)
                                    If RFin!FTStaHoliday.ToString = "1" And _StatePass Then _StatePass = Not (_SPDateType = 0)
                                    If RFin!FTStaCheckWorkTime.ToString = "1" And _StatePass Then
                                        _StatePass = Not ((_FNTimeMin + _FNOT1_5Min + _FNOT3Min) < Val(RFin!FTCheckWorkTimeMin.ToString))
                                    End If

                                    If RFin!FTStaCheckLeave.ToString = "1" And _StatePass Then _StatePass = Not ((_SumLeave) < Val(RFin!FTLeaveMin.ToString))
                                    If RFin!FTStaMaternityleaveNotpay.ToString = "1" And _StatePass Then _StatePass = Not (_StateFTStaMaternityleaveNotpay)

                                    If RFin!FTOTTime.ToString <> "" And _StatePass Then
                                        Dim _STime As String = (IIf(_Over > _OutOT, _ActualNextDate, _ActualDate)) & " " & _OutOT
                                        Dim _ETime As String = (IIf(_Over > RFin!FTOTTime.ToString, _ActualNextDate, _ActualDate)) & " " & RFin!FTOTTime.ToString.Replace(".", ":")

                                        If _STime.Length = _ETime.Length Then
                                            If IsDate(_STime) And IsDate(_ETime) Then
                                                If CDate(_STime) < CDate(_ETime) Or _InOT = "" Or _OutOT = "" Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If
                                        Else
                                            _StatePass = False
                                        End If

                                    End If

                                    If RFin!FNOTTimeM.ToString <> "" And _StatePass Then
                                        If Val(RFin!FNOTTimeM.ToString) > 0 Then

                                            If _FNOT1 + _FNOT2 + _FNOT4 > 0 Then
                                                If (_FNOT1 + _FNOT2 + _FNOT4) < Val(RFin!FNOTTimeM.ToString) Then
                                                    _StatePass = False
                                                End If
                                            Else
                                                _StatePass = False
                                            End If

                                        End If
                                    End If


                                    If RFin!FTStaVacation.ToString = "1" Then _StatePass = Not (_StateLeavacation)

                                    If _StatePass Then
                                        _ShiftOTAmt = _ShiftOTValue
                                    End If
                                Next
                                '--------------------------- ค่ากะ OT ----------------------------------

                            End If
                            '----------------- รายได้อื่นๆประจำวัน กรณีมาทำงาน---------------------

                        End If

                    Next






                    If _FTEmpState = "2" Or _FTEmpState = "3" Then
                        _FNSlaryPerMonth = CDbl(Format((_FCSalary), "0.00"))

                        If _FTEmpState = "3" Then
                            _FNSlaryPerMonth = CDbl(Format(_FNSlaryPerMonth / 2, "0.00"))
                        End If

                        _FNSlaryPerDay = CDbl(Format((_FCSalary) / _WorkDayPerMonth, "0.00"))
                    Else
                        _FNSlaryPerMonth = 0
                        _FNSlaryPerDay = CDbl(Format((_FCSalary) / _WorkDayPerMonth, "0.00"))
                    End If

                    _FNSlaryPerHour = CDbl(Format(_FNSlaryPerDay / 8, "0.00000000000"))
                    _FNSlaryPerMin = CDbl(Format(_FNSlaryPerHour / 60, "0.00000000000"))
                    _FNSlaryOTPerMin = CDbl(Format(CDbl(Format((_FNSlaryPerDay) / 8, "0.00000000000")) / 60, "0.00000000000"))
                    _FNSlaryOTPerHour = CDbl(Format((_FNSlaryPerDay) / 8, "0.00"))

                    If _FTShift = "" Then
                        If _Holiday <> "" Then
                            _oHoliday = 1
                            _TotalHoliDay = _TotalHoliDay + 1
                        End If
                    Else
                        If _Holiday <> "" Then
                            _oHoliday = 1
                            _TotalHoliDay = _TotalHoliDay + 1
                        End If

                        If (_FNTime + _FNOT1Min + _FNOT1_5Min + _FNOT2Min + _FNOT3Min + _FNOT4Min > 0) Then
                            _WorkDay = _WorkDay + 1
                        End If

                    End If

                    _FNLeavePay = 0 : _FNLeaveVacation = 0
                    _FNLeaveNotPay = 0

                    For Each sR As DataRow In _dtLeave.Select("FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'")
                        _LeaveCode = sR!LFTLeaveCode.ToString

                        _FNLeaveNotPay = Val(sR!FNTotalNotPayMinute.ToString)

                        If Val(sR!LeaveType) = 1 Then
                            _FNLeaveVacation = Val(sR!FNTotalPayMinute.ToString)
                        Else
                            _FNLeavePay = Val(sR!FNTotalPayMinute.ToString)
                        End If
                    Next


                    _FNEmpBaht = CDbl(Format((_FNTimeMin) * _FNSlaryPerMin, "0.00"))


                    _FNEmpBahtOT1 = CDbl(Format((_FNOT1Min) * ((_FNSlaryOTPerMin) * _RateOT1), "0.000000"))


                    If FTHldType = 1 And _FNOT3Min > 0 Then
                        _GAmtPlus = _GAmtPlus + _AmtPlus
                    End If

                    _FNEmpBahtOT15 = CDbl(Format((_FNOT1_5Min) * ((_FNSlaryOTPerMin) * _RateOT15), "0.00"))
                    _FNEmpBahtOT2 = CDbl(Format((_FNOT2Min) * ((_FNSlaryOTPerMin) * _RateOT2), "0.00"))
                    _FNEmpBahtOT3 = CDbl(Format((_FNOT3Min) * ((_FNSlaryOTPerMin) * _RateOT3), "0.00"))
                    _FNEmpBahtOT4 = CDbl(Format((_FNOT4Min) * ((_FNSlaryOTPerMin) * _RateOT4), "0.00"))

                    _nBahtAbsent = _nBahtAbsent + CDbl(Format(_FNAbsent * _FNSlaryPerMin, "0.00"))
                    _LateCutAmt = _LateCutAmt + CDbl(Format((_FNLateNormalCut) * _FNSlaryPerMin, "0.00"))
                    _LateCutAmtAbsent = _LateCutAmtAbsent + CDbl(Format((_LateCutAbsent) * _FNSlaryPerMin, "0.00"))

                    _LaNotpaid = _LaNotpaid + CDbl(Format(_FNLeaveNotPay * _FNSlaryPerMin, "0.00"))
                    Dim _TmpLapaidAmt As Double = CDbl(Format(_FNLeavePay * _FNSlaryPerMin, "0.00"))
                    _Lapaid = _Lapaid + _TmpLapaidAmt

                    If _LeaveCode <> "" And _FNLeaveVacation > 0 Then
                        _FCPayVacationBaht = _FCPayVacationBaht + CDbl(Format(_FNLeaveVacation * _FNSlaryPerMin, "0.00"))
                    Else
                        _FCPayVacationBaht = _FCPayVacationBaht + CDbl(Format(_FNLeaveVacation * _FNSlaryPerMin, "0.00"))
                    End If

                    If _FTStatePayHoliday <> "1" Then '--------- ไม่ได้ค่าจ้างวันหยุด---------------
                    Else


                        If _FNLeaveNotPay <= 0 Then
                            If _TmpLapaidAmt <= 0 Then ' กรณีไม่มีลาจ่ายในวันนักขัต 
                                _HBaht = _HBaht + CDbl(Format(_oHoliday * _FNSlaryPerDay, "0.00"))
                            Else ' กรณีมีลาจ่ายในวันนักขัต  ไม่ได้นักขัต ได้ลาจ่าย
                                _TotalHoliDay = _TotalHoliDay - _oHoliday
                            End If
                        Else ' กรณีมีลาไม่จ่ายในวันนักขัต  ไม่ได้นักขัต
                            _TotalHoliDay = _TotalHoliDay - _oHoliday
                        End If

                    End If

                    _Qry = "Select Top 1 FNHSysEmpID, FTDateTrans "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_FTEmpCode) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'  "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                        _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                        _Qry &= vbCrLf & " SET FNAmtNormal=" & (_FNEmpBaht + _FNEmpBahtOT15 + _FNEmpBahtOT3) & ""
                        _Qry &= vbCrLf & ", FNAmtOT=" & (_FNEmpBahtOT1 + _FNEmpBahtOT2 + _FNEmpBahtOT4) & " "
                        _Qry &= vbCrLf & ", FNNetAmt=" & (_FNEmpBaht + _FNEmpBahtOT15 + _FNEmpBahtOT3) + (_FNEmpBahtOT1 + _FNEmpBahtOT2 + _FNEmpBahtOT4) & " "
                        _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_FTEmpCode) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'  "
                    Else
                        _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                        _Qry &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime,  FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT,FNProOther, FNNetProAmt) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ," & Val(_FTEmpCode) & ",'" & HI.UL.ULDate.ConvertEnDB(_FTStartCalculateDate) & "'"
                        _Qry &= vbCrLf & " ," & (_FNEmpBaht + _FNEmpBahtOT15 + _FNEmpBahtOT3) & ""
                        _Qry &= vbCrLf & " ," & (_FNEmpBahtOT1 + _FNEmpBahtOT2 + _FNEmpBahtOT4) & ""
                        _Qry &= vbCrLf & " ," & (_FNEmpBaht + _FNEmpBahtOT15 + _FNEmpBahtOT3) + (_FNEmpBahtOT1 + _FNEmpBahtOT2 + _FNEmpBahtOT4) & ""
                        _Qry &= vbCrLf & " ,0,0,0,0"
                    End If

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _FTStartCalculateDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_FTStartCalculateDate, 1))

                Loop

            End If

        Next

        Return True

    End Function

    Public Shared Function _GetTermYearPay(ByVal _SDate As String, ByVal _EDate As String, ByVal _EmpType As Integer) As Integer
        Try
            Dim _Qry As String = "" : Dim _day As Integer = 0
            _Qry = "SELECT Top 1     FNWorkDay"
            _Qry &= vbCrLf & "FROM THRMCfgPayDT WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE  FDCalDateBegin <= '" & _SDate & "'"
            _Qry &= vbCrLf & "and FDCalDateEnd >= '" & _EDate & "'"
            _Qry &= vbCrLf & "and FNHSysEmpTypeId =" & Integer.Parse(_EmpType)
            _day = Integer.Parse(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "26"))
            Return IIf(_day > 26, 26, _day)
        Catch ex As Exception
            Return 26
        End Try
    End Function

    Public Shared Function _GetNewSalary(ByVal _EmpId As Integer) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 FTNewSlary From THRTEmployeeUpdateSlary"
            _Cmd &= vbCrLf & " where FTEffectiveDate <= " & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & " and FTStateActive = '1'"
            _Cmd &= vbCrLf & " and FNHSysEmpID =" & Integer.Parse(Val(_EmpId))
            _Cmd &= vbCrLf & "Order by FNSeq Desc"
             Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function _GetWageScale(_EmpId As Integer, _EmpType As Integer, _DateStart As String, _Salary As Double) As Double
        Try
            Dim _Cmd As String = ""
            Dim _Wage As Double = 0
            Dim _oDtWageRate As DataTable
            Dim _WorkAge As Integer = 0
            Dim _WorkDateStart As String = ""
            Dim _DateVel As String = ""
            Dim _CSalary As Double = 0
            Dim _Wageper As Double = 0
            '1306010002 M
            '1408150001 N

            _Cmd = "Select Top 1 FDDateStart From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FNHSysEmpID = " & Integer.Parse("0" & _EmpId)
            _WorkDateStart = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "")

            _Cmd = "Select Top 1  datediff(month,convert(date,FDDateStart),convert(date,'" & _DateStart & "')) AS FTMonth  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FNHSysEmpID=" & Integer.Parse("0" & _EmpId)
            _WorkAge = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")

            _Cmd = "Select FNWorkAgeMonthStrat , FNWorkAgeMonthEnd , FNWageScaleTotal ,FNWageLevelType From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMWageScale WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FNHSysEmpTypeId=" & Integer.Parse("0" & _EmpType)
            _Cmd &= vbCrLf & "and FNWorkAgeMonthStrat <=" & _WorkAge
            _Cmd &= vbCrLf & " And Isnull(FTStateActive,'0') = '1'"
            _Cmd &= vbCrLf & "Order by FNWageLevelType ASC"
            _oDtWageRate = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            For Each R As DataRow In _oDtWageRate.Select("FNWorkAgeMonthStrat > 0", " FNWorkAgeMonthStrat DESC")


                If (Integer.Parse("0" & _EmpType) = 1306010002 Or Integer.Parse("0" & _EmpType) = 1408150001) Then
                    _DateVel = HI.Conn.SQLConn.GetField("Select convert(varchar(10), dateadd(Month," & Integer.Parse("0" & R!FNWorkAgeMonthStrat.ToString) & ",'" & _WorkDateStart & "'),111)", Conn.DB.DataBaseName.DB_HR, "")

                    _Cmd = "SELECT Top 1  R.FNSalary + R.FNHarmfulBaht+ R.FNSkillBaht AS FTSalary"
                    _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgPayDT AS D WITH(NOLOCK) INNER JOIN  "
                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTPayRoll AS R WITH(NOLOCK) ON D.FTPayYear = R.FTPayYear and D.FTPayTerm = R.FTPayTerm"
                    _Cmd &= vbCrLf & "WHERE  (D.FNHSysEmpTypeId = " & Integer.Parse("0" & _EmpType) & ")  And R.FNHSysEmpID =" & Integer.Parse("0" & _EmpId)
                    _Cmd &= vbCrLf & "and FDCalDateBegin <= '" & _DateVel & "' and FDCalDateEnd >= '" & _DateVel & "' "
                    _CSalary = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")

                    If _CSalary = 0 Then
                        _Cmd = "Select Top 1  FNNewSlary "
                        _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeMasterChangeSlary WITH(NOLOCK) "
                        _Cmd &= vbCrLf & " WHERE FTEffectiveDate <='" & _DateVel & "'"
                        _Cmd &= vbCrLf & " And FNHSysEmpId =" & Integer.Parse("0" & _EmpId)
                        _Cmd &= vbCrLf & "Order by FTEffectiveDate desc"
                        _CSalary = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")
                    End If
                End If
                Exit For
            Next


            For Each R As DataRow In _oDtWageRate.Rows
                If (Integer.Parse("0" & _EmpType) = 1306010002 Or Integer.Parse("0" & _EmpType) = 1408150001) Then
                    _CSalary += _Wageper
                    _Wageper = (_CSalary * Double.Parse("0" & R!FNWageScaleTotal.ToString)) / 100
                    _Wage += _Wageper
                Else
                    _Salary += _Wageper
                    _Wageper = (_Salary * Double.Parse("0" & R!FNWageScaleTotal.ToString)) / 100
                    _Wage += _Wageper
                End If
            Next

            Return _Wage
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

#Region "Procedure"

    Public Shared Sub LoadIncomeTax(ByVal FTEmpIdNo As String, ByVal PayYear As String, ByVal PayTerm As String, _
                              ByRef BeforeIncome As Double, ByRef BeforeTax As Double, ByRef BeforeSocial As Double, _
                              ByRef CountTerm As Double, ByRef _TotalCalContributedAcc As Double, ByRef _TotalCalWorkmenAcc As Double, FNHSysEmpID As Integer)

        Dim _Qry As String
        Dim _dt As DataTable

        _Qry = " Select  Count(P.FNTotalIncome) As CTerm , 0 As FCTotalIncome" 'SUM(ISNULL(P.FNTotalIncome,0)) ' change by Noh
        _Qry &= " , SUM(ISNULL(P.FNSocial,0)) As FCSocial"
        _Qry &= ", SUM(ISNULL(P.FNTax,0)) As FCTax"
        _Qry &= ", SUM(ISNULL(P.FNContributedAmt,0) ) As FTContributedAmt"
        _Qry &= ", SUM(ISNULL(P.FNWorkmenAmt,0)  ) As FTWorkmenAmt"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll As P With (NOLOCK)"
        _Qry &= vbCrLf & " WHERE P.FTPayYear = '" & (PayYear) & "'"
        _Qry &= vbCrLf & " AND P.FTPayTerm < '" & (PayTerm) & "'"
        _Qry &= vbCrLf & " AND ISNULL(P.FNTotalIncome,0) > 0"
        _Qry &= vbCrLf & " AND P.FTEmpIdNo ='" & HI.UL.ULF.rpQuoted(FTEmpIdNo) & "' AND P.FTEmpIdNo <>'' "
        '_Qry &= vbCrLf & " AND P.FTEmpIdNo ='" & HI.UL.ULF.rpQuoted(FTEmpIdNo) & "' AND P.FTEmpIdNo <>'' "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each _Row As DataRow In _dt.Rows

            BeforeIncome = Val(_Row!FCTotalIncome.ToString)
            BeforeTax = Val(_Row!FCTax.ToString)
            BeforeSocial = Val(_Row!FCSocial.ToString)
            CountTerm = Val(_Row!CTerm.ToString)
            _TotalCalContributedAcc = Val(_Row!FTContributedAmt.ToString)
            _TotalCalWorkmenAcc = Val(_Row!FTWorkmenAmt.ToString)

        Next

        If _dt.Rows.Count <= 0 Then

            _Qry = " SELECT TOP 1  FTPayYear "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Integer.Parse(Val(FNHSysEmpID)) & " "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                '_Qry = "SELECT TOP 1 FCIncomeBefore,FCTaxBefore,FCSocialBefore FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK)  "
                '_Qry &= vbCrLf & "WHERE LEFT(FDDateStart,4) = '" & (PayYear) & "'"
                '_Qry &= vbCrLf & "AND FTEmpIdNo ='" & HI.UL.ULF.rpQuoted(FTEmpIdNo) & "' AND FTEmpIdNo <>''     "
                _Qry = "SELECT TOP 1 FCIncomeBefore,FCTaxBefore,FCSocialBefore FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID =" & Integer.Parse(Val(FNHSysEmpID)) & " AND FTTaxNo <>''     "

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each _Row As DataRow In _dt.Rows
                    BeforeIncome = BeforeIncome + Val(_Row!FCIncomeBefore.ToString)
                    BeforeTax = BeforeTax + Val(_Row!FCTaxBefore.ToString)
                    BeforeSocial = BeforeSocial + Val(_Row!FCSocialBefore.ToString)
                Next
            End If

        End If

        _dt.Dispose()

    End Sub

    Private Shared Sub GetConfig(ByRef _RateNotStudied As Double, ByRef _RateStudy As Double, ByRef _MaxNumberOfChildren As Double, _
           ByRef _DeducToTheFund As Double, ByRef _DeducToTheFundBoss As Double, _
           ByRef _ContributedToTheFund As Double, ByRef _ContributedIncomeMax As Double)

        Dim _Qry As String
        Dim _Dt As DataTable

        _Qry = "    SELECT       FTKeyCode, FTKeyValue"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfig"
        _Qry &= vbCrLf & "  WHERE       (FTKeyCode = N'Cfg_ContributedDeducToTheFund') "
        _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ContributedDeducToTheFundBoss') "
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ContributedIncomeMax') "
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ContributedToTheFund') "
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateNotStudied')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateNumberOfChildren')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModChildAllowanceRateStudy')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModDeductibleDonations')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModFatherReduction')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModInsurancePremiums')"
        _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModLendingforHousing')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModLTF')"
        _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModLTFChk')"
        _Qry &= vbCrLf & "  OR (FTKeyCode = N'Cfg_ModMateFatherReduction')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModMateMotherReduction')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModMotherReduction')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModPersonalExpen')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModPersonalExpenChk')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRateReductionsByMarital')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRateReductionsBySingle')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRMF')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModRMFChk')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnly')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlyChk')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlytheExcess')"
        _Qry &= vbCrLf & " 	OR (FTKeyCode = N'Cfg_ModSavingsFundOnlytheExcessChk')"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _Dt.Rows

            Select Case R!FTKeyCode.ToString.ToUpper
                '-------------ลดหย่อนบุตร-----------------
                Case "Cfg_ModChildAllowanceRateNotStudied".ToUpper 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
                    _RateNotStudied = Val(R!FTKeyValue.ToString)
                Case "Cfg_ModChildAllowanceRateStudy".ToUpper 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
                    _RateStudy = Val(R!FTKeyValue.ToString)
                Case "Cfg_ModChildAllowanceRateNumberOfChildren".ToUpper 'บุตรศึกษา อัตราลดหย่อนบุตร บุตร กำลังศึกษา คนละ
                    _MaxNumberOfChildren = Val(R!FTKeyValue.ToString)
                    '-------------ลดหย่อนบุตร-----------------
                    '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
                Case "Cfg_ContributedDeducToTheFund".ToUpper 'ลูกจ้าง
                    _DeducToTheFund = Val(R!FTKeyValue.ToString)
                Case "Cfg_ContributedDeducToTheFundBoss".ToUpper 'นายจ้าง
                    _DeducToTheFundBoss = Val(R!FTKeyValue.ToString)
                    '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
                    'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
                Case "Cfg_ContributedToTheFund".ToUpper ' %
                Case "Cfg_ContributedIncomeMax".ToUpper 'จำนวนเงินสูงสุด
                    'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
                Case "Cfg_ModDeductibleDonations".ToUpper ' % ลดหย่อนเงินบริจาค
                Case "Cfg_ModFatherReduction".ToUpper 'ลดหย่อนบิดา
                Case "Cfg_ModInsurancePremiums".ToUpper 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
                Case "Cfg_ModLendingforHousing".ToUpper 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
                Case "Cfg_ModLTF".ToUpper '% หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF)
                Case "Cfg_ModLTFChk".ToUpper 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
                Case "Cfg_ModMateFatherReduction".ToUpper 'ลดหย่อนบิดา คู่สมรส
                Case "Cfg_ModMateMotherReduction".ToUpper 'ลดหย่อนมารดา คู่สมรส
                Case "Cfg_ModMotherReduction".ToUpper 'ลดหย่อนมารดา
                Case "Cfg_ModPersonalExpen".ToUpper '% หัก ค่าใช้จ่ายส่วนบุคคล
                Case "Cfg_ModPersonalExpenChk".ToUpper ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน
                Case "Cfg_ModRateReductionsByMarital".ToUpper 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
                Case "Cfg_ModRateReductionsBySingle".ToUpper 'อัตราลดหย่อน ตาม สถานภาพ โสด 
                Case "Cfg_ModRMF".ToUpper ' % หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF)
                Case "Cfg_ModRMFChk".ToUpper ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
                Case "Cfg_ModSavingsFundOnly".ToUpper 'เปอร์เซนต์ หักเงินสะสมกองทุนสำรองเลี้ยงชีพ ของค่าจ้าง
                Case "Cfg_ModSavingsFundOnlyChk".ToUpper 'หักเงินสะสมกองทุนสำรองเลี้ยงชีพไม่เกิน
                Case "Cfg_ModSavingsFundOnlytheExcess".ToUpper 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน
                Case "Cfg_ModSavingsFundOnlytheExcessChk".ToUpper 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน ไม่เกิน


                Case Else

            End Select
        Next
    End Sub

#End Region

End Class

Public Class HCfg

    '...Sect control / Position control

    Public Structure structTaxRate
        Dim TaxIncomeMin As Double
        Dim TaxIncomeMax As Double
        Dim CalTaxRate As Double
    End Structure

    Public Shared HCfg_TaxRate() As structTaxRate

    Public Structure DiscountTax

        Dim Cfg_ModPersonalExpen As Double '% หัก ค่าใช้จ่ายส่วนบุคคล
        Dim Cfg_ModPersonalExpenChk As Double ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน
        Dim Cfg_ModChildAllowanceRateNotStudied As Double 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
        Dim Cfg_ModChildAllowanceRateStudy As Double 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
        Dim Cfg_ModChildAllowanceRateNumberOfChildren As Double 'บุตรศึกษา อัตราลดหย่อนบุตร บุตร กำลังศึกษา คนละ
        '-------------ลดหย่อนบุตร-----------------
        '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
        Dim Cfg_ContributedDeducToTheFund As Double 'ลูกจ้าง

        Dim Cfg_ContributedDeducToTheFundBoss As Double 'นายจ้าง

        '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
        'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้
        Dim Cfg_ContributedToTheFund As Double ' %
        Dim Cfg_ContributedIncomeMax As Double 'จำนวนเงินสูงสุด
        'กองทุนเงินทดแทน เปอร์เซนต์ จ่ายเงินสมทบเข้ากองทุน ของรายได้

        Dim Cfg_ModDeductibleDonations As Double ' % ลดหย่อนเงินบริจาค
        Dim Cfg_ModDeductDonateStudy As Double  ' ลดหย่อนเงินบริจาคเพื่อการศึกษา
        Dim Cfg_ModFatherReduction As Double 'ลดหย่อนบิดา
        Dim Cfg_ModInsurancePremiums As Double 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
        Dim Cfg_ModLendingforHousing As Double 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
        Dim Cfg_ModLTF As Double '% หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF)
        Dim Cfg_ModLTFChk As Double 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
        Dim Cfg_ModRMF As Double ' % หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF)
        Dim Cfg_ModRMFChk As Double ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 

        Dim Cfg_ModMateFatherReduction As Double 'ลดหย่อนบิดา คู่สมรส
        Dim Cfg_ModMateMotherReduction As Double 'ลดหย่อนมารดา คู่สมรส
        Dim Cfg_ModMotherReduction As Double 'ลดหย่อนมารดา

        Dim Cfg_ModRateReductionsByMarital As Double 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
        Dim Cfg_ModRateReductionsBySingle As Double 'อัตราลดหย่อน ตาม สถานภาพ โสด 

        Dim Cfg_ModSavingsFundOnly As Double 'เปอร์เซนต์ หักเงินสะสมกองทุนสำรองเลี้ยงชีพ ของค่าจ้าง
        Dim Cfg_ModSavingsFundOnlyChk As Double 'หักเงินสะสมกองทุนสำรองเลี้ยงชีพไม่เกิน

        Dim Cfg_ModSavingsFundOnlytheExcess As Double 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน
        Dim Cfg_ModSavingsFundOnlytheExcessChk As Double 'เงินสะสมกองทุนสำรองเลี้ยงชีพ เฉพาะส่วนที่เกิน ไม่เกิน

    End Structure

    Public Shared _DiscountTax As DiscountTax

    Public Structure EmployeeDiscountTax
        Dim BaseSlary As Double
        Dim OtherSlary As Double
        Dim BeforeIncom As Double
        Dim BeforeTax As Double

        Dim Cfg_ModChildAllowanceRateNotStudied As Double 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
        Dim Cfg_ModChildAllowanceRateStudy As Double 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
        '-------------ลดหย่อนบุตร-----------------

        '-- หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
        Dim Cfg_ContributedDeducToTheFund As Double 'ลูกจ้าง

        ' Dim Cfg_ContributedDeducToTheFundBoss As Double 'นายจ้าง
        Dim FTMateIncome As Boolean
        '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ

        Dim Cfg_ModDeductibleDonations As Double ' % ลดหย่อนเงินบริจาค
        Dim Cfg_ModDeductDonateStudy As Double  ' ลดหย่อนเงินบริจาคเพื่อการศึกษา
        Dim Cfg_ModFatherReduction As Double 'ลดหย่อนบิดา
        Dim Cfg_ModInsurancePremiums As Double 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
        Dim Cfg_ModLendingforHousing As Double 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย
        Dim Cfg_ModLTFChk As Double 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
        Dim Cfg_ModMateFatherReduction As Double 'ลดหย่อนบิดา คู่สมรส
        Dim Cfg_ModMateMotherReduction As Double 'ลดหย่อนมารดา คู่สมรส
        Dim Cfg_ModMotherReduction As Double 'ลดหย่อนมารดา
        Dim Cfg_ModPersonalExpenChk As Double ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน
        Dim Cfg_ModRateReductionsByMarital As Double 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
        Dim Cfg_ModRateReductionsBySingle As Double 'อัตราลดหย่อน ตาม สถานภาพ โสด 
        Dim Cfg_ModRMFChk As Double ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
        Dim FCDisabledDependents As Double
        Dim FCHealthInsurFatherMotherMate As Double
        Dim FTSosial As Double

        Dim FCExceptAgeOver As Double   'รายการเงินได้ที่ได้รับยกเว้น ของผู้มีเงินได้ตั้งแต่ 65 ปีขึ้นไป 
        Dim FCExceptAgeOverMate As Double   'รายการเงินได้ที่ได้รับยกเว้น ของคู่สมรสอายุตั้งแต่ 65 ปีขึ้นไป
        Dim FCDeductDividend As Double 'รายการลดหย่อนเงินปันผล

    End Structure

    Public Structure structSocialRate
        Dim SocialIncomeMin As Double
        Dim SocialIncomeMax As Double
        Dim CalSocialRate As Double
        Dim tTypeRecalSocial As String
    End Structure

    Public Structure structInsuranceVNRate
        'Dim SocialInsurance As Double
        'Dim HealthInsurance As Double
        'Dim UnemploymentInsurance As Double
        'Dim UnionInsurance As Double
        Dim FNInsuranceVN As Integer
        Dim FTInsuranceDesc As String
        Dim FNEmployeeRate As Double
        Dim FNEmployerRate As Double
    End Structure

    Public Shared HCfg_InsuranceVNRate() As structInsuranceVNRate

    Public Structure EmpTaxYear

        Dim FTAmt As Double  'เงินได้ก่อนหักค่าใช้จ่าย
        Dim FTExpenses As Double 'ค่าใช้จ่ายส่วนตัว
        Dim FTNetAmt As Double 'เงินได้หลังหักค่าใช้จ่าย
        Dim FTModEmp As Double 'ลดหย่อนส่วนตัว
        Dim FTModMate As Double 'ลดหย่อนคู่สมรส
        Dim FNChildNotLern As Double 'จำนวนบุตรไม่ศึกษา
        Dim FNChildLern As Double 'จำนวนบุตรศึกษา
        Dim FTChildNotLern As Double 'ลดหย่อนบุตรไม่ศึกษา
        Dim FTChildLern As Double 'ลดหย่อนบุตรศึกษา
        Dim FTInsurance As Double 'ลดหย่อนเบี้ยประกัน
        Dim FTProvidentfund As Double 'กองทุนเลียงชีพส่วนที่ไม่เกิน 10000
        Dim FTInterest As Double 'ดอกเบี้ยเงินกู้
        Dim FTSocial As Double 'ประกันสังคม
        Dim FTDonation As Double 'เงินบริจาค
        Dim FTProvidentfundOver As Double 'กองทุนเลียงชีพส่วนที่เกิน 10000
        Dim FTGPF As Double 'เงิน กบข.
        Dim FTSavingsFund As Double 'เงินกองทุนสงเคราะห์
        Dim FTCommutation As Double 'เงินชดเชยตามกฎหมายแรงงาน
        Dim FTUnitRMF As Double 'ค่าซื้อหน่วยลงทุน RTF
        Dim FTModFather As Double 'ลดหย่อนบิดา
        Dim FTModMother As Double 'ลดหย่อนมารดา
        Dim FTModFatherMate As Double 'ลดหย่อนบิดาคู่สมรส
        Dim FTModMotherMate As Double 'ลดหย่อนมารดาคู่สมรส
        Dim FTUnitLTF As Double 'ค่าซื้อหน่วยลงทุน LTF
        Dim FTDonationLern As Double 'เงินบริจาคเพื่อสนับสนุนการศึกษา
        Dim FTParentsHealthInsurance As Double 'เบี้ยประกันสุขภาพบิดามารดา
        Dim FTSupportSport As Double 'เงินสนับสนุนการกีฬา
        Dim FTAcquisitionOfProperty As Double 'ค่าซื้ออาคาร
        Dim FTPension As Double 'บำนาญ
        Dim FTTravel As Double 'ท่องเที่ยวในประเทศ
        Dim FTTotalCalTax As Double 'เงินได้สุทธิ
        Dim FTTotalTax As Double 'ภาษีที่ต้องจ่าย

        Dim FTTotalCalPayTax As Double 'เงินได้สุทธิ
        Dim FTTotalTaxPay As Double 'ภาษีที่ต้องจ่าย

    End Structure

    Public Shared HEmployeeTaxYear As EmpTaxYear
    Public Shared HSocialRate As structSocialRate
    Public Shared HMaxSocialBaht As Double
    Public Shared HMarryIncome As Boolean

    Public Shared _SkillRate As Double
    Public Shared _HarmfulRate As Double

End Class
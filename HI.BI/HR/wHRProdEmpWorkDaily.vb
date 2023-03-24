Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Public Class wHRProdEmpWorkDaily

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()


    End Sub
    Private WorkDay As Integer = 0
    Private sumBusiHr As Integer = 0
    Private sumVacHr As Integer = 0
    Private sumMTHr As Integer = 0
    Private sumFlowPlanHr As Integer = 0
    Private sumSickHr As Integer = 0
    Private sumAbHr As Integer = 0
    Private sumBusiOutHr As Integer = 0
    Private sumOutPlanHr As Integer = 0
    Private AllHr As Integer = 0

    Private sumBusiMin As Integer = 0
    Private sumVacMin As Integer = 0
    Private sumMTMin As Integer = 0
    Private sumFlowPlanMin As Integer = 0
    Private sumSickMin As Integer = 0
    Private sumAbMin As Integer = 0
    Private sumBusiOutMin As Integer = 0
    Private sumOutPlanMin As Integer = 0
    Private AllMin As Integer = 0


#Region "Procedure"

    Private Sub LoadData(_Spls As HI.TL.SplashScreen)

        Dim _Qry As String = ""
        Dim dt As New DataTable

        'Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")


        Try
            '_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_PROD_EMPWORK_DAILY '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'," & HI.ST.Lang.Language & "," & Val(HI.ST.SysInfo.CmpID) & " "
            '_Qry &= vbCrLf & "," & Val(Me.FNHSysDeptId.Properties.Tag) & "," & Val(Me.FNHSysDeptIdTo.Properties.Tag) & ""
            '_Qry &= vbCrLf & "," & Val(Me.FNHSysSectId.Properties.Tag) & "," & Val(Me.FNHSysSectIdTo.Properties.Tag) & ""
            '_Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag) & "," & Val(Me.FNHSysUnitSectIdTo.Properties.Tag) & ""

            _Qry = "Declare @StateHideSunday varchar(1)"
            _Qry &= vbCrLf & "Declare @FTUserLogIn nvarchar(50)='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "Declare @TransDate varchar(10)"
            _Qry &= vbCrLf & "Declare @StartDate varchar(10)='" & HI.UL.ULDate.ConvertEnDB(SFTDateTrans.Text) & "'"
            _Qry &= vbCrLf & "Declare @EndDate varchar(10)='" & HI.UL.ULDate.ConvertEnDB(EFTDateTrans.Text) & "'"
            _Qry &= vbCrLf & "Declare @Lang int=" & HI.ST.Lang.Language & ""
            _Qry &= vbCrLf & "Declare @FNHSysCmpId int=" & Val(HI.ST.SysInfo.CmpID) & ""
            _Qry &= vbCrLf & "Declare @FNDay int "
            _Qry &= vbCrLf & "Declare @FNMonth int"
            _Qry &= vbCrLf & "Declare @FTMonthName varchar(50)"
            _Qry &= vbCrLf & "Declare @FNYear int"
            _Qry &= vbCrLf & "Declare @FNWeekDay int "
            _Qry &= vbCrLf & "Declare @FTWeekDayName varchar(50) "
            _Qry &= vbCrLf & "Declare @Holiday varchar(10)"
            _Qry &= vbCrLf & "SET @StateHideSunday =''"
            _Qry &= vbCrLf & "SELECT TOP 1 @StateHideSunday =ISNULL(FTStateHideSunday,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK)"
            _Qry &= vbCrLf & "SET  @TransDate='" & HI.UL.ULDate.ConvertEnDB(SFTDateTrans.Text) & "'"
            _Qry &= vbCrLf & ""
            _Qry &= vbCrLf & "CREATE TABLE #Tab("
            _Qry &= vbCrLf & "[FTDateTrans] [nvarchar](10) NOT NULL,"
            _Qry &= vbCrLf & "[FNDay] [int] NOT NULL,"
            _Qry &= vbCrLf & "[FTDayName] [nvarchar](30) NOT NULL,"
            _Qry &= vbCrLf & "[FNMonth] [int] NOT NULL,"
            _Qry &= vbCrLf & "[FTMonthName] [nvarchar](30) NOT NULL,"
            _Qry &= vbCrLf & "[FNYear] [int] NOT NULL,"
            _Qry &= vbCrLf & "[FTEmpCode] [nvarchar](30) NOT NULL,"
            _Qry &= vbCrLf & "[FTEmpName] [nvarchar](200) NOT NULL,"
            _Qry &= vbCrLf & "[FTSex] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FNEmpAgeYear] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpAgeMonth] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpWorkAgeYear] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth] [int]  NULL,"
            _Qry &= vbCrLf & "[FTUnitSectCode] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTNormalWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTOTWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTTotalWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FNLateMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveVacationHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveVacationMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveMaternityHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveMaternityMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveSickHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveSickMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveAbsentHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveAbsentMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNTotalHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNTotalMin] [int]  NULL"
            If LeaveType() Then
                _Qry &= vbCrLf & ",[FNLeaveMin] [int] NULL"
            End If
            _Qry &= vbCrLf & ",[FTEmpTypeCode]  [nvarchar](50)  NULL"
            _Qry &= vbCrLf & ",[FTEmpTypeName] [nvarchar](50)   NULL"

            '-----------------------
            _Qry &= vbCrLf & ") ON [PRIMARY]"

            _Qry &= vbCrLf & "WHILE @TransDate <= @EndDate"
            _Qry &= vbCrLf & "BEGIN"

            _Qry &= vbCrLf & "SET @Holiday =''"
            _Qry &= vbCrLf & "Select @FNDay=Day(@TransDate) "
            _Qry &= vbCrLf & ",@FNMonth=Month(@TransDate)"
            _Qry &= vbCrLf & ",@FNYear =Year(@TransDate) "
            _Qry &= vbCrLf & ",@FNWeekDay=DATEPART ( WEEKDAY ,@TransDate) "
            _Qry &= vbCrLf & "SELECT TOP 1 @Holiday = FDHolidayDate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) WHERE FDHolidayDate=@TransDate AND FNHSysCmpId=@FNHSysCmpId AND FTStateActive=1 "

            _Qry &= vbCrLf & "IF (@StateHideSunday='1' AND   @FNWeekDay = 1) OR @Holiday <>''"
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "Set @FTMonthName=''"
            _Qry &= vbCrLf & "End"
            _Qry &= vbCrLf & "Else"
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "IF @Lang = 2"
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "SET @FTMonthName = CASE WHEN  @FNMonth =1 THEN 'ม.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =2 THEN 'ก.พ.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =3 THEN 'มี.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =4 THEN 'เม.ย.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =5 THEN 'พ.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =6 THEN 'มิ.ย.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =7 THEN 'ก.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =8 THEN 'ส.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =9 THEN 'ก.ย.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =10 THEN 'ต.ค.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =11 THEN 'พ.ย.'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =12 THEN 'ธ.ค.'"
            _Qry &= vbCrLf & "Else  '' END"
            _Qry &= vbCrLf & "End"
            _Qry &= vbCrLf & "ELSE"
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "SET @FTMonthName = CASE WHEN  @FNMonth =1 THEN 'JAN'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =2 THEN 'FAB'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =3 THEN 'MAR'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =4 THEN 'APR'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =5 THEN 'MAY'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =6 THEN 'JUN'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =7 THEN 'JUL'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =8 THEN 'AUG'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =9 THEN 'SEP'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =10 THEN 'OCT'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =11 THEN 'NOV'"
            _Qry &= vbCrLf & "WHEN  @FNMonth =12 THEN 'DEC'"
            _Qry &= vbCrLf & "Else  '' END"
            _Qry &= vbCrLf & "End"

            _Qry &= vbCrLf & "IF @Lang = 2 "
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "SET @FTWeekDayName = CASE WHEN @FNWeekDay =1 THEN 'อาทิตย์'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =2 THEN 'จันทร์'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =3 THEN 'อังคาร'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =4 THEN 'พุธ'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =5 THEN 'พฤหัสบดี'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =6 THEN 'ศุกร์'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =7 THEN 'เสาร์'"
            _Qry &= vbCrLf & "Else '' END"
            _Qry &= vbCrLf & "End"
            _Qry &= vbCrLf & "ELSE"
            _Qry &= vbCrLf & "BEGIN"
            _Qry &= vbCrLf & "SET @FTWeekDayName = CASE WHEN @FNWeekDay =1 THEN 'Sunday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =2 THEN 'Monday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =3 THEN 'Tuesday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =4 THEN 'Wednesday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =5 THEN 'Thursday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =6 THEN 'Friday'"
            _Qry &= vbCrLf & "WHEN @FNWeekDay =7 THEN 'Saturday'"
            _Qry &= vbCrLf & "ELSE '' END"
            _Qry &= vbCrLf & "End"

            _Qry &= vbCrLf & "INSERT INTO #Tab([FTDateTrans],[FNDay],[FTDayName],[FNMonth],"
            _Qry &= vbCrLf & "[FTMonthName],[FNYear],[FTEmpCode],[FTEmpName],"
            _Qry &= vbCrLf & "[FTSex],[FNEmpAgeYear],[FNEmpAgeMonth],[FNEmpWorkAgeYear],"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth],[FTUnitSectCode],[FTNormalWorkTime],[FTOTWorkTime],"
            _Qry &= vbCrLf & "[FTTotalWorkTime],[FNLateMin],[FNLeaveBusinessHour],[FNLeaveBusinessMin],"
            _Qry &= vbCrLf & "[FNLeaveVacationHour],[FNLeaveVacationMin],[FNLeaveMaternityHour],[FNLeaveMaternityMin],"
            _Qry &= vbCrLf & "[FNLeaveSickHour] ,[FNLeaveSickMin],[FNLeaveAbsentHour],[FNLeaveAbsentMin],"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentHour],[FNLeaveBusinessUrgentMin],[FNTotalHour],[FNTotalMin]"
            If LeaveType() Then
                _Qry &= vbCrLf & ",[FNLeaveMin]"
            End If
            _Qry &= vbCrLf & ",[FTEmpTypeCode] "
            _Qry &= vbCrLf & ",[FTEmpTypeName] "
            _Qry &= vbCrLf & ")"

            _Qry &= vbCrLf & "Select  @TransDate,@FNDay,@FTWeekDayName,@FNMonth"
            _Qry &= vbCrLf & ",@FTMonthName,@FNYear,M.FTEmpCode ,Case When @Lang = 2 Then M.FTEmpNameTH  Else M.FTEmpNameEN End As FTEmpName"
            _Qry &= vbCrLf & ",M.FTEmpSex,Floor(M.FNEmpAge/12) As FNEmpAgeYear ,(M.FNEmpAge%12) As FNEmpAgeMonth,Floor(M.FNEmpWorkAge/12) As FNEmpWorkAgeYear"
            _Qry &= vbCrLf & ",(M.FNEmpWorkAge%12) As FNEmpWorkAgeMonth,M.FTUnitSectCode "
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNTimeMin,0) ) / 60.00))),2)"
            _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNTimeMin,0) ) % 60.00))),2))  AS FTNormalWorkTime"
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNOT1Min,0)  ) / 60.00))),2)"
            _Qry &= vbCrLf & "		+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNOT1Min,0) ) % 60.00))),2))  AS FTOTWorkTime"
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNTimeMin,0)+ISNULL(T.FNOT1Min,0)  ) / 60.00))),2)"
            _Qry &= vbCrLf & "		+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNTimeMin,0)+ISNULL(T.FNOT1Min,0) ) % 60.00))),2))  AS FTTotalWorkTime"
            _Qry &= vbCrLf & ",ISNULL(T.FNLateNormalMin,0) AS FNLateMin"
            _Qry &= vbCrLf & ",Floor((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) / 60) FNLeaveBusinessHour"
            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) % 60) FNLeaveBusinessMin"

            _Qry &= vbCrLf & ",Floor((ISNULL(T.FNLeaveVacation,0) ) / 60) FNLeaveVacationHour"
            _Qry &= vbCrLf & ",(( ISNULL(T.FNLeaveVacation,0)  ) % 60) FNLeaveVacationMin"

            _Qry &= vbCrLf & ",Floor((ISNULL(T.FNMaternity,0) ) / 60) FNLeaveMaternityHour"
            _Qry &= vbCrLf & ",(( ISNULL(T.FNMaternity,0)  ) % 60) FNLeaveMaternityMin"

            _Qry &= vbCrLf & ",Floor((ISNULL(T.FNLeaveSick,0) ) / 60) FNLeaveSickHour"
            _Qry &= vbCrLf & ",(( ISNULL(T.FNLeaveSick,0)  ) % 60) FNLeaveSickMin"

            _Qry &= vbCrLf & ",Floor((ISNULL(T.FNAbsent,0) ) / 60) FNLeaveAbsentHour"
            _Qry &= vbCrLf & ",(( ISNULL(T.FNAbsent,0)  ) % 60) FNLeaveAbsentMin"

            _Qry &= vbCrLf & ",Floor((CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) / 60) FNLeaveBusinessUrgentHour"
            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0) > 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) % 60) FNLeaveBusinessUrgentMin"

            _Qry &= vbCrLf & ",Floor((ISNULL(T.FNBusiness,0) + ISNULL(T.FNLeaveVacation,0)  + ISNULL(T.FNMaternity,0)  +ISNULL(T.FNLeaveSick,0)  +ISNULL(T.FNAbsent,0)   ) / 60)  AS  FNTotalHour"
            _Qry &= vbCrLf & ",(( ISNULL(T.FNBusiness,0) + ISNULL(T.FNLeaveVacation,0)  + ISNULL(T.FNMaternity,0)  +ISNULL(T.FNLeaveSick,0)  +ISNULL(T.FNAbsent,0)    ) % 60) FNTotalMin"
            If LeaveType() Then
                _Qry &= vbCrLf & " " & _Leave & " "
            End If
            _Qry &= vbCrLf & " , M.FTEmpTypeCode , M.FTEmpTypeName"
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT M.FNHSysEmpID"
            _Qry &= vbCrLf & ", M.FTEmpCode"
            _Qry &= vbCrLf & ", ISNULL(P.FTPreNameNameTH,'') + ' ' + M.FTEmpNameTH + ' ' + M.FTEmpSurnameTH AS  FTEmpNameTH"
            _Qry &= vbCrLf & ", ISNULL(P.FTPreNameNameEN,'') + ' ' + M.FTEmpNameEN + ' ' + M.FTEmpSurnameEN AS  FTEmpNameEN"
            _Qry &= vbCrLf & ", U.FTUnitSectCode"
            _Qry &= vbCrLf & ",M.FDBirthDate, M.FDDateStart,  M.FDDateEnd"
            _Qry &= vbCrLf & ",CASE WHEN M.FNEmpSex = 0 THEN  (CASE WHEN @Lang = 2 Then 'ช' ELSE 'M' END) ELSE (CASE WHEN @Lang = 2 Then 'ญ' ELSE 'F' END)  END AS FTEmpSex"
            _Qry &= vbCrLf & ",dbo.FN_Get_Emp_WorkAge(M.FDBirthDate,'') AS FNEmpAge"
            _Qry &= vbCrLf & ",dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNEmpWorkAge"
            _Qry &= vbCrLf & " , ET.FTEmpTypeCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , ET.FTEmpTypeNameTH as FTEmpTypeName"
            Else
                _Qry &= vbCrLf & " , ET.FTEmpTypeNameEN as FTEmpTypeName"
            End If

            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH(NOLOCK)  ON M.FNHSysPreNameId = P.FNHSysPreNameId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH(NOLOCK) ON M.FNHSysDeptId=D.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON M.FNHSysSectId=S.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK)  ON M.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Qry &= vbCrLf & "WHERE M.FNHSYSCMPID=@FNHSysCmpId AND M.FDDateStart <=@TransDate AND (ISNULL(M.FDDateEnd,'') ='' OR ISNULL(M.FDDateEnd,'') > @TransDate)"

            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & "AND D.FTDeptCode >='" & Me.FNHSysDeptId.Text & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND D.FTDeptCode <='" & Me.FNHSysDeptIdTo.Text & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTSectCode >='" & Me.FNHSysSectId.Text & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTSectCode <='" & Me.FNHSysSectIdTo.Text & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & "AND U.FTUnitSectCode >='" & Me.FNHSysUnitSectId.Text & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND U.FTUnitSectCode <='" & Me.FNHSysUnitSectIdTo.Text & "'"
            End If
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & "AND ET.FTEmpTypeCode ='" & Me.FNHSysEmpTypeId.Text & "'"
            End If


            _Qry &= vbCrLf & "	) AS M LEFT OUTER JOIN "
            _Qry &= vbCrLf & "( SELECT FNHSysEmpID, FTDateTrans, FNLateNormalMin, FNRetireNormalMin, FNLateOtMin, FNRetireOtMin, FNAbsentCut, FNAbsent"
            _Qry &= vbCrLf & ", FNTimeMin, FNOT1Min, FNOT1_5Min, FNOT2Min, FNOT3Min, FNOT4Min, FNLateMMin, "
            _Qry &= vbCrLf & "FNLateAfMin, FNRetireMMin, FNRetireAfMin, FNCutAbsent, FNLateNormalNotCut"
            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '0')"

            _Qry &= vbCrLf & "),0) AS FNLeaveSick"
            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '98')"

            _Qry &= vbCrLf & "),0) AS FNLeaveVacation"
            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '97')"

            _Qry &= vbCrLf & "),0) AS FNMaternity"
            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "		FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND NOT(FTLeaveType IN ('97','98','0'))"

            _Qry &= vbCrLf & "),0) AS FNBusiness"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE  FTDateTrans =@TransDate  ) AS T ON M.FNHSysEmpID = T.FNHSysEmpID "


            _Qry &= vbCrLf & "End"


            _Qry &= vbCrLf & "SET @TransDate = CONVERT(varchar(10),Dateadd(day,1,convert(Datetime,@TransDate)),111)"

            _Qry &= vbCrLf & "End"

            _Qry &= vbCrLf & "SELECT *"
            _Qry &= vbCrLf & "FROM #Tab"
            If LeaveType() Then
                _Qry &= vbCrLf & "WHERE FNLeaveMin >= " & (Val(FTSTime.Text) * 60) + Microsoft.VisualBasic.Right(FTSTime.Text, 2) & " AND FNLeaveMin <= " & (Val(FTETime.Text) * 60) + Microsoft.VisualBasic.Right(FTETime.Text, 2) & ""
            End If
            _Qry &= vbCrLf & "ORDER BY FTDateTrans,FTUnitSectCode"

            _Qry &= vbCrLf & "DROP TABLE #Tab"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.ogdtime.DataSource = dt.Copy

            '_Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

    Private Sub LoadData2(_Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        Try

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_STATIC_EMPLEAVE_MONTH '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


            _Qry = "Declare @TransDate varchar(10)"
            'Start parameter
            _Qry &= vbCrLf & "Declare @FTUserLogIn nvarchar(50)"
            _Qry &= vbCrLf & "Declare @StartDate varchar(10)"
            _Qry &= vbCrLf & "Declare @EndDate varchar(10)"
            _Qry &= vbCrLf & "Declare @Lang int"
            _Qry &= vbCrLf & "Declare @FNHSysCmpId int"
            _Qry &= vbCrLf & "Declare @Dept int"
            _Qry &= vbCrLf & "Declare @DeptTo int"
            _Qry &= vbCrLf & "Declare @Sect int"
            _Qry &= vbCrLf & "Declare @SectTo int"
            _Qry &= vbCrLf & "Declare @UnitSect int"
            _Qry &= vbCrLf & "Declare @UnitSectTo int"
            'End parameter
            'SET Value parametrer
            _Qry &= vbCrLf & "SET @FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "SET @StartDate='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "'"
            _Qry &= vbCrLf & "SET @EndDate='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Qry &= vbCrLf & "SET @Lang=" & HI.ST.Lang.Language & ""
            _Qry &= vbCrLf & "SET @FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""
            _Qry &= vbCrLf & "SET @Dept=" & Val(Me.FNHSysDeptId.Properties.Tag) & ""
            _Qry &= vbCrLf & "SET @DeptTo=" & Val(Me.FNHSysDeptIdTo.Properties.Tag) & ""
            _Qry &= vbCrLf & "SET @Sect=" & Val(Me.FNHSysSectId.Properties.Tag) & ""
            _Qry &= vbCrLf & "SET @SectTo=" & Val(Me.FNHSysSectIdTo.Properties.Tag) & ""
            _Qry &= vbCrLf & "SET @UnitSect=" & Val(Me.FNHSysUnitSectId.Properties.Tag) & ""
            _Qry &= vbCrLf & "SET @UnitSectTo=" & Val(Me.FNHSysUnitSectIdTo.Properties.Tag) & ""
            'End SET value

            _Qry &= vbCrLf & "Declare @StateHideSunday varchar(1)"
            _Qry &= vbCrLf & "Declare @Holiday varchar(10)"
            _Qry &= vbCrLf & "SET @StateHideSunday =''"
            _Qry &= vbCrLf & "SELECT TOP 1 @StateHideSunday =ISNULL(FTStateHideSunday,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK)"
            _Qry &= vbCrLf & "SET  @TransDate= @StartDate"
            _Qry &= vbCrLf & "CREATE TABLE #Tab("

            _Qry &= vbCrLf & "[FTEmpCode] [nvarchar](30) NOT NULL,"
            _Qry &= vbCrLf & "[FTEmpName] [nvarchar](200) NOT NULL,"
            _Qry &= vbCrLf & "[FTSex] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FNEmpAgeYear] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpAgeMonth] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpWorkAgeYear] [int]  NULL,"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth] [int]  NULL,"
            _Qry &= vbCrLf & "[FTUnitSectCode] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTNormalWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTOTWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FTTotalWorkTime] [nvarchar](30)  NULL,"
            _Qry &= vbCrLf & "[FNLateMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveVacationHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveVacationMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveMaternityHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveMaternityMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveSickHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveSickMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveAbsentHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveAbsentMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNTotalHour] [int]  NULL,"
            _Qry &= vbCrLf & "[FNTotalMin] [int]  NULL,"
            _Qry &= vbCrLf & "[FNLateMMin] [int] NULL,"
            _Qry &= vbCrLf & "[FNLateAfMin] [int] NULL,"
            _Qry &= vbCrLf & "[FNLateOtMin] [int] NULL,"
            _Qry &= vbCrLf & "[TotalMin] [int] NULL,"

            _Qry &= vbCrLf & "[MorAmountLateTime] [int] NULL,"
            _Qry &= vbCrLf & "[AFAmountLateTime] [int] NULL,"
            _Qry &= vbCrLf & "[OTAmountLateTime] [int] NULL,"
            _Qry &= vbCrLf & "[SumLateAmountTime] [int] NULL,"
            _Qry &= vbCrLf & "[AmountBusiTime] [int] NULL,"
            _Qry &= vbCrLf & "[AmountVacTime] [int] NULL,"
            _Qry &= vbCrLf & "[AmountMTTime] [int] NULL,"

            _Qry &= vbCrLf & "[SumAmountTimeFlowPlan] [int] NULL,"
            _Qry &= vbCrLf & "[SumHrFlowPlan] [int] NULL,"
            _Qry &= vbCrLf & "[SumMinuteFlowPlan] [int] NULL,"
            _Qry &= vbCrLf & "[AmountSickTime] [int] NULL,"
            _Qry &= vbCrLf & "[AmountAbTime] [int] NULL,"
            _Qry &= vbCrLf & "[oAmountBusiTime] [int] NULL,"
            _Qry &= vbCrLf & "[SumAmountTimeOutPlan] [int] NULL ,"
            _Qry &= vbCrLf & "[SumHrOutPlan] [int] NULL,"
            _Qry &= vbCrLf & "[SumMinuteOutPlan] [int] NULL,"
            _Qry &= vbCrLf & "[SumAllAmount] [int] NULL,"
            _Qry &= vbCrLf & "[SumAllHr] [int] NULL,"
            _Qry &= vbCrLf & "[SumAllMinute] [int] NULL,"

            _Qry &= vbCrLf & "[BusiPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[VacPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[MTPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[SumPercenFlowPlan] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[SickPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[AbPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[oBusiPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[SumPercenOutPlan] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[SumAllPercen] [numeric](18,4) NULL,"
            _Qry &= vbCrLf & "[WorkDay] [int] NULL"
            If LeaveType() Then
                _Qry &= vbCrLf & ",[FNLeaveMin] [int] NULL"
            End If
            _Qry &= vbCrLf & ",[FTEmpTypeCode]  [nvarchar](50)  NULL"
            _Qry &= vbCrLf & ",[FTEmpTypeName] [nvarchar](50)   NULL"
            _Qry &= vbCrLf & ") ON [PRIMARY]"

            _Qry &= vbCrLf & "INSERT INTO #Tab([FTEmpCode],[FTEmpName],"
            _Qry &= vbCrLf & "[FTSex], [FNEmpAgeYear], [FNEmpAgeMonth], [FNEmpWorkAgeYear],"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth], [FTUnitSectCode], [FTNormalWorkTime], [FTOTWorkTime],"
            _Qry &= vbCrLf & "[FTTotalWorkTime], [FNLateMin], [FNLeaveBusinessHour], [FNLeaveBusinessMin],"
            _Qry &= vbCrLf & "[FNLeaveVacationHour], [FNLeaveVacationMin], [FNLeaveMaternityHour], [FNLeaveMaternityMin],"
            _Qry &= vbCrLf & "[FNLeaveSickHour], [FNLeaveSickMin], [FNLeaveAbsentHour], [FNLeaveAbsentMin],"
            _Qry &= vbCrLf & "[FNLeaveBusinessUrgentHour], [FNLeaveBusinessUrgentMin], [FNTotalHour], [FNTotalMin],"
            _Qry &= vbCrLf & "[FNLateMMin], [FNLateAfMin], [FNLateOtMin], [TotalMin]"
            _Qry &= vbCrLf & "			,[MorAmountLateTime]"
            _Qry &= vbCrLf & ",[AFAmountLateTime]"
            _Qry &= vbCrLf & ",[OTAmountLateTime]"
            _Qry &= vbCrLf & ",[SumLateAmountTime]"
            _Qry &= vbCrLf & ",[AmountBusiTime]"
            _Qry &= vbCrLf & ",[AmountVacTime]"
            _Qry &= vbCrLf & ",[AmountMTTime]"
            _Qry &= vbCrLf & ",[SumAmountTimeFlowPlan]"
            _Qry &= vbCrLf & ",[SumHrFlowPlan]"
            _Qry &= vbCrLf & ",[SumMinuteFlowPlan]"
            _Qry &= vbCrLf & ",[AmountSickTime]"
            _Qry &= vbCrLf & ",[AmountAbTime]"
            _Qry &= vbCrLf & ",[oAmountBusiTime]"
            _Qry &= vbCrLf & ",[SumAmountTimeOutPlan]"
            _Qry &= vbCrLf & ",[SumHrOutPlan]"
            _Qry &= vbCrLf & ",[SumMinuteOutPlan]"
            _Qry &= vbCrLf & ",[SumAllAmount]"
            _Qry &= vbCrLf & ",[SumAllHr]"
            _Qry &= vbCrLf & ",[SumAllMinute]"
            _Qry &= vbCrLf & ",[BusiPercen]"
            _Qry &= vbCrLf & ",[VacPercen]"
            _Qry &= vbCrLf & ",[MTPercen]"
            _Qry &= vbCrLf & ",[SumPercenFlowPlan]"
            _Qry &= vbCrLf & ",[SickPercen]"
            _Qry &= vbCrLf & ",[AbPercen]"
            _Qry &= vbCrLf & ",[oBusiPercen]"
            _Qry &= vbCrLf & ",[SumPercenOutPlan]"
            _Qry &= vbCrLf & ",[SumAllPercen]"
            _Qry &= vbCrLf & ",[WorkDay]"
            If LeaveType() Then
                _Qry &= vbCrLf & ",[FNLeaveMin]"
            End If
            _Qry &= vbCrLf & ",[FTEmpTypeCode]  "
            _Qry &= vbCrLf & ",[FTEmpTypeName] "
            _Qry &= vbCrLf & ")"

            _Qry &= vbCrLf & "SELECT  M.FTEmpCode ,CASE WHEN @Lang = 2 THEN M.FTEmpNameTH  ELSE M.FTEmpNameEN END AS FTEmpName"
            _Qry &= vbCrLf & ",M.FTEmpSex,Floor(M.FNEmpAge/12) AS FNEmpAgeYear ,(M.FNEmpAge%12) As FNEmpAgeMonth,Floor(M.FNEmpWorkAge/12) AS FNEmpWorkAgeYear"
            _Qry &= vbCrLf & ",(M.FNEmpWorkAge%12) AS FNEmpWorkAgeMonth,M.FTUnitSectCode "
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNTimeMin,0) ) / 60.00))),2)"
            _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNTimeMin,0) ) % 60.00))),2))  AS FTNormalWorkTime"
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNOT1Min,0)  ) / 60.00))),2)"
            _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNOT1Min,0) ) % 60.00))),2))  AS FTOTWorkTime"
            _Qry &= vbCrLf & ", (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(T.FNTimeMin,0)+ISNULL(T.FNOT1Min,0)  ) / 60.00))),2)"
            _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((ISNULL(T.FNTimeMin,0)+ISNULL(T.FNOT1Min,0) ) % 60.00))),2))  AS FTTotalWorkTime"
            _Qry &= vbCrLf & ",ISNULL(T.FNLateNormalMin,0) AS FNLateMin"
            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)  AS FNLeaveBusinessHour"
            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) AS FNLeaveBusinessMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNLeaveVacation,0)  AS FNLeaveVacationHour"
            _Qry &= vbCrLf & ",ISNULL(T.FNLeaveVacation,0) AS FNLeaveVacationMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNMaternity,0)  AS FNLeaveMaternityHour"
            _Qry &= vbCrLf & ",ISNULL(T.FNMaternity,0) AS FNLeaveMaternityMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNLeaveSick,0) AS FNLeaveSickHour"
            _Qry &= vbCrLf & ",ISNULL(T.FNLeaveSick,0)  AS FNLeaveSickMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNAbsent,0)  AS FNLeaveAbsentHour"
            _Qry &= vbCrLf & ",ISNULL(T.FNAbsent,0)  AS FNLeaveAbsentMin"

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END AS FNLeaveBusinessUrgentHour"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNTimeMin,0) > 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END AS FNLeaveBusinessUrgentMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNBusiness,0) + ISNULL(T.FNLeaveVacation,0)  + ISNULL(T.FNMaternity,0)  +ISNULL(T.FNLeaveSick,0)  +ISNULL(T.FNAbsent,0) AS  FNTotalHour"
            _Qry &= vbCrLf & ",ISNULL(T.FNBusiness,0) + ISNULL(T.FNLeaveVacation,0)  + ISNULL(T.FNMaternity,0)  +ISNULL(T.FNLeaveSick,0)  +ISNULL(T.FNAbsent,0)  AS FNTotalMin"

            _Qry &= vbCrLf & ",ISNULL(T.FNLateMMin,0) AS FNLateMMin"
            _Qry &= vbCrLf & ",ISNULL(T.FNLateAfMin,0) AS FNLateAfMin"
            _Qry &= vbCrLf & ",ISNULL(T.FNLateOtMin,0) AS FNLateOtMin"
            _Qry &= vbCrLf & ",ISNULL(T.FNLateMMin,0)+ISNULL(T.FNLateAfMin,0)+ISNULL(T.FNLateOtMin,0) AS TotalMin"



            _Qry &= vbCrLf & ",ISNULL(MAmounts,0) AS MorAmountLateTime"
            _Qry &= vbCrLf & ",ISNULL(AAmounts,0) AS AFAmountLateTime"
            _Qry &= vbCrLf & ",ISNULL(OTAmounts,0) AS OTAmountLateTime"
            _Qry &= vbCrLf & ",ISNULL(TotalAmountlate,0) AS SumLateAmountTime"

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNTimeMin,0)<=0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END  AS AmountBusiTime"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNLeaveVacation,0)>0 THEN 1 ELSE 0 END AS AmountVacTime"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNMaternity,0) >0 THEN 1 ELSE 0 END  AS AmountMTTime"

            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0)<=0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNLeaveVacation,0)>0 THEN 1 ELSE 0 END)+(CASE WHEN ISNULL(T.FNMaternity,0) >0 THEN 1 ELSE 0 END)) AS SumAmountTimeFlowPlan"

            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) +ISNULL(T.FNLeaveVacation,0) +ISNULL(T.FNMaternity,0) AS SumHrFlowPlan"

            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+ ISNULL(T.FNLeaveVacation,0)+ ISNULL(T.FNMaternity,0) AS SumMinuteFlowPlan"


            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNLeaveSick,0)>0 THEN 1 ELSE 0 END AS AmountSickTime"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNAbsent,0)>0 THEN 1 ELSE 0 END AS AmountAbTime"

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END AS oAmountBusiTime"
            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNLeaveSick,0)>0 THEN 1 ELSE 0 END)+(CASE WHEN ISNULL(T.FNAbsent,0)>0 THEN 1 ELSE 0 END)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END)) AS SumAmountTimeOutPlan"

            _Qry &= vbCrLf & ",ISNULL(T.FNLeaveSick,0)+ISNULL(T.FNAbsent,0)+(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) AS SumHrOutPlan"
            _Qry &= vbCrLf & ", ISNULL(T.FNLeaveSick,0)+ ISNULL(T.FNAbsent,0)+(CASE WHEN ISNULL(T.FNTimeMin,0) > 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) AS SumMinuteOutPlan"

            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0)<=0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNLeaveVacation,0)>0 THEN 1 ELSE 0 END)+(CASE WHEN ISNULL(T.FNMaternity,0) >0 THEN 1 ELSE 0 END))+"
            _Qry &= vbCrLf & "((CASE WHEN ISNULL(T.FNLeaveSick,0)>0 THEN 1 ELSE 0 END)+(CASE WHEN ISNULL(T.FNAbsent,0)>0 THEN 1 ELSE 0 END)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN CASE WHEN ISNULL(T.FNBusiness,0)>0 THEN 1 ELSE 0 END ELSE 0 END)) AS SumAllAmount"

            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) +ISNULL(T.FNLeaveVacation,0) +ISNULL(T.FNMaternity,0) +"
            _Qry &= vbCrLf & "ISNULL(T.FNLeaveSick,0)+ISNULL(T.FNAbsent,0)+(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) AS SumAllHr"

            _Qry &= vbCrLf & ",(CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+ ISNULL(T.FNLeaveVacation,0)+ ISNULL(T.FNMaternity,0) +"
            _Qry &= vbCrLf & "ISNULL(T.FNLeaveSick,0)+ ISNULL(T.FNAbsent,0)+(CASE WHEN ISNULL(T.FNTimeMin,0) > 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END) AS SumAllMinute"


            ''คำนวนเปอร์เซ็นการทำงาน ตามแผน
            '_Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)/FNTotalWorkDayInYear)*100 AS BusiPercen"
            '_Qry &= vbCrLf & ",(ISNULL(T.FNLeaveVacation,0)/FNTotalWorkDayInYear)*100 AS VacPercen"
            '_Qry &= vbCrLf & ",(ISNULL(T.FNMaternity,0)/FNTotalWorkDayInYear)*100 AS MTPercen"
            '' Sum PERCEN ตามแผน
            '_Qry &= vbCrLf & ",(((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNLeaveVacation,0)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNMaternity,0))/FNTotalWorkDayInYear)*100 AS SumPercenFlowPlan"

            ''คำนวณเปอร์เซ็นการทำงาน นอกแผน
            '_Qry &= vbCrLf & ",(ISNULL(T.FNLeaveSick,0)/FNTotalWorkDayInYear)*100 AS SickPercen"
            '_Qry &= vbCrLf & ",(ISNULL(T.FNAbsent,0)/FNTotalWorkDayInYear)*100 AS AbPercen"
            '_Qry &= vbCrLf & ",((CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)/FNTotalWorkDayInYear)*100 AS oBusiPercen"
            ''Sum PERCEN นอกแผน
            '_Qry &= vbCrLf & ",((ISNULL(T.FNLeaveSick,0)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNAbsent,0)+"
            '_Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END))/FNTotalWorkDayInYear)*100 AS SumPercenOutPlan"

            ''sum ALL ในแผน+นอกแผน
            '_Qry &= vbCrLf & ",(((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNLeaveVacation,0)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNMaternity,0)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNLeaveSick,0)+"
            '_Qry &= vbCrLf & "ISNULL(T.FNAbsent,0)+"
            '_Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END))/FNTotalWorkDayInYear)*100 AS SumAllPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else ((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)/FNTotalWorkDayInYear)*100 end AS BusiPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (ISNULL(T.FNLeaveVacation,0)/FNTotalWorkDayInYear)*100 end AS VacPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (ISNULL(T.FNMaternity,0)/FNTotalWorkDayInYear)*100 end AS MTPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+"
            _Qry &= vbCrLf & "ISNULL(T.FNLeaveVacation,0)+"
            _Qry &= vbCrLf & "ISNULL(T.FNMaternity,0))/FNTotalWorkDayInYear)*100 end AS SumPercenFlowPlan"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (ISNULL(T.FNLeaveSick,0)/FNTotalWorkDayInYear)*100 end AS SickPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (ISNULL(T.FNAbsent,0)/FNTotalWorkDayInYear)*100 end AS AbPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else ((CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)/FNTotalWorkDayInYear)*100 end AS oBusiPercen"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else ((ISNULL(T.FNLeaveSick,0)+"
            _Qry &= vbCrLf & "ISNULL(T.FNAbsent,0)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END))/FNTotalWorkDayInYear)*100 end AS SumPercenOutPlan"
            _Qry &= vbCrLf & ",Case when FNTotalWorkDayInYear=0 then 0 else (((CASE WHEN ISNULL(T.FNTimeMin,0) <=0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END)+"
            _Qry &= vbCrLf & "ISNULL(T.FNLeaveVacation,0)+"
            _Qry &= vbCrLf & "ISNULL(T.FNMaternity,0)+"
            _Qry &= vbCrLf & "ISNULL(T.FNLeaveSick,0)+"
            _Qry &= vbCrLf & "ISNULL(T.FNAbsent,0)+"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(T.FNTimeMin,0) >0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END))/FNTotalWorkDayInYear)*100 end AS SumAllPercen"

            _Qry &= vbCrLf & ",ISNULL(FNTotalWorkDayInYear,0) AS WorkDay"
            If LeaveType() Then
                _Qry &= vbCrLf & " " & _Leave & " "
            End If
            _Qry &= vbCrLf & " , M.FTEmpTypeCode , M.FTEmpTypeName"
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT M.FNHSysEmpID"
            _Qry &= vbCrLf & ", M.FTEmpCode"
            _Qry &= vbCrLf & ", ISNULL(P.FTPreNameNameTH,'') + ' ' + M.FTEmpNameTH + ' ' + M.FTEmpSurnameTH AS  FTEmpNameTH"
            _Qry &= vbCrLf & ", ISNULL(P.FTPreNameNameEN,'') + ' ' + M.FTEmpNameEN + ' ' + M.FTEmpSurnameEN AS  FTEmpNameEN"
            _Qry &= vbCrLf & ", U.FTUnitSectCode"
            _Qry &= vbCrLf & ",M.FDBirthDate, M.FDDateStart,  M.FDDateEnd"
            _Qry &= vbCrLf & ",CASE WHEN M.FNEmpSex = 0 THEN  (CASE WHEN @Lang = 2 Then 'ช' ELSE 'M' END) ELSE (CASE WHEN @Lang = 2 Then 'ญ' ELSE 'F' END)  END AS FTEmpSex"
            _Qry &= vbCrLf & ",dbo.FN_Get_Emp_WorkAge(M.FDBirthDate,'') AS FNEmpAge"
            _Qry &= vbCrLf & ",dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNEmpWorkAge"
            _Qry &= vbCrLf & ",ISNULL(MST.FNTotalWorkDayInYear,0)*480  AS FNTotalWorkDayInYear"
            _Qry &= vbCrLf & " , ET.FTEmpTypeCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , ET.FTEmpTypeNameTH as FTEmpTypeName"
            Else
                _Qry &= vbCrLf & " , ET.FTEmpTypeNameEN as FTEmpTypeName"
            End If
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempEmpStaticByYear AS MST WITH(NOLOCK) ON M.FNHSysEmpID=MST.FNHSysEmpID LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH(NOLOCK)  ON M.FNHSysPreNameId = P.FNHSysPreNameId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN"

            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH(NOLOCK) ON M.FNHSysDeptId=D.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON M.FNHSysSectId=S.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK)  ON M.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Qry &= vbCrLf & "WHERE M.FNHSYSCMPID= @FNHSysCmpId AND (M.FDDateStart <=@StartDate OR M.FDDateStart>@StartDate) AND (ISNULL(M.FDDateEnd,'') ='' OR ISNULL(M.FDDateEnd,'') > @StartDate) "



            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & "AND D.FTDeptCode >='" & Me.FNHSysDeptId.Text & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND D.FTDeptCode <='" & Me.FNHSysDeptIdTo.Text & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTSectCode >='" & Me.FNHSysSectId.Text & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTSectCode <='" & Me.FNHSysSectIdTo.Text & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & "AND U.FTUnitSectCode >='" & Me.FNHSysUnitSectId.Text & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND U.FTUnitSectCode <='" & Me.FNHSysUnitSectIdTo.Text & "'"
            End If
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & "AND ET.FTEmpTypeCode ='" & Me.FNHSysEmpTypeId.Text & "'"
            End If


            _Qry &= vbCrLf & "AND MST.FTUserLogin=@FTUserLogIn"
            _Qry &= vbCrLf & ") AS M LEFT OUTER JOIN "
            _Qry &= vbCrLf & "( SELECT FNHSysEmpID, FTDateTrans, FNLateNormalMin, FNRetireNormalMin, FNRetireOtMin, FNAbsentCut, FNAbsent"
            _Qry &= vbCrLf & ", FNTimeMin, FNOT1Min, FNOT1_5Min, FNOT2Min, FNOT3Min, FNOT4Min"
            _Qry &= vbCrLf & ", FNRetireMMin, FNRetireAfMin, FNCutAbsent, FNLateNormalNotCut"
            _Qry &= vbCrLf & ",FNLateAfMin"
            _Qry &= vbCrLf & ", FNLateMMin"
            _Qry &= vbCrLf & ", FNLateOtMin"

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(FNLateMMin,0) >0 THEN 1 else 0 end AS MAmounts"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(FNLateAfMin,0) >0 THEN 1 else 0 end AS AAmounts"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(FNLateOtMin,0) >0 THEN 1 else 0 end AS OTAmounts"

            _Qry &= vbCrLf & ",((CASE WHEN ISNULL(FNLateMMin,0) >0 THEN 1 else 0 end) +"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(FNLateAfMin,0) >0 THEN 1 else 0 end) +"
            _Qry &= vbCrLf & "(CASE WHEN ISNULL(FNLateOtMin,0) >0 THEN 1 else 0 end)) AS TotalAmountlate"

            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '0')"

            _Qry &= vbCrLf & "),0) AS FNLeaveSick"

            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '98')"

            _Qry &= vbCrLf & "),0) AS FNLeaveVacation"

            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND (FTLeaveType = '97')"

            _Qry &= vbCrLf & "),0) AS FNMaternity"

            _Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute) AS Expr1"

            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TS WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE(FNHSysEmpID = T.FNHSysEmpID)"
            _Qry &= vbCrLf & "AND (FTDateTrans = T.FTDateTrans) "
            _Qry &= vbCrLf & "AND NOT(FTLeaveType IN ('97','98','0'))"


            _Qry &= vbCrLf & "),0) AS FNBusiness"


            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE  FTDateTrans >=@StartDate AND FTDateTrans<=@EndDate"
            _Qry &= vbCrLf & "group by FNHSysEmpID, FTDateTrans, FNLateNormalMin, FNRetireNormalMin, FNRetireOtMin, FNAbsentCut, FNAbsent"
            _Qry &= vbCrLf & ", FNTimeMin, FNOT1Min, FNOT1_5Min, FNOT2Min, FNOT3Min, FNOT4Min"
            _Qry &= vbCrLf & ", FNRetireMMin, FNRetireAfMin, FNCutAbsent, FNLateNormalNotCut"
            _Qry &= vbCrLf & ",FNLateAfMin"
            _Qry &= vbCrLf & ", FNLateMMin"
            _Qry &= vbCrLf & ", FNLateOtMin"
            _Qry &= vbCrLf & ") AS T ON M.FNHSysEmpID = T.FNHSysEmpID "
            _Qry &= vbCrLf & "WHERE T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END"

            _Qry &= vbCrLf & "SELECT [FTEmpCode],[FTEmpName],"

            _Qry &= vbCrLf & "([FTSex]),[FNEmpAgeYear],[FNEmpAgeMonth],[FNEmpWorkAgeYear],"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth], [FTUnitSectCode]"
            _Qry &= vbCrLf & ",sum([FNLateMin]) AS [FNLateMin]"
            _Qry &= vbCrLf & ",floor(sum([FNLeaveBusinessHour])/60) AS [FNLeaveBusinessHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveBusinessMin])%60 AS [FNLeaveBusinessMin] "
            _Qry &= vbCrLf & ",floor(sum([FNLeaveVacationHour])/60) AS [FNLeaveVacationHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveVacationMin])%60 AS [FNLeaveVacationMin]"
            _Qry &= vbCrLf & ",floor(sum([FNLeaveMaternityHour])/60) AS [FNLeaveMaternityHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveMaternityMin])%60 AS [FNLeaveMaternityMin]"
            _Qry &= vbCrLf & ",floor(sum([FNLeaveSickHour])/60) AS [FNLeaveSickHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveSickMin])%60 AS [FNLeaveSickMin]"
            _Qry &= vbCrLf & ",floor(sum([FNLeaveAbsentHour])/60) AS [FNLeaveAbsentHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveAbsentMin])%60 AS [FNLeaveAbsentMin]"
            _Qry &= vbCrLf & ",floor(sum([FNLeaveBusinessUrgentHour])/60) AS [FNLeaveBusinessUrgentHour]"
            _Qry &= vbCrLf & ",sum([FNLeaveBusinessUrgentMin])%60 AS [FNLeaveBusinessUrgentMin]"
            _Qry &= vbCrLf & ",floor(sum([FNTotalHour])/60) AS [FNTotalHour]"
            _Qry &= vbCrLf & ",sum([FNTotalMin])%60 AS [FNTotalMin]"
            _Qry &= vbCrLf & ",sum([FNLateMMin])%60 AS [FNLateMMin]"
            _Qry &= vbCrLf & ",sum([FNLateAfMin])%60 AS [FNLateAfMin]"
            _Qry &= vbCrLf & ",sum([FNLateOtMin])%60 AS [FNLateOtMin]"
            _Qry &= vbCrLf & ",sum([TotalMin])%60 AS [TotalMin]"

            _Qry &= vbCrLf & ",sum([MorAmountLateTime]) AS [MorAmountLateTime]"
            _Qry &= vbCrLf & ",sum([AFAmountLateTime]) AS [AFAmountLateTime]"
            _Qry &= vbCrLf & ",sum([OTAmountLateTime]) AS [OTAmountLateTime]"
            _Qry &= vbCrLf & ",sum([SumLateAmountTime]) AS [SumLateAmountTime]"
            _Qry &= vbCrLf & ",sum([AmountBusiTime]) AS [AmountBusiTime]"
            _Qry &= vbCrLf & ",sum([AmountVacTime]) AS [AmountVacTime]"
            _Qry &= vbCrLf & ",sum([AmountMTTime]) AS [AmountMTTime]"
            _Qry &= vbCrLf & ",sum([SumAmountTimeFlowPlan]) AS [SumAmountTimeFlowPlan]"
            _Qry &= vbCrLf & ",floor(sum([SumHrFlowPlan])/60) AS [SumHrFlowPlan]"
            _Qry &= vbCrLf & ",sum([SumMinuteFlowPlan])%60 AS [SumMinuteFlowPlan]"
            _Qry &= vbCrLf & ",sum([AmountSickTime]) AS [AmountSickTime]"
            _Qry &= vbCrLf & ",sum([AmountAbTime]) AS [AmountAbTime]"
            _Qry &= vbCrLf & ",sum([oAmountBusiTime]) AS [oAmountBusiTime]"
            _Qry &= vbCrLf & ",sum([SumAmountTimeOutPlan]) AS [SumAmountTimeOutPlan]"
            _Qry &= vbCrLf & ",floor(sum([SumHrOutPlan])/60) AS [SumHrOutPlan]"
            _Qry &= vbCrLf & ",sum([SumMinuteOutPlan])%60 AS [SumMinuteOutPlan]"
            _Qry &= vbCrLf & ",sum([SumAllAmount]) AS [SumAllAmount]"
            _Qry &= vbCrLf & ",floor(sum([SumAllHr])/60) AS [SumAllHr]"
            _Qry &= vbCrLf & ",sum([SumAllMinute])%60 AS [SumAllMinute]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([BusiPercen])) AS [BusiPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([VacPercen])) AS [VacPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([MTPercen])) AS [MTPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([SumPercenFlowPlan])) AS [SumPercenFlowPlan]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([SickPercen])) AS [SickPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([AbPercen])) AS [AbPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([oBusiPercen])) AS [oBusiPercen]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([SumPercenOutPlan])) AS [SumPercenOutPlan]"
            _Qry &= vbCrLf & ",convert(numeric(18,2),sum([SumAllPercen])) AS [SumAllPercen]"
            _Qry &= vbCrLf & ",max([WorkDay]) AS [WorkDay]"
            _Qry &= vbCrLf & " , FTEmpTypeCode , FTEmpTypeName "
            _Qry &= vbCrLf & "FROM #Tab"
            If LeaveType() Then
                _Qry &= vbCrLf & "WHERE FNLeaveMin >= " & (Val(FTSTime.Text) * 60) + Microsoft.VisualBasic.Right(FTSTime.Text, 2) & " AND FNLeaveMin <= " & (Val(FTETime.Text) * 60) + Microsoft.VisualBasic.Right(FTETime.Text, 2) & ""
            End If
            _Qry &= vbCrLf & "GROUP BY [FTEmpCode],[FTEmpName],[FNEmpAgeYear],[FNEmpAgeMonth],[FNEmpWorkAgeYear],([FTSex]),"
            _Qry &= vbCrLf & "[FNEmpWorkAgeMonth], [FTUnitSectCode]  , FTEmpTypeCode , FTEmpTypeName "
            _Qry &= vbCrLf & "ORDER BY FTUnitSectCode"
            _Qry &= vbCrLf & "drop table #Tab"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            WorkDay = 0
            sumBusiHr = 0
            sumVacHr = 0
            sumMTHr = 0
            sumFlowPlanHr = 0
            sumSickHr = 0
            sumAbHr = 0
            sumBusiOutHr = 0
            sumOutPlanHr = 0
            AllHr = 0

            sumBusiMin = 0
            sumVacMin = 0
            sumMTMin = 0
            sumFlowPlanMin = 0
            sumSickMin = 0
            sumAbMin = 0
            sumBusiOutMin = 0
            sumOutPlanMin = 0
            AllMin = 0

            For Each R As DataRow In _dt.Rows
                WorkDay = WorkDay + R!WorkDay.ToString
                sumBusiHr = sumBusiHr + R!FNLeaveBusinessHour.ToString
                sumVacHr = sumVacHr + R!FNLeaveVacationHour.ToString
                sumMTHr = sumMTHr + R!FNLeaveMaternityHour.ToString
                sumFlowPlanHr = sumFlowPlanHr + R!SumHrFlowPlan.ToString
                sumSickHr = sumSickHr + R!FNLeaveSickHour.ToString
                sumAbHr = sumAbHr + R!FNLeaveAbsentHour.ToString
                sumBusiOutHr = sumBusiOutHr + R!FNLeaveBusinessUrgentHour.ToString
                sumOutPlanHr = sumOutPlanHr + R!SumHrOutPlan.ToString
                AllHr = AllHr + R!SumAllHr.ToString

                sumBusiMin = sumBusiMin + R!FNLeaveBusinessMin.ToString
                sumVacMin = sumVacMin + R!FNLeaveVacationMin.ToString
                sumMTMin = sumMTMin + R!FNLeaveMaternityMin.ToString
                sumFlowPlanMin = sumFlowPlanMin + R!SumMinuteFlowPlan.ToString
                sumSickMin = sumSickMin + R!FNLeaveSickMin.ToString
                sumAbMin = sumAbMin + R!FNLeaveAbsentMin.ToString
                sumBusiOutMin = sumBusiOutMin + R!FNLeaveBusinessUrgentMin.ToString
                sumOutPlanMin = sumOutPlanMin + R!SumMinuteOutPlan.ToString
                AllMin = AllMin + R!SumAllMinute.ToString
            Next

            Me.ogctime2.DataSource = _dt.Copy

        Catch ex As Exception

        End Try

        _dt.Dispose()
    End Sub


#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        Dim _FieldSum As String = "MorAmountLateTime|FNLateMMin|AFAmountLateTime|FNLateAfMin|OTAmountLateTime|FNLateOtMin|SumLateAmountTime|TotalMin|AmountBusiTime|FNLeaveBusinessHour|AmountVacTime|FNLeaveVacationHour|AmountMTTime|FNLeaveMaternityHour|SumAmountTimeFlowPlan|SumHrFlowPlan|AmountSickTime|FNLeaveSickHour|AmountAbTime|FNLeaveAbsentHour|oAmountBusiTime|FNLeaveBusinessUrgentHour|SumAmountTimeOutPlan|SumHrOutPlan|SumAllAmount|SumAllHr"
        Dim _FieldCustomSum As String = "BusiPercen|VacPercen|MTPercen|SumPercenFlowPlan|SickPercen|AbPercen|oBusiPercen|SumPercenOutPlan|SumAllPercen"
        Dim _FieldCustomMin As String = "|FNLeaveBusinessMin|FNLeaveVacationMin|FNLeaveMaternityMin|SumMinuteFlowPlan|FNLeaveSickMin|FNLeaveAbsentMin|FNLeaveBusinessUrgentMin|SumMinuteOutPlan|SumAllMinute"
        'Dim _FieldCustomGrpSum As String = "BusiPercen"
        With ogvtime2
            For Each Str As String In _FieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next
            For Each Str As String In _FieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next
            For Each Str As String In _FieldCustomMin.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded


            .ExpandAllGroups()
            .RefreshData()
        End With

    End Sub
#End Region

#Region "Custom summaries"

    Private totalMinBusi As Integer = 0
    'Private B As Double
    'Private V As Double
    'Private M As Double
    'Private S As Double
    'Private A As Double
    'Private Bo As Double

    Private Sub InitStartValue()
        totalMinBusi = 0
        'B = 0
        'V = 0
        'M = 0
        'S = 0
        'A = 0
        'Bo = 0
        'sumPerPlan = 0
    End Sub


    Private Sub AdvBandedGridView1_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime2.CustomSummaryCalculate

        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName.ToString

            Case "BusiPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then

                    If e.IsTotalSummary Then
                        totalMinBusi = (sumBusiHr * 60) + sumBusiMin Mod 60
                    End If

                    If e.IsTotalSummary Then
                        Dim Netbusi As Double
                        Netbusi = (totalMinBusi * 100) / WorkDay
                        e.TotalValue = Netbusi
                    End If
                End If
            Case "VacPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetVac As Double
                        NetVac = (((sumVacHr * 60) + (sumVacMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetVac
                    End If
                End If
            Case "MTPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetMT As Double
                        NetMT = (((sumMTHr * 60) + (sumMTMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetMT
                    End If
                End If
            Case "SickPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSick As Double
                        NetSick = (((sumSickHr * 60) + (sumSickMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetSick
                    End If
                End If
            Case "AbPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetAb As Double
                        NetAb = (((sumAbHr * 60) + (sumAbMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetAb
                    End If
                End If
            Case "oBusiPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetoBusi As Double
                        NetoBusi = (((sumBusiOutHr * 60) + (sumBusiOutMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetoBusi
                    End If
                End If
            Case "SumPercenFlowPlan"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumPlan As Double
                        NetSumPlan = (((sumFlowPlanHr * 60) + (sumFlowPlanMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetSumPlan
                    End If
                End If
            Case "SumPercenOutPlan"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumOutPlan As Double
                        NetSumOutPlan = (((sumOutPlanHr * 60) + (sumOutPlanMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetSumOutPlan
                    End If
                End If
            Case "SumAllPercen"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Double
                        NetSumAll = (((AllHr * 60) + (AllMin Mod 60)) * 100) / WorkDay
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveBusinessMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumBusiMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveVacationMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumVacMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveMaternityMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumMTMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "SumMinuteFlowPlan"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumFlowPlanMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveSickMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumSickMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveAbsentMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumAbMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "FNLeaveBusinessUrgentMin"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumBusiOutMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "SumMinuteOutPlan"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = sumOutPlanMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
            Case "SumAllMinute"
                If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
                    If e.IsTotalSummary Then
                        Dim NetSumAll As Integer
                        NetSumAll = AllMin Mod 60
                        e.TotalValue = NetSumAll
                    End If
                End If
        End Select

    End Sub
#End Region


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
            Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

            Call LoadData(_Spls)
            Call LoadData2(_Spls)
            _Spls.Close()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub
    Dim _Leave As String = ""
    Dim _LeaveMin As Integer
    Dim _LeaveType As Integer
    Private Function LeaveType() As Boolean
        If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) <> 0 Then
            _LeaveType = HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex)
            Select Case _LeaveType
                Case "1"
                    _Leave = ",ISNULL(T.FNLeaveSick,0)"
                Case "2"
                    _Leave = ",CASE WHEN ISNULL(T.FNTimeMin,0) <= 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END AS FNLeaveMin"
                Case "3"
                    _Leave = ",CASE WHEN ISNULL(T.FNTimeMin,0) > 0 THEN ISNULL(T.FNBusiness,0)  ELSE 0 END AS FNLeaveMin"
                Case "97"
                    _Leave = ",ISNULL(T.FNMaternity,0)"
                Case "98"
                    _Leave = ",ISNULL(T.FNLeaveVacation,0)"
                Case "99"
                    _Leave = ",ISNULL(T.FNAbsent,0)"
            End Select
            _LeaveMin = (Val(FTSTime.Text) * 60) + Microsoft.VisualBasic.Right(FTSTime.Text, 2)
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call SetGrid()
    End Sub

    Private Sub SetGrid()
        With ogvtime
            .OptionsView.ColumnAutoWidth = False
        End With
        With ogvtime2
            .OptionsView.ColumnAutoWidth = False
            .OptionsView.ShowAutoFilterRow = True
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
    End Sub

    Private Sub FNLeaveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNLeaveType.SelectedIndexChanged
        Select Case FNLeaveType.SelectedIndex
            Case 0
                FTSTime.Text = ""
                FTETime.Text = ""
        End Select
    End Sub


End Class
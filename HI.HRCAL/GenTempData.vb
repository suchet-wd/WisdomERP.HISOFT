Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

Public Class GenTempData



    Public Shared Function GetCauseResign(ByVal _User As String, _condition As String) As String

        Dim _QrySql As String
        Try

            _QrySql = "DELETE FROM TRPTTmpCauseResign WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _QrySql &= "INSERT INTO TRPTTmpCauseResign(UserLogin,FNHSysEmpID,FTResign,FTYear,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12)"
            _QrySql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS   UserLogin,THRMEmployee.FNHSysEmpID"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _QrySql &= vbCrLf & ", isnull (R.FTResignCode+' '+ R.FTResignNameTH ,'')"
            Else
                _QrySql &= vbCrLf & ", isnull (R.FTResignCode+' '+ R.FTResignNameEN ,'')"
            End If

            _QrySql &= vbCrLf & " ,CASE WHEN ISDATE(THRMEmployee.FDDateEnd) = 1 THEN "
            _QrySql &= vbCrLf & "YEAR(THRMEmployee.FDDateEnd) ELSE 0 END AS YEAR"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 1 THEN 1 ELSE 0 END else 0 end as M1"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 2 THEN 1 ELSE 0 END else 0 end as M2"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 3 THEN 1 ELSE 0 END else 0 end as M3"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 4 THEN 1 ELSE 0 END else 0 end as M4"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 5 THEN 1 ELSE 0 END else 0 end as M5"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 6 THEN 1 ELSE 0 END else 0 end as M6"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 7 THEN 1 ELSE 0 END else 0 end as M7"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 8 THEN 1 ELSE 0 END else 0 end as M8"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 9 THEN 1 ELSE 0 END else 0 end as M9"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 10 THEN 1 ELSE 0 END else 0 end as M10"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 11 THEN 1 ELSE 0 END else 0 end as M11"
            _QrySql &= vbCrLf & ",case when isdate(THRMEmployee.FDDateEnd)=1 then CASE WHEN MONTH(THRMEmployee.FDDateEnd) = 12 THEN 1 ELSE 0 END else 0 end as M12"
            _QrySql &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS THRMEmployee "
            _QrySql &= vbCrLf & "   INNER Join "
            _QrySql &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS THRMEmpType WITH(NOLOCK)  ON THRMEmployee.FNHSysEmpTypeId = THRMEmpType.FNHSysEmpTypeId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS TCNMDepartment WITH (Nolock) ON THRMEmployee.FNHSysDeptId = TCNMDepartment.FNHSysDeptId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS TCNMDivision WITH (NOLOCK) ON THRMEmployee.FNHSysDivisonId = TCNMDivision.FNHSysDivisonId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS TCNMSect WITH (NOLOCK) ON THRMEmployee.FNHSysSectId = TCNMSect.FNHSysSectId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS TCNMUnitSect WITH (NOLOCK) ON THRMEmployee.FNHSysUnitSectId = TCNMUnitSect.FNHSysUnitSectId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMResign AS R WITH (NOLOCK) ON THRMEmployee.FNHSysResignId = R.FNHSysResignId "
            If _condition <> "" Then
                _QrySql &= vbCrLf & " WHERE " & _condition
            End If

            HI.Conn.SQLConn.ExecuteNonQuery(_QrySql, Conn.DB.DataBaseName.DB_HR)
            Return "TRPTTmpCauseResign"

        Catch ex As Exception
            Return ""
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function


    Public Shared Function GetTempTRPTTempTWorkingTimernOver(ByVal _User As String, ByVal _condition As String, ByVal _condition2 As String) As String

        Dim _QrySql As String

        Try

            _QrySql = "DELETE FROM TRPTTempTWorkingTimernOver WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _QrySql &= vbCrLf & "INSERT INTO TRPTTempTWorkingTimernOver(UserLogin, FNHSysSectId, EMPCOUNT, T1, IN1, OUT1, PER1, T2, IN2, OUT2, PER2, T3, IN3, OUT3, PER3, T4, IN4, OUT4, PER4, T5,"
            _QrySql &= vbCrLf & " IN5, OUT5, PER5, T6, IN6, OUT6, PER6, T7, IN7, OUT7, PER7, T8, IN8, OUT8, PER8, T9, IN9, OUT9, PER9, T10, IN10, OUT10, PER10, T11, IN11, OUT11, PER11, T12, "
            _QrySql &= vbCrLf & " IN12, OUT12, PER12)"
            _QrySql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS UserLogin,FNHSysSectId,EMPCOUNT"
            For i = 1 To 12

                _QrySql &= vbCrLf & ",T" & i & ",IN" & i & " ,OUT" & i & ",CASE WHEN T" & i & "= 0 THEN 0 ELSE  CONVERT(NUMERIC(18,2),OUT" & i & "*100/T" & i & " )END AS PER" & i
            Next
            _QrySql &= vbCrLf & "FROM("
            _QrySql &= vbCrLf & "      SELECT FNHSysSectId,COUNT(COUNTEmp) AS EMPCOUNT"
            For i = 1 To 12
                _QrySql &= vbCrLf & "      ,SUM(T" & i & ") AS T" & i & ",SUM(IN" & i & ") AS IN" & i & ",CONVERT(NUMERIC(18,2),SUM(OUT" & i & ")) AS OUT" & i & ""
            Next
            _QrySql &= vbCrLf & "      FROM ("
            _QrySql &= vbCrLf & "      SELECT M.FNHSysEmpID,M.FNHSysEmpTypeId,M.FNHSysSectId,M.FDDateStart,M.FDDateEnd,1 AS COUNTEmp"

            For i = 1 To 12
                _QrySql &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateEnd) = 1 THEN CASE WHEN LEFT(M.FDDateEnd,7) > '" & _condition2 & "/" & Format(i, "00") & "' THEN 1 ELSE 0 END ELSE CASE WHEN LEFT(M.FDDateStart,7)<='" & _condition2 & "/" & Format(i, "00") & "' THEN 1 ELSE 0 END END  AS T" & i
                _QrySql &= vbCrLf & "  ,CASE WHEN M.FDDateEnd='' AND LEFT(M.FDDateStart,7) = '" & _condition2 & "/" & Format(i, "00") & "' THEN 1 ELSE 0 END  AS IN" & i & ""
                _QrySql &= vbCrLf & "  ,CASE WHEN  ISDATE(M.FDDateEnd)= 1 THEN CASE WHEN LEFT(M.FDDateEnd,7)='" & _condition2 & "/" & Format(i, "00") & "' THEN 1 ELSE 0 END ELSE 0 END AS OUT" & i & ""
            Next

            _QrySql &= vbCrLf & " FROM THRMEmployee AS M WITH(NOLOCK)"
            _QrySql &= vbCrLf & "   INNER Join "
            _QrySql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "

            If _condition <> "" Then
                _QrySql &= vbCrLf & " WHERE " & _condition
            End If

            _QrySql &= vbCrLf & " ) AS T"
            _QrySql &= vbCrLf & " GROUP BY FNHSysSectId"
            _QrySql &= vbCrLf & " ) AS E"

            HI.Conn.SQLConn.ExecuteNonQuery(_QrySql, Conn.DB.DataBaseName.DB_HR)

            Return "TRPTTempTWorkingTimernOver"
        Catch ex As Exception
            Return ""
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Shared Function GenTempWorkingTimeAttendance(ByVal _user As String, ByVal _condition As String, ByVal tDate As String) As String
        Dim _QrySql As String

        Try
            _QrySql = "  DELETE FROM THRTTempWorkingAttendant WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_QrySql, Conn.DB.DataBaseName.DB_HR)

            _QrySql = "INSERT INTO  THRTTempWorkingAttendant( UserLogIn,  FNTypeSeq, FNGrpSeq, FNCountEmp, FNDay01, FNDay02, FNDay03, FNDay04, FNDay05, FNDay06, FNDay07, FNDay08, "
            _QrySql &= vbCrLf & "             FNDay09, FNDay10, FNDay11, FNDay12, FNDay13, FNDay14, FNDay15, FNDay16, FNDay17, FNDay18, FNDay19, FNDay20, FNDay21, FNDay22, FNDay23, "
            _QrySql &= vbCrLf & "             FNDay24, FNDay25, FNDay26, FNDay27, FNDay28, FNDay29, FNDay30, FNDay31)"
            _QrySql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',* "
            _QrySql &= vbCrLf & "FROM ("

            _QrySql &= vbCrLf & "SELECT FNHSysSectId AS FNTypeSeq,1 As FNGrpSeq,Count ( FNHSysEmpID) AS FNCountEmp "
            For i = 1 To 31
                _QrySql &= vbCrLf & ",SUM( CASE WHEN FDDateEnd ='" & tDate & "/" & Format(i, "00") & "'  THEN 1  Else 0 END ) AS FNDay" & Format(i, "00")
            Next

            _QrySql &= vbCrLf & "  FROM  ("
            _QrySql &= vbCrLf & "  SELECT   M.FNHSysEmpID,M.FNHSysSectId,ISNULL(M.FDDateStart,'') AS FDDateEnd"
            '_QrySql &= vbCrLf & "  SELECT   M.FNHSysEmpID,M.FNHSysSectId,ISNULL(M.FDDateEnd,'') AS FDDateEnd"
            _QrySql &= vbCrLf & "  FROM  THRMEmployee AS M WITH (NOLOCK)"
            _QrySql &= vbCrLf & "   INNER Join "
            _QrySql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
            _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " ].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "
            _QrySql &= vbCrLf & "  WHERE  M.FNHSysEmpID <>0  "

            If _condition <> "" Then
                _QrySql &= vbCrLf & "    AND  " & _condition
            End If

            _QrySql &= vbCrLf & " ) T  Group By FNHSysSectId "
            _QrySql &= vbCrLf & " ) AS TF "

            HI.Conn.SQLConn.ExecuteNonQuery(_QrySql, Conn.DB.DataBaseName.DB_HR)
            Return "THRTTempWorkingAttendant"
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return ""
        End Try
    End Function


    Public Shared Sub GenerateEmpPicture(Condition As Object)
        Dim _spls As New HI.TL.SplashScreen("Gerating... Employee Picture.Please Wait.", "Preview Report")
        Try
            Dim _Formular As String = ""
            Dim tText As String = ""
            Try
                tText = Condition.GetCriteria
            Catch ex As Exception
            End Try

            If tText <> "" Then
                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= "" & tText
            End If


            Dim _PathEmpPic As String
            _PathEmpPic = ""
            Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

            _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


            Dim oDtb As DataTable
            Dim tsql As String
            Dim path As String


            tsql = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp   WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

            tsql = "   SELECT        M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, "
            tsql &= vbCrLf & " M.FNHSysPreNameId, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN,"
            tsql &= vbCrLf & "  M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID, M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId,"
            tsql &= vbCrLf & " M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId, M.FNHSysSectId,"
            tsql &= vbCrLf & " M.FNHSysUnitSectId, M.FNHSysPositId, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight,"
            tsql &= vbCrLf & " M.FCHeight, M.FNHSysRaceId, M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign,"
            tsql &= vbCrLf & "  M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo, M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta,"
            tsql &= vbCrLf & "  M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,"
            tsql &= vbCrLf & "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome,"
            tsql &= vbCrLf & "  M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad, M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1,"
            tsql &= vbCrLf & "  M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1, M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1,"
            tsql &= vbCrLf & " M.FTMobile, M.FTEmail, M.FNSalary, M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,"
            tsql &= vbCrLf & "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1,"
            tsql &= vbCrLf & "  M.FTRefNote1, M.FTFatherName, M.FNFatherLife, M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel,"
            tsql &= vbCrLf & "  M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer, M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName,"
            tsql &= vbCrLf & "  M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel, M.FTMateFatherName,"
            tsql &= vbCrLf & " M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium,"
            tsql &= vbCrLf & " M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents,"
            tsql &= vbCrLf & " M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather, M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,"
            tsql &= vbCrLf & " M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd, M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire,"
            tsql &= vbCrLf & "   M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode"
            tsql &= vbCrLf & " FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
            tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
            tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN"
            tsql &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            tsql &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
            tsql &= vbCrLf & " WHERE M.FTEmpCode<>'' "

            If _Formular <> "" Then
                tsql &= vbCrLf & "  AND ( " & _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") & " ) "
            End If

            If Not (HI.ST.SysInfo.Admin) Then

                tsql &= vbCrLf & "  AND M.FNHSysEmpTypeId IN ("
                tsql &= vbCrLf & " Select DISTINCT UPT.FNHSysEmpTypeId"
                tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
                tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
                tsql &= vbCrLf & "  )      "
                tsql &= vbCrLf & " AND M.FNHSysSectId IN ( "
                tsql &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
                tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
                tsql &= vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
                tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
                tsql &= vbCrLf & "  AND UPT.FTStateAll='1' "
                tsql &= vbCrLf & " UNION"
                tsql &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
                tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
                tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
                tsql &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT.FNHSysSectId = S.FNHSysSectId  "
                tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
                tsql &= vbCrLf & "  )      "

            End If

            oDtb = HI.Conn.SQLConn.GetDataTable(tsql, Conn.DB.DataBaseName.DB_HR)

            If oDtb.Rows.Count > 0 Then

                Dim _TGrp As Integer = 0

                If oDtb.Rows.Count Mod 2 = 1 Then
                    _TGrp = (oDtb.Rows.Count \ 2) + 1
                End If

                Dim GrpSeqNo As Integer = 1
                Dim SeqNo As Integer = 1
                For Each Item In oDtb.Rows

                    If GrpSeqNo = _TGrp And _TGrp <> 0 Then

                        tsql = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo)"
                        tsql &= vbCrLf & " VALUES (''," & Item!FNHSysEmpID.ToString & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & CInt(_TGrp) & "','" & CInt(99) & "')"
                        HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

                    End If


                    If Item!FTEmpPicName.ToString Like "*.*" Or Item!FTEmpPicName = "" Then
                        If _PathEmpPic = "" Then
                            path = HI.ST.SysInfo.SysPath & "EmpPicture\" & Item!FTEmpPicName.ToString
                        Else
                            path = _PathEmpPic & Item!FTEmpPicName.ToString
                        End If

                        If File.Exists(path) Then

                            Dim Imag As Byte() = HI.UL.ULImage.ConvertImageToByteArray(path)

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
                            HI.Conn.SQLConn.SqlConnectionOpen()

                            tsql = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo,FTEmpPicture) "
                            tsql &= vbCrLf & " VALUES( @CmpCode,@EmpCode,@UserLogin,@GrpSeqNo,@SeqNo,@Images)"

                            Dim cmd As New SqlCommand(tsql, HI.Conn.SQLConn.Cnn)
                            cmd.Parameters.AddWithValue("@CmpCode", "")
                            cmd.Parameters.AddWithValue("@EmpCode", Integer.Parse(Val(Item!FNHSysEmpID.ToString)))
                            cmd.Parameters.AddWithValue("@UserLogin", HI.ST.UserInfo.UserName)
                            cmd.Parameters.AddWithValue("@GrpSeqNo", CInt(GrpSeqNo))
                            cmd.Parameters.AddWithValue("@SeqNo", CInt(SeqNo))

                            Dim p As New SqlParameter("@Images", SqlDbType.VarBinary)
                            p.Value = Imag
                            cmd.Parameters.Add(p)
                            cmd.ExecuteNonQuery()

                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                        Else

                            tsql = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo)"
                            tsql &= vbCrLf & " VALUES (''," & Item!FNHSysEmpID.ToString & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & CInt(GrpSeqNo) & "','" & CInt(SeqNo) & "')"

                            HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

                        End If

                        If SeqNo = 2 Then
                            SeqNo = 0
                            GrpSeqNo += 1
                        End If

                        SeqNo += 1
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
        _spls.Close()
    End Sub


    'Public Shared Sub GenerateEmpPicture(Condition As Object)
    '    Dim _spls As New HI.TL.SplashScreen("Gerating... Employee Picture.Please Wait.", "Preview Report")
    '    Try
    '        Dim _Formular As String = ""
    '        Dim tText As String = ""
    '        Try
    '            tText = Condition.GetCriteria
    '        Catch ex As Exception
    '        End Try

    '        If tText <> "" Then
    '            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
    '            _Formular &= "" & tText
    '        End If

    '        Dim oDtb As DataTable
    '        Dim tsql As String
    '        Dim path As String


    '        tsql = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp   WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '        HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

    '        tsql = "   SELECT        M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, "
    '        tsql &= vbCrLf & " M.FNHSysPreNameId, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN,"
    '        tsql &= vbCrLf & "  M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID, M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId,"
    '        tsql &= vbCrLf & " M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId, M.FNHSysSectId,"
    '        tsql &= vbCrLf & " M.FNHSysUnitSectId, M.FNHSysPositId, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight,"
    '        tsql &= vbCrLf & " M.FCHeight, M.FNHSysRaceId, M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign,"
    '        tsql &= vbCrLf & "  M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo, M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta,"
    '        tsql &= vbCrLf & "  M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,"
    '        tsql &= vbCrLf & "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome,"
    '        tsql &= vbCrLf & "  M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad, M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1,"
    '        tsql &= vbCrLf & "  M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1, M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1,"
    '        tsql &= vbCrLf & " M.FTMobile, M.FTEmail, M.FNSalary, M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,"
    '        tsql &= vbCrLf & "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1,"
    '        tsql &= vbCrLf & "  M.FTRefNote1, M.FTFatherName, M.FNFatherLife, M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel,"
    '        tsql &= vbCrLf & "  M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer, M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName,"
    '        tsql &= vbCrLf & "  M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel, M.FTMateFatherName,"
    '        tsql &= vbCrLf & " M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium,"
    '        tsql &= vbCrLf & " M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents,"
    '        tsql &= vbCrLf & " M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather, M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,"
    '        tsql &= vbCrLf & " M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd, M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire,"
    '        tsql &= vbCrLf & "   M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode"
    '        tsql &= vbCrLf & " FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
    '        tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
    '        tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
    '        tsql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN"
    '        tsql &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
    '        tsql &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
    '        tsql &= vbCrLf & " WHERE M.FTEmpCode<>'' "

    '        If _Formular <> "" Then
    '            tsql &= vbCrLf & "  AND ( " & _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") & " ) "
    '        End If

    '        If Not (HI.ST.SysInfo.Admin) Then

    '            tsql &= vbCrLf & "  AND M.FNHSysEmpTypeId IN ("
    '            tsql &= vbCrLf & " Select DISTINCT UPT.FNHSysEmpTypeId"
    '            tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
    '            tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
    '            tsql &= vbCrLf & "  )      "
    '            tsql &= vbCrLf & " AND M.FNHSysSectId IN ( "
    '            tsql &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
    '            tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
    '            tsql &= vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
    '            tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
    '            tsql &= vbCrLf & "  AND UPT.FTStateAll='1' "
    '            tsql &= vbCrLf & " UNION"
    '            tsql &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
    '            tsql &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
    '            tsql &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
    '            tsql &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT.FNHSysSectId = S.FNHSysSectId  "
    '            tsql &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            tsql &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
    '            tsql &= vbCrLf & "  )      "

    '        End If

    '        oDtb = HI.Conn.SQLConn.GetDataTable(tsql, Conn.DB.DataBaseName.DB_HR)

    '        If oDtb.Rows.Count > 0 Then

    '            Dim _TGrp As Integer = 0

    '            If oDtb.Rows.Count Mod 2 = 1 Then
    '                _TGrp = (oDtb.Rows.Count \ 2) + 1
    '            End If

    '            Dim GrpSeqNo As Integer = 1
    '            Dim SeqNo As Integer = 1
    '            For Each Item In oDtb.Rows

    '                If GrpSeqNo = _TGrp And _TGrp <> 0 Then

    '                    tsql = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo)"
    '                    tsql &= vbCrLf & " VALUES (''," & Item!FNHSysEmpID.ToString & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & CInt(_TGrp) & "','" & CInt(99) & "')"
    '                    HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

    '                End If


    '                If Item!FTEmpPicName.ToString Like "*.*" Or Item!FTEmpPicName = "" Then

    '                    path = HI.ST.SysInfo.SysPath & "EmpPicture\" & Item!FTEmpPicName.ToString

    '                    If File.Exists(path) Then


    '                        Dim Imag As Byte() = HI.UL.ULImage.ConvertImageToByteArray(path)

    '                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '                        HI.Conn.SQLConn.SqlConnectionOpen()

    '                        tsql = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo,FTEmpPicture) "
    '                        tsql &= vbCrLf & " VALUES( @CmpCode,@EmpCode,@UserLogin,@GrpSeqNo,@SeqNo,@Images)"

    '                        Dim cmd As New SqlCommand(tsql, HI.Conn.SQLConn.Cnn)
    '                        cmd.Parameters.AddWithValue("@CmpCode", "")
    '                        cmd.Parameters.AddWithValue("@EmpCode", Integer.Parse(Val(Item!FNHSysEmpID.ToString)))
    '                        cmd.Parameters.AddWithValue("@UserLogin", HI.ST.UserInfo.UserName)
    '                        cmd.Parameters.AddWithValue("@GrpSeqNo", CInt(GrpSeqNo))
    '                        cmd.Parameters.AddWithValue("@SeqNo", CInt(SeqNo))

    '                        Dim p As New SqlParameter("@Images", SqlDbType.VarBinary)
    '                        p.Value = Imag
    '                        cmd.Parameters.Add(p)
    '                        cmd.ExecuteNonQuery()

    '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

    '                    Else

    '                        tsql = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeePicTemp(FTCmpCode,FNHSysEmpID,UserLogIn,FTGrpSeqNo,FTSeqNo)"
    '                        tsql &= vbCrLf & " VALUES (''," & Item!FNHSysEmpID.ToString & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & CInt(GrpSeqNo) & "','" & CInt(SeqNo) & "')"

    '                        HI.Conn.SQLConn.ExecuteNonQuery(tsql, Conn.DB.DataBaseName.DB_HR)

    '                    End If

    '                    If SeqNo = 2 Then
    '                        SeqNo = 0
    '                        GrpSeqNo += 1
    '                    End If

    '                    SeqNo += 1
    '                End If

    '            Next
    '        End If
    '    Catch ex As Exception

    '    End Try
    '    _spls.Close()
    'End Sub

End Class

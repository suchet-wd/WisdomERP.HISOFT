Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Globalization.DateTimeFormatInfo
Imports Microsoft.VisualBasic
Imports System.Math
Imports System.Text

Public Class Social

    Public Enum TaxType As Integer
        ภงด1 = 0
        ภงด1ก = 1
        ภงด91 = 2
    End Enum

    Public Shared Function Export(_Tax As TaxType, _CmpCode As String, _EmpType As String,
                                    _Year As String, _Month As String, Optional _Formular As String = "") As DataTable

        Dim _File As New DataTable
        Dim _Qry As String = ""
        Dim _DtPayRoll As DataTable
        Dim _Dt As DataTable
        Dim _StateExport As Boolean = True

        Dim _ComName As String = ""
        Dim _ComID As String = ""
        Dim _ComTaxID As String = ""
        Dim _ComBnkBranchID As String = ""
        Dim _ComAcc As String = ""
        Dim _GAmount As Double = 0
        Dim _TotalRec As Integer = 0

        Dim FTYearThai As String = Format(Val(_Year) + 543, "0000")

        Dim _PayDateforTransfer As String = ""

        _Qry = " SELECT TOP 1 FNHSysCmpId, FNHSysCmpTitleId, FTCmpCode, FTCmpNameTH, FTCmpNameEN, "
        _Qry &= vbCrLf & " FTAddr1TH, FTAddr2TH, FTSubDistrictTH, FTDistrictTH, FTProvinceTH, FTAddr1EN, FTAddr2EN, FTSubDistrictEN, FTDistrictEN, FTProvinceEN, FTPostCode, FTPhone,"
        _Qry &= vbCrLf & "  FTFax, FTMobile, FTMail, FTWebSite, FTNote, FTTaxNo, FTSocialNo, FNHSysBankId, FTBankBranchCode, FTDepositCode, FTBnkAccNo, FTBnkAccName,"
        _Qry &= vbCrLf & "  FTBchSocial, FTBchTax, FPImageCmpLogo, FTDocRun, FTStateActive"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In _Dt.Rows
            _ComName = R!FTCmpNameTH.ToString
            _ComID = R!FTDepositCode.ToString
            _ComAcc = R!FTBnkAccNo.ToString.Replace("-", "")
            _ComTaxID = R!FTTaxNo.ToString
            _ComBnkBranchID = ""
        Next
        _PayDateforTransfer = Format(Val(_Year), "0000") & "/" & Format(Val(_Month), "00") & "/" & "01"

        _Qry = "select CONVERT(varchar,dateadd(d,-(day(dateadd(m,1,'" & _PayDateforTransfer & "'))),dateadd(m,1,'" & _PayDateforTransfer & "')),112) AS 'paydate'"
        _PayDateforTransfer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = ""

        Dim pay30day_flag As String
        _Qry = " SELECT ISNULL(FTCfgData, 0) AS FTCfgData FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig WHERE  (FTCfgName = 'Pay30day_Flag') "
        pay30day_flag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        Select Case _Tax
            Case TaxType.ภงด1


                _Qry = "  	SELECT    ROW_NUMBER() OVER(ORDER BY P.FTEmpIdNo) AS FROMTYPE, '0000000000000' AS ComIDNO,'" & _ComTaxID & "' AS COMTaxNo, '" & _ComBnkBranchID & "' AS Tax_BranchNo,"
                _Qry &= vbCrLf & "   P.FTEmpIdNo , '0000000000' AS FTTaxNo, PP.FTPreNameNameTH AS FTEmpPreCode, M.FTEmpNameTH, M.FTEmpSurnameTH,"
                _Qry &= vbCrLf & "   (ISNULL(M.FTAddrNo,'')+ISNULL(M.FTAddrHome,'')+ISNULL(M.FTAddrMoo,'')+ISNULL(M.FTAddrSoi,'')+ISNULL(M.FTAddrRoad,'')+ISNULL(M.FTAddrTumbol,'')+ISNULL(M.FTAddrAmphur,'')+ISNULL(M.FTAddrProvince,'')) AS EmpAddress,"
                _Qry &= vbCrLf & "   M. FTAddrPostCode,'" & Format(Val(_Month), "00") & "'  AS MonthPay,'" & FTYearThai & "'  AS YearPay, '1' AS IncomCode, '" & _PayDateforTransfer & "' AS PayDate, '0' AS TaxRate,"
                _Qry &= vbCrLf & "   SUM(P.FNTotalRecalTAX) AS TOTALPAY"
                _Qry &= vbCrLf & "   ,SUM(P.FNTax) AS SUMTAX, '1' AS TaxCondition"

                _Qry &= vbCrLf & "   FROM   (((THRTPayRoll P "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN THRMCfgPayDT PD ON ((P.FTPayYear=PD.FTPayYear)  "
                _Qry &= vbCrLf & "    AND (P.FTPayTerm=PD.FTPayTerm)) AND (P.FNHSysEmpTypeId=PD.FNHSysEmpTypeId)) "
                _Qry &= vbCrLf & "   INNER JOIN THRMEmployee M ON P.FNHSysEmpID=M.FNHSysEmpID) "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN V_MPrename PP ON M.FNHSysPreNameId=PP.FNHSysPreNameId) "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN V_MCmp V_MCmp ON M.FNHSysCmpId=V_MCmp.FNHSysCmpId "


                If pay30day_flag = "1" Then
                    _Qry &= vbCrLf & "  LEFT JOIN HITECH_MASTER.dbo.THRMEmpType ET ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
                    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                    _Qry &= vbCrLf & " AND (ET.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND FTEmpTypeCode not in ('M','N','O', 'M1','N1','O1', 'M2','N2', 'M3','N3'))"

                Else
                    _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
                    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

                End If
                '_Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
                '_Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "


                _Qry &= vbCrLf & "  GROUP BY PP.FTPreNameNameTH, V_MCmp.FTTaxNo, PD.FNMonth
                                     , PD.FTPayYear, P.FTEmpIdNo, M.FTEmpNameTH, M.FTEmpSurnameTH
                                     , V_MCmp.FTBchTax
                                     , M.FTTaxNo, P.FTPayYear
                                     ,ISNULL(M.FTAddrNo,''),ISNULL(M.FTAddrHome,''),ISNULL(M.FTAddrMoo,''),ISNULL(M.FTAddrSoi,''),ISNULL(M.FTAddrRoad,''),ISNULL(M.FTAddrTumbol,''),ISNULL(M.FTAddrAmphur,''),ISNULL(M.FTAddrProvince,'')
                                     ,M. FTAddrPostCode "
            Case TaxType.ภงด1ก
                _Qry = "  SELECT FROMTYPE, ComIDNO, COMTaxNo, Tax_BranchNo ,FTEmpIdNo , FTTaxNo "
                _Qry &= vbCrLf & "   , PP.FTPreNameNameTH AS FTEmpPreCode , FTEmpNameTH, FTEmpSurnameTH "
                _Qry &= vbCrLf & "    ,(ISNULL(FTAddrNo,'')+ISNULL(FTAddrHome,'')+ISNULL(FTAddrMoo,'')+ISNULL(FTAddrSoi,'')+ISNULL(FTAddrRoad,'')+ISNULL(FTAddrTumbol,'')+ISNULL(FTAddrAmphur,'')+ISNULL(FTAddrProvince,'')) AS EmpAddress "
                _Qry &= vbCrLf & "   ,FTAddrPostCode"
                _Qry &= vbCrLf & "  , MonthPay, YearPay, IncomCode, PayDate, TaxRate "
                _Qry &= vbCrLf & "  , TOTALPAY, SUMTAX, TaxCondition "
                _Qry &= vbCrLf & "  FROM (  "
                _Qry &= vbCrLf & " "


                _Qry &= vbCrLf & " 	SELECT    ROW_NUMBER() OVER(ORDER BY P.FTEmpIdNo) AS FROMTYPE, '0000000000000' AS ComIDNO,'" & _ComTaxID & "' AS COMTaxNo, '" & _ComBnkBranchID & "' AS Tax_BranchNo,"
                _Qry &= vbCrLf & "   P.FTEmpIdNo , '0000000000' AS FTTaxNo"
                _Qry &= vbCrLf & "  ,'" & Format(Val(_Month), "00") & "'  AS MonthPay,'" & FTYearThai & "'  AS YearPay, '1' AS IncomCode, '" & _PayDateforTransfer & "' AS PayDate, '0' AS TaxRate,"
                _Qry &= vbCrLf & "   SUM(P.FNTotalRecalTAX) AS TOTALPAY"
                _Qry &= vbCrLf & "   ,SUM(P.FNTax) AS SUMTAX, '1' AS TaxCondition"

                _Qry &= vbCrLf & "   FROM   (((THRTPayRoll P "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN THRMCfgPayDT PD ON ((P.FTPayYear=PD.FTPayYear)  "
                _Qry &= vbCrLf & "    AND (P.FTPayTerm=PD.FTPayTerm)) AND (P.FNHSysEmpTypeId=PD.FNHSysEmpTypeId)) "
                _Qry &= vbCrLf & "   INNER JOIN THRMEmployee M ON P.FNHSysEmpID=M.FNHSysEmpID) "
                _Qry &= vbCrLf & "   ) "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN V_MCmp V_MCmp ON M.FNHSysCmpId=V_MCmp.FNHSysCmpId "

                If pay30day_flag = "1" Then

                    _Qry &= vbCrLf & "  LEFT JOIN HITECH_MASTER.dbo.THRMEmpType ET ON P.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' and P.FNTotalRecalTAX>0 "
                    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                    _Qry &= vbCrLf & " AND (ET.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND FTEmpTypeCode not in ('M','N','O', 'M1','N1','O1', 'M2','N2', 'M3','N3'))"

                Else

                    _Qry &= vbCrLf & "  WHERE  P.FTPayYear='" & _Year & "'and P.FNTotalRecalTAX>0  "
                    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

                End If


                _Qry &= vbCrLf & "  GROUP BY  V_MCmp.FTTaxNo
                                     , PD.FTPayYear, P.FTEmpIdNo
                                     , V_MCmp.FTBchTax
                                     , P.FTPayYear "
                _Qry &= vbCrLf & " "
                _Qry &= vbCrLf & " ) Dt "
                _Qry &= vbCrLf & " CROSS APPLY (SELECT TOP 1 FNHSysPreNameId ,FTEmpNameTH, FTEmpSurnameTH  , FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi, FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode "
                _Qry &= vbCrLf & " FROM HITECH_HR.dbo.THRMEmployee WHERE  FTTaxNo = Dt.FTEmpIdNo  AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  ) DE "
                _Qry &= vbCrLf & " LEFT OUTER JOIN V_MPrename PP ON DE.FNHSysPreNameId=PP.FNHSysPreNameId  "
                _Qry &= vbCrLf & "  ORDER BY FROMTYPE "
                _Qry &= vbCrLf & " "
                _Qry &= vbCrLf & " "



                '_Qry = "  	SELECT    ROW_NUMBER() OVER(ORDER BY P.FTEmpIdNo) AS FROMTYPE, '0000000000000' AS ComIDNO,'" & _ComTaxID & "' AS COMTaxNo, '" & _ComBnkBranchID & "' AS Tax_BranchNo,"
                '_Qry &= vbCrLf & "   P.FTEmpIdNo , '0000000000' AS FTTaxNo, PP.FTPreNameNameTH AS FTEmpPreCode, M.FTEmpNameTH, M.FTEmpSurnameTH,"
                '_Qry &= vbCrLf & "   (ISNULL(M.FTAddrNo,'')+ISNULL(M.FTAddrHome,'')+ISNULL(M.FTAddrMoo,'')+ISNULL(M.FTAddrSoi,'')+ISNULL(M.FTAddrRoad,'')+ISNULL(M.FTAddrTumbol,'')+ISNULL(M.FTAddrAmphur,'')+ISNULL(M.FTAddrProvince,'')) AS EmpAddress,"
                '_Qry &= vbCrLf & "   M. FTAddrPostCode,'" & Format(Val(_Month), "00") & "'  AS MonthPay,'" & FTYearThai & "'  AS YearPay, '1' AS IncomCode, '" & _PayDateforTransfer & "' AS PayDate, '0' AS TaxRate,"
                '_Qry &= vbCrLf & "   SUM(P.FNTotalRecalTAX) AS TOTALPAY"
                '_Qry &= vbCrLf & "   ,SUM(P.FNTax) AS SUMTAX, '1' AS TaxCondition"

                '_Qry &= vbCrLf & "   FROM   (((THRTPayRoll P "
                '_Qry &= vbCrLf & "    LEFT OUTER JOIN THRMCfgPayDT PD ON ((P.FTPayYear=PD.FTPayYear)  "
                '_Qry &= vbCrLf & "    AND (P.FTPayTerm=PD.FTPayTerm)) AND (P.FNHSysEmpTypeId=PD.FNHSysEmpTypeId)) "
                '_Qry &= vbCrLf & "   INNER JOIN THRMEmployee M ON P.FNHSysEmpID=M.FNHSysEmpID) "
                '_Qry &= vbCrLf & "   LEFT OUTER JOIN V_MPrename PP ON M.FNHSysPreNameId=PP.FNHSysPreNameId) "
                '_Qry &= vbCrLf & "  LEFT OUTER JOIN V_MCmp V_MCmp ON M.FNHSysCmpId=V_MCmp.FNHSysCmpId "

                'If pay30day_flag = "1" Then

                '    _Qry &= vbCrLf & "  LEFT JOIN HITECH_MASTER.dbo.THRMEmpType ET ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
                '    _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' "
                '    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                '    _Qry &= vbCrLf & " AND (ET.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND FTEmpTypeCode not in ('M','N','O', 'M1','N1','O1', 'M2','N2', 'M3','N3'))"

                'Else

                '    _Qry &= vbCrLf & "  WHERE  P.FTPayYear='" & _Year & "' "
                '    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

                'End If


                '_Qry &= vbCrLf & "  GROUP BY PP.FTPreNameNameTH, V_MCmp.FTTaxNo
                '                     , PD.FTPayYear, P.FTEmpIdNo, M.FTEmpNameTH, M.FTEmpSurnameTH
                '                     , V_MCmp.FTBchTax
                '                     , M.FTTaxNo, P.FTPayYear
                '                     ,ISNULL(M.FTAddrNo,''),ISNULL(M.FTAddrHome,''),ISNULL(M.FTAddrMoo,''),ISNULL(M.FTAddrSoi,''),ISNULL(M.FTAddrRoad,''),ISNULL(M.FTAddrTumbol,''),ISNULL(M.FTAddrAmphur,''),ISNULL(M.FTAddrProvince,'')
                '                     ,M. FTAddrPostCode "


            Case TaxType.ภงด91

                _Qry = "  SELECT     CASE WHEN M.FNEmpSex = 0 THEN '1' ELSE '2' END AS TYPEEMPTAX,"
                _Qry &= vbCrLf & " '" & _ComTaxID & "' AS ComTaxNo, M.FTEmpIdNo, M.FTTaxNo,PP.FTPreNameNameTH AS FTEmpPreCode, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTMateIDNo, "
                _Qry &= vbCrLf & "   M.FTMateIDNo As FTMateTaxNo, M.FTMateName, (CASE WHEN M.FTMaritalCode = 0 THEN '1' ELSE (CASE WHEN M.FTMateIncome = '0' THEN '3' ELSE '7' END) END)"
                _Qry &= vbCrLf & "   AS TypeSend1, '0' AS TypeSend2, '' AS YearMarry, '' AS AumMarry, '' AS ProvinveMarry, '' AS AumMarryT1, '' AS ProvinceMarryT1, M.FNHSysBankId AS FTBnkCode, M.FTAccNo,"
                _Qry &= vbCrLf & "  P.FNAmt AS  FTAmt "
                _Qry &= vbCrLf & "  , P.FNExpenses AS FTExpenses"
                _Qry &= vbCrLf & " , P.FNNetAmt  AS  FTNetAmt"
                _Qry &= vbCrLf & " , P.FNModEmp  AS  FTModEmp"
                _Qry &= vbCrLf & " , P.FNModMate AS  FTModMate"
                _Qry &= vbCrLf & " ,P.FNChildNotLern"
                _Qry &= vbCrLf & " ,P.FNChildLern"
                _Qry &= vbCrLf & " , P.FNChildNotLern   AS  FTChildNotLern"
                _Qry &= vbCrLf & " , P.FNChildLern   AS  FTChildLern"
                _Qry &= vbCrLf & " , P.FNInsurance  AS  FTInsurance"
                _Qry &= vbCrLf & "  , P.FNProvidentfund   AS FTProvidentfund"
                _Qry &= vbCrLf & " , P.FNInterest   AS  FTInterest"
                _Qry &= vbCrLf & " , P.FNSocial  AS FTSocial"
                _Qry &= vbCrLf & " ,P.FNDonation   AS FTDonation"
                _Qry &= vbCrLf & "  , P.FNProvidentfundOver   AS FTProvidentfundOver"
                _Qry &= vbCrLf & " , P.FNGPF   AS FTGPF"
                _Qry &= vbCrLf & " , P.FNSavingsFund   AS FTSavingsFund"
                _Qry &= vbCrLf & " , P.FNCommutation   AS FTCommutation"
                _Qry &= vbCrLf & ", P.FNUnitRMF   AS FTUnitRMF"
                _Qry &= vbCrLf & ", P.FNModFather   AS FTModFather"
                _Qry &= vbCrLf & " , P.FNModMother   AS FTModMother"
                _Qry &= vbCrLf & ", P.FNModFatherMate   AS FTModFatherMate"
                _Qry &= vbCrLf & ", P.FNModMotherMate   AS FTModMotherMate"
                _Qry &= vbCrLf & ", P.FNUnitLTF  AS FTUnitLTF"
                _Qry &= vbCrLf & ", P.FNDonationLern  AS FTDonationLern"
                _Qry &= vbCrLf & ", P.FNParentsHealthInsurance   AS FTParentsHealthInsurance"
                _Qry &= vbCrLf & ", P.FNSupportSport  AS FTSupportSport"
                _Qry &= vbCrLf & ", P.FNAcquisitionOfProperty   AS  FTAcquisitionOfProperty"
                _Qry &= vbCrLf & ", P.FNPension   AS FTPension"
                _Qry &= vbCrLf & ", P.FNTravel   AS FTTravel"
                _Qry &= vbCrLf & ", P.FNTotalCalTax   AS FTTotalCalTax"
                _Qry &= vbCrLf & ", P.FNTotalTax   AS FTTotalTax"
                _Qry &= vbCrLf & ",P.FNTotalTax  AS  FTTotalTax"
                _Qry &= vbCrLf & ", 0 AS TaxRet, 0 AS TaxRet2, CASE WHEN Len(M.FTFatherIDNo)"
                _Qry &= vbCrLf & "    = 13 THEN 1 ELSE 0 END AS FID1, M.FTFatherIDNo, CASE WHEN Len(M.FTMotherIDNo) = 13 THEN 1 ELSE 0 END AS MID1, M.FTMotherIDNo,"
                _Qry &= vbCrLf & "   CASE WHEN Len(M.FTMateFatherIDNo) = 13 THEN 1 ELSE 0 END AS WFID1, M.FTMateFatherIDNo, "
                _Qry &= vbCrLf & "   CASE WHEN Len(M.FTMateMotherIDNo) = 13 THEN 1 ELSE 0 END AS WMID1, M.FTMateMotherIDNo,  0 AS N001, 0 AS N002,"
                _Qry &= vbCrLf & "  '' AS C001, '' AS C002, '' AS C003, '' AS C004, 0 AS N005, 0 AS N006, ' ' AS C007, '   ' AS C008, 0 AS N009, 0 AS N010, 0 AS N011"
                _Qry &= vbCrLf & "   FROM         THRTTaxYear AS P WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "  ( SELECT Max(M.FNHSysEmpID) AS FNHSysEmpID,MAX(M.FNEmpSex) AS FNEmpSex,MAX(M.FNHSysPreNameId) AS FNHSysPreNameId"
                _Qry &= vbCrLf & "  ,MAX(M.FTEmpNameTH) AS FTEmpNameTH,MAX(M.FTEmpSurnameTH) AS FTEmpSurnameTH,MAX(M.FTTaxNo) AS FTTaxNo"
                _Qry &= vbCrLf & "  ,MAX(M.FNMaritalStatus) AS FTMaritalCode,MAX(M.FTMateIncome) AS FTMateIncome"
                _Qry &= vbCrLf & "  ,MAX(M.FNHSysBankId) As FNHSysBankId,MAX(M.FTAccNo ) AS FTAccNo,Max(FTMateName) AS FTMateName"
                _Qry &= vbCrLf & "  ,REPLACE(M.FTEmpIdNo,'-','') AS FTEmpIdNo"
                _Qry &= vbCrLf & "  ,MAX(REPLACE(M.FTMateIDNo,'-','')) AS FTMateIDNo"
                _Qry &= vbCrLf & "  ,MAX(REPLACE(M.FTFatherIDNo,'-','')) AS FTFatherIDNo"
                _Qry &= vbCrLf & "  ,MAX(REPLACE(M.FTMotherIDNo,'-','')) AS FTMotherIDNo"
                _Qry &= vbCrLf & "  ,MAX(REPLACE(M.FTMateFatherIDNo,'-','')) AS FTMateFatherIDNo"
                _Qry &= vbCrLf & "  ,MAX(REPLACE(M.FTMateMotherIDNo,'-','')) AS FTMateMotherIDNo"
                _Qry &= vbCrLf & "  ,MAX(ISNULL(M.FTAddrNo,'')+ISNULL(M.FTAddrHome,'')+ISNULL(M.FTAddrMoo,'')+ISNULL(M.FTAddrSoi,'')+ISNULL(M.FTAddrRoad,'')+ISNULL(M.FTAddrTumbol,'')+ISNULL(M.FTAddrAmphur,'')+ISNULL(M.FTAddrProvince,'')) AS EmpAddress"
                _Qry &= vbCrLf & "  ,MAX(ISNULL(M.FTAddrPostCode,'')) As FTAddrPostCode"
                _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee M  WITH(NOLOCK)"
                _Qry &= vbCrLf & "   INNER Join "
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "

                If _Formular <> "" Then
                    _Qry &= vbCrLf & "  WHERE M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " AND  " & _Formular
                Else
                    _Qry &= vbCrLf & "  WHERE M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                End If

                _Qry &= vbCrLf & " GROUP BY FTEmpIdNo "
                _Qry &= vbCrLf & "   )"
                _Qry &= vbCrLf & "  AS M ON P.FTEmpIdNo = M.FTEmpIdNo  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PP ON M.FNHSysPreNameId = PP.FNHSysPreNameId "
                _Qry &= vbCrLf & " WHERE  P.FTYear  ='" & _Year & "' "
                _Qry &= vbCrLf & "   ORDER BY M.FNHSysEmpID"

        End Select

        If _Qry <> "" Then
            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _DtPayRoll.Rows.Count > 0 Then
                If (_StateExport) Then

                    Select Case _Tax
                        Case TaxType.ภงด1, TaxType.ภงด1ก
                            _File = ExportTax1(_EmpType, FTYearThai, _Month, _Tax, _DtPayRoll)
                        Case TaxType.ภงด91
                            _File = ExportTax91(_EmpType, FTYearThai, _Month, _Tax, _DtPayRoll)
                        Case Else
                    End Select

                End If
            End If
        End If

        Return _File
    End Function
    Private Shared Function GenerateTable(ByVal _MaxCol As Integer) As DataTable
        Dim _Dt As New DataTable

        For I = 1 To _MaxCol
            _Dt.Columns.Add("C" & I.ToString, GetType(String))
        Next

        Return _Dt
    End Function

    Private Shared Function ExportTax1(ByVal _EmpType As String, ByVal _Year As String, ByVal _Month As String, ByVal _Tax As TaxType, ByVal _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(20)
        Dim Ind As Integer = 1
        Dim _Amt As Integer = 0
        Dim _Arr() As String

        For Each R As DataRow In _PayRoll.Rows

            If _Tax = TaxType.ภงด1ก Then

                _Arr = {R!FROMTYPE.ToString & "|",
                              Left(R!COMTaxNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!COMTaxNo.ToString.Replace("-", ""), 13)), " ") & "|",
                              Left(R!FTTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!FTTaxNo.ToString.Replace("-", "0"), 10)), " ") & "|",
                              Left(R!Tax_BranchNo.ToString.Replace("-", ""), 4) & "".PadRight(4 - Len(Left(R!Tax_BranchNo.ToString.Replace("-", "0"), 4)), " ") & "|",
                              Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13)), " ") & "|",
                              Left(R!FTTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!FTTaxNo.ToString.Replace("-", "0"), 10)), " ") & "|",
                              R!FTEmpPreCode.ToString & "|",
                              R!FTEmpNameTH.ToString & "|",
                              R!FTEmpSurnameTH.ToString & "|",
                              Left(R!EmpAddress.ToString, 80) & "|",
                              IIf(R!EmpAddress.ToString.Length <= 80, "", Left(Mid(R!EmpAddress.ToString, 81, R!EmpAddress.ToString.Length), 80)) & "|",
                              R!FTAddrPostCode.ToString & "|",
                              "00" & "|",
                              _Year & "|",
                              R!IncomCode.ToString & "|",
                              Mid(R!payDate.ToString, 9, 2) & Mid(R!payDate.ToString, 6, 2) & Format(Val(Mid(R!payDate.ToString, 1, 4)) + 543, "0000") & "|",
                              R!TaxRate.ToString & "|",
                              Format(Val(R!TOTALPAY.ToString), "0.00") & "|",
                              Format(Val(R!SumTax.ToString), "0.00") & "|",
                              R!TaxCondition.ToString}

            Else

                _Arr = {R!FROMTYPE.ToString & "|",
                              Left(R!COMTaxNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!COMTaxNo.ToString.Replace("-", ""), 13)), " ") & "|",
                              Left(R!FTTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!FTTaxNo.ToString.Replace("-", "0"), 10)), " ") & "|",
                              Left(R!Tax_BranchNo.ToString.Replace("-", ""), 4) & "".PadRight(4 - Len(Left(R!Tax_BranchNo.ToString.Replace("-", "0"), 4)), " ") & "|",
                              Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13)), " ") & "|",
                              Left(R!FTTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!FTTaxNo.ToString.Replace("-", "0"), 10)), " ") & "|",
                              R!FTEmpPreCode.ToString & "|",
                              R!FTEmpNameTH.ToString & "|",
                              R!FTEmpSurnameTH.ToString & "|",
                              Left(R!EmpAddress.ToString, 80) & "|",
                              IIf(R!EmpAddress.ToString.Length <= 80, "", Left(Mid(R!EmpAddress.ToString, 81, R!EmpAddress.ToString.Length), 80)) & "|",
                              R!FTAddrPostCode.ToString & "|",
                              _Month & "|",
                              _Year & "|",
                              R!IncomCode.ToString & "|",
                              R!payDate.ToString & "|",
                              R!TaxRate.ToString & "|",
                              Format(Val(R!TOTALPAY.ToString), "0.00") & "|",
                              Format(Val(R!SumTax.ToString), "0.00") & "|",
                              R!TaxCondition.ToString}


            End If
            'Mid(R!payDate.ToString, 9, 2) & Mid(R!payDate.ToString, 6, 2) & Format(Val(Mid(R!payDate.ToString, 1, 4)) + 543, "0000") & "|",

            _File.Rows.Add(_Arr)

        Next

        Return _File
    End Function

    Private Shared Function ExportTax91(ByVal _EmpType As String, ByVal _Year As String, ByVal _Month As String, ByVal _Tax As TaxType, ByVal _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(80)
        Dim Ind As Integer = 1
        Dim _Amt As Integer = 0
        Dim _Arr() As String

        For Each R As DataRow In _PayRoll.Rows
            Ind = Ind + 1

            Dim FTAmt As String = Format(CDbl(IIf(IsNumeric(R!FTAmt.ToString), CDbl(R!FTAmt.ToString), 0)), "0.00")
            Dim FTExpenses As String = Format(CDbl(IIf(IsNumeric(R!FTExpenses.ToString), CDbl(R!FTExpenses.ToString), 0)), "0.00")
            Dim FTNetAmt As String = Format(CDbl(IIf(IsNumeric(R!FTNetAmt.ToString), CDbl(R!FTNetAmt.ToString), 0)), "0.00")
            Dim FTModEmp As String = Format(CDbl(IIf(IsNumeric(R!FTModEmp.ToString), CDbl(R!FTModEmp.ToString), 0)), "0")
            Dim FTModMate As String = Format(CDbl(IIf(IsNumeric(R!FTModMate.ToString), CDbl(R!FTModMate.ToString), 0)), "0")

            Dim FNChildNotLern As Integer = Integer.Parse(R!FNChildNotLern.ToString)
            Dim FNChildLern As Integer = Integer.Parse(R!FNChildLern.ToString)

            Dim FTChildNotLern As String = Format(CDbl(IIf(IsNumeric(R!FTChildNotLern.ToString), CDbl(R!FTChildNotLern.ToString), 0)), "0.")
            Dim FTChildLern As String = Format(CDbl(IIf(IsNumeric(R!FTChildLern.ToString), CDbl(R!FTChildLern.ToString), 0)), "0")
            Dim FTInsurance As String = Format(CDbl(IIf(IsNumeric(R!FTInsurance.ToString), CDbl(R!FTInsurance.ToString), 0)), "0.00")
            Dim FTProvidentfund As String = Format(CDbl(IIf(IsNumeric(R!FTProvidentfund.ToString), CDbl(R!FTProvidentfund.ToString), 0)), "0.00")

            Dim FTInterest As String = Format(CDbl(IIf(IsNumeric(R!FTInterest.ToString), CDbl(R!FTInterest.ToString), 0)), "0.00")
            Dim FTSocial As String = Format(CDbl(IIf(IsNumeric(R!FTSocial.ToString), CDbl(R!FTSocial.ToString), 0)), "0.00")
            Dim FTDonation As String = Format(CDbl(IIf(IsNumeric(R!FTDonation.ToString), CDbl(R!FTDonation.ToString), 0)), "0.00")
            Dim FTProvidentfundOver As String = Format(CDbl(IIf(IsNumeric(R!FTProvidentfundOver.ToString), CDbl(R!FTProvidentfundOver.ToString), 0)), "0.00")

            Dim FTGPF As String = Format(CDbl(IIf(IsNumeric(R!FTGPF.ToString), CDbl(R!FTGPF.ToString), 0)), "0.00")
            Dim FTSavingsFund As String = Format(CDbl(IIf(IsNumeric(R!FTSavingsFund.ToString), CDbl(R!FTSavingsFund.ToString), 0)), "0.00")
            Dim FTCommutation As String = Format(CDbl(IIf(IsNumeric(R!FTCommutation.ToString), CDbl(R!FTCommutation.ToString), 0)), "0.00")

            Dim FTUnitRMF As String = Format(CDbl(IIf(IsNumeric(R!FTUnitRMF.ToString), CDbl(R!FTUnitRMF.ToString), 0)), "0.00")
            Dim FTModFather As String = Format(CDbl(IIf(IsNumeric(R!FTModFather.ToString), CDbl(R!FTModFather.ToString), 0)), "0.00")
            Dim FTModMother As String = Format(CDbl(IIf(IsNumeric(R!FTModMother.ToString), CDbl(R!FTModMother.ToString), 0)), "0.00")
            Dim FTModFatherMate As String = Format(CDbl(IIf(IsNumeric(R!FTModFatherMate.ToString), CDbl(R!FTModFatherMate.ToString), 0)), "0.00")
            Dim FTModMotherMate As String = Format(CDbl(IIf(IsNumeric(R!FTModMotherMate.ToString), CDbl(R!FTModMotherMate.ToString), 0)), "0.00")

            Dim FTUnitLTF As String = Format(CDbl(IIf(IsNumeric(R!FTUnitLTF.ToString), CDbl(R!FTUnitLTF.ToString), 0)), "0.00")
            Dim FTDonationLern As String = Format(CDbl(IIf(IsNumeric(R!FTDonationLern.ToString), CDbl(R!FTDonationLern.ToString), 0)), "0.00")
            Dim FTParentsHealthInsurance As String = Format(CDbl(IIf(IsNumeric(R!FTParentsHealthInsurance.ToString), CDbl(R!FTParentsHealthInsurance.ToString), 0)), "0.00")
            Dim FTSupportSport As String = Format(CDbl(IIf(IsNumeric(R!FTSupportSport.ToString), CDbl(R!FTSupportSport.ToString), 0)), "0.00")
            Dim FTAcquisitionOfProperty As String = Format(CDbl(IIf(IsNumeric(R!FTAcquisitionOfProperty.ToString), CDbl(R!FTAcquisitionOfProperty.ToString), 0)), "0.00")
            Dim FTPension As String = Format(CDbl(IIf(IsNumeric(R!FTPension.ToString), CDbl(R!FTPension.ToString), 0)), "0.00")
            Dim FTTravel As String = Format(CDbl(IIf(IsNumeric(R!FTTravel.ToString), CDbl(R!FTTravel.ToString), 0)), "0.00")
            Dim FTTotalCalTax As String = Format(CDbl(IIf(IsNumeric(R!FTTotalCalTax.ToString), CDbl(R!FTTotalCalTax.ToString), 0)), "0.00")
            Dim FTTotalTax As String = Format(CDbl(IIf(IsNumeric(R!FTTotalTax.ToString), CDbl(R!FTTotalTax.ToString), 0)), "0.00")


            _Arr = {R!TYPEEMPTAX.ToString,
                    Left(R!COMTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!COMTaxNo.ToString.Replace("-", ""), 10)), " "),
                    Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTEmpIdNo.ToString.Replace("-", ""), 13)), " "),
                    Left(R!FTTaxNo.ToString.Replace("-", ""), 10) & "".PadRight(13 - Len(Left(R!FTTaxNo.ToString.Replace("-", ""), 10)), " "),
                    Left(R!FTEmpPreCode.ToString, 10) & "".PadRight(10 - Len(Left(R!FTEmpPreCode.ToString, 10)), " "),
                    Left(R!FTEmpNameTH.ToString, 30) & "".PadRight(30 - Len(Left(R!FTEmpNameTH.ToString, 30)), " "),
                    Left(R!FTEmpSurnameTH.ToString, 30) & "".PadRight(30 - Len(Left(R!FTEmpSurnameTH.ToString, 30)), " "),
                    Left(R!FTMateIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTMateIDNo.ToString.Replace("-", ""), 13)), " "),
                    Left(R!FTMateIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTMateIDNo.ToString.Replace("-", ""), 13)), " "),
                    "".PadRight(10, " "),
                    Left(R!FTMateName.ToString, 30) & "".PadRight(30 - Len(Left(R!FTMateName.ToString, 30)), " "),
                    "".PadRight(30, " "),
                    R!TypeSend1.ToString,
                    R!TypeSend2.ToString,
                    "".PadRight(4, " "),
                    "".PadRight(30, " "),
                    "".PadRight(30, " "),
                    "".PadRight(30, " "),
                    "".PadRight(30, " "),
                    Left(R!FTBnkCode.ToString, 3) & "".PadRight(3 - Len(Left(R!FTBnkCode.ToString, 3)), " "),
                    Left(R!FTAccNo.ToString.Replace("-", ""), 15) & "".PadRight(15 - Len(Left(R!FTAccNo.ToString.Replace("-", ""), 15)), " "),
                    "".PadRight(15 - Len(FTAmt)) & FTAmt,
                    "".PadRight(12 - Len(FTExpenses)) & FTExpenses,
                    "".PadRight(15 - Len(FTNetAmt)) & FTNetAmt,
                    "".PadRight(IIf(6 - Len(FTModEmp) > 0, 6 - Len(FTModEmp), 0)) & FTModEmp,
                    "".PadRight(IIf(6 - Len(FTModMate) > 0, 6 - Len(FTModMate), 0)) & FTModMate,
                    FNChildNotLern.ToString,
                    FNChildLern.ToString,
                    "".PadRight(IIf(6 - Len(FTChildNotLern) > 0, 6 - Len(FTChildNotLern), 0)) & FTChildNotLern,
                    "".PadRight(IIf(6 - Len(FTChildLern) > 0, 6 - Len(FTChildLern), 0)) & FTChildLern,
                    "".PadRight(11 - Len(FTInsurance)) & FTInsurance,
                    "".PadRight(11 - Len(FTProvidentfund)) & FTProvidentfund,
                    "".PadRight(11 - Len(FTInterest)) & FTInterest,
                    "".PadRight(11 - Len(FTSocial)) & FTSocial,
                    "".PadRight(11 - Len(FTDonation)) & FTDonation,
                    "".PadRight(11 - Len(FTGPF)) & FTGPF,
                    "".PadRight(11 - Len(FTSavingsFund)) & FTSavingsFund,
                    "".PadRight(11 - Len(FTCommutation)) & FTCommutation,
                    "".PadRight(11 - Len(FTUnitRMF)) & FTUnitRMF,
                    "".PadRight(16 - Len(FTTotalCalTax)) & FTTotalCalTax,
                    "".PadRight(16 - Len(FTTotalTax)) & FTTotalTax,
                    "".PadRight(16 - Len(FTTotalTax)) & FTTotalTax,
                    "".PadRight(16 - Len("0.00")) & "0.00",
                    "".PadRight(16 - Len("0.00")) & "0.00",
                    R!FID1.ToString,
                    Left(R!FTFatherIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTFatherIDNo.ToString.Replace("-", ""), 13)), " "),
                     "".PadRight(11 - Len(FTModFather)) & FTModFather,
                     R!Mid1.ToString,
                     Left(R!FTMotherIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTMotherIDNo.ToString.Replace("-", ""), 13)), " "),
                     "".PadRight(11 - Len(FTModMother)) & FTModMother,
                      R!WFID1.ToString,
                     Left(R!FTMateFatherIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTMateFatherIDNo.ToString.Replace("-", ""), 13)), " "),
                     "".PadRight(11 - Len(FTModFatherMate)) & FTModFatherMate,
                     R!WMID1.ToString,
                     Left(R!FTMateMotherIDNo.ToString.Replace("-", ""), 13) & "".PadRight(13 - Len(Left(R!FTMateMotherIDNo.ToString.Replace("-", ""), 13)), " "),
                     "".PadRight(11 - Len(FTModMotherMate)) & FTModMotherMate,
                     "".PadRight(13 - Len(FTUnitLTF)) & FTUnitLTF,
                    "".PadRight(13 - Len(FTDonationLern)) & FTDonationLern,
                    Left(R!C001.ToString, 13) & "".PadRight(13 - Len(Left(R!C001.ToString, 13)), " "),
                    Left(R!C002.ToString, 13) & "".PadRight(13 - Len(Left(R!C002.ToString, 13)), " "),
                    Left(R!C003.ToString, 13) & "".PadRight(13 - Len(Left(R!C003.ToString, 13)), " "),
                    Left(R!C004.ToString, 13) & "".PadRight(13 - Len(Left(R!C004.ToString, 13)), " "),
                    "".PadRight(11 - Len(FTParentsHealthInsurance)) & FTParentsHealthInsurance,
                    "".PadRight(13 - Len(FTSupportSport)) & FTSupportSport,
                    R!C007.ToString,
                    R!C008.ToString,
                    "".PadRight(13 - Len(FTAcquisitionOfProperty)) & FTAcquisitionOfProperty,
                    "".PadRight(13 - Len(FTPension)) & FTPension,
                    "".PadRight(13 - Len(FTTravel)) & FTTravel}

            _File.Rows.Add(_Arr)

        Next

        Return _File
    End Function

    Public Shared Function CreateTextFileSSO(ByVal _CmpCode As String, ByVal _EmpType As String, ByVal _Year As String, ByVal _Month As String, ByVal _SsoPayDate As String, ByVal _Formula As String) As DataTable
        Try


            Dim _File As DataTable = GenerateTable(12)
            Dim _Arr() As String
            Dim _Qry As String = ""
            Dim _Dt As DataTable
            Dim _FileName As String = "SSOSENT.DAT"
            Dim _data As String = ""
            Dim _EmployerID As String = ""
            Dim _EmployerBrnch As String = ""
            Dim _EmployerName As String = ""
            Dim _RecType As String = "1"
            Dim _YearSend As String = Format(IIf(Integer.Parse(_Year) < 2500, Integer.Parse(_Year) + 543, Integer.Parse(_Year)), "0000")
            Dim _SendPayDate As String = Left(_SsoPayDate, 2) & Mid(_SsoPayDate, 4, 2) & Right(_YearSend, 2)
            Dim _Rate As String = Format(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FTKeyValue FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfig WITH(NOLOCK) WHERE FTKeyCode='Cfg_SocialRate' ", Conn.DB.DataBaseName.DB_HR, "0")) * 100, "0000")
            Dim _GAmt As Double = 0
            Dim _GSOAmt As Double = 0
            Dim _GSOCmpAmt As Double = 0
            Dim _TotalEmp As String = ""
            Dim _TotalWageEmp As String = ""
            Dim _TotalSO As String = ""
            Dim _TotalSOEmp As String = ""
            Dim _TotalSOEmpY As String = ""

            _Month = Format(Integer.Parse(_Month), "00")
            Dim _PayPeriod As String = _Month & Right(_YearSend, 2)
            Dim _SSoID As String = ""
            Dim _PrefixID As String = ""
            Dim _FName As String = ""
            Dim _LName As String = ""
            Dim _Blank As String = ""
            Dim _Wage As String = ""
            Dim _PayAmt As String = ""
            Dim DateBf As String = Format(IIf(Integer.Parse(_Year) > 2500, Integer.Parse(_Year) - 543, Integer.Parse(_Year)), "0000") & "/" & _Month & "/01"

            _Qry = " SELECT     TOP 1   FTCmpCode,   FTCmpNameEN,  FTBchTax, FTSocialNo, FTBchSocial"
            _Qry &= vbCrLf & "  FROM  TCNMCmp WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each R As DataRow In _Dt.Rows
                _EmployerID = R!FTSocialNo.ToString
                _EmployerBrnch = R!FTBchSocial.ToString
                _EmployerName = R!FTCmpNameEN.ToString
            Next

            '_Qry = "   SELECT     M.FTPayYear,  MAX(EM.FTEmpCode) AS FTEmpCode, M.FTEmpIdNo, M.FNMonth, SUM(CONVERT(numeric(18, 2), M.FNTotalInCome))"
            '_Qry &= vbCrLf & "  AS FNTotalInCome, SUM(CONVERT(numeric(18, 2), M.FNSocial)) AS FNSocial, PN.FTPreNameCode,PN.FTSocialCode, EM.FTEmpNameEN, EM.FTEmpSurnameEN, EM.FTEmpNicknameEN, "
            '_Qry &= vbCrLf & "   EM.FTEmpNameTH, EM.FTEmpSurnameTH, EM.FTSocialNo"
            '_Qry &= vbCrLf & "  FROM            (SELECT        P.FTPayYear,  P.FNHSysEmpID,P.FNHSysEmpTypeID, P.FTEmpIdNo, D.FNMonth,  "
            '_Qry &= vbCrLf & "  P.FNTotalRecalSSO AS FNTotalInCome"
            '_Qry &= vbCrLf & "  , P.FNSocial AS FNSocial"
            '_Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON  P.FTPayTerm = D.FTPayTerm AND P.FTPayYear = D.FTPayYear AND "
            '_Qry &= vbCrLf & "    P.FNHSysEmpTypeID = D.FNHSysEmpTypeID"

            '_Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS EM WITH (NOLOCK) ON  P.FNHSysEmpID = EM.FNHSysEmpID"


            '_Qry &= vbCrLf & "  WHERE        (P.FTPayYear ='" & Format(IIf(Integer.Parse(_Year) > 2500, Integer.Parse(_Year) - 543, Integer.Parse(_Year)), "0000") & "')"
            '_Qry &= vbCrLf & "   AND (D.FNMonth =" & Val(_Month) & " ) "
            '_Qry &= vbCrLf & _Formula

            '_Qry &= vbCrLf & "  AND       (((P.FNSocial) > 0) OR (ISNULL(P.FTStateCalSocial,'')='0') ) "

            'If _EmpType <> "" Then
            '    _Qry &= vbCrLf & "   AND P.FNHSysEmpTypeID=" & Integer.Parse(Val(_EmpType)) & "  "
            'End If

            '_Qry &= vbCrLf & " AND EM.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

            '_Qry &= vbCrLf & "   ) AS M INNER JOIN"
            '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS EM WITH (NOLOCK) ON  M.FNHSysEmpID = EM.FNHSysEmpID"
            '_Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PN WITH(NOLOCK) ON EM.FNHSysPreNameId = PN.FNHSysPreNameId"
            '_Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision  WITH(NOLOCK) ON EM.FNHSysDivisonId = TCNMDivision.FNHSysDivisonId "
            '_Qry &= vbCrLf & "Where EM.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "   "
            '_Qry &= vbCrLf & "  GROUP BY M.FTPayYear,  M.FTEmpIdNo,  M.FNMonth, PN.FTPreNameCode,PN.FTSocialCode, EM.FTEmpNameEN, EM.FTEmpSurnameEN, EM.FTEmpNicknameEN, "
            '_Qry &= vbCrLf & "  EM.FTEmpNameTH, EM.FTEmpSurnameTH, EM.FTSocialNo"


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            _Qry = "   SELECT FTPayYear , FNMonth, FTEmpIdNo"
            _Qry &= vbCrLf & " ,FNTotalRecalSSO AS FNTotalInCome, FNSocial, FNSocialCmp "
            _Qry &= vbCrLf & " , FTPreNameCode,FTSocialCode, FTEmpNameEN, FTEmpSurnameEN,FTEmpNicknameEN "
            _Qry &= vbCrLf & " , FTEmpNameTH, FTEmpSurnameTH, FTSocialNo "
            _Qry &= vbCrLf & "  FROM V_PayrollSSO P"

            _Qry &= vbCrLf & "  WHERE        (P.FTPayYear ='" & Format(IIf(Integer.Parse(_Year) > 2500, Integer.Parse(_Year) - 543, Integer.Parse(_Year)), "0000") & "')"
            _Qry &= vbCrLf & "   AND (P.FNMonth =" & Val(_Month) & " ) "
            _Qry &= vbCrLf & _Formula



            If _EmpType <> "" Then
                _Qry &= vbCrLf & "   AND P.FNHSysEmpTypeID=" & Integer.Parse(Val(_EmpType)) & "  "
            End If

            _Qry &= vbCrLf & " AND P.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "





            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            _TotalEmp = _Dt.Rows.Count.ToString

            For Each R As DataRow In _Dt.Rows
                _GAmt = _GAmt + Val(R!FNTotalInCome.ToString)
                _GSOAmt = _GSOAmt + Val(R!FNSocial.ToString)
                _GSOCmpAmt = _GSOCmpAmt + Val(R!FNSocialCmp.ToString)
            Next

            _EmployerID = Left(_EmployerID & "".PadRight(10, " "), 10) 'ความยาว 10 ตัวอักษร
            _EmployerBrnch = Left(_EmployerBrnch & "".PadRight(6, " "), 6) 'ความยาว 6 ตัวอักษร
            _EmployerName = Left(_EmployerName & "".PadRight(45, " "), 45) 'ความยาว 45 ตัวอักษร
            _TotalEmp = Right("".PadRight(6, "0") & _TotalEmp, 6)  'ความยาว 6 ตัวอักษร
            _TotalWageEmp = Right("".PadRight(12, "0") & Format(CDbl(Format(_GAmt, "0.00")) * 100, "0"), 12)  'ความยาว 12 ตัวอักษร
            _TotalSO = Right("".PadRight(12, "0") & Format((CDbl(Format(_GSOAmt, "0.00")) + CDbl(Format(_GSOCmpAmt, "0.00"))) * 100, "0"), 12)  'ความยาว 12 ตัวอักษร
            _TotalSOEmp = Right("".PadRight(10, "0") & Format((CDbl(Format(_GSOAmt, "0.00"))) * 100, "0"), 10)  'ความยาว 10 ตัวอักษร
            _TotalSOEmpY = Right("".PadRight(10, "0") & Format((CDbl(Format(_GSOCmpAmt, "0.00"))) * 100, "0"), 10)  'ความยาว 10 ตัวอักษร
            _SendPayDate = Left(_SendPayDate, 6)

            _RecType = "1"
            _Arr = {_RecType, _EmployerID, _EmployerBrnch, Left(_SendPayDate, 6), Left(_PayPeriod, 4), _EmployerName, _Rate, _TotalEmp, _TotalWageEmp, _TotalSO, _TotalSOEmp, _TotalSOEmpY}
            _File.Rows.Add(_Arr)


            _RecType = "2"

            For Each R As DataRow In _Dt.Rows
                _GAmt = Val(R!FNTotalInCome.ToString)
                _GSOAmt = Val(R!FNSocial.ToString)
                _SSoID = Left((R!FTSocialNo.ToString & " ".PadRight(13)), 13)  'ความยาว 13 ตัวอักษร
                _PrefixID = Left((R!FTSocialCode.ToString & " ".PadRight(3)), 3)  'ความยาว 3 ตัวอักษร
                _FName = Left((R!FTEmpNameTH.ToString & " ".PadRight(30)), 30)  'ความยาว 30 ตัวอักษร
                _LName = Left((R!FTEmpSurnameTH.ToString & " ".PadRight(35)), 35)  'ความยาว 35 ตัวอักษร
                _Blank = " ".PadRight(28)  'ความยาว 28 ตัวอักษร
                _Wage = Right("".PadRight(8, "0") & Format((CDbl(Format(_GAmt, "0.00"))) * 100, "0"), 8)  'ความยาว 8 ตัวอักษร
                _PayAmt = Right("".PadRight(7, "0") & Format((CDbl(Format(_GSOAmt, "0.00"))) * 100, "0"), 7)  'ความยาว 7 ตัวอักษร

                _data = _RecType & _SSoID & _PrefixID & _FName & _LName & _Wage & _PayAmt & _Blank

                _Arr = {_RecType, _SSoID, _PrefixID, _FName, _LName, _Wage, _PayAmt, _Blank, "", "", "", ""}
                _File.Rows.Add(_Arr)

            Next

            Return _File

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class

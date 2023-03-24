using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace HI.EXPORTBANK
{
   public static class ExporForTMB41
    {


        public static DataTable ExportDataFile(String _BankCode, String _CmpCode, String[] _EmpType,
                                 String _Year, String _Term, String _Period, String _DateStart,
                                 String _DateEnd, String _DatePay,ref String MsgShow)
        {
            DataTable _Dt = new DataTable();
            DataTable _DtPayRoll = new DataTable();
            String _Qry;

            String _ComName;
            String _ComID;
            String _ComTaxID;
            String _ComBnkBranchID ="";
            String _ComAcc;
            Double _GAmount = 0;
            int _TotalRec;

            //get BnkBranchID  and wait edit employee not fixible
            _Qry = @"SELECT TOP 1 FTCmpCode,   FNHSysCmpTitleId AS  FTCompanyTitle, FTCmpNameEN As FTCmpNamePri, FTCmpNameTH As FTCmpNameSec
             ,FTTaxNo AS FTCmpTaxNo, FTSocialNo AS FTCmpTSocialNo,FNHSysBankId AS  FTBnkCode 
              ,FTBankBranchCode AS  FTBnkBchCode, FTDepositCode, FTBnkAccNo, FTBnkAccName, FTBchSocial
              ,FTCmpNameEN AS FTCmpAddrPri1,FTCmpNameTH AS FTCmpAddrPri2
             FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMCmp AS C WITH (NOLOCK) 
             WHERE FNHSysCmpId=" + (HI.ST.SysInfo.CmpID);
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);

            foreach (DataRow _dr in _Dt.Rows)
            {
                _ComName = _dr["FTBnkAccName"].ToString();
                _ComID = _dr["FTDepositCode"].ToString();
                _ComAcc = _dr["FTBnkAccNo"].ToString();
                _ComTaxID = _dr["FTCmpTaxNo"].ToString();
                _ComBnkBranchID = _dr["FTBnkBchCode"].ToString();
            }


            _Qry = @"    SELECT * FROM ( SELECT         --M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpPreCode       
                        --,REPLACE(REPLACE(M.FTEmpName1, CHAR(13), ''), CHAR(10), '') AS  FTEmpName1
                         --,REPLACE(REPLACE(M.FTEmpSurname1, CHAR(13), ''), CHAR(10), '') AS FTEmpSurname1
                       -- , M.FTEmpNickname1
                       -- ,REPLACE(REPLACE(M.FTEmpName2, CHAR(13), ''), CHAR(10), '') AS  FTEmpName2
                       -- ,REPLACE(REPLACE(M.FTEmpSurname2, CHAR(13), ''), CHAR(10), '') AS FTEmpSurname2
                       -- , M.FTEmpNickname2
                         --, P.FTPayYear, P.FTPayTerm
						 --,  P.FTEmpIdNo, P.FNHSysEmpTypeId
                         --,P.FTPayDate
                         --,B.FTBankCode
						 --,P.FNHSysBankBranchId AS   FTBranchCode
						 '0000' AS CompId " + Environment.NewLine +
                         @",'08' AS TransactionType
						 --, P.FTAccNo
                        , REPLACE(P.FTAccNo,'-','') AS FTAccNo
                         --,P.FNSalary AS FCSalary
						 ,'000' AS HomeBranch
						 ,'0000000' AS PaymentRef
						 ,'2' AS RecordCode                         
						 ,RIGHT( REPLACE('00000000' + SUBSTRING(CAST(P.FNNetpay AS nvarchar(15)) , 1, LEN(P.FNNetpay)-3),'.',''),12) as Netpay						 
						 ,'01' AS itemNo
						 
						 ,P.FNNetpay AS FNNetpay
                         FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRTPayRoll AS P WITH (NOLOCK)
                          INNER JOIN (
                          SELECT      MAX(Emp.FTEmpCode) AS FTEmpCode, MAX(Emp.FTEmpCodeRefer) AS FTEmpCodeRefer, MAX(PN.FTPreNameCode ) As FTEmpPreCode
                         , Max(Emp.FTEmpNameEN) As FTEmpName1
                         , MAX(Emp.FTEmpSurnameEN) As FTEmpSurname1
                         , MAX(Emp.FTEmpNicknameEN) As FTEmpNickname1
                         , MAX(Emp.FTEmpNameTH) AS FTEmpName2
                         , MAX(Emp.FTEmpSurnameTH) As FTEmpSurname2
                         , Max(Emp.FTEmpNicknameTH) AS  FTEmpNickname2 
                        , Emp.FNHSysEmpID
                         FROM      [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMEmployee AS Emp WITH (NOLOCK)
                         LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPrename AS PN WITH(NOLOCK)
                         ON Emp.FNHSysPreNameId = PN.FNHSysPreNameId
                         WHERE  Emp.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + @" AND FNHSysEmpTypeId <> 0  ";

            _Qry = HI.ST.Security.PermissionEmpType(_Qry);

            _Qry += @"  GROUP BY FNHSysEmpID
             ) AS M ON P.FNHSysEmpID = M.FNHSysEmpID  
              INNER JOIN  [" +  HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMCfgPayDT AS PD WITH(NOLOCK) ON  P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId 
             AND P.FTPayTerm = PD.FTPayTerm 
             AND P.FTPayYear = PD.FTPayYear 
             LEFT JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMBank AS B WITH(NOLOCK)
             ON P.FNHSysBankId = B.FNHSysBankId
               WHERE (P.FNHSysEmpTypeId <> 0)
             AND (P.FNHSysPayRollPayId IN (SELECT FNHSysPayRollPayId FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPaymentType AS PM WITH(NOLOCK) WHERE FTStatePackBank='1'  ) ) ";


            if (_EmpType.Length > 0)
            {
                _Qry += @" AND ( ";
                int Seq = 0;

                foreach (String Str in _EmpType)
                {
                    if (Str != "")
                    {
                        if (Seq == 0)
                        {
                            _Qry += @"  P.FNHSysEmpTypeId =" + Str + " ";
                        }
                        else
                        {
                            _Qry += @" OR P.FNHSysEmpTypeId =" + Str + " ";
                        }

                        Seq = Seq + 1;
                    }
                }
                _Qry += @" ) ";
            }

            _Qry += @" AND  PD.FTPayYear ='" + HI.UL.ULF.rpQuoted(_Year) + @"' 
                        AND  PD.FTPayTerm ='" + HI.UL.ULF.rpQuoted(_Term) + @"'  
                         AND (B.FTBankCode = N'" + HI.UL.ULF.rpQuoted(_BankCode) + @"')
                         ) AS TM
                         WHERE FNNetpay > 0";


            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR);

            _TotalRec = _DtPayRoll.Rows.Count;

            foreach (DataRow dr in _DtPayRoll.Rows)
            {
                _GAmount = _GAmount + Double.Parse(dr["FNNetpay"].ToString());
            }

            
            _DtPayRoll.Columns.Remove("FNNetpay");

            _Dt = _DtPayRoll;
            MsgShow = " Totol Record : " + _TotalRec + Environment.NewLine + "Total Amount : " +  String.Format("{0:#,0.####}", _GAmount);

            //MessageBox.Show(Message);
          //  MsgShow = "Total Record : " & (Format(_TotalRec, "#,#0"))
//MsgShow &= vbCrLf & "Total Amount : " & (Format(_GAmount, "#,#0.00"))
//

            return _Dt;
        }

        public static DataTable ExportDataFile_TMB128(String _BankCode, String _CmpCode, String[] _EmpType,
                                    String _Year, String _Term, String _Period, String _DateStart,
                                    String _DateEnd, String _DatePay, ref String MsgShow, ref String TotalRec, ref String TotalAmount, ref String line1, ref String line2, ref String line5, String type)
        {
            DataTable _Dt = new DataTable();
            DataTable _DtPayRoll = new DataTable();
            DataTable _DtPayRollSum = new DataTable();
            String _Qry;

            String _ComName;
            String _ComID;
            String _ComTaxID;
            String _ComBnkBranchID = "";
            String _ComAcc;
            Double _GAmount = 0;


            String _TotalAmount = "";
            String _line001 = "";
            String _Line002 = "";
            String _line999 = "";


            String _TotalRec = "";

            _Qry = @"SELECT 
                        'T' AS 'RECORD_TYPE'
                        , RIGHT('0000000' + CAST(COUNT(P.FNHSysEmpID) + 2 AS nvarchar(7)), 6) AS 'SEQUENCE_NUMBER'
                        , RIGHT('011',3) AS 'BANK_CODE'
                        , RIGHT('0000000000',10) AS 'COMPANY_ACCOUNT_NO'
                        , RIGHT('0000000',7) AS 'NO_OF_DR_TRANSACTION'
                        , RIGHT('00000000000000',13) AS 'TOTAL_DR_AMOUNT'
                        ,RIGHT( '0000000' + CAST(COUNT(P.FNHSysEmpID) AS nvarchar(7)), 7) AS 'NO_OF_CR_TRANSACTION'
                        , RIGHT('0000000000000000' + REPLACE(CONVERT( decimal(18,2),SUM(FNNetpay)) ,'.','') , 13) AS 'TOTAL_CR_AMOUNT'

                        , RIGHT('0000000',7) AS 'NO_OF_REJECT_DR_TRANS'
                        , RIGHT('0000000000000',13) AS 'TOTAL_REJECT_DR_AMOUNT'
                        , RIGHT('0000000',7) AS 'NO_OF_REJECT_CR_TRANS'
                        , RIGHT('0000000000000',13) AS 'TOTAL_REJECT_CR_AMOUNT'
                        , RIGHT('                              ',28) AS 'SPARE'
, CONVERT( decimal(18,2),SUM(FNNetpay))  AS SUM_FNNetpay
                FROM HITECH_HR.dbo.THRTPayRoll  P
                 INNER JOIN(
                       SELECT MAX(Emp.FTEmpCode) AS FTEmpCode, MAX(Emp.FTEmpCodeRefer) AS FTEmpCodeRefer, MAX(PN.FTPreNameCode ) As FTEmpPreCode

                      , Max(Emp.FTEmpNameEN) As FTEmpName1

                      , MAX(Emp.FTEmpSurnameEN) As FTEmpSurname1

                      , MAX(Emp.FTEmpNicknameEN) As FTEmpNickname1

                      , MAX(Emp.FTEmpNameTH) AS FTEmpName2

                      , MAX(Emp.FTEmpSurnameTH) As FTEmpSurname2

                      , Max(Emp.FTEmpNicknameTH) AS  FTEmpNickname2

                     , Emp.FNHSysEmpID
                      FROM      [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMEmployee AS Emp WITH(NOLOCK)
                         LEFT JOIN[" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPrename AS PN WITH(NOLOCK)
                         ON Emp.FNHSysPreNameId = PN.FNHSysPreNameId
                         WHERE Emp.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + @" AND FNHSysEmpTypeId<> 0  ";

            _Qry = HI.ST.Security.PermissionEmpType(_Qry);

            _Qry += @"  GROUP BY FNHSysEmpID
             ) AS M ON P.FNHSysEmpID = M.FNHSysEmpID  
              INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMCfgPayDT AS PD WITH(NOLOCK) ON  P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId 
             AND P.FTPayTerm = PD.FTPayTerm 
             AND P.FTPayYear = PD.FTPayYear 
             LEFT JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMBank AS B WITH(NOLOCK)
             ON P.FNHSysBankId = B.FNHSysBankId 
               WHERE (P.FNHSysEmpTypeId <> 0)  AND P.FNNetPay >0 AND ISNULL(P.FTAccNo,'') <> '' AND ISNULL(P.FNHSysBankBranchId,0) >0
             AND (P.FNHSysPayRollPayId IN (SELECT FNHSysPayRollPayId FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPaymentType AS PM WITH(NOLOCK) WHERE FTStatePackBank='1'  ) ) ";

            if (_EmpType.Length > 0)
            {
                _Qry += @" AND ( ";
                int Seq = 0;

                foreach (String Str in _EmpType)
                {
                    if (Str != "")
                    {
                        if (Seq == 0)
                        {
                            _Qry += @"  P.FNHSysEmpTypeId =" + Str + " ";
                        }
                        else
                        {
                            _Qry += @" OR P.FNHSysEmpTypeId =" + Str + " ";
                        }

                        Seq = Seq + 1;
                    }
                }
                _Qry += @" ) ";
            }

            _Qry += @" AND  PD.FTPayYear ='" + HI.UL.ULF.rpQuoted(_Year) + @"' 
                        AND  PD.FTPayTerm ='" + HI.UL.ULF.rpQuoted(_Term) + @"'  
                         AND (B.FTBankCode = N'" + HI.UL.ULF.rpQuoted(_BankCode) + @"')


                        ";
            _DtPayRollSum = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);


            


            //// Detail

            _Qry = @" SELECT 
                        RIGHT('D',1) AS 'RECORD_TYPE'
                        ,RIGHT('000000' + (CAST(ROW_NUMBER() OVER(ORDER BY P.fnhsysempid ASC)+1 AS nvarchar(6))) , 6)AS 'SEQUENCE_NUMBER'
                        ,RIGHT('011',3) AS 'BANK_CODE'
                        ,LEFT(REPLACE(P.FTAccNo,'-','') + '                         ',10) AS 'ACCOUNT_NUMBER'
                        ,RIGHT('C',1) AS 'TRANSACTION_CODE'
                        ,RIGHT('0000000000000000' + REPLACE(CONVERT(decimal(18, 2), FNNetpay), '.', ''), 10) as 'AMOUNT'
                        ,RIGHT('08',2) AS 'SERVICE_TYPE'
                        ,RIGHT('9',1) AS 'STATUS'
                        ,RIGHT('          ',10) AS 'REFERENCE_AREA1'
                        ,RIGHT(REPLACE(convert(varchar(10),cast(FTPayDate as date),103),'/',''),2) + RIGHT(REPLACE(convert(varchar(10),cast(FTPayDate as date),112),'/',''),4)  AS 'INSERVICE_DATE'
                        ,RIGHT('0000',4) AS 'COMPANY_CODE'
                        ,RIGHT('001',3) AS 'HOME_BRANCH'
                        ,RIGHT('                    ',20) AS 'REFERENCE_AREA2'
                        ,RIGHT('      ',6) AS 'TMB_FLAG'
                        ,RIGHT('          ',10) AS 'SPARE'
                        ,LEFT (M.FTEmpName1 + ' ' + REPLACE(M.FTEmpSurname1,'-','')+ '                                      ' ,35) AS 'ACCOUNT_NAME'



                    FROM HITECH_HR.dbo.THRTPayRoll P
            INNER JOIN(
                       SELECT MAX(Emp.FTEmpCode) AS FTEmpCode, MAX(Emp.FTEmpCodeRefer) AS FTEmpCodeRefer, MAX(PN.FTPreNameNameEN ) As FTEmpPreCode

                      , Max(Emp.FTEmpNameEN) As FTEmpName1

                      , MAX(Emp.FTEmpSurnameEN) As FTEmpSurname1

                      , MAX(Emp.FTEmpNicknameEN) As FTEmpNickname1

                      , MAX(Emp.FTEmpNameTH) AS FTEmpName2

                      , MAX(Emp.FTEmpSurnameTH) As FTEmpSurname2

                      , Max(Emp.FTEmpNicknameTH) AS  FTEmpNickname2

                     , Emp.FNHSysEmpID
                      FROM      [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMEmployee AS Emp WITH(NOLOCK)
                         LEFT JOIN[" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPrename AS PN WITH(NOLOCK)
                         ON Emp.FNHSysPreNameId = PN.FNHSysPreNameId
                         WHERE Emp.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + @" AND FNHSysEmpTypeId<> 0  ";

            _Qry = HI.ST.Security.PermissionEmpType(_Qry);

            _Qry += @"  GROUP BY FNHSysEmpID
             ) AS M ON P.FNHSysEmpID = M.FNHSysEmpID  
              INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMCfgPayDT AS PD WITH(NOLOCK) ON  P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId 
             AND P.FTPayTerm = PD.FTPayTerm 
             AND P.FTPayYear = PD.FTPayYear 
             LEFT JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMBank AS B WITH(NOLOCK)
             ON P.FNHSysBankId = B.FNHSysBankId
            LEFT JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMBankBranch AS BB WITH(NOLOCK)
             ON P.FNHSysBankBranchId = BB.FNHSysBankBranchId
               WHERE (P.FNHSysEmpTypeId <> 0) AND P.FNNetPay >0 AND ISNULL(P.FTAccNo,'') <> '' AND ISNULL(P.FNHSysBankBranchId,0) >0
             AND (P.FNHSysPayRollPayId IN (SELECT FNHSysPayRollPayId FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPaymentType AS PM WITH(NOLOCK) WHERE FTStatePackBank='1'  ) ) ";


            if (_EmpType.Length > 0)
            {
                _Qry += @" AND ( ";
                int Seq = 0;

                foreach (String Str in _EmpType)
                {
                    if (Str != "")
                    {
                        if (Seq == 0)
                        {
                            _Qry += @"  P.FNHSysEmpTypeId =" + Str + " ";
                        }
                        else
                        {
                            _Qry += @" OR P.FNHSysEmpTypeId =" + Str + " ";
                        }

                        Seq = Seq + 1;
                    }
                }
                _Qry += @" ) ";
            }

            _Qry += @" AND  PD.FTPayYear ='" + HI.UL.ULF.rpQuoted(_Year) + @"' 
                        AND  PD.FTPayTerm ='" + HI.UL.ULF.rpQuoted(_Term) + @"'  
                         AND (B.FTBankCode = N'" + HI.UL.ULF.rpQuoted(_BankCode) + @"') ";

            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR);





            //Header 

            _Qry = @"SELECT RIGHT('H',1) AS 'RECORD_TYPE'
                    , RIGHT('000001',6) AS 'SEQUENCE_NUMBER'
                    , RIGHT('011',3) AS 'BANK_CODE'
                    , LEFT( CAST(REPLACE(FTBnkAccNo,'-','')  AS nvarchar(10)) + '          ',10) AS 'COMPANY_ACCOUNT_NO'
                    , LEFT( LTRIM(FTCmpNameEN)+ '                    ',25) AS 'COMPANY_NAME'
                    ,  LEFT('" + _DatePay + @"',6)  AS 'POST_DATE'

                    , LEFT('        ',6) AS 'TAPE_NUMBER'
                    , LEFT('                                                                                  ',71) AS 'SPARE'

                     FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMCmp AS C WITH (NOLOCK) 
                     WHERE FNHSysCmpId=" + (HI.ST.SysInfo.CmpID);
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);

            String COMPANY_ACCOUNT_NO = "";
            foreach (DataRow r in _Dt.Rows)
            {
                _line001 = r["RECORD_TYPE"].ToString() + r["SEQUENCE_NUMBER"].ToString() + r["BANK_CODE"].ToString() + r["COMPANY_ACCOUNT_NO"].ToString() + r["COMPANY_NAME"].ToString() + r["POST_DATE"].ToString() + r["TAPE_NUMBER"].ToString() + r["SPARE"].ToString() ;
                //  _Line002 = r["2_1RecordType"].ToString() + r["2_2ProductCode"].ToString() + r["2_3ValueDate"].ToString() + r["2_4AccountNo"].ToString() + r["2_5AccountType"].ToString() + r["2_6DebitBranchCode"].ToString() + r["2_DebitCurrency"].ToString() + r["TotalAmount"].ToString() + r["2_9InRef"].ToString() + r["2_10NoCredits"].ToString() + r["2_11FeeDebitAccount"].ToString() + r["2_12Filler"].ToString() + r["2_13MCC"].ToString() + r["2_14AccountTypeFee"].ToString() + r["2_15DebitBranchCodeFee"].ToString();
                COMPANY_ACCOUNT_NO =  r["COMPANY_ACCOUNT_NO"].ToString();
            }

            foreach (DataRow dr999 in _DtPayRollSum.Rows)
            {
                _line999 = dr999["RECORD_TYPE"].ToString() + dr999["SEQUENCE_NUMBER"].ToString() + dr999["BANK_CODE"].ToString() + COMPANY_ACCOUNT_NO.ToString() + dr999["NO_OF_DR_TRANSACTION"].ToString() + dr999["TOTAL_DR_AMOUNT"].ToString() + dr999["NO_OF_CR_TRANSACTION"].ToString() + dr999["TOTAL_CR_AMOUNT"].ToString() + dr999["NO_OF_REJECT_DR_TRANS"].ToString() + dr999["TOTAL_REJECT_DR_AMOUNT"].ToString() + dr999["NO_OF_REJECT_CR_TRANS"].ToString() + dr999["TOTAL_REJECT_CR_AMOUNT"].ToString() + dr999["SPARE"].ToString();
                _TotalAmount = dr999["TOTAL_CR_AMOUNT"].ToString();
                _TotalRec = Int32.Parse(dr999["NO_OF_CR_TRANSACTION"].ToString()).ToString();
                _GAmount = Double.Parse(dr999["SUM_FNNetpay"].ToString());
            }


            MsgShow = " Totol Record : " + _TotalRec + Environment.NewLine + "Total Amount : " + String.Format("{0:#,0.####}", _GAmount);

            line1 = _line001;
            line2 = _Line002;
            line5 = _line999;


            return _DtPayRoll;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace HI.EXPORTBANK
{
   public static class ExporForSCB
    {
        public static DataTable ExportDataFile_HT(String _BankCode, String _CmpCode, String[] _EmpType,
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

            _Qry = @"SELECT * FROM(
                SELECT 
                '999' AS '5_1RecordType'
                , '000001' AS '5_2TotalNoDebits'
                , RIGHT( '000000' + CAST(COUNT(P.FNHSysEmpID) AS nvarchar(6)), 6) AS '5_3TotalNoCredits'
                , RIGHT('0000000000000000' + REPLACE(CONVERT( decimal(18,3),SUM(FNNetpay)) ,'.','') , 16) as '5_4CreditAmount'
, COUNT(P.FNHSysEmpID) AS totelRec
, SUM(FNNetpay) AS SumNetPay
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


                         ) AS TM ";
            _DtPayRollSum = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);


            foreach (DataRow dr999 in _DtPayRollSum.Rows)
            {
                _line999 = dr999["5_1RecordType"].ToString() + dr999["5_2TotalNoDebits"].ToString() + dr999["5_3TotalNoCredits"].ToString() + dr999["5_4CreditAmount"].ToString();
                _TotalAmount = dr999["5_4CreditAmount"].ToString();
                _TotalRec =  dr999["totelRec"].ToString();
                _GAmount = Double.Parse(dr999["SumNetPay"].ToString());
            }

           


            _Qry = @" SELECT * FROM( SELECT 
                     '003'AS  'C3_1RecordType'
                    , RIGHT('000000' + CAST(ROW_NUMBER() OVER(ORDER BY P.fnhsysempid ASC) AS nvarchar(6)), 6)AS 'C3_2CreditsNumberSequence'
                    , LEFT(REPLACE(FTAccNo,'-','') + '                         ',25) AS 'C3_4CreditAccount' 
                    , RIGHT('0000000000000000' + REPLACE(CONVERT(decimal(18, 3), FNNetpay), '.', ''), 16) as 'C3_4CreditAmount'
                    ,'THB' AS 'C3_5CreditCurrency'
                    ,'00000001' AS 'C3_6InRef'
                    ,'N' AS 'C3_7'
                    ,'N' AS 'C3_8'
                    ,'N' AS 'C3_9'
                    ,' ' AS 'C3_10'
                    ,LEFT('    ', 4) AS 'C3_11PickLocation'
                    ,LEFT('00', 2) AS 'C3_12WHTFromType'
                    ,  LEFT('              ', 14) AS 'C3_13'
                    ,  LEFT('000000', 6) AS 'C3_14'
                    ,  LEFT('00', 2) AS 'C3_15'
                    ,  LEFT('0000000000000000', 16) AS 'C3_16'
                    ,  LEFT('000000', 6) AS 'C3_17'
                    ,  LEFT('0000000000000000', 16) AS 'C3_18'
                    ,  LEFT('0', 1) AS 'C3_19'
                    ,  LEFT('                                        ', 40) AS 'C3_20WHTRemark'
                    ,  LEFT('        ', 8) AS 'C3_21'
                    , '014' AS 'C3_22RecBankCode'
                    ,  LEFT('                                   ', 35) AS 'C3_23'
                    ,  RIGHT('0000' + BB.FTBankBranchCode , 4) AS 'C3_24'
                    ,  LEFT('                                   ', 35) AS 'C3_25'
                    ,  LEFT(' ', 1) AS 'C3_26'
                    ,  LEFT('N', 1) AS 'C3_27'
                    ,  LEFT('                    ', 20) AS 'C3_28'
                    ,  LEFT(' ', 1) AS 'C3_29'
                    ,  LEFT('   ', 3) AS 'C3_30'
                    ,  LEFT('  ', 2) AS 'C3_31'
                    ,  LEFT('                                                  ', 50) AS 'C3_32'
                    ,  LEFT('                  ', 18) AS 'C3_33'
                    ,  LEFT('  ', 2) AS 'C3_34'


                    ,'004' AS 'C4_1RecordType'
                    ,'00000001' AS 'C4_2InRef'
                    , RIGHT('000000' + CAST(ROW_NUMBER() OVER(ORDER BY P.fnhsysempid ASC) AS nvarchar(6)), 6)AS 'C4_3CreditsNumberSequence'
                    ,  LEFT('               ', 15) AS 'C4_4Payee1ID'
                    , LEFT(M.FTEmpPreCode + M.FTEmpName1+ ' ' + M.FTEmpSurname1+'                                                                                                ', 100) AS 'C4_5'
                    ,  LEFT('                                                                       ', 70) AS 'C4_6PayeeAddr1'
                    ,  LEFT('                                                                       ', 70) AS 'C4_7PayeeAddr2'
                    ,  LEFT('                                                                       ', 70) AS 'C4_8PayeeAddr3'
                    ,  LEFT('           ', 10) AS 'C4_9PayeeTaxID'
                    ,  LEFT('                                                                       ', 70) AS 'C4_10PayeeNameEng'
                    ,  LEFT('           ', 10) AS 'C4_11'
                    ,  LEFT('           ', 10) AS 'C4_12'
                    ,  LEFT('                                                                 ', 64) AS 'C4_13Payee1Email'
                    ,  LEFT('                                                                                                     ', 100) AS 'C4_14Payee2NameThai'
                    ,  LEFT('                                                                       ', 70) AS 'C4_15Payee2Address1'
                    ,  LEFT('                                                                       ', 70) AS 'C4_16Payee2Address2'
                    ,  LEFT('                                                                       a', 70) AS 'C4_17Payee2Address3'


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
                         AND (B.FTBankCode = N'" + HI.UL.ULF.rpQuoted(_BankCode) + @"')



                         ) AS TM ";

            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR);




            String _typePay = "";
            if (type == "D")
            {
                _typePay = "PA2";
            }
            else                        {
                _typePay = "PAY";
            }

//, '" + _DatePay + @"' AS '2_3ValueDate'
            
            _Qry = @"SELECT TOP 1 

                        '001' AS 'RecordType'
                        ,LEFT(FTDepositCode + '                  ',12) AS 'CompanyID'
                        ,LEFT( 'PAY000000001' + '                                ',32) AS 'CustomerRef'
                        , convert(varchar(8),cast(getdate() as date),112) AS 'FileDate'
                        ,REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','') AS 'FileTime'
                        , 'BCM' AS 'ChannelID'
                        ,LEFT( 'PAY000000001' + '                                ',32) AS 'BatchRef'


                        ,'002' AS '2_1RecordType'
                      ,'" + _typePay + @"' AS '2_2ProductCode'
                        ,'" + _DatePay + @"' AS '2_3ValueDate' 

                        
                        , left (REPLACE(REPLACE(FTBnkAccNo,'-',''),'-','') + '                         ',25) AS '2_4AccountNo'
                        , '0' + SUBSTRING(REPLACE(FTBnkAccNo,'-',''),4,1) AS '2_5AccountType'
                        , '0' + SUBSTRING(REPLACE(FTBnkAccNo,'-',''),0,4) AS '2_6DebitBranchCode'
                        , 'THB' AS '2_DebitCurrency'
                        , '" + _TotalAmount  + @"' as 'TotalAmount' 
                        ,'00000001' AS '2_9InRef' 
                        ,RIGHT('0000000' + CAST(" + _TotalRec + @" AS nvarchar(6)) ,6) AS '2_10NoCredits'
                        , left (CAST(REPLACE(FTBnkAccNo,'-','') AS nvarchar(50)) + '                         ',15) AS '2_11FeeDebitAccount'
                        , left ('         ',9) AS '2_12Filler'
                        , ' ' as '2_13MCC'
                        , '0' + SUBSTRING(REPLACE(FTBnkAccNo,'-',''),4,1) AS '2_14AccountTypeFee'
                        , '0' + SUBSTRING(REPLACE(FTBnkAccNo,'-',''),0,4) AS '2_15DebitBranchCodeFee'

                     FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMCmp AS C WITH (NOLOCK) 
                     WHERE FNHSysCmpId=" + (HI.ST.SysInfo.CmpID);
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);


            foreach (DataRow r in _Dt.Rows)
            {
                _line001 = r["RecordType"].ToString() + r["CompanyID"].ToString() + r["CustomerRef"].ToString() + r["FileDate"].ToString() + r["FileTime"].ToString() + r["ChannelID"].ToString() + r["BatchRef"].ToString();
                _Line002 = r["2_1RecordType"].ToString() + r["2_2ProductCode"].ToString() + r["2_3ValueDate"].ToString() + r["2_4AccountNo"].ToString() + r["2_5AccountType"].ToString() + r["2_6DebitBranchCode"].ToString() + r["2_DebitCurrency"].ToString() + r["TotalAmount"].ToString() + r["2_9InRef"].ToString() + r["2_10NoCredits"].ToString() + r["2_11FeeDebitAccount"].ToString() + r["2_12Filler"].ToString() + r["2_13MCC"].ToString() + r["2_14AccountTypeFee"].ToString() + r["2_15DebitBranchCodeFee"].ToString();

            }





            MsgShow = " Totol Record : " + _TotalRec + Environment.NewLine + "Total Amount : " + String.Format("{0:#,0.####}", _GAmount);

            line1 = _line001;
            line2 = _Line002;
            line5 = _line999;
           

            return _DtPayRoll;
        }

            public static DataTable ExportDataFile(String _BankCode, String _CmpCode, String[] _EmpType,
                                 String _Year, String _Term, String _Period, String _DateStart,
                                 String _DateEnd, String _DatePay,ref String MsgShow, ref String TotalRec, ref String TotalAmount)
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


            _Qry = @"    SELECT * FROM ( SELECT         
						 
						  LEFT(REPLACE(P.FTAccNo,'-','')+ REPLICATE(' ',10),10)  AS [FTAccNo]
		 ,'" + _DatePay + @"' AS [Eff_Date]
		,'0' AS [Sign]                         
		,RIGHT( REPLACE('00000000' + SUBSTRING(CAST(P.FNNetpay AS nvarchar(15)) , 1, LEN(P.FNNetpay)-3),'.',''),11) as [Amount]					 
		, LEFT(REPLACE(REPLACE(FTEmpName2, CHAR(13), ''), CHAR(10), '') + ' '+ REPLACE(REPLACE(FTEmpSurname2, CHAR(13), ''), CHAR(10), '') + REPLICATE(' ',52),52)  AS [FTEmpName]		
		,P.FNNetpay AS [FNNetpay]

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
            TotalRec = _TotalRec.ToString();


            foreach (DataRow dr in _DtPayRoll.Rows)
            {
                _GAmount = _GAmount + Double.Parse(dr["FNNetpay"].ToString());
            }
            Double Amount = 0;

            foreach (DataRow dr in _DtPayRoll.Rows)
            {
                Amount = Amount + Double.Parse(dr["Amount"].ToString());
            }
            TotalAmount = Amount.ToString();

            _DtPayRoll.Columns.Remove("FNNetpay");

            _Dt = _DtPayRoll;
            MsgShow = " Totol Record : " + _TotalRec + Environment.NewLine + "Total Amount : " +  String.Format("{0:#,0.####}", _GAmount);

            //MessageBox.Show(Message);
          //  MsgShow = "Total Record : " & (Format(_TotalRec, "#,#0"))
//MsgShow &= vbCrLf & "Total Amount : " & (Format(_GAmount, "#,#0.00"))
//

            return _Dt;
        }

    }
}

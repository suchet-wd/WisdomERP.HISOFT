using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace HI.EXPORTBANK
{
   public static class ExporForTanachartFund
    {


        public static DataTable ExportDataFile(String _Year, String _Month, ref String MsgShow)
        {
            DataTable _Dt = new DataTable();
            DataTable _DtPayRoll = new DataTable();

            String _Qry;
            //DateTime _y = new DateTime();
            //_y = Convert.ToDateTime(DateTime.Parse(_Year).ToString("yyyy"));
            //_Year = _y.ToString();
          

            //_Month = ("00" + _Month).Substring(1, 2);
            String DateBf = "";


            if (int.Parse(_Year) > 2500)
            {
                DateBf = (int.Parse(_Year) - 543).ToString() + "/" + _Month + "/01";
            }
            else {
                DateBf = (int.Parse(_Year)).ToString() + "/" + _Month + "/01";
            } 
            _Qry = @" SELECT    
                           '0000' AS FUND_CODE
                           ,'00000' AS COMP_CODE
                        , SUBSTRING(MAX(EM.FTEmpCode)+'                    ',1,20) AS MEMBER_ACCOUNT_NO
                        , SUBSTRING(PN.FTPreNameCode +'                    ',1,20) AS PRENAME
                        , SUBSTRING(EM.FTEmpNameTH +'                              ',1,30) AS NAME
                        , SUBSTRING(EM.FTEmpSurnameTH +'                              ',1,30) AS SURNAME
                        , SUBSTRING(M.FTEmpIdNo +'               ',1,15) AS ID_CARD
                        , SUBSTRING(FORMAT(dateadd(year, +543, EM.FDDateStart), 'ddMMyyyy') +'        ',1,8) AS START_WORK_DATE
                        , SUBSTRING(FORMAT(dateadd(year, +543, EM.FDFundBegin), 'ddMMyyyy') +'        ',1,8) AS FIRST_MEMBER_DATE
                        , SUBSTRING(FORMAT(dateadd(year, +543, EM.FDFundBegin), 'ddMMyyyy') +'        ',1,8) AS  WORK_DATE
                        ,RIGHT('               ' + CAST(SUM(CONVERT(numeric(18, 2), M.FNContributedAmt)) AS nvarchar(15)),15) AS MCONT_AMT	
                        ,RIGHT('               ' + CAST(SUM(CONVERT(numeric(18, 2), M.FNCmpContributedAmt)) AS nvarchar(15)),15) AS CCONT_AMT
                        , SUBSTRING('               ',1,15) AS DEPT_CODE
                        , SUBSTRING('00',1,2) AS MEM_TYPE

                          FROM           
		                           (SELECT  P.FTPayYear,  P.FNHSysEmpID,P.FNHSysEmpTypeID, P.FTEmpIdNo, D.FNMonth
		                          , EM.FDDateStart, EM.FDFundBegin, EM.FDFundEnd
		                          , P.FNContributedAmt, P.FNCmpContributedAmt
		                          FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRTPayRoll AS P WITH (NOLOCK) 
		                          INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON  P.FTPayTerm = D.FTPayTerm AND P.FTPayYear = D.FTPayYear AND P.FNHSysEmpTypeID = D.FNHSysEmpTypeID
		                          INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMEmployee AS EM WITH (NOLOCK) ON  P.FNHSysEmpID = EM.FNHSysEmpID
		                          WHERE        (P.FTPayYear ='"+ _Year  + @"') AND ISNULL(EM.FDFundBegin,'')<>'' 
				                        AND (D.FNMonth ="+ _Month  + @" ) 
				                        AND       (((P.FNContributedAmt) > 0) ) 
				                        AND EM.FNHSysCmpId="+ HI.ST.SysInfo.CmpID  + @" AND ( ISNULL(EM.FDDateEnd,'')='' OR  ( ISNULL(EM.FDDateEnd,'')>'"+DateBf+@"') ) 
		                           ) AS M INNER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + @"].dbo.THRMEmployee AS EM WITH (NOLOCK) ON  M.FNHSysEmpID = EM.FNHSysEmpID
                         LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.THRMPrename AS PN WITH(NOLOCK) ON EM.FNHSysPreNameId = PN.FNHSysPreNameId
                         LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + @"].dbo.TCNMDivision  WITH(NOLOCK) ON EM.FNHSysDivisonId = TCNMDivision.FNHSysDivisonId 
                        Where EM.FNHSysCmpId=1306010001   
                          GROUP BY M.FTPayYear,  M.FTEmpIdNo,  M.FNMonth, PN.FTPreNameCode,PN.FTSocialCode, EM.FTEmpNameEN, EM.FTEmpSurnameEN, EM.FTEmpNicknameEN, 
                          EM.FTEmpNameTH, EM.FTEmpSurnameTH, EM.FTSocialNo
                        , EM.FDDateStart, EM.FDFundBegin, EM.FDFundEnd
                        , M.FNContributedAmt, M.FNCmpContributedAmt ";


            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR);


            _Dt = _DtPayRoll;
            MsgShow = "";

            return _Dt;
        }

    }
}

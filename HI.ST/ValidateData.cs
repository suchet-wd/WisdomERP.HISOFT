using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HI.ST
{
    public static class ValidateData
    {
        public static Boolean  CloseJob(string _OrderNo)
        {
            string _Qry = null;
            _Qry = " SELECT TOP 1 FNJobState FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder WITH(NOLOCK) WHERE FTOrderNo ='" + HI.UL.ULF.rpQuoted(_OrderNo) + "'";
            return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")=="3");
        }

        public static Boolean CloseJobDocIn(string _DocumentNo)
        {
            string _Qry = null;
            _Qry = " SELECT TOP 1 A.FNJobState ";
            _Qry = _Qry + " FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS A WITH(NOLOCK) ";
            _Qry = _Qry + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo ";
            _Qry = _Qry + " WHERE B.FTDocumentNo ='" + HI.UL.ULF.rpQuoted(_DocumentNo) + "' AND A.FNJobStat=3";
            return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") == "3");
        }

        public static Boolean CloseJobDocOut(string _DocumentNo)
        {
            string _Qry = null;
           
            _Qry = " SELECT TOP 1 A.FNJobState ";
            _Qry = _Qry + " FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS A WITH(NOLOCK) ";
            _Qry = _Qry + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo ";
            _Qry = _Qry + " WHERE B.FTDocumentNo ='" + HI.UL.ULF.rpQuoted(_DocumentNo) + "' AND A.FNJobStat=3";

            return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") == "3");

        }

        public static Boolean CloseJobDocOutAfIssue(string _DocumentNo)
        {
          
            string _Qry = null;

            _Qry = " SELECT TOP 1 A.FNJobState ";
            _Qry = _Qry + " FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS A WITH(NOLOCK) ";
            _Qry = _Qry + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo ";
            _Qry = _Qry + " WHERE B.FTDocumentNo ='" + HI.UL.ULF.rpQuoted(_DocumentNo) + "' AND A.FNJobStat=3";

            return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") == "3");

        }


    }
}

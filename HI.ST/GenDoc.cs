using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HI.ST
{
    public static class GenDoc
    {
        public static string GetDocNo(string _TblName, string _DocType, bool _GetFotmat = false)
        {
            string _Strsql = null;
            _Strsql = " EXEC ["+ HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM ) +"].dbo.SP_GEN_DOCUMENTNO '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.ModuleName) + "','" + HI.UL.ULF.rpQuoted(_TblName) + "','" + HI.UL.ULF.rpQuoted(_DocType) + "','" + (_GetFotmat ? "Y" : "") + "'";
            return HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM, "");
        }
        public static string GetDocNo(string _MdlCode, string _TblName, string _DocType, bool _GetFotmat = false)
        {
            string _Strsql = null;
            _Strsql = " EXEC [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.SP_GEN_DOCUMENTNO '" + HI.UL.ULF.rpQuoted(_MdlCode) + "','" + HI.UL.ULF.rpQuoted(_TblName) + "','" + HI.UL.ULF.rpQuoted(_DocType) + "','" + (_GetFotmat ? "Y" : "") + "'";
            return HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM, "");
        }
    }
}

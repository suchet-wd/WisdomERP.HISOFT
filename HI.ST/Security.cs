using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HI.ST
{
    public static class Security
    {
        #region "Form Object"
        public static DataTable PermissionObject(string _MenuName, Control _ObjectForm)
        {
            DataTable DT_Temp = new DataTable();
            try
            {
                string Strsql = null;

                if ((HI.ST.SysInfo.Admin))
                {

                    if ((_ObjectForm != null))
                    {
                        if (Strings.Left(_ObjectForm.Name.ToString(), 7).ToUpper() != "AddEdit".ToUpper())
                        {
                            Strsql = " SELECT  TOP 1    FTFormName  ";
                            Strsql += "  FROM         [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysObjectForm WITH (NOLOCK)   ";
                            Strsql += " WHERE    FTMnuName='" + HI.UL.ULF.rpQuoted(_MenuName) + "' ";
                            Strsql += " AND  FTFormName='" + HI.UL.ULF.rpQuoted(_ObjectForm.Name.ToString()) + "' ";


                            if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_SYSTEM, "")))
                            {
                                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysObjectForm(FTInsUser, FDInsDate, FTInsTime, FTMnuName, FTFormName) ";
                                Strsql += Constants.vbCrLf + " SELECT '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114) ";
                                Strsql += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(_MenuName) + "' ";
                                Strsql += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(_ObjectForm.Name.ToString()) + "' ";
                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_SYSTEM);

                            }

                            Strsql = " SELECT  TOP 1   FTObjectName ";
                            Strsql += "  FROM           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysObjectControl WITH(NOLOCK) ";
                            Strsql += " WHERE     FTMnuName='" + HI.UL.ULF.rpQuoted(_MenuName) + "' ";
                            Strsql += " AND  FTFormName='" + HI.UL.ULF.rpQuoted(_ObjectForm.Name.ToString()) + "' AND ISNULL(FTObjectName,'') <>'' ";


                            if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_SYSTEM, "")))
                            {
                                InsertObj(_ObjectForm, _MenuName, _ObjectForm.Name.ToString());

                            }

                        }
                    }


                }
                else
                {

                    DisableObj(_ObjectForm);

                    Strsql = "       SELECT        A.FTUserName, B.FTMnuName, B.FTFormName, B.FTObjName";
                    Strsql += Constants.vbCrLf + "  FROM            [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A WITH(NOLOCK) INNER JOIN";
                    Strsql += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionObjectControl AS B WITH(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID";
                    Strsql += Constants.vbCrLf + "  WHERE A.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    Strsql += Constants.vbCrLf + "  AND  B.FTMnuName='" + HI.UL.ULF.rpQuoted(_MenuName) + "' ";
                    Strsql += Constants.vbCrLf + "  AND  B.FTFormName='" + HI.UL.ULF.rpQuoted(_ObjectForm.Name.ToString()) + "' ";

                    DT_Temp = HI.Conn.SQLConn.GetDataTable(Strsql, Conn.DB.DataBaseName.DB_SECURITY);

                    foreach (DataRow R in DT_Temp.Rows)
                    {
                        foreach (object ObjN in _ObjectForm.Controls.Find((R["FTObjName"]).ToString(), true))
                        {
                            switch (HI.ENM.Control.GeTypeControl(ObjN))
                            {
                                case ENM.Control.ControlType.SimpleButton:

                                    ((DevExpress.XtraEditors.SimpleButton)ObjN).Enabled = true;
                                    break;
                            }
                        }
                    }

                }

                //Msgbox(ex.Message)
            }
            catch (
                Exception
                )
            {
            }

            return DT_Temp;

        }

        private static void InsertObj(object obj, string _MenuId, string _FormName)
        {
            string Strsql = null;
            try
            {
                foreach (Control ObjN in ((Control)obj).Controls)
                {
                    switch (HI.ENM.Control.GeTypeControl(ObjN))
                    {
                        case ENM.Control.ControlType.SimpleButton:

                            if (((DevExpress.XtraEditors.SimpleButton)ObjN).Name.ToString().ToUpper() == "ocmexit".ToUpper())
                            {
                            }
                            else
                            {
                                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysObjectControl(FTInsUser, FDInsDate, FTInsTime,  FTMnuName, FTFormName";
                                Strsql += Constants.vbCrLf + " , FTObjectName) ";
                                Strsql += Constants.vbCrLf + " SELECT '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114) ";
                                Strsql += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(_MenuId) + "' ";
                                Strsql += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(_FormName) + "' ";
                                Strsql += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(ObjN.Name.ToString()) + "' ";

                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_SYSTEM);

                            }
                            break;
                    }

                    InsertObj(ObjN, _MenuId, _FormName);

                }
            }
            catch (Exception ex)
            {
            }

        }


        private static void DisableObj(object obj)
        {
            try
            {
                foreach (Control ObjN in ((Control)obj).Controls)
                {
                    switch (HI.ENM.Control.GeTypeControl(ObjN))
                    {
                        case ENM.Control.ControlType.SimpleButton:

                            if (((DevExpress.XtraEditors.SimpleButton)ObjN).Name.ToString().ToUpper() == "ocmexit".ToUpper())
                            {
                                ((DevExpress.XtraEditors.SimpleButton)ObjN).Enabled = true;
                            }
                            else
                            {
                                ((DevExpress.XtraEditors.SimpleButton)ObjN).Enabled = false;
                            }

                            break;
                    }

                    DisableObj(ObjN);
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region "HR"


        #endregion

        public static string PermissionEmpData(string _StrQuery)
        {
            string _StrJoin = "";
            string _QrySP = "";
           

            //if (!(HI.ST.SysInfo.Admin))
            //{
            //    string _QrySP = "";
            //    _QrySP = " EXEC [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.SP_GET_USER_PERMISSTION_TEMP '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "','" + HI.ST.SysInfo.CmpID + "' ";
            //    HI.Conn.SQLConn.ExecuteOnly(_QrySP, Conn.DB.DataBaseName.DB_SECURITY);

            //    _StrJoin += Constants.vbCrLf + "";
            //    _StrJoin += Constants.vbCrLf + " INNER  JOIN (SELECT FNHSysEmpTypeId,FNHSysSectId ";
            //    _StrJoin += Constants.vbCrLf + " FROM    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.tmpPermissionTCNMSect  WITH (NOLOCK) ";
            //    _StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' )) AS perSec  ";
            //    _StrJoin += Constants.vbCrLf + " ON perSec.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND perSec.FNHSysSectId=M.FNHSysSectId ";
            //    _StrJoin += Constants.vbCrLf + "";
            //    _StrJoin += Constants.vbCrLf + " INNER JOIN (SELECT FNHSysEmpTypeId,FNHSysUnitSectId ";
            //    _StrJoin += Constants.vbCrLf + " FROM   [HITECH_SECURITY].dbo.tmpPermissionTCNMUnitSect  WITH (NOLOCK) ";
            //    _StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')) AS perUSec ";
            //    _StrJoin += Constants.vbCrLf + " ON perUSec.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND perUSec.FNHSysUnitSectId=M.FNHSysUnitSectId ";
            //    _StrJoin += Constants.vbCrLf + "";

            //}

            string _Str = "";

            _Str = "  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";

            if (!(HI.ST.SysInfo.Admin))
            {
                

                _StrJoin = "";

                _QrySP = QueryPermissionData();


                _StrJoin += Constants.vbCrLf + " INNER JOIN @TabEmp  AS perEmp    ON  M.FNHSysEmpId  =perEmp.FNHSysEmpIdx ";

                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FTStateAllx,X.FTStateAllUnitx FROM  @TabType  X  WHERE X.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId ) AS perTyep     ";
                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysSectId,X.FTStateAllUnit FROM  @TabSect  X  WHERE X.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND X.FNHSysSectId=M.FNHSysSectId  ) AS perSec     ";
                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysUnitSectId FROM  @TabUnitSeclt AS X WHERE X.FNHSysEmpTypeIdx =M.FNHSysEmpTypeIdx AND X.FNHSysUnitSectId=M.FNHSysUnitSectId  ) AS  perUSec ";

                //_QrySPWhere = " perTyep.FNHSysEmpTypeIdx >0 ";
                //_QrySPWhere += Constants.vbCrLf + " AND ( CASE WHEN perTyep.FTStateAllx='1' OR M.FNHSysSectId=0 THEN 1 ELSE (CASE WHEN  perSec.FNHSysSectId >0 THEN 1 ELSE 0 END) END =1 ) ";
                //_QrySPWhere += Constants.vbCrLf + " AND ( CASE WHEN perTyep.FTStateAllUnitx='1' OR M.FNHSysUnitSectId=0 THEN 1 ELSE (CASE WHEN  perUSec.FNHSysUnitSectId >0 THEN 1 ELSE 0 END) END =1 ) ";

                //_StrJoin += Constants.vbCrLf + " INNER  JOIN (SELECT  DISTINCT FNHSysEmpTypeId AS FNHSysEmpTypeIdx,FNHSysSectId ";
                //_StrJoin += Constants.vbCrLf + " FROM    @TabSect ";
                //_StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' )) AS perSec  ";
                //_StrJoin += Constants.vbCrLf + " ON perSec.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND perSec.FNHSysSectId=M.FNHSysSectId ";
                //_StrJoin += Constants.vbCrLf + "";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN (SELECT DISTINCT  FNHSysEmpTypeId AS FNHSysEmpTypeIdx ,FNHSysUnitSectId ";
                //_StrJoin += Constants.vbCrLf + " FROM   @TabUnitSeclt ";
                //_StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')) AS perUSec ";
                //_StrJoin += Constants.vbCrLf + " ON perUSec.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND perUSec.FNHSysUnitSectId=M.FNHSysUnitSectId ";
                //_StrJoin += Constants.vbCrLf + " ";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN  (SELECT FNHSysEmpTypeId AS FNHSysEmpTypeIdx FROM @TabType) AS perTyep ";
                //_StrJoin += Constants.vbCrLf + " ON perTyep.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId  ";
                //_StrJoin += Constants.vbCrLf + " ";


                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = _StrJoin + "  WHERE  " + _Str;
                }
                else
                {
                    
                        int n = 0;
                        n = _StrQuery.ToLower().LastIndexOf("where");
                    if (n > 5)
                    {
                        string cmd = _StrQuery.Substring(0, n);

                        int Indx1 = cmd.ToLower().LastIndexOf("(");
                        int Indx2 = cmd.ToLower().LastIndexOf(")");

                        if (Indx2 > Indx1)
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + cmd + _StrJoin + _StrQuery.Substring(n, _StrQuery.Length - n) + " AND " + _Str  ;
                        }
                        else
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin  ;
                        }
                    }
                    else
                    {
                        _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin   ;
                    }
                }

            }
            else
            {
                // _Str = _StrQuery;
                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = "  WHERE  " + _Str;
                }
                else
                {
                    _Str = _StrQuery + "  AND  " + _Str;


                }
            }

            return _Str;

        }

        public static string PermissionEmpType(string _StrQuery)
        {
            string _Str = "";
            string _QrySP = "";
            string _StrJoin = "";

            if (!(HI.ST.SysInfo.Admin))
            {

                //_QrySP = " EXEC [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.SP_GET_USER_PERMISSTION_TEMP '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "','" + HI.ST.SysInfo.CmpID + "' ";
                //HI.Conn.SQLConn.ExecuteOnly(_QrySP, Conn.DB.DataBaseName.DB_SECURITY);

                //_Str = "   FNHSysEmpTypeId IN (";
                //_Str += Constants.vbCrLf + " Select DISTINCT UPT.FNHSysEmpTypeId";
                //_Str += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                //_Str += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                //_Str += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                //_Str += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                //_Str += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                //_Str += Constants.vbCrLf + "  )      ";


                _QrySP = QueryPermission(true);

                _StrJoin += Constants.vbCrLf + " ";


                _StrJoin += Constants.vbCrLf + " INNER JOIN    @TabType  AS perTyep ON perTyep.FNHSysEmpTypeIdx =FNHSysEmpTypeId ";
                _StrJoin += Constants.vbCrLf + " ";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN (SELECT DISTINCT FNHSysEmpTypeId AS FNHSysEmpTypeIdx FROM @TabType) AS perTyep ";
                //_StrJoin += Constants.vbCrLf + " ON perTyep.FNHSysEmpTypeIdx=FNHSysEmpTypeId  ";
                //_StrJoin += Constants.vbCrLf + " ";


                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = "  WHERE  " + _Str;
                }
                else
                {
                    // _Str = _StrQuery + "  AND  " + _Str;
                    
                        int n = 0;
                        n = _StrQuery.ToLower().LastIndexOf("where");
                    if (n > 5)
                    {
                        string cmd = _StrQuery.Substring(0, n);

                        int Indx1 = cmd.ToLower().LastIndexOf("(");
                        int Indx2 = cmd.ToLower().LastIndexOf(")");

                        if (Indx2 > Indx1)
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + cmd + _StrJoin + _StrQuery.Substring(n, _StrQuery.Length - n);
                        }
                        else
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin;
                        }
                    }
                    else
                    {
                        _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin;
                    }

                }

            }
            else
            {
                _Str = _StrQuery;
            }

            return _Str;

        }

        public static string PermissionOrderCmpData(string _StrQuery)
        {
            string _Str = "";

            _Str = "";

            _Str = "SELECT TOP 1 FTFilterPermissionCmp FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysMenu WITH(NOLOCK) ";
            _Str += Constants.vbCrLf + " WHERE FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";

            if (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") == "1")
            {

                if (!(HI.ST.SysInfo.Admin))
                {
                    string _Qry = "";

                    _Qry = "   FTOrderNo IN ( SELECT FTOrderNo FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder WITH(NOLOCK) ";
                    _Qry += Constants.vbCrLf + " WHERE FNHSysCmpId IN ( Select DISTINCT UPT.FNHSysCmpId";
                    _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                    _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                    _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                    _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                    _Qry += Constants.vbCrLf + " ) OR FNOrderType<>0 )  ";

                    if (string.IsNullOrEmpty(_StrQuery))
                    {
                        _Str = "  WHERE  " + _Qry;
                    }
                    else
                    {
                        _Str = _StrQuery + "  AND  " + _Qry;
                    }

                }
                else
                {
                    _Str = _StrQuery;
                }
            }
            else
            {
                _Str = _StrQuery;
            };

            return _Str;

        }

        public static string QueryPermission(bool StateTypeOnly = false)
        {

            string cmdstring = " ";


            cmdstring += Constants.vbCrLf + "     Declare @TabType AS Table ( FNHSysEmpTypeIdx int,";

            cmdstring += Constants.vbCrLf + "   UNIQUE NONCLUSTERED(FNHSysEmpTypeIdx )";
            cmdstring += Constants.vbCrLf + " )";

            cmdstring += Constants.vbCrLf + "   INSERT INTO @TabType(FNHSysEmpTypeIdx)";
            cmdstring += Constants.vbCrLf + " Select DISTINCT UPT.FNHSysEmpTypeId";
            cmdstring += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
            cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
            cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
            cmdstring += Constants.vbCrLf + "    RIGHT OUTER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].[dbo].[THRMEmpType] ET WITH (NOLOCK) ON ET.FNHSysEmpTypeId=UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId =" + HI.ST.SysInfo.CmpID;
            cmdstring += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
            cmdstring += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";


            if (StateTypeOnly == false)
            {

                cmdstring += Constants.vbCrLf + "  Declare @TabSect AS Table (REf varchar(30), ";
                cmdstring += Constants.vbCrLf + "   FNHSysEmpTypeIdx int,";
                cmdstring += Constants.vbCrLf + "   FNHSysSectId int,";
                cmdstring += Constants.vbCrLf + "   FTUserName varchar(50),";
                cmdstring += Constants.vbCrLf + "   FTMnuName varchar(200),";
                cmdstring += Constants.vbCrLf + "   FNHSysCmpID int";
                cmdstring += Constants.vbCrLf + "    UNIQUE NONCLUSTERED(REf, FNHSysEmpTypeIdx, FNHSysSectId, FTUserName, FTMnuName, FNHSysCmpID)";
                cmdstring += Constants.vbCrLf + " 	)";

                cmdstring += Constants.vbCrLf + "     Declare @TabUnitSeclt AS Table (REf varchar(30),";
                cmdstring += Constants.vbCrLf + "    FNHSysEmpTypeIdx int,";
                cmdstring += Constants.vbCrLf + "  [FNHSysUnitSectId] int,";
                cmdstring += Constants.vbCrLf + "   FTUserName varchar(50),";
                cmdstring += Constants.vbCrLf + "   FTMnuName varchar(200),";
                cmdstring += Constants.vbCrLf + "   FNHSysCmpID int";
                cmdstring += Constants.vbCrLf + "   UNIQUE NONCLUSTERED(REf, FNHSysEmpTypeIdx,[FNHSysUnitSectId], FTUserName, FTMnuName, FNHSysCmpID)";
                cmdstring += Constants.vbCrLf + " )";




                cmdstring += Constants.vbCrLf + "   INSERT INTO @TabSect(REf,[FNHSysEmpTypeIdx],[FNHSysSectId],[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "  SELECT DISTINCT '1' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysSectId, UP.FTUserName, UPM.FTMnuName," + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "   FROM HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH (NOLOCK)INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "   HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID RIGHT OUTER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " CROSS JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK)";
                cmdstring += Constants.vbCrLf + "      WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "     AND(ISNULL(UPT.FTStateAll, '') = '1')";
                cmdstring += Constants.vbCrLf + "   AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";
                cmdstring += Constants.vbCrLf + "   AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";

                cmdstring += Constants.vbCrLf + "   INSERT INTO @TabSect(REf,[FNHSysEmpTypeIdx],[FNHSysSectId],[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT DISTINCT '2' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysSectId, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "     FROM     HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_SECURITY.dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "    UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID RIGHT OUTER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId";
                cmdstring += Constants.vbCrLf + "     OUTER APPLY(SELECT TOP 1  X.[FNHSysEmpTypeIdx]";
                cmdstring += Constants.vbCrLf + "      FROM @TabSect AS X";
                cmdstring += Constants.vbCrLf + "     WHERE X.FNHSysEmpTypeIdx = UPT.FNHSysEmpTypeId";
                cmdstring += Constants.vbCrLf + "     AND X.[FNHSysSectId] = S.FNHSysSectId";
                cmdstring += Constants.vbCrLf + "    AND X.[FTUserName] = UP.FTUserName";
                cmdstring += Constants.vbCrLf + "    AND X.[FTMnuName] = UPM.FTMnuName";
                cmdstring += Constants.vbCrLf + "   )    AS Tmp";
                cmdstring += Constants.vbCrLf + "     WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "     AND(ISNULL(UPT.FTStateAll, '') <> '1')";
                cmdstring += Constants.vbCrLf + "     AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";
                cmdstring += Constants.vbCrLf + "     AND Tmp.FNHSysEmpTypeIdx IS NULL";

                cmdstring += Constants.vbCrLf + "    INSERT INTO @TabSect(REf,[FNHSysEmpTypeIdx],[FNHSysSectId],[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT DISTINCT '3' AS Ref, UPT.FNHSysEmpTypeId, 0, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "   FROM      HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                cmdstring += Constants.vbCrLf + "     OUTER APPLY(SELECT TOP 1  X.[FNHSysEmpTypeIdx]";
                cmdstring += Constants.vbCrLf + "        FROM @TabSect AS X";
                cmdstring += Constants.vbCrLf + "      WHERE X.FNHSysEmpTypeIdx = UPT.FNHSysEmpTypeId";
                cmdstring += Constants.vbCrLf + "     AND X.[FNHSysSectId] = 0";
                cmdstring += Constants.vbCrLf + "     AND X.[FTUserName] = UP.FTUserName";
                cmdstring += Constants.vbCrLf + "     AND X.[FTMnuName] = UPM.FTMnuName";
                cmdstring += Constants.vbCrLf + "   )    AS Tmp";
                cmdstring += Constants.vbCrLf + "    WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(ISNULL(UPT.FTStateAll, '') = '1')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "     AND Tmp.FNHSysEmpTypeIdx IS NULL";

                cmdstring += Constants.vbCrLf + "   INSERT INTO @TabUnitSeclt(REf,[FNHSysEmpTypeIdx], FNHSysUnitSectId,[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT DISTINCT '1' AS REf, UPT.FNHSysEmpTypeId, S.FNHSysUnitSectId, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "    FROM      HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID RIGHT OUTER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " CROSS JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_MASTER.dbo.TCNMUnitSect AS S WITH(NOLOCK)";
                cmdstring += Constants.vbCrLf + "      WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(ISNULL(UPT.FTStateAllUnit, '') = '1')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";

                cmdstring += Constants.vbCrLf + "     INSERT INTO @TabUnitSeclt(REf,[FNHSysEmpTypeIdx], FNHSysUnitSectId,[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT DISTINCT '1' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysUnitSectId, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "    FROM     HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "      UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID RIGHT OUTER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " CROSS JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_MASTER.dbo.TCNMUnitSect AS S WITH(NOLOCK)";
                cmdstring += Constants.vbCrLf + "    OUTER APPLY(SELECT TOP 1  X.[FNHSysEmpTypeIdx]";
                cmdstring += Constants.vbCrLf + "     FROM @TabUnitSeclt AS X";
                cmdstring += Constants.vbCrLf + "      WHERE X.FNHSysEmpTypeIdx = UPT.FNHSysEmpTypeId";
                cmdstring += Constants.vbCrLf + "     AND X.FNHSysUnitSectId = S.FNHSysUnitSectId";
                cmdstring += Constants.vbCrLf + "    AND X.[FTUserName] = UP.FTUserName";
                cmdstring += Constants.vbCrLf + "     AND X.[FTMnuName] = UPM.FTMnuName";
                cmdstring += Constants.vbCrLf + "      )    AS Tmp";
                cmdstring += Constants.vbCrLf + "   WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(ISNULL(UPT.FTStateAll, '') <> '1')";
                cmdstring += Constants.vbCrLf + "    AND(ISNULL(UPT2.FTStateAll, '') = '1')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";
                cmdstring += Constants.vbCrLf + "   AND Tmp.FNHSysEmpTypeIdx IS NULL";

                cmdstring += Constants.vbCrLf + "    INSERT INTO @TabUnitSeclt(REf,[FNHSysEmpTypeIdx], FNHSysUnitSectId,[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT DISTINCT '1' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysUnitSectId, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "    FROM     HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "    HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_SECURITY.dbo.TSEPermissionEmployeeTypeUnitSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "    UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID RIGHT OUTER JOIN";
                cmdstring += Constants.vbCrLf + "     HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_MASTER.dbo.TCNMUnitSect AS S WITH(NOLOCK) ON UPT2.FNHSysUnitSectId = S.FNHSysUnitSectId";
                cmdstring += Constants.vbCrLf + "     OUTER APPLY(SELECT TOP 1  X.[FNHSysEmpTypeIdx]";
                cmdstring += Constants.vbCrLf + "       FROM @TabUnitSeclt AS X";
                cmdstring += Constants.vbCrLf + "      WHERE X.FNHSysEmpTypeIdx = UPT.FNHSysEmpTypeId";
                cmdstring += Constants.vbCrLf + "     AND X.FNHSysUnitSectId = S.FNHSysUnitSectId";
                cmdstring += Constants.vbCrLf + "      AND X.[FTUserName] = UP.FTUserName";
                cmdstring += Constants.vbCrLf + "     AND X.[FTMnuName] = UPM.FTMnuName";
                cmdstring += Constants.vbCrLf + "    )    AS Tmp";
                cmdstring += Constants.vbCrLf + "   WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "   AND(ISNULL(UPT.FTStateAllUnit, '') <> '1')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "     AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";
                cmdstring += Constants.vbCrLf + "    AND Tmp.FNHSysEmpTypeIdx IS NULL";

                cmdstring += Constants.vbCrLf + "       INSERT INTO @TabUnitSeclt(REf,[FNHSysEmpTypeIdx], FNHSysUnitSectId,[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "     SELECT  DISTINCT '1' AS Ref, UPT.FNHSysEmpTypeId, 0, UP.FTUserName, UPM.FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "    FROM     HITECH_SECURITY.dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "      HITECH_SECURITY.dbo.TSEPermissionEmployeeTypeUnitSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "      UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID";
                cmdstring += Constants.vbCrLf + "      OUTER APPLY(SELECT TOP 1  X.[FNHSysEmpTypeIdx]";
                cmdstring += Constants.vbCrLf + "           FROM @TabUnitSeclt AS X";
                cmdstring += Constants.vbCrLf + "          WHERE X.FNHSysEmpTypeIdx = UPT.FNHSysEmpTypeId";
                cmdstring += Constants.vbCrLf + "          AND X.FNHSysUnitSectId = 0";
                cmdstring += Constants.vbCrLf + "       AND X.[FTUserName] = UP.FTUserName";
                cmdstring += Constants.vbCrLf + "      AND X.[FTMnuName] = UPM.FTMnuName";
                cmdstring += Constants.vbCrLf + "    )    AS Tmp";
                cmdstring += Constants.vbCrLf + "     WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "    AND Tmp.FNHSysEmpTypeIdx IS NULL";



            }



            return cmdstring;
        }

        public static string QueryPermissionData(bool StateTypeOnly = false)
        {

            string cmdstring = " ";


            cmdstring += Constants.vbCrLf + "     Declare @TabType AS Table ( FNHSysEmpTypeIdx int, FTStateAllx varchar(1) , FTStateAllUnitx varchar(1) ,";

            cmdstring += Constants.vbCrLf + "   UNIQUE NONCLUSTERED(FNHSysEmpTypeIdx ,FTStateAllx,FTStateAllUnitx)";
            cmdstring += Constants.vbCrLf + " )";

            cmdstring += Constants.vbCrLf + "   INSERT INTO @TabType(FNHSysEmpTypeIdx,FTStateAllx,FTStateAllUnitx)";
            cmdstring += Constants.vbCrLf + " Select  UPT.FNHSysEmpTypeId,MAX(FTStateAll) AS FTStateAll,MAX(FTStateAllUnit) As FTStateAllUnit";
            cmdstring += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
            cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
            cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
            cmdstring += Constants.vbCrLf + "     INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].[dbo].[THRMEmpType] ET WITH (NOLOCK) ON ET.FNHSysEmpTypeId=UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId =" + HI.ST.SysInfo.CmpID;
            cmdstring += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
            cmdstring += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
            cmdstring += Constants.vbCrLf + " GROUP BY UPT.FNHSysEmpTypeId ";

            if (StateTypeOnly == false)
            {


                cmdstring += Constants.vbCrLf + "  Declare @TabEmp AS Table ( ";
                cmdstring += Constants.vbCrLf + "   FNHSysEmpIdx int,";               
                cmdstring += Constants.vbCrLf + "    UNIQUE NONCLUSTERED(FNHSysEmpIdx)";
                cmdstring += Constants.vbCrLf + " 	)";

                cmdstring += Constants.vbCrLf + "  Declare @TabSect AS Table (REf varchar(30), ";
                cmdstring += Constants.vbCrLf + "   FNHSysEmpTypeIdx int,";
                cmdstring += Constants.vbCrLf + "   FNHSysSectId int,";
                cmdstring += Constants.vbCrLf + "   FTUserName varchar(50),";
                cmdstring += Constants.vbCrLf + "   FTMnuName varchar(200),";
                cmdstring += Constants.vbCrLf + "   FNHSysCmpID int,";
                cmdstring += Constants.vbCrLf + "   FTStateAllUnit varchar(1),";
                cmdstring += Constants.vbCrLf + "    UNIQUE NONCLUSTERED(REf, FNHSysEmpTypeIdx, FNHSysSectId, FTUserName, FTMnuName, FNHSysCmpID,FTStateAllUnit)";
                cmdstring += Constants.vbCrLf + " 	)";

                cmdstring += Constants.vbCrLf + "     Declare @TabUnitSeclt AS Table (REf varchar(30),";
                cmdstring += Constants.vbCrLf + "    FNHSysEmpTypeIdx int,";
                cmdstring += Constants.vbCrLf + "  [FNHSysUnitSectId] int,";
                cmdstring += Constants.vbCrLf + "   FTUserName varchar(50),";
                cmdstring += Constants.vbCrLf + "   FTMnuName varchar(200),";
                cmdstring += Constants.vbCrLf + "   FNHSysCmpID int";
                cmdstring += Constants.vbCrLf + "   UNIQUE NONCLUSTERED(REf, FNHSysEmpTypeIdx,[FNHSysUnitSectId], FTUserName, FTMnuName, FNHSysCmpID)";
                cmdstring += Constants.vbCrLf + " )";


                cmdstring += Constants.vbCrLf + "   INSERT INTO @TabSect(REf,[FNHSysEmpTypeIdx],[FNHSysSectId],[FTUserName],[FTMnuName],[FNHSysCmpID],FTStateAllUnit)";

                cmdstring += Constants.vbCrLf + "    SELECT  '2' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysSectId, MAX(UP.FTUserName) As FTUserName, MAX(UPM.FTMnuName) AS FTMnuName, " + HI.ST.SysInfo.CmpID + ",MAX(UPT2.FTStateAll) As FTStateAll";
                cmdstring += Constants.vbCrLf + "     FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "              [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "              [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "              [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "    UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID  INNER JOIN";
                cmdstring += Constants.vbCrLf + "    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " INNER JOIN";
                cmdstring += Constants.vbCrLf + "    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId";
       
                cmdstring += Constants.vbCrLf + "     WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";             
                cmdstring += Constants.vbCrLf + "     AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "     AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";
                cmdstring += Constants.vbCrLf + "    GROUP BY  UPT.FNHSysEmpTypeId, S.FNHSysSectId ";


                cmdstring += Constants.vbCrLf + "    INSERT INTO @TabUnitSeclt(REf,[FNHSysEmpTypeIdx], FNHSysUnitSectId,[FTUserName],[FTMnuName],[FNHSysCmpID])";
                cmdstring += Constants.vbCrLf + "    SELECT '1' AS Ref, UPT.FNHSysEmpTypeId, S.FNHSysUnitSectId, MAX(UP.FTUserName) As FTUserName, MAX(UPM.FTMnuName) AS FTMnuName, " + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "    FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH(NOLOCK) INNER JOIN";
                cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH(NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeUnitSect AS UPT2 WITH(NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND";
                cmdstring += Constants.vbCrLf + "    UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID INNER JOIN";
                cmdstring += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH(NOLOCK) ON ET.FNHSysEmpTypeId = UPT.FNHSysEmpTypeId AND ET.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + " INNER JOIN";
                cmdstring += Constants.vbCrLf + "      [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS S WITH(NOLOCK) ON UPT2.FNHSysUnitSectId = S.FNHSysUnitSectId";      
                cmdstring += Constants.vbCrLf + "   WHERE(UP.FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";
                cmdstring += Constants.vbCrLf + "    AND(UPM.FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')";
                cmdstring += Constants.vbCrLf + "     AND(S.FNHSysCmpId = " + HI.ST.SysInfo.CmpID + ")";

                cmdstring += Constants.vbCrLf + "    GROUP BY  UPT.FNHSysEmpTypeId, S.FNHSysUnitSectId ";


                cmdstring += Constants.vbCrLf + "   INSERT INTO @TabEmp(FNHSysEmpIdx)";
                cmdstring += Constants.vbCrLf + "    select Emp.FNHSysEmpID ";
                cmdstring += Constants.vbCrLf + "    FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee AS Emp with(nolock) ";
                cmdstring += Constants.vbCrLf + "   OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FTStateAllx,X.FTStateAllUnitx FROM  @TabType  X  WHERE X.FNHSysEmpTypeIdx=Emp.FNHSysEmpTypeId ) AS perTyep     ";
                cmdstring += Constants.vbCrLf + "   OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysSectId,X.FTStateAllUnit FROM  @TabSect  X  WHERE X.FNHSysEmpTypeIdx=Emp.FNHSysEmpTypeId AND X.FNHSysSectId=Emp.FNHSysSectId  ) AS perSec     ";
                cmdstring += Constants.vbCrLf + "   OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysUnitSectId FROM  @TabUnitSeclt AS X WHERE X.FNHSysEmpTypeIdx =Emp.FNHSysEmpTypeId AND X.FNHSysUnitSectId=Emp.FNHSysUnitSectId  ) AS  perUSec ";

                cmdstring += Constants.vbCrLf + " where emp.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + "";
                cmdstring += Constants.vbCrLf + "  AND  perTyep.FNHSysEmpTypeIdx >0 ";
                cmdstring += Constants.vbCrLf + "   AND ( CASE WHEN perTyep.FTStateAllx='1' OR Emp.FNHSysSectId=0 THEN 1 ELSE (CASE WHEN  perSec.FNHSysSectId >0 THEN 1 ELSE 0 END) END =1 ) ";
                cmdstring += Constants.vbCrLf + "   AND ( CASE WHEN perTyep.FTStateAllUnitx='1' OR Emp.FNHSysUnitSectId=0 THEN 1 ELSE (CASE WHEN  perUSec.FNHSysUnitSectId >0 THEN 1 ELSE 0 END) END =1 ) ";

            }



            return cmdstring;
        }

        public static string PermissionFilterEmployee(string _StrQuery = "")
        {
            string _Str = "";
            string _Qry = "";
            string _StrJoin = "";
            

            _Qry = "  AND  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";

            if (!(HI.ST.SysInfo.Admin))
            {
                string _QrySP = "";



                _QrySP = QueryPermissionData();


                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FTStateAllx,X.FTStateAllUnitx FROM  @TabType  X  WHERE X.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId ) AS perTyep     ";
                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysSectId,X.FTStateAllUnit FROM  @TabSect  X  WHERE X.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND X.FNHSysSectId=M.FNHSysSectId  ) AS perSec     ";
                //_StrJoin += Constants.vbCrLf + " OUTER APPLY (SELECT TOP 1 X.FNHSysEmpTypeIdx,X.FNHSysUnitSectId FROM  @TabUnitSeclt AS X WHERE X.FNHSysEmpTypeIdx =M.FNHSysEmpTypeId AND X.FNHSysUnitSectId=M.FNHSysUnitSectId  ) AS  perUSec ";

                //_QrySPWhere = " perTyep.FNHSysEmpTypeIdx >0 ";
                //_QrySPWhere += Constants.vbCrLf + " AND ( CASE WHEN perTyep.FTStateAllx='1' OR M.FNHSysSectId=0 THEN 1 ELSE (CASE WHEN  perSec.FNHSysSectId >0 THEN 1 ELSE 0 END) END =1 ) ";
                //_QrySPWhere += Constants.vbCrLf + " AND ( CASE WHEN perTyep.FTStateAllUnitx='1' OR M.FNHSysUnitSectId=0 THEN 1 ELSE (CASE WHEN  perUSec.FNHSysUnitSectId >0 THEN 1 ELSE 0 END) END =1 ) ";

                //_StrJoin += Constants.vbCrLf + " INNER JOIN @TabSect  AS perSec    ON perSec.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND perSec.FNHSysSectId=M.FNHSysSectId ";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN @TabUnitSeclt  perUSec ON perUSec.FNHSysEmpTypeIdx =M.FNHSysEmpTypeId AND perUSec.FNHSysUnitSectId=M.FNHSysUnitSectId ";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN @TabType  AS perTyep ON perTyep.FNHSysEmpTypeIdx =M.FNHSysEmpTypeId  ";



                _StrJoin += Constants.vbCrLf + " INNER JOIN @TabEmp  AS perEmp    ON  M.FNHSysEmpId  =perEmp.FNHSysEmpIdx  ";
                

                //_StrJoin += Constants.vbCrLf + " ";
                //_StrJoin += Constants.vbCrLf + " INNER  JOIN (SELECT  DISTINCT FNHSysEmpTypeId AS FNHSysEmpTypeIdx,FNHSysSectId ";
                //_StrJoin += Constants.vbCrLf + " FROM    @TabSect ";
                //_StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' )) AS perSec  ";
                //_StrJoin += Constants.vbCrLf + " ON perSec.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND perSec.FNHSysSectId=M.FNHSysSectId ";
                //_StrJoin += Constants.vbCrLf + "";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN (SELECT DISTINCT  FNHSysEmpTypeId AS FNHSysEmpTypeIdx ,FNHSysUnitSectId ";
                //_StrJoin += Constants.vbCrLf + " FROM   @TabUnitSeclt ";
                //_StrJoin += Constants.vbCrLf + " WHERE (FTUserName = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ) AND (FTMnuName = '" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "')) AS perUSec ";
                //_StrJoin += Constants.vbCrLf + " ON perUSec.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId AND perUSec.FNHSysUnitSectId=M.FNHSysUnitSectId ";
                //_StrJoin += Constants.vbCrLf + " ";
                //_StrJoin += Constants.vbCrLf + " INNER JOIN  (SELECT FNHSysEmpTypeId AS FNHSysEmpTypeIdx FROM @TabType) AS perTyep ";
                //_StrJoin += Constants.vbCrLf + " ON perTyep.FNHSysEmpTypeIdx=M.FNHSysEmpTypeId  ";
                //_StrJoin += Constants.vbCrLf + " ";




                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = _StrJoin + "  WHERE  " + _Qry;
                }
                else
                {
                    
                        int n = 0;
                        n = _StrQuery.ToLower().LastIndexOf("where");

                    if (n > 5)
                    {
                        string cmd = _StrQuery.Substring(0, n);

                        int Indx1 = cmd.ToLower().LastIndexOf("(");
                        int Indx2 = cmd.ToLower().LastIndexOf(")");

                        if (Indx2 > Indx1)
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + cmd + _StrJoin + _StrQuery.Substring(n, _StrQuery.Length - n) + _Qry ;
                        }
                        else
                        {
                            _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin ;
                        }
                    }
                    else
                    {
                        _Str = _QrySP + Constants.vbCrLf + " " + _StrQuery + _StrJoin  ;
                    }

                }
            }
            else
            {
                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = "  WHERE  " + _Qry;
                }
                else
                {
                    _Str = _StrQuery + "   " + _Qry;
                }
            }
            return _Str;
        }

        public static string PermissionFilterEmployeeSalary()
        {

            string _Qry = "";

            _Qry = "  AND  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";
            if (!(HI.ST.SysInfo.Admin))
            {

                _Qry += Constants.vbCrLf + "  AND  M.FNHSysEmpTypeId IN (";
                _Qry += Constants.vbCrLf + " Select DISTINCT UPT.FNHSysEmpTypeId";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  )      ";

                _Qry += Constants.vbCrLf + " AND Convert(Varchar(30),M.FNHSysEmpTypeId) +'|'+ Convert(Varchar(30),M.FNHSysSectId) IN ( ";
                _Qry += Constants.vbCrLf + " Select DISTINCT Convert(Varchar(30),UPT.FNHSysEmpTypeId) + '|' + Convert(Varchar(30),S.FNHSysSectId)";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Qry += Constants.vbCrLf + "   CROSS JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S  WITH(NOLOCK) ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'')='1' ";
                _Qry += Constants.vbCrLf + " UNION";
                _Qry += Constants.vbCrLf + " Select DISTINCT Convert(Varchar(30),UPT.FNHSysEmpTypeId) + '|' + Convert(Varchar(30),S.FNHSysSectId)";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID  INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "   INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S   WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <>'1' ";
                _Qry += Constants.vbCrLf + "  )      ";


                _Qry += Constants.vbCrLf + " AND Convert(Varchar(30),M.FNHSysEmpTypeId) +'|'+ Convert(Varchar(30),M.FNHSysUnitSectId)  IN ( ";
                _Qry += Constants.vbCrLf + " Select DISTINCT  Convert(Varchar(30),UPT.FNHSysEmpTypeId) + '|' + Convert(Varchar(30),S.FNHSysUnitSectId) ";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Qry += Constants.vbCrLf + "   CROSS JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS S  WITH(NOLOCK)";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAllUnit,'')='1' ";
                _Qry += Constants.vbCrLf + " UNION";
                _Qry += Constants.vbCrLf + " Select DISTINCT Convert(Varchar(30),UPT.FNHSysEmpTypeId) + '|' + Convert(Varchar(30),S.FNHSysUnitSectId)";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID  INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "  CROSS JOIN    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS S WITH(NOLOCK) ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <>'1' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT2.FTStateAll,'')='1' ";
                _Qry += Constants.vbCrLf + " UNION";
                _Qry += Constants.vbCrLf + " Select DISTINCT Convert(Varchar(30),UPT.FNHSysEmpTypeId) + '|' + Convert(Varchar(30),S.FNHSysUnitSectId) ";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID  INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeUnitSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "   INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS S WITH(NOLOCK) ON UPT2.FNHSysUnitSectId = S.FNHSysUnitSectId  ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAllUnit,'') <>'1' ";
                _Qry += Constants.vbCrLf + "  )      ";

            }

            return _Qry;
        }

        public static string PermissionEmpSalary(string _StrQuery, string _AriasName = "")
        {
            string _Str = "";



            if (!(HI.ST.SysInfo.Admin))
            {
                _Str = "   FNHSysEmpTypeId IN (";
                _Str += Constants.vbCrLf + " Select DISTINCT UPT.FNHSysEmpTypeId";
                _Str += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Str += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Str += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Str += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Str += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'  AND  ISNULL(UPT.FTStateSalary,'1') ";
                _Str += Constants.vbCrLf + "  )      ";

                if (string.IsNullOrEmpty(_StrQuery))
                {
                    _Str = "  WHERE  " + _Str;
                }
                else
                {
                    _Str = _StrQuery + "  AND  " + _Str;
                }

            }
            else
            {
                _Str = _StrQuery;
            }

            return _Str;

        }

        public static void CreateTempEmpMaster(object Condition)
        {
            //If (HI.ST.SysInfo.Admin) Then
            //    Exit Sub
            //End If 


            string _Qry = "";
            string _Formular = "";
            string tText = "";
            try
            {
                tText = Condition.GetType().GetProperty("GetCriteria").GetValue(Condition, null).ToString().Trim();
            }
            catch (Exception ex)
            {
            }

            if (!string.IsNullOrEmpty(tText))
            {
                _Formular += (!string.IsNullOrEmpty(_Formular.Trim()) ? " AND " : "");
                _Formular += "" + tText;
            }

            // _Qry = "Select ";

            _Qry = " DELETE FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee_TmpReport WHERE FTUserLogIn='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
            _Qry += Constants.vbCrLf + "  INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee_TmpReport(FTUserLogIn, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH, ";
            _Qry += Constants.vbCrLf + "   FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,";
            _Qry += Constants.vbCrLf + "   FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,";
            _Qry += Constants.vbCrLf + "   FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,";
            _Qry += Constants.vbCrLf + "   FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,";
            _Qry += Constants.vbCrLf + "   FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,";
            _Qry += Constants.vbCrLf + "   FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,";
            _Qry += Constants.vbCrLf + "   FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNSalary, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,";
            _Qry += Constants.vbCrLf + "   FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,";
            _Qry += Constants.vbCrLf + "   FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,";
            _Qry += Constants.vbCrLf + "   FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FCModFather, FCModMother, FCModMateFather, FCModMateMother,";
            _Qry += Constants.vbCrLf + "   FCPremium, FCInterest, FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDeductDonateStudy, FCDeductDividend, FCDisabledDependents, FCHealthInsurFatherMotherMate, FTHealthInsurIDFather,";
            _Qry += Constants.vbCrLf + "   FTHealthInsurIDMother, FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FCIncomeBefore, FCTaxBefore, FCSocialBefore, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver,";
            _Qry += Constants.vbCrLf + "   FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode, FTWorkAgeTH, FTWorkAgeEN, FTEmpAgeTH , FTEmpAgeEN )";


            _Qry += Constants.vbCrLf + " SELECT   '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AS  FTUserLogIn,";

            _Qry += Constants.vbCrLf + " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, ";
            _Qry += Constants.vbCrLf + "  FTEmpSurnameTH, FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID,";
            _Qry += Constants.vbCrLf + "  FNScanCtrl, FDDateStart, FDDateEnd, FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId,";
            _Qry += Constants.vbCrLf + "  FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader, FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId,";
            _Qry += Constants.vbCrLf + "  FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign, FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo,";
            _Qry += Constants.vbCrLf + "  FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus, FTCarId, FTCarLicense, FNMotorCycleStatus,";
            _Qry += Constants.vbCrLf + "  FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi, FTAddrRoad,";
            _Qry += Constants.vbCrLf + "  FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1,";
            _Qry += Constants.vbCrLf + "  FTAddrProvince1, FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail";

            _Qry += Constants.vbCrLf + " , FNSalary ";

            _Qry += Constants.vbCrLf + " , FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel,";
            _Qry += Constants.vbCrLf + "  FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1, FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife,";
            _Qry += Constants.vbCrLf + "  FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel, FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer,";
            _Qry += Constants.vbCrLf + "  FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr, FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel,";
            _Qry += Constants.vbCrLf + "  FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FCModFather, FCModMother, FCModMateFather, FCModMateMother, FCPremium, FCInterest,";
            _Qry += Constants.vbCrLf + "  FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDeductDonateStudy, FCDeductDividend, FCDisabledDependents, FCHealthInsurFatherMotherMate, FTHealthInsurIDFather,";
            _Qry += Constants.vbCrLf + "  FTHealthInsurIDMother, FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FCIncomeBefore, FCTaxBefore, FCSocialBefore, FTFundIDNo, FDFundBegin, FDFundEnd,";
            _Qry += Constants.vbCrLf + "  FCExceptAgeOver, FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode";


            _Qry += Constants.vbCrLf + " ,Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' เดือน ' + convert(nvarchar(30), A.FNMonthWorkAgeDay) +' วัน' AS FTWorkAgeTH ";
            _Qry += Constants.vbCrLf + " ,Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' Month ' + convert(nvarchar(30) , A.FNMonthWorkAgeDay) +' Day'  AS FTWorkAgeEN ";
            _Qry += Constants.vbCrLf + " ,Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' เดือน' AS FTEmpAgeTH ";
            _Qry += Constants.vbCrLf + "  ,Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' Month' AS FTEmpAgeEN ";




            _Qry += Constants.vbCrLf + "  FROM ( ";



            _Qry += Constants.vbCrLf + " SELECT M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, M.FNHSysPreNameId, M.FTEmpNameTH, ";
            _Qry += Constants.vbCrLf + "  M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN, M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID,";
            _Qry += Constants.vbCrLf + "  M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId, M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId,";
            _Qry += Constants.vbCrLf + "  M.FNHSysSectId, M.FNHSysUnitSectId, M.FNHSysPositId, M.FNHSysEmpIDLeader, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight, M.FCHeight, M.FNHSysRaceId,";
            _Qry += Constants.vbCrLf + "  M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign, M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo,";
            _Qry += Constants.vbCrLf + "  M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta, M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,";
            _Qry += Constants.vbCrLf + "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad,";
            _Qry += Constants.vbCrLf + "  M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1,";
            _Qry += Constants.vbCrLf + "  M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1, M.FTMobile, M.FTEmail";

            _Qry += Constants.vbCrLf + " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE CASE WHEN ES.FTStateSalary = '1' THEN M.FNSalary ELSE 0 END END AS FNSalary ";

            _Qry += Constants.vbCrLf + " , M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,";
            _Qry += Constants.vbCrLf + "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1, M.FTRefNote1, M.FTFatherName, M.FNFatherLife,";
            _Qry += Constants.vbCrLf + "  M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel, M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer,";
            _Qry += Constants.vbCrLf + "  M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName, M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel,";
            _Qry += Constants.vbCrLf + "  M.FTMateFatherName, M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium, M.FCInterest,";
            _Qry += Constants.vbCrLf + "  M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,";
            _Qry += Constants.vbCrLf + "  M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate, M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd,";
            _Qry += Constants.vbCrLf + "  M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire, M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode";

            _Qry += Constants.vbCrLf + "  , [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAge";
            _Qry += Constants.vbCrLf + "  , [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.FN_Get_Emp_WorkAge_Day(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAgeDay";
            _Qry += Constants.vbCrLf + "  , [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.FN_Get_Emp_Age(M.FDBirthDate) AS FNMonthEmpAge";

            _Qry += Constants.vbCrLf + " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee AS M WITH(NOLOCK) ";
            _Qry += Constants.vbCrLf + "  LEFT OUTER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId";

            _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
            _Qry += Constants.vbCrLf + " (";

            if ((HI.ST.SysInfo.Admin))
            {
                _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysEmpTypeId , '1' as FTStateSalary FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType WITH(NOLOCK)  ";
            }
            else
            {
                _Qry += Constants.vbCrLf + " SELECT   DISTINCT     RT.FNHSysEmpTypeId  , max(FTStateSalary)over ( partition by FNHSysEmpTypeId) as FTStateSalary";
                _Qry += Constants.vbCrLf + "  FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS RT WITH(NOLOCK)   INNER JOIN";
                _Qry += Constants.vbCrLf + "           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS U WITH(NOLOCK)   ON RT.FNHSysPermissionID = U.FNHSysPermissionID ";
                //_Qry += Constants.vbCrLf + "  WHERE U.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + "  WHERE U.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + " ";
            }

            _Qry += Constants.vbCrLf + " )  As ES ON M.FNHSysEmpTypeId = ES.FNHSysEmpTypeId ";
            _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
            _Qry += Constants.vbCrLf + "   (";

            if ((HI.ST.SysInfo.Admin))
            {
                _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysSectId  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect WITH(NOLOCK)   ";

            }
            else
            {
                _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Qry += Constants.vbCrLf + "   CROSS JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S  WITH(NOLOCK)";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                //_Qry += Constants.vbCrLf + "  AND UPT.FTStateAll='1' AND UPT.FTStateSalary='1'    ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateAll='1'     ";
                _Qry += Constants.vbCrLf + " UNION";
                _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN ";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "   INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";
                //_Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <> '1' AND UPT.FTStateSalary='1'   ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <> '1'    ";

            }


            //if ((!HI.ST.SysInfo.Admin))
            //{
            //    _Qry += Constants.vbCrLf + "LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.tmpPermissionTCNMSect TMPS WITH (NOLOCK) ON TMPS.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND TMPS.FNHSysSectId=M.FNHSysSectId AND TMPS.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND TMPS.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";
            //    _Qry += Constants.vbCrLf + "LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.tmpPermissionTCNMUnitSect TMPUS WITH (NOLOCK) ON TMPUS.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND TMPUS.FNHSysUnitSectId=M.FNHSysUnitSectId AND TMPUS.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND TMPS.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";

            //}

            _Qry += Constants.vbCrLf + ")  As LS ON M.FNHSysSectId = LS.FNHSysSectId ";
            _Qry += Constants.vbCrLf + " WHERE M.FTEmpCode<>'' ";

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry);

            _Qry += Constants.vbCrLf + "  AND  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";
            _Qry += Constants.vbCrLf + "  AND  M.FNHSysEmpID<> 0 ";

            if (!string.IsNullOrEmpty(_Formular))
            {
                _Qry += Constants.vbCrLf + "  AND ( " + _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") + " ) ";
            }
            _Qry += Constants.vbCrLf + " ) A";
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR);
        }


        //public static void CreateTempEmpMaster(object Condition)
        //{
        //    //If (HI.ST.SysInfo.Admin) Then
        //    //    Exit Sub
        //    //End If 


        //    string _Qry = "";
        //    string _Formular = "";
        //    string tText = "";
        //    try
        //    {
        //        tText = Condition.GetType().GetProperty("GetCriteria").GetValue(Condition, null).ToString().Trim();
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    if (!string.IsNullOrEmpty(tText))
        //    {
        //        _Formular += (!string.IsNullOrEmpty(_Formular.Trim()) ? " AND " : "");
        //        _Formular += "" + tText;
        //    }

        //    // _Qry = "Select ";

        //    _Qry = " DELETE FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee_TmpReport WHERE FTUserLogIn='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
        //    _Qry += Constants.vbCrLf + "  INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee_TmpReport(FTUserLogIn, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH, ";
        //    _Qry += Constants.vbCrLf + "   FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,";
        //    _Qry += Constants.vbCrLf + "   FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,";
        //    _Qry += Constants.vbCrLf + "   FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,";
        //    _Qry += Constants.vbCrLf + "   FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,";
        //    _Qry += Constants.vbCrLf + "   FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,";
        //    _Qry += Constants.vbCrLf + "   FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,";
        //    _Qry += Constants.vbCrLf + "   FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNSalary, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,";
        //    _Qry += Constants.vbCrLf + "   FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,";
        //    _Qry += Constants.vbCrLf + "   FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,";
        //    _Qry += Constants.vbCrLf + "   FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FCModFather, FCModMother, FCModMateFather, FCModMateMother,";
        //    _Qry += Constants.vbCrLf + "   FCPremium, FCInterest, FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDeductDonateStudy, FCDeductDividend, FCDisabledDependents, FCHealthInsurFatherMotherMate, FTHealthInsurIDFather,";
        //    _Qry += Constants.vbCrLf + "   FTHealthInsurIDMother, FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FCIncomeBefore, FCTaxBefore, FCSocialBefore, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver,";
        //    _Qry += Constants.vbCrLf + "   FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode )";
        //    _Qry += Constants.vbCrLf + " SELECT   '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AS  FTUserLogIn,";
        //    _Qry += Constants.vbCrLf + " M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, M.FNHSysPreNameId, M.FTEmpNameTH, ";
        //    _Qry += Constants.vbCrLf + "  M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN, M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID,";
        //    _Qry += Constants.vbCrLf + "  M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId, M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId,";
        //    _Qry += Constants.vbCrLf + "  M.FNHSysSectId, M.FNHSysUnitSectId, M.FNHSysPositId, M.FNHSysEmpIDLeader, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight, M.FCHeight, M.FNHSysRaceId,";
        //    _Qry += Constants.vbCrLf + "  M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign, M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo,";
        //    _Qry += Constants.vbCrLf + "  M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta, M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,";
        //    _Qry += Constants.vbCrLf + "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad,";
        //    _Qry += Constants.vbCrLf + "  M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1,";
        //    _Qry += Constants.vbCrLf + "  M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1, M.FTMobile, M.FTEmail";

        //    _Qry += Constants.vbCrLf + " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE CASE WHEN ES.FTStateSalary = '1' THEN M.FNSalary ELSE 0 END END AS FNSalary ";

        //    _Qry += Constants.vbCrLf + " , M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,";
        //    _Qry += Constants.vbCrLf + "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1, M.FTRefNote1, M.FTFatherName, M.FNFatherLife,";
        //    _Qry += Constants.vbCrLf + "  M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel, M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer,";
        //    _Qry += Constants.vbCrLf + "  M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName, M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel,";
        //    _Qry += Constants.vbCrLf + "  M.FTMateFatherName, M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium, M.FCInterest,";
        //    _Qry += Constants.vbCrLf + "  M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,";
        //    _Qry += Constants.vbCrLf + "  M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate, M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd,";
        //    _Qry += Constants.vbCrLf + "  M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire, M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode";
        //    _Qry += Constants.vbCrLf + " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee AS M WITH(NOLOCK) ";
        //    _Qry += Constants.vbCrLf + "  LEFT OUTER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN";
        //    _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN";
        //    _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN";
        //    _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN";
        //    _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId";

        //    _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
        //    _Qry += Constants.vbCrLf + " (";

        //    if ((HI.ST.SysInfo.Admin))
        //    {
        //        _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysEmpTypeId , '1' as FTStateSalary FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType WITH(NOLOCK)  ";
        //    }
        //    else
        //    {
        //        _Qry += Constants.vbCrLf + " SELECT   DISTINCT     RT.FNHSysEmpTypeId  , max(FTStateSalary)over ( partition by FNHSysEmpTypeId) as FTStateSalary";
        //        _Qry += Constants.vbCrLf + "  FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS RT WITH(NOLOCK)   INNER JOIN";
        //        _Qry += Constants.vbCrLf + "           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS U WITH(NOLOCK)   ON RT.FNHSysPermissionID = U.FNHSysPermissionID ";
        //        //_Qry += Constants.vbCrLf + "  WHERE U.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND FTStateSalary='1' ";
        //        _Qry += Constants.vbCrLf + "  WHERE U.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
        //        _Qry += Constants.vbCrLf + " ";
        //    }

        //    _Qry += Constants.vbCrLf + " )  As ES ON M.FNHSysEmpTypeId = ES.FNHSysEmpTypeId ";
        //    _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
        //    _Qry += Constants.vbCrLf + "   (";

        //    if ((HI.ST.SysInfo.Admin))
        //    {
        //        _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysSectId  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect WITH(NOLOCK)   ";

        //    }
        //    else
        //    {
        //        _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
        //        _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
        //        _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
        //        _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
        //        _Qry += Constants.vbCrLf + "   CROSS JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S  WITH(NOLOCK)";
        //        _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
        //        _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
        //        //_Qry += Constants.vbCrLf + "  AND UPT.FTStateAll='1' AND UPT.FTStateSalary='1'    ";
        //        _Qry += Constants.vbCrLf + "  AND UPT.FTStateAll='1'     ";
        //        _Qry += Constants.vbCrLf + " UNION";
        //        _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
        //        _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
        //        _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
        //        _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN ";
        //        _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
        //        _Qry += Constants.vbCrLf + "   INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  ";
        //        _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
        //        _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";
        //        //_Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <> '1' AND UPT.FTStateSalary='1'   ";
        //        _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <> '1'    ";

        //    }


        //    //if ((!HI.ST.SysInfo.Admin))
        //    //{
        //    //    _Qry += Constants.vbCrLf + "LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.tmpPermissionTCNMSect TMPS WITH (NOLOCK) ON TMPS.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND TMPS.FNHSysSectId=M.FNHSysSectId AND TMPS.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND TMPS.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";
        //    //    _Qry += Constants.vbCrLf + "LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.tmpPermissionTCNMUnitSect TMPUS WITH (NOLOCK) ON TMPUS.FNHSysEmpTypeId=M.FNHSysEmpTypeId AND TMPUS.FNHSysUnitSectId=M.FNHSysUnitSectId AND TMPUS.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND TMPS.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";

        //    //}

        //    _Qry += Constants.vbCrLf + ")  As LS ON M.FNHSysSectId = LS.FNHSysSectId ";
        //    _Qry += Constants.vbCrLf + " WHERE M.FTEmpCode<>'' ";

        //    _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry);

        //    _Qry += Constants.vbCrLf + "  AND  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";
        //    _Qry += Constants.vbCrLf + "  AND  M.FNHSysEmpID<> 0 ";

        //    if (!string.IsNullOrEmpty(_Formular))
        //    {
        //        _Qry += Constants.vbCrLf + "  AND ( " + _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") + " ) ";
        //    }

        //    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR);
        //}



        public static void CreateTempPayroll(object Condition, string FTPayYear, string FTPayTerm = null)
        {

            string _Qry = "";
            string _Formular = "";
            string tText = "";

            try
            {
                tText = Condition.GetType().GetProperty("GetCriteria").GetValue(Condition, null).ToString().Trim();
            }
            catch (Exception ex)
            {
            }

            if (!string.IsNullOrEmpty(tText))
            {
                _Formular += (!string.IsNullOrEmpty(_Formular.Trim()) ? " AND " : "");
                _Formular += "" + tText;
            }

            _Qry = " DELETE FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRTPayRoll_TmpReport WHERE FTUserLogIn='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
            _Qry += Constants.vbCrLf + "  INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRTPayRoll_TmpReport(FTUserLogIn,  FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTPayYear, FTPayTerm, FNHSysEmpID, ";
            _Qry += Constants.vbCrLf + "      FTEmpIdNo, FNHSysEmpTypeId, FTPayDate, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId,  ";
            _Qry += Constants.vbCrLf + "      FNHSysPayRollPayId, FNHSysBankId, FNHSysBankBranchId, FTAccNo, FNHoliday, FNSalary, FNWorkingHour,  FNOt1, FNOt15, FNOt2, FNOt3, ";
            _Qry += Constants.vbCrLf + "     FNOt4, FNTotalLeavePay, FNTotalLeaveNotPay, FNTotalLeave, FNTotalWKNMin,  FNOt1Min, FNOt15Min, FNOt2Min, FNOt3Min, FNOt4Min, ";
            _Qry += Constants.vbCrLf + "    FNTotalLateMin, FNLateCutMin, FNLateCutAbsentMin, FNAbsentMin, FNTotalWKMin, FNTotalLeavePayMin, FNTotalLeaveNotPayMin, FNTotalLeaveMin, FCBaht, ";
            _Qry += Constants.vbCrLf + "     FCOt1_Baht, FCOt15_Baht, FCOt2_Baht, FCOt3_Baht, FCOt4_Baht, FCNetBaht, FNDiligentBaht, FNPayLeaveVacationBaht, ";
            _Qry += Constants.vbCrLf + "   FNPayLeaveOtherBaht, FNLateCutAmt, FNLateCutAbsentAmt, FNAbsentAmt, FNTotalRecalSSO, FNTotalRecalTAX, FNTotalAdd, FNTotalAddOther, FNTotalExpense, ";
            _Qry += Constants.vbCrLf + "    FNTotalExpenseOther, FNTotalIncome, FNSocial, FNTax, FHolidayBaht, FNNetpay, FNAccumulateIncomeYear, FNAccumulateSocialYear, FNAccumulateTax, ";
            _Qry += Constants.vbCrLf + "    FTStateInDustin, FNTotalCalContributedAmt, FNContributedAmt, FNCmpContributedAmt, FNTotalCalWorkmen, FNWorkmenAmt, ";
            _Qry += Constants.vbCrLf + "    FNAmtRetire,   FNPayLeaveSSo, ";
            _Qry += Constants.vbCrLf + "    FNWorkingDay, FNAdjBeforeCal, FNIncentiveAmt,FNNetpayOrg";
            _Qry += Constants.vbCrLf + ", FNAttandanceAmt, FNHealtCareAmt, ";
            _Qry += Constants.vbCrLf + " FNTransportAmt, FNChildCareAmt, FNOTMealAmt, FNSocialBase, FNWorkAgeSalary, FNOTMealAmtUS, FNExchangeRate, FNSickLeaveBaht, FNSickLeaveMin, FNBusinessLeaveBaht, FNBusinessLeaveMin,";
            _Qry += Constants.vbCrLf + " FNSpecialLeaveBaht, FNSpecialLeaveMin, FNParturitionLeaveBaht, FNParturitionLeaveMin, FNVacationRetMin, FNVacationRetAmt, FTStateCalSocial, FTStateCalTax, FNTotalIncomeDiff, FNNetpayDiff";

            _Qry += Constants.vbCrLf + "  , FNTaxExchangeRate, FNExchangeRateTHB, FNWorkDay, FNSkillRate, FNHarmfulRate, FNBasicSalaries, FNSocialInsuranceEmployee";
            _Qry += Constants.vbCrLf + "  , FNSocialInsuranceEmployer, FNHealthInsuranceEmployee, FNHealthInsuranceEmployer, FNUnemploymentInsuranceEmployee, FNUnemploymentInsuranceEmployer";
            _Qry += Constants.vbCrLf + "  , FNUnionInsuranceEmployee, FNUnionInsuranceEmployer, FNBusinessWorkday, FNSkillBaht, FNHarmfulBaht, FNPayRestOTBaht,  FNPayRestOTMin ,FNSocialExchangeRate";
            _Qry += Constants.vbCrLf + "  , FNTotalRecalPensionScheme,FNPensionSchemeRateEmp,FNPensionSchemeRateCmp,FNPensionScheme,FNPensionSchemeCmp ";

            _Qry += Constants.vbCrLf + ")";
            _Qry += Constants.vbCrLf + "  SELECT  '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AS  FTUserLogIn,";
            _Qry += Constants.vbCrLf + "   P.FTInsUser, P.FTInsDate, P.FTInsTime, P.FTUpdUser, P.FTUpdDate, P.FTUpdTime, P.FTPayYear, P.FTPayTerm, P.FNHSysEmpID,";
            _Qry += Constants.vbCrLf + "  P.FTEmpIdNo, P.FNHSysEmpTypeId, P.FTPayDate, P.FNHSysDeptId, P.FNHSysDivisonId, P.FNHSysSectId, P.FNHSysUnitSectId, P.FNHSysPositId, ";
            _Qry += Constants.vbCrLf + "  P.FNHSysPayRollPayId, P.FNHSysBankId, P.FNHSysBankBranchId, P.FTAccNo, P.FNHoliday";
            _Qry += Constants.vbCrLf + " ,  CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNSalary END AS FNSalary,";
            _Qry += Constants.vbCrLf + " P.FNWorkingHour,  P.FNOt1, P.FNOt15, P.FNOt2, P.FNOt3, ";
            _Qry += Constants.vbCrLf + "  P.FNOt4, P.FNTotalLeavePay, P.FNTotalLeaveNotPay, P.FNTotalLeave, P.FNTotalWKNMin,  P.FNOt1Min, P.FNOt15Min, P.FNOt2Min, P.FNOt3Min, P.FNOt4Min,";
            _Qry += Constants.vbCrLf + "  P.FNTotalLateMin, P.FNLateCutMin, P.FNLateCutAbsentMin, P.FNAbsentMin, P.FNTotalWKMin, P.FNTotalLeavePayMin, P.FNTotalLeaveNotPayMin, P.FNTotalLeaveMin";
            _Qry += Constants.vbCrLf + " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCBaht END AS  FCBaht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCOt1_Baht END AS  FCOt1_Baht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCOt15_Baht END AS   FCOt15_Baht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCOt2_Baht END AS   FCOt2_Baht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCOt3_Baht END AS   FCOt3_Baht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCOt4_Baht END AS   FCOt4_Baht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FCNetBaht END AS  FCNetBaht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNDiligentBaht END AS   FNDiligentBaht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNPayLeaveVacationBaht END AS  FNPayLeaveVacationBaht, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNPayLeaveOtherBaht END AS    FNPayLeaveOtherBaht,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNLateCutAmt END AS  FNLateCutAmt, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNLateCutAbsentAmt END AS   FNLateCutAbsentAmt,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAbsentAmt END AS  FNAbsentAmt, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalRecalSSO END AS  FNTotalRecalSSO, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE 500 END AS  FNTotalRecalTAX, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalAdd END AS  FNTotalAdd2,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalAddOther END AS   FNTotalAddOther2,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalExpense END AS FNTotalExpense2, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalExpenseOther END AS FNTotalExpenseOther2,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalIncome END AS FNTotalIncome2,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNSocial END AS FNSocial,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTax END AS FNTax,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FHolidayBaht END AS FHolidayBaht2, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNNetpay END AS FNNetpay2,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAccumulateIncomeYear END AS  FNAccumulateIncomeYear, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAccumulateSocialYear END AS  FNAccumulateSocialYear, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAccumulateTax END AS  FNAccumulateTax, ";
            _Qry += Constants.vbCrLf + " P.FTStateInDustin  AS   FTStateInDustin, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalCalContributedAmt END AS  FNTotalCalContributedAmt,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNContributedAmt END AS  FNContributedAmt, ";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNCmpContributedAmt END AS  FNCmpContributedAmt,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNTotalCalWorkmen END AS  FNTotalCalWorkmen,";
            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNWorkmenAmt END AS  FNWorkmenAmt, ";

            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAmtRetire END AS   FNAmtRetire,";

            _Qry += Constants.vbCrLf + " CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNPayLeaveSSo END AS  FNPayLeaveSSo, ";
            _Qry += Constants.vbCrLf + "   FNWorkingDay";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNAdjBeforeCal END AS  FNAdjBeforeCal2";
            _Qry += Constants.vbCrLf + " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNIncentiveAmt END AS  FNIncentiveAmt ";
            _Qry += Constants.vbCrLf + " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNNetpayOrg END AS  FNNetpayOrg ";
            _Qry += Constants.vbCrLf + " ,FNAttandanceAmt";
            _Qry += Constants.vbCrLf + ", FNHealtCareAmt ";
            _Qry += Constants.vbCrLf + " ,FNTransportAmt";
            _Qry += Constants.vbCrLf + ", FNChildCareAmt";
            _Qry += Constants.vbCrLf + ", FNOTMealAmt";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNSocialBase END AS FNSocialBase";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNWorkAgeSalary END AS FNWorkAgeSalary";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNOTMealAmtUS END AS FNOTMealAmtUS";
            _Qry += Constants.vbCrLf + ", FNExchangeRate";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNSickLeaveBaht END AS FNSickLeaveBaht";
            _Qry += Constants.vbCrLf + ", FNSickLeaveMin";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNBusinessLeaveBaht END AS FNBusinessLeaveBaht";
            _Qry += Constants.vbCrLf + ", FNBusinessLeaveMin";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNSpecialLeaveBaht END AS FNSpecialLeaveBaht";
            _Qry += Constants.vbCrLf + ", FNSpecialLeaveMin";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNParturitionLeaveBaht END AS FNParturitionLeaveBaht";
            _Qry += Constants.vbCrLf + ", FNParturitionLeaveMin";
            _Qry += Constants.vbCrLf + ", FNVacationRetMin";
            _Qry += Constants.vbCrLf + ",CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE P.FNVacationRetAmt END AS FNVacationRetAmt";
            _Qry += Constants.vbCrLf + ", P.FTStateCalSocial, P.FTStateCalTax, P.FNTotalIncomeDiff, P.FNNetpayDiff";

            _Qry += Constants.vbCrLf + "  , P.FNTaxExchangeRate , P.FNExchangeRateTHB, P.FNWorkDay, P.FNSkillRate, P.FNHarmfulRate";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNBasicSalaries END AS FNBasicSalaries";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNSocialInsuranceEmployee END AS FNSocialInsuranceEmployee ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNSocialInsuranceEmployer END AS FNSocialInsuranceEmployer";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNHealthInsuranceEmployee END AS FNHealthInsuranceEmployee";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNHealthInsuranceEmployer END AS FNHealthInsuranceEmployer ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNUnemploymentInsuranceEmployee END AS FNUnemploymentInsuranceEmployee ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNUnemploymentInsuranceEmployer END AS FNUnemploymentInsuranceEmployer";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNUnionInsuranceEmployee END AS FNUnionInsuranceEmployee ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNUnionInsuranceEmployer END AS FNUnionInsuranceEmployer ";
            _Qry += Constants.vbCrLf + "  , P.FNBusinessWorkday";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNSkillBaht END AS FNSkillBaht ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNHarmfulBaht END AS FNHarmfulBaht ";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPayRestOTBaht END AS  FNPayRestOTBaht";
            _Qry += Constants.vbCrLf + "  , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPayRestOTMin END AS FNPayRestOTMin";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNSocialExchangeRate END AS FNSocialExchangeRate  ";

            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNTotalRecalPensionScheme END AS FNTotalRecalPensionScheme  ";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPensionSchemeRateEmp END AS FNPensionSchemeRateEmp  ";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPensionSchemeRateCmp END AS FNPensionSchemeRateCmp  ";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPensionScheme END AS FNPensionScheme  ";
            _Qry += Constants.vbCrLf + "   , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE  P.FNPensionSchemeCmp END AS FNPensionSchemeCmp  ";



            //, FNVacationRetMin, FNVacationRetAmt
            _Qry += Constants.vbCrLf + " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee AS M WITH(NOLOCK) ";
            _Qry += Constants.vbCrLf + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRTPayRoll AS P WITH(NOLOCK) ";
            _Qry += Constants.vbCrLf + " ON M.FNHSysEmpID=P.FNHSysEmpID";
            _Qry += Constants.vbCrLf + "  LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN";
            _Qry += Constants.vbCrLf + " [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId";
            _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
            _Qry += Constants.vbCrLf + " (";

            if ((HI.ST.SysInfo.Admin))
            {
                _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysEmpTypeId FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType WITH(NOLOCK)  ";
            }
            else
            {
                _Qry += Constants.vbCrLf + " SELECT     DISTINCT   RT.FNHSysEmpTypeId";
                _Qry += Constants.vbCrLf + "  FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS RT WITH(NOLOCK)   INNER JOIN";
                _Qry += Constants.vbCrLf + "           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS U WITH(NOLOCK)   ON RT.FNHSysPermissionID = U.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "  WHERE U.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND FTStateSalary='1' ";
                _Qry += Constants.vbCrLf + " ";
            }

            _Qry += Constants.vbCrLf + " )  As ES ON M.FNHSysEmpTypeId = ES.FNHSysEmpTypeId ";
            _Qry += Constants.vbCrLf + " LEFT OUTER JOIN ";
            _Qry += Constants.vbCrLf + "   (";

            if ((HI.ST.SysInfo.Admin))
            {
                _Qry += Constants.vbCrLf + " SELECT DISTINCT FNHSysSectId  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect WITH(NOLOCK)   ";

            }
            else
            {
                _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID";
                _Qry += Constants.vbCrLf + "   CROSS JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S  WITH(NOLOCK)";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPT.FTStateAll='1' AND UPT.FTStateSalary='1'    ";
                _Qry += Constants.vbCrLf + " UNION";
                _Qry += Constants.vbCrLf + " Select DISTINCT S.FNHSysSectId";
                _Qry += Constants.vbCrLf + " FROM             [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN ";
                _Qry += Constants.vbCrLf + "     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID ";
                _Qry += Constants.vbCrLf + "   INNER JOIN   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  ";
                _Qry += Constants.vbCrLf + "  WHERE UP.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _Qry += Constants.vbCrLf + "  AND UPM.FTMnuName='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "'   ";
                _Qry += Constants.vbCrLf + "  AND ISNULL(UPT.FTStateAll,'') <> '1' AND UPT.FTStateSalary='1'   ";

            }

            _Qry += Constants.vbCrLf + ")  As LS ON M.FNHSysSectId = LS.FNHSysSectId ";
            _Qry += Constants.vbCrLf + " WHERE M.FNHSysEmpID<> 0  ";
            _Qry += Constants.vbCrLf + "  AND  M.FNHSysCmpId=" + HI.ST.SysInfo.CmpID + " ";

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry);

            if (!string.IsNullOrEmpty(_Formular))
            {
                _Qry += Constants.vbCrLf + "  AND ( " + _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") + " ) ";
            }

            _Qry += Constants.vbCrLf + "  AND   P.FTPayYear='" + HI.UL.ULF.rpQuoted(FTPayYear) + "' ";

            if ((FTPayTerm != null))
            {
                _Qry += Constants.vbCrLf + " AND P.FTPayTerm='" + HI.UL.ULF.rpQuoted(FTPayTerm) + "' ";
            }
            //เบส
            // _Qry += Constants.vbCrLf + " AND P.FTPayTerm='25' ";

            _Qry += Constants.vbCrLf + "  AND  (NOT(ES.FNHSysEmpTypeId IS NULL) OR  NOT(LS.FNHSysSectId IS NULL ))";

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR);
        }


    }
}

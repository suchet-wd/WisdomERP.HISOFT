using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.DirectoryServices;

namespace HI.ST
{
    public static class UserInfo
    {
      
        public static string UserName{ get; set;}

        public static string WindowUserName { get; set; }

        public static string UserPassword{get; set; }
        public static string UserDescTH { get; set;}
        public static string UserDescEN {get;set;}
        public static string UserLogInComputer{get; set;}
        public static string UserLogInComputerIP{get; set;}
        public static string LogINDate{get; set;}
        public static string LogINTime{get; set;}
        public static int LimitLogINTime{get;set;}
        public static int CountLogINTime {get;set;}
        public static string UserDepartment{get; set;}
        public static System.Drawing.Image UserImage{ get;set;}
        public static string UserCompany{ get; set;}
        public static string UserLevel{get;set;}

        public static bool Login(bool UserAD =false )
        {

            bool _Confirm = false;


            try
            {
                DataTable _dttmp = null;
                string _Str = "";

                if (UserAD == false) {
                        wUserLogIn _LogIn = new wUserLogIn();
                        _LogIn.ShowDialog();
                        if (_LogIn.Confirm)
                        {
                            //HI.ST.UserInfo.UserName = _LogIn.otbLogin.Text;
                            //HI.ST.UserInfo.UserPassword = _LogIn.otbPassword.Text;
                            HI.ST.UserInfo.UserCompany = _LogIn.FNHSysCmpId.Text;
                            _Confirm = true;
                        }
                        else
                        {
                            _Confirm = false;
                        }

                }else {

                    _Confirm = true;

                }

                if (!(_Confirm))
                {
                    HI.ST.UserInfo.UserName = "";
                    HI.ST.UserInfo.UserPassword = "";
                }
                else
                {

                    UserLogInComputer = System.Environment.MachineName;
                    UserLogInComputerIP = GetIP(UserLogInComputer);

                    HI.Conn.DB.UserNameLogIn = HI.ST.UserInfo.UserName;

                    if (string.IsNullOrEmpty(HI.ST.SysInfo.SystemFontName)) { HI.ST.SysInfo.SystemFontName = "Tahoma"; };

                    DateTime _Date = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM);
                    LogINDate = Strings.Format(_Date, "dd/MM/yyyy");
                    LogINTime = Strings.Format(_Date, "HH:mm:ss");
                    HI.ST.SysInfo.AppActScreen = 0;

                    string _username = "";


                    _Str = "EXEC  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.SP_CHACK_USER_MAIL '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    HI.ST.SysInfo.StateMail = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") == "1");

                    DataTable dtteamGrp = null;

                    _Str = "  SELECT U.FTUserName, T.FTTeamGrpCode, T.FTStatePurchase, T.FTStateActive, T.FTStateStock, T.FTStateProd, T.FTStateAccount, T.FTStateQA, T.FTStateQAFinal";


                    _Str += Constants.vbCrLf + "  FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin AS U WITH(NOLOCK) LEFT OUTER JOIN ";
                    _Str += Constants.vbCrLf + "           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS T  WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId";
                    _Str += Constants.vbCrLf + "  WHERE  (U.FTUserName = N'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";

                    dtteamGrp = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY);
                    HI.ST.SysInfo.StateUserPurchaseTeam = false;
                    HI.ST.SysInfo.StateUserStockTeam = false;
                    HI.ST.SysInfo.StateUserProdStaff = false;

                    foreach (DataRow Rxi in dtteamGrp.Rows)
                    {

                        HI.ST.SysInfo.StateUserPurchaseTeam = ((Rxi["FTStatePurchase"]).ToString() == "1");
                        //HI.ST.SysInfo.StyleRiskCritical = ((Rxi["FTStateStyleRiskCritical"]).ToString() == "1");
                        HI.ST.SysInfo.StateUserStockTeam = ((Rxi["FTStateStock"]).ToString() == "1");

                        //HI.ST.SysInfo.StateUserProdStaff = ((Rxi["FTStateProd"]).ToString() == "1" && (Rxi["FTStateProdStaff"]).ToString() == "1");

                        break;
                    };

                    dtteamGrp.Dispose();

                    DataTable dtse = null;
                    _Str = "SELECT   TOP 1  FTLanguageLocal,FTStateHideSunday,FNCheckInvoiceChargeDay,FNCheckInvoiceChargeTimeMin  ";
                    _Str += Constants.vbCrLf + " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEConfig AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTCmpCode='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) + "' ";
                    dtse = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY);

                    HI.ST.SysInfo.LanguageLocal = 2;
                    HI.ST.SysInfo.HideSunday = false;
                    HI.ST.SysInfo.StateUserInvoiceChargeDay = 0;
                    HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 10;

                    foreach (DataRow Rxi in dtse.Rows)
                    {

                        HI.ST.SysInfo.LanguageLocal = (int)Conversion.Val((Rxi["FTLanguageLocal"]));
                        HI.ST.SysInfo.HideSunday = ((Rxi["FTStateHideSunday"]).ToString() == "1");
                        //HI.ST.SysInfo.StateUserInvoiceChargeDay = (int)Conversion.Val((Rxi["FNCheckInvoiceChargeDay"].ToString() ));
                        //HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = (int)Conversion.Val((Rxi["FNCheckInvoiceChargeTimeMin"].ToString()));

                        break;
                    };

                    dtse.Dispose();
                    if (HI.ST.SysInfo.StateUserInvoiceChargeTimeMin == 0) { HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 10; };
                    if (HI.ST.SysInfo.StateUserInvoiceChargeDay == 0) { HI.ST.SysInfo.StateUserInvoiceChargeDay = 20; };

                    //_Str = " EXEC [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.SP_GET_USER_PERMISSTION_INVOICE_CHARGE '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    //HI.ST.SysInfo.StateUserInvoiceCharge = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "") !="");

                    //HI.ST.SysInfo.StateUserInvoiceCharge = true;
                    //HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 1;

                    //_Str = "  SELECT   TOP 1   FTUserName ";
                    //_Str += Constants.vbCrLf + " FROM  (";
                    //_Str += Constants.vbCrLf + " SELECT    FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS A WITH(NOLOCK) ";
                    //_Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' OR  FTUserNameTo='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
                    //_Str += Constants.vbCrLf + " UNION ";
                    //_Str += Constants.vbCrLf + " SELECT     FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMerTeam AS A WITH(NOLOCK) ";
                    //_Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    //_Str += Constants.vbCrLf + " ) AS A ";
                    //HI.ST.SysInfo.StateManager = (!string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "")));

                    HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Cmp, HI.ST.SysInfo.CmpCode);

                    if (!(HI.ST.SysInfo.Admin))
                    {

                        _Str = "SELECT   TOP 1  FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom";
                        _Str += Constants.vbCrLf + " FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginState ";
                        _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                        _dttmp = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY);

                        if (_dttmp.Rows.Count > 0)
                        {

                            string _msg = "User ared Connected";

                            _msg += Constants.vbCrLf + "User Connect By IP : " + (_dttmp.Rows[0]["FTLogInIP"]).ToString();
                            _msg += Constants.vbCrLf + "User Connect By Computername : " + (_dttmp.Rows[0]["FTLogInCom"]).ToString();
                            _msg += Constants.vbCrLf + " ";
                            _msg += Constants.vbCrLf + "Yes to Connect By this Computer And Cancel previously active. ";
                            _msg += Constants.vbCrLf + "Cancel for previously active. ";


                            if (System.Windows.Forms.MessageBox.Show(_msg, "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                _Confirm = false;
                            }
                            else
                            {

                                _Str = "Update  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginState SET ";
                                _Str += Constants.vbCrLf + "   FTLogInIP='" + HI.UL.ULF.rpQuoted(UserLogInComputerIP) + "' ";
                                //_Str += Constants.vbCrLf + ", FTLogInDate=" + HI.UL.ULDate.FormatDateDB;
                                //_Str += Constants.vbCrLf + ", FTLogInTime=" + HI.UL.ULDate.FormatTimeDB;
                                _Str += Constants.vbCrLf + ", FTLogInDate='" + HI.UL.ULDate.ConvertEnDB(LogINDate) + "'";
                                _Str += Constants.vbCrLf + ", FTLogInTime='" + HI.UL.ULF.rpQuoted(LogINTime) + "'";
                                _Str += Constants.vbCrLf + ", FTLogInCom='" + HI.UL.ULF.rpQuoted(UserLogInComputer) + "' ";
                                _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY);

                            }
                        }
                        else
                        {

                            _Str = "INSERT INTO  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginState (FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom) ";
                            _Str += Constants.vbCrLf + " SELECT '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(UserLogInComputerIP) + "'";
                            //_Str += Constants.vbCrLf + " ," + HI.UL.ULDate.FormatDateDB + " ";
                            //_Str += Constants.vbCrLf + " ," + HI.UL.ULDate.FormatTimeDB + " ";
                            _Str += Constants.vbCrLf + " ,'" + HI.UL.ULDate.ConvertEnDB(LogINDate) + "'";
                            _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(LogINTime) + "'";
                            _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(UserLogInComputer) + "'";

                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY);


                        }
                    }

                    _Str = "INSERT INTO  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginLogOutHistory (FTUserName, FTIP, FTDate, FTTime, FTCom,FTStateStatus) ";
                    _Str += Constants.vbCrLf + " SELECT '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                    _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(UserLogInComputerIP) + "'";
                    //_Str += Constants.vbCrLf + " ," + HI.UL.ULDate.FormatDateDB + " ";
                    //_Str += Constants.vbCrLf + " ," + HI.UL.ULDate.FormatTimeDB + " ";
                    _Str += Constants.vbCrLf + " ,'" + HI.UL.ULDate.ConvertEnDB(LogINDate) + "'";
                    _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(LogINTime) + "'";
                    _Str += Constants.vbCrLf + " ,'" + HI.UL.ULF.rpQuoted(UserLogInComputer) + "','0'";

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _Confirm;
        }

        private static void SetUserDataInfo()

        {

            string _Str = "";

           


        }

        public static bool VerifyLoginUserAD(string UserAD)
        {

            bool _Verify = false;
            string _Sql = null;

            try
            {

                string mUserName = UserAD;
                HI.ST.UserInfo.UserName = "";// this.otbLogin.Text;
                HI.ST.UserInfo.UserPassword = "";
                HI.ST.UserInfo.UserCompany = "";

                HI.ST.SysInfo.ADUserName = UserAD;

              
                _Sql = "SELECT TOP 1  *  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin ";
                _Sql += Constants.vbCrLf + " WHERE FTUserAD='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.ADUserName) + "' AND FTStateActive ='1'";

                DataTable _dt = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_SECURITY, null, false);

                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow R in _dt.Rows)
                    {

                        HI.ST.UserInfo.UserName = (R["FTUserName"]).ToString();
                        HI.ST.UserInfo.UserPassword = HI.Conn.DB.FuncDecryptData((R["FTPassword"]).ToString());

                        if ((R["FTStateActive"]).ToString() == "1")
                        {
                            _Verify = true;

                            HI.ST.SysInfo.Admin = ((R["FTStateAdmin"]).ToString() == "1");
                            HI.ST.SysInfo.AdminAllModule = ((R["FTStateAdminFollowModule"]).ToString() != "1"); //(string.IsNullOrEmpty((R["FTStateAdminFollowModule"]).ToString()));
                            HI.Conn.DB.UserNameLogIn = HI.ST.UserInfo.UserName;

                            if (!(HI.ST.SysInfo.Admin))
                            {

                                _Sql = "  SELECT TOP 1   A.FTUserName, C.FTCmpCode";
                                _Sql += Constants.vbCrLf + " FROM            [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A WITH(NOLOCK) INNER JOIN";
                                _Sql += Constants.vbCrLf + "  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS B WITH(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID INNER JOIN";
                                _Sql += Constants.vbCrLf + " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON B.FNHSysCmpId = C.FNHSysCmpId";
                                _Sql += Constants.vbCrLf + " WHERE A.FTUserName='" + HI.ST.UserInfo.UserName + "'";
                                _Sql += Constants.vbCrLf + " AND  C.FTCmpCode='" + HI.ST.SysInfo.CmpDefualtCode + "'";

                                if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_SECURITY, "")))
                                {


                                    _Sql = "  SELECT TOP 1   A.FTUserName, C.FTCmpCode";
                                    _Sql += Constants.vbCrLf + " FROM            [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A WITH(NOLOCK) INNER JOIN";
                                    _Sql += Constants.vbCrLf + "  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS B WITH(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID INNER JOIN";
                                    _Sql += Constants.vbCrLf + " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON B.FNHSysCmpId = C.FNHSysCmpId";
                                    _Sql += Constants.vbCrLf + " WHERE A.FTUserName='" + HI.ST.UserInfo.UserName + "'";


                                    HI.ST.SysInfo.CmpDefualtCode = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_SECURITY, "");

                                    if (HI.ST.SysInfo.CmpDefualtCode == "")
                                    {
                                        _Verify = false;
                                       // Interaction.MsgBox("Username Can not Access This Company Please Contact System Admin !!!");
                                    }


                                }

                            }

                            else
                            {
                                if (HI.ST.SysInfo.CmpDefualtCode == "")
                                {

                                    _Sql = "  SELECT TOP 1    C.FTCmpCode";
                                    _Sql += Constants.vbCrLf + " FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK) ";

                                    HI.ST.SysInfo.CmpDefualtCode = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_MASTER, "");


                                }

                            }

                            try
                            {
                                HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R["FPUserImage"]);
                            }
                            catch (Exception ex)
                            {
                                HI.ST.UserInfo.UserImage = null;
                            }

                        }
                        else
                        {
                            //Interaction.MsgBox("Username is Not Active Please Contact System Admin !!!");
                        }

                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                else
                {
                   // Interaction.MsgBox("ไม่พบข้อมูลการกำหนดสิทธิเข้าใช้งานระบบ Wisdom !!!");
                
                }

                _dt.Dispose();


            }



            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            HI.ST.UserInfo.UserCompany = HI.ST.SysInfo.CmpDefualtCode;
            HI.ST.SysInfo.CmpCode = HI.ST.SysInfo.CmpDefualtCode;


            if (_Verify) {

                _Sql = "  SELECT TOP 1    C.FTCmpCode,C.FNHSysCmpId,C.FTDocRun ";
                _Sql += Constants.vbCrLf + " FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK) ";
                _Sql += Constants.vbCrLf + " WHERE  C.FTCmpCode='" + HI.ST.SysInfo.CmpDefualtCode + "'";


                DataTable dt = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_MASTER);

                if (dt.Rows.Count > 0) {

                    HI.ST.SysInfo.CmpID = int.Parse ( dt.Rows[0]["FNHSysCmpId"].ToString());
                    HI.ST.SysInfo.CmpRunID = dt.Rows[0]["FTDocRun"].ToString();
                }


            }

           
          
            return _Verify;
        }

        public static bool ServiceLogin()
        {

            bool _Confirm = false;

            try
            {
                
                string _Str = "";

                    UserLogInComputer = System.Environment.MachineName;
                    UserLogInComputerIP = GetIP(UserLogInComputer);

                    if (string.IsNullOrEmpty(HI.ST.SysInfo.SystemFontName)) { HI.ST.SysInfo.SystemFontName = "Tahoma"; };

                    DateTime _Date = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM);
                    LogINDate = Strings.Format(_Date, "dd/MM/yyyy");
                    LogINTime = Strings.Format(_Date, "HH:mm:ss");
                    HI.ST.SysInfo.AppActScreen = 0;

                   _Str = "SELECT TOP 1  FTStateAdmin FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin ";
                   _Str += Constants.vbCrLf + " WHERE FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                   HI.ST.SysInfo.Admin = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "")=="1");


                   _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSECinfigDirector AS A WITH(NOLOCK) ";
                _Str += Constants.vbCrLf + " WHERE  FTComputerName='" + HI.UL.ULF.rpQuoted(UserLogInComputer) + "'";// AND FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                   HI.ST.SysInfo.DirectorName = "";
                   HI.ST.SysInfo.DirectorName = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "");

                    HI.ST.SysInfo.StateDirector = (!string.IsNullOrEmpty(HI.ST.SysInfo.DirectorName));

                    _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    string _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                    HI.ST.SysInfo.StateFactoryManager = (!string.IsNullOrEmpty(_username));

                    _Str = "SELECT  Top 1 FTUserNameChk  ";
                    _Str += Constants.vbCrLf + "FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMPostAccount WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + "Where FTStateActive = '1'";
                    _Str += Constants.vbCrLf + "and ((FTUserNameChk = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ";
                    _Str += Constants.vbCrLf + "OR (FTUserNameMngFac='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ";
                    _Str += Constants.vbCrLf + "OR (FTUserNameApp='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ";
                    _Str += Constants.vbCrLf + "OR (FTUserNameDirector='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'))";

                    _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                    HI.ST.SysInfo.StateOrderCostApp = (!string.IsNullOrEmpty(_username));

                //_Str = "SELECT   TOP 1  FTDCUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS A WITH(NOLOCK) ";
                //_Str += Constants.vbCrLf + " WHERE  FTDCUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                _Str = " SELECT TOP 1  FTUserNameChk From ( Select FTUserNameChk From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMPostDocument WITH(NOLOCK)  ";
                _Str += Constants.vbCrLf + "UNION ";
                _Str += Constants.vbCrLf + "Select FTUserNameApp From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMPostDocument WITH(NOLOCK) ) AS A   ";
                _Str += Constants.vbCrLf + " WHERE  FTUserNameChk='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                    HI.ST.SysInfo.StateUserDCControl = (!string.IsNullOrEmpty(_username));

                    _Str = "SELECT   TOP 1  FTPRUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTPRUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";

                    _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                    HI.ST.SysInfo.StateUserAppPR = (!string.IsNullOrEmpty(_username));

                    _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSECinfigUserWHAppCM AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                    HI.ST.SysInfo.StateWHAppCM = (!string.IsNullOrEmpty(_username));


                    _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMerTeam AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                    HI.ST.SysInfo.StateSuperVisorMer = (!string.IsNullOrEmpty(_username));

                    _Str = "EXEC  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.SP_CHACK_USER_MAIL '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    HI.ST.SysInfo.StateMail = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") == "1");

                    DataTable dtteamGrp = null;

                    _Str = "  SELECT U.FTUserName, T.FTTeamGrpCode, T.FTStatePurchase, T.FTStateActive, T.FTStateStock, T.FTStateProd, T.FTStateAccount, T.FTStateQA, T.FTStateQAFinal";
                    _Str += Constants.vbCrLf + ",ISNULL((SELECT TOP 1 XA.FTStateStyleRiskCritical ";
                    _Str += Constants.vbCrLf + "  FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermission AS XA ";
                    _Str += Constants.vbCrLf + "  INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS XB ON XA.FNHSysPermissionID = XB.FNHSysPermissionID";
                    _Str += Constants.vbCrLf + "  WHERE XB.FTUserName =N'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND ISNULL((XA.FTStateStyleRiskCritical),'')='1'";
                    _Str += Constants.vbCrLf + " ),'') AS FTStateStyleRiskCritical ";

                    _Str += Constants.vbCrLf + ",ISNULL((SELECT TOP 1 XA.FTStateStaff ";
                    _Str += Constants.vbCrLf + "  FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermission AS XA ";
                    _Str += Constants.vbCrLf + "  INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS XB ON XA.FNHSysPermissionID = XB.FNHSysPermissionID";
                    _Str += Constants.vbCrLf + "  WHERE XB.FTUserName =N'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND ISNULL((XA.FTStateStaff),'')='1'";
                    _Str += Constants.vbCrLf + " ),'') AS FTStateProdStaff";

                    _Str += Constants.vbCrLf + "  FROM     [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin AS U WITH(NOLOCK) LEFT OUTER JOIN ";
                    _Str += Constants.vbCrLf + "           [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS T  WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId";
                    _Str += Constants.vbCrLf + "  WHERE  (U.FTUserName = N'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "')";

                    dtteamGrp = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY);
                    HI.ST.SysInfo.StateUserPurchaseTeam = false;
                    HI.ST.SysInfo.StateUserStockTeam = false;
                    HI.ST.SysInfo.StateUserProdStaff = false;

                    foreach (DataRow Rxi in dtteamGrp.Rows)
                    {

                        HI.ST.SysInfo.StateUserPurchaseTeam = ((Rxi["FTStatePurchase"]).ToString() == "1");
                        HI.ST.SysInfo.StyleRiskCritical = ((Rxi["FTStateStyleRiskCritical"]).ToString() == "1");
                        HI.ST.SysInfo.StateUserStockTeam = ((Rxi["FTStateStock"]).ToString() == "1");
                        HI.ST.SysInfo.StateUserProdStaff = ((Rxi["FTStateProd"]).ToString() == "1" && (Rxi["FTStateProdStaff"]).ToString() == "1");
                        break;

                    };

                    dtteamGrp.Dispose();

                   _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS A WITH(NOLOCK) ";
                   _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND ISNULL(FTStateQAFinal,'') ='1' ";
                   _username = "";
                   _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");

                   HI.ST.SysInfo.StateUserQAFinalLeader = (!string.IsNullOrEmpty(_username));

                  _Str = "SELECT   TOP 1  '1' AS FTData FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSESystemConfig AS A WITH(NOLOCK) ";
                  _Str += Constants.vbCrLf + " WHERE  FTCfgName LIKE 'SMPAppIncentive%' AND FTCfgData='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                  HI.ST.SysInfo.StateAppSMP = false;

                  HI.ST.SysInfo.StateAppSMP = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") == "1");

                  _Str = "SELECT   TOP 1  '1' AS FTData FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSESystemConfig AS A WITH(NOLOCK) ";
                  _Str += Constants.vbCrLf + " WHERE  FTCfgName LIKE 'SMPMGRAppIncentive%' AND FTCfgData='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                   HI.ST.SysInfo.StateAppSMPMGR = false;

                  HI.ST.SysInfo.StateAppSMPMGR = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") == "1");

                  _Str = "SELECT   TOP 1  FTCfgData FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSESystemConfig AS A WITH(NOLOCK) ";
                  _Str += Constants.vbCrLf + " WHERE  FTCfgName='TimeRetryWorkTimeDeductSPTime' ";
                  HI.ST.SysInfo.RetryWorkTimeDeductSPTime = false;

                  HI.ST.SysInfo.RetryWorkTimeDeductSPTime = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") == "1");

                  _Str = "SELECT   TOP 1  FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS A WITH(NOLOCK) ";
                   _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  AND ISNULL(FTStateSew,'') ='1' ";
                   _username = "";
                   _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                    HI.ST.SysInfo.StateUserSewingLineLeader = (!string.IsNullOrEmpty(_username));
             
                    _Str = "SELECT   TOP 1  FTUserNameMngFac From   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee  WITH(NOLOCK)  ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserNameMngFac='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'   ";
                    _Str += Constants.vbCrLf + "UNION ";
                    _Str += Constants.vbCrLf + "Select top 1 FTUserNameChk From   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee  WITH(NOLOCK)  ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserNameChk='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'   ";

                    _username = "";
                    _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                    HI.ST.SysInfo.StateEmpLeaveApp = (!string.IsNullOrEmpty(_username));

                _Str = "SELECT   TOP 1  FTManagerUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin AS C WITH(NOLOCK) ";
                _Str += Constants.vbCrLf + " WHERE  FTManagerUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                _username = "";
                _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                HI.ST.SysInfo.StateSuperVisorPRAsset = (!string.IsNullOrEmpty(_username));


                //_Str = "SELECT   TOP 1    C.FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request AS R  WITH(NOLOCK)  ";
                ////_Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin AS L WITH(NOLOCK)  ON R.FTPRPurchaseBy=L.FTUserName";
                ////_Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDirectorGrpUser AS DG  WITH(NOLOCK) ON L.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId";
                //_Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetConfigLevel AS C ON R.FNFixedAssetType=C.FNFixedAssetType  AND R.FNHSysCmpId=C.FNHSysCmpId   ";
                //_Str += Constants.vbCrLf + " WHERE    C.FTUserName ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND FTStateDirector='1' ";
                //_username = "";
                //_username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                //HI.ST.SysInfo.StateDirectorAssetPR = (!string.IsNullOrEmpty(_username));

                _Str = "SELECT   TOP 1    C.FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request AS R  WITH(NOLOCK)  ";
                _Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request_Detail AS RD WITH(NOLOCK)  ON R.FTPRPurchaseNo=RD.FTPRPurchaseNo";
                _Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetConfigLevel AS C ON RD.FNFixedAssetType=C.FNFixedAssetType  AND R.FNHSysCmpId=C.FNHSysCmpId   ";
                _Str += Constants.vbCrLf + " WHERE    C.FTUserName ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND FTStateDirector='1' ";
                _username = "";
                _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                HI.ST.SysInfo.StateDirectorAssetPR = (!string.IsNullOrEmpty(_username));



                _Str = "SELECT   TOP 1  FTManagerName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetApprovedPO AS PO WITH(NOLOCK) ";
                _Str += Constants.vbCrLf + " WHERE  FTManagerName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                _username = "";
                _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                HI.ST.SysInfo.StateSuperVisorAssetPO = (!string.IsNullOrEmpty(_username));

                _Str = "SELECT   TOP 1   L.FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetConfigLevel AS L  WITH(NOLOCK) ";
                _Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.V_TFIXEDTPurchase AS PO  WITH(NOLOCK) ON L.FNFixedAssetType=PO.FNFixedAssetType AND L.FNHSysCmpId=PO.FNHSysCmpId ";
                _Str += Constants.vbCrLf + " WHERE  L.FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' AND L.FTStateFactory='1'";
                _username = "";
                _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                HI.ST.SysInfo.StateDirectorAssetPO = (!string.IsNullOrEmpty(_username));


                //_Str = "SELECT   TOP 1    C.FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request AS R  WITH(NOLOCK)  ";
                //_Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetSafety AS C ON R.FNFixedAssetType=C.FNFixedAssetType  AND R.FNHSysCmpId=C.FNHSysCmpId   ";
                //_Str += Constants.vbCrLf + " WHERE    C.FTUserName ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
                //_username = "";
                //_username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                //HI.ST.SysInfo.StateSafetyPRAsset = (!string.IsNullOrEmpty(_username));

                _Str = "SELECT   TOP 1    C.FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request AS R  WITH(NOLOCK)  ";
                _Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) + "].dbo.TFIXEDTPurchase_Request_Detail AS RD WITH(NOLOCK)  ON R.FTPRPurchaseNo=RD.FTPRPurchaseNo";
                _Str += Constants.vbCrLf + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TASMAssetSafety AS C ON RD.FNFixedAssetType=C.FNFixedAssetType  AND R.FNHSysCmpId=C.FNHSysCmpId   ";
                _Str += Constants.vbCrLf + " WHERE    C.FTUserName ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
                _username = "";
                _username = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "");
                HI.ST.SysInfo.StateSafetyPRAsset = (!string.IsNullOrEmpty(_username));


                DataTable dtse = null;
                    _Str = "SELECT   TOP 1  FTLanguageLocal,FTStateHideSunday,FNCheckInvoiceChargeDay,FNCheckInvoiceChargeTimeMin  ";
                    _Str += Constants.vbCrLf + " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEConfig AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTCmpCode='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) + "' ";
                    dtse = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY);

                    HI.ST.SysInfo.LanguageLocal = 2;
                    HI.ST.SysInfo.HideSunday = false;
                    HI.ST.SysInfo.StateUserInvoiceChargeDay = 0;
                    HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 10;

                    foreach (DataRow Rxi in dtse.Rows)
                    {

                        HI.ST.SysInfo.LanguageLocal = (int)Conversion.Val((Rxi["FTLanguageLocal"]));
                        HI.ST.SysInfo.HideSunday = ((Rxi["FTStateHideSunday"]).ToString() == "1");
                        HI.ST.SysInfo.StateUserInvoiceChargeDay = (int)Conversion.Val((Rxi["FNCheckInvoiceChargeDay"].ToString()));
                        HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = (int)Conversion.Val((Rxi["FNCheckInvoiceChargeTimeMin"].ToString()));

                        break;
                    };

                    dtse.Dispose();
                    if (HI.ST.SysInfo.StateUserInvoiceChargeTimeMin == 0) { HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 10; };
                    if (HI.ST.SysInfo.StateUserInvoiceChargeDay == 0) { HI.ST.SysInfo.StateUserInvoiceChargeDay = 20; };

                    _Str = " EXEC [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.SP_GET_USER_PERMISSTION_INVOICE_CHARGE '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    HI.ST.SysInfo.StateUserInvoiceCharge = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "") != "");

                    //HI.ST.SysInfo.StateUserInvoiceCharge = true;
                    //HI.ST.SysInfo.StateUserInvoiceChargeTimeMin = 1;

                    _Str = "  SELECT   TOP 1   FTUserName ";
                    _Str += Constants.vbCrLf + " FROM  (";
                    _Str += Constants.vbCrLf + " SELECT    FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' OR  FTUserNameTo='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'  ";
                    _Str += Constants.vbCrLf + " UNION ";
                    _Str += Constants.vbCrLf + " SELECT     FTUserName  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMerTeam AS A WITH(NOLOCK) ";
                    _Str += Constants.vbCrLf + " WHERE  FTUserName='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' ";
                    _Str += Constants.vbCrLf + " ) AS A ";

                    HI.ST.SysInfo.StateManager = (!string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "")));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _Confirm;
        }


        private static string GetIP(string strHostName)
        {
            try
            {
                string _GetIPv4Address = "";
                System.Net.IPHostEntry iphe = System.Net.Dns.GetHostEntry(strHostName);
                foreach (System.Net.IPAddress ipheal in iphe.AddressList)
                {
                    if (ipheal.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        _GetIPv4Address = ipheal.ToString();
                        break; 
                    }
                }

                return _GetIPv4Address;

            }
            catch 
            {
                return "";
            }
        }


      

    }
}

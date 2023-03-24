using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace HI.ST
{
    public static class SysInfo
    {
        public static bool Admin{get;set;}
        public static string ADUserName { get; set; }
        public static bool HideSunday{ get;set;}
        public static bool StateDirector{ get;set; }
        public static bool StateFactoryManager { get; set; }
        public static string DirectorName { get; set; }
        public static bool StateManager{get;set;}
        public static bool AdminAllModule{ get; set;}
        public static string CmpDefualtCode {get;set;}
        public static string CmpCode{get;set;}
        public static int CmpID{ get;set; }
        public static int AppActScreen { get; set; }
        public static string CmpRunID{get;set; }
        public static string ModuleID{ get;set;}
        public static string ModuleName{get;set;}
        public static string MenuName{ get; set;}
        public static string FTOptionMouseScoll { get; set; }
        public static bool DevelopMode { get; set; }
        public static bool StateWHAppCM { get; set; }
        public static bool StateSuperVisorMer { get; set; }
        public static bool StateUserInvoiceCharge { get; set; }
        public static int StateUserInvoiceChargeDay { get; set; }
        public static int StateUserInvoiceChargeTimeMin { get; set; }
        public static bool StateUserSewingLineLeader { get; set; }
        public static bool StateUserQAFinalLeader { get; set; }
        public static bool StateUserDCControl { get; set; }
        public static bool StateUserPurchaseTeam { get; set; }
        public static bool StateUserStockTeam { get; set; }
        public static bool StateUserProdStaff { get; set; }
        public static bool StateUserAppPR { get; set; }
        public static bool StateOrderCostApp { get; set; }
        public static bool StateMail { get; set; }
        public static bool StateEmpLeaveApp { get; set; }
        public static string SystemFontName { get; set; }

        public static bool RetryWorkTimeDeductSPTime { get; set; }
        public static bool StyleRiskCritical { get; set; }

        public static bool StateSuperVisorAssetPO { get; set; }
        public static bool StateSuperVisorPRAsset { get; set; }
        public static bool StateSafetyPRAsset { get; set; }

        public static bool StateDirectorAssetPO { get; set; }
        public static bool StateDirectorAssetPR { get; set; }
        public static bool StateAppSMP { get; set; }
        public static bool StateAppSMPMGR { get; set; }
        public static bool StateAppRDSam{ get; set; }

        private static string _AppName = "Wisdom System";
        public static string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }

        private static string _PathFileDLL = "C:\\WISDOM";
        public static string PathFileDLL
        {
            get { return _PathFileDLL; }
            set { _PathFileDLL = value; }
        }

        private static int _LanguageLocal = 0;
        public static int LanguageLocal
        {
            get { return _LanguageLocal; }

            set { _LanguageLocal = value; }
        }

        private static string _SysPath = Application.StartupPath + (Strings.Right(Application.StartupPath, 1) == "\\" ? "" : "\\");
        public static string SysPath
        {
            get { return _SysPath; }
        }

          public static  string FontInstalled(String FontSysName  ) {
              try {
		            System.Drawing.FontFamily FontFamily = new System.Drawing.FontFamily(FontSysName);
		            FontFamily.Dispose();
		            return FontSysName;
	               } catch {
                       return "Tahoma";
	            }
          }


    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security;
using System.Security.Principal;
using System.Security.AccessControl;

namespace HI.UL
{
    public static  class AppRegistry
    {
        public enum KeyName : int
        {
            Language = 0,
            Theme = 1,
            Cmp = 2,
            Font = 3
        }

        public static string ReadRegistry(KeyName Key)
        {
            RegistryKey regKey = null;
            string valreturn = "";


            try {
                regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                if (regKey == null)
                {

                    Registry.CurrentUser.CreateSubKey("Software\\SWM", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                }

                valreturn = regKey.GetValue(Key.ToString(), "").ToString();
                regKey.Close();
            } catch { }
           

            return valreturn;
        }

        public static void WriteRegistry(KeyName Key, object value)
        {
            RegistryKey regKey = null;
            try {
                regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);
                if (regKey == null)
                {

                    Registry.CurrentUser.CreateSubKey("Software\\SWM", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                    regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                }

                regKey.SetValue(Key.ToString(), value.ToString());
                regKey.Close();
            } catch { }
           

        }

        public static string ReadRegistry(String Key)
        {
            RegistryKey regKey = null;
            string valreturn = "";


            try {

                regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                if (regKey == null)
                {

                    Registry.CurrentUser.CreateSubKey("Software\\SWM", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                }
            } catch { }
           

            valreturn = regKey.GetValue(Key.ToString(), "").ToString();
            regKey.Close();

            return valreturn;

        }

        public static void WriteRegistry(String Key, object value)
        {
            RegistryKey regKey = null;

            try {
                regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);
                if (regKey == null)
                {

                    Registry.CurrentUser.CreateSubKey("Software\\SWM", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                    regKey = Registry.CurrentUser.OpenSubKey("Software\\SWM", true);

                }

                regKey.SetValue(Key.ToString(), value.ToString());
                regKey.Close();
            } catch { }
           

        }

        public static void LoadLayoutGridFromRegistry(object FormObj, DevExpress.XtraGrid.Views.Grid.GridView Ogv)
        {
            try
            {

                Ogv.RestoreLayoutFromRegistry("Software\\SWM\\Layouts\\" + FormObj.GetType().GetProperty("Name").GetValue(FormObj, null).ToString() + Ogv.Name.ToString());

                try
                {
                    Ogv.ClearColumnsFilter();
                    Ogv.ActiveFilter.Clear();
                }
                catch// (Exception ex)
                {
                }

            }
            catch //(Exception ex)
            {
            }
        }

        public static void SaveLayoutGridToRegistry(object FormObj, DevExpress.XtraGrid.Views.Grid.GridView Ogv)
        {
            Ogv.SaveLayoutToRegistry("Software\\SWM\\Layouts\\" + FormObj.GetType().GetProperty("Name").GetValue(FormObj, null).ToString() + Ogv.Name.ToString());

        }

        public static void DeleteLayoutGridToRegistry(object FormObj, DevExpress.XtraGrid.Views.Grid.GridView Ogv)
        {

            try {
                Registry.CurrentUser.DeleteSubKeyTree("Software\\SWM\\Layouts\\" + FormObj.GetType().GetProperty("Name").GetValue(FormObj, null).ToString() + Ogv.Name.ToString()); 
            }
            catch { }

        }

    }
}

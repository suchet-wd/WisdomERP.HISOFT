﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HI.CH
{
    public class CheckEnvironment
    {


        public CheckEnvironment() { 




        }




        public void Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    Console.WriteLine("Version: " + CheckFor45DotVersion(releaseKey));
                }
               
            }


            // Checking the version using >= will enable forward compatibility,  
            // however you should always compile your code on newer versions of 
            // the framework to ensure your app works the same. 
          
        }

        private string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
            {
                return "4.7.2 or later";
            }
            if (releaseKey >= 461308)
            {
                return "4.7.1 or later";
            }
            if (releaseKey >= 460798)
            {
                return "4.7 or later";
            }
            if (releaseKey >= 394802)
            {
                return "4.6.2 or later";
            }
            if (releaseKey >= 394254)
            {
                return "4.6.1 or later";
            }
            if (releaseKey >= 393295)
            {
                return "4.6 or later";
            }
            if (releaseKey >= 393273)
            {
                return "4.6 RC or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return "No 4.5 or later version detected";
        }


        public bool Get452installed()
        {


            bool state = false;
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    state = CheckFor452Installed(releaseKey);
                }
              
            }
            return state;

            // Checking the version using >= will enable forward compatibility,  
            // however you should always compile your code on newer versions of 
            // the framework to ensure your app works the same. 

        }

     
        private bool CheckFor452Installed(int releaseKey)
        {
            if (releaseKey >= 461808)
            {
                return true; //"4.7.2 or later";
            }
            if (releaseKey >= 461308)
            {
                return true; //"4.7.1 or later";
            }
            if (releaseKey >= 460798)
            {
                return true; //"4.7 or later";
            }
            if (releaseKey >= 394802)
            {
                return true;// "4.6.2 or later";
            }
            if (releaseKey >= 394254)
            {
                return true; //"4.6.1 or later";
            }
            if (releaseKey >= 393295)
            {
                return true;// "4.6 or later";
            }
            if (releaseKey >= 393273)
            {
                return true;// "4.6 RC or later";
            }
            if ((releaseKey >= 379893))
            {
                return true;// "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return false; //"4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return false;// "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return false; // "No 4.5 or later version detected";
        }

    }

   

}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HI.ST
{
    public static class Config
    {
        public static string ExcFormat
        {
            get
            {
                if (Conversion.Val(ExcDigit) <= 0)
                {
                    return "0";
                }
                else
                {
                    return "0." + "".PadRight(ExcDigit, '0');
                }
            }
        }

        public static string PriceFormat
        {
            get
            {
                if (Conversion.Val(PriceDigit) <= 0)
                {
                    return "0";
                }
                else
                {
                    return "0." + "".PadRight(PriceDigit, '0');
                }
            }
        }

        public static string QtyFormat
        {
            get
            {
                if (Conversion.Val(QtyDigit) <= 0)
                {
                    return "0";
                }
                else
                {
                    return "0." + "".PadRight(QtyDigit, '0');
                }
            }
        }


        public static string AmtFormat
        {
            get
            {
                if (Conversion.Val(AmtDigit) <= 0)
                {
                    return "0";
                }
                else
                {
                    return "0." + "".PadRight(AmtDigit, '0');
                }
            }
        }


        public static string PercentFormat
        {
            get
            {
                if (Conversion.Val(PercentDigit) <= 0)
                {
                    return "0";
                }
                else
                {
                    return "0." + "".PadRight(PercentDigit, '0');
                }
            }
        }

        private static int _ExcDigit = 5;
        public static int ExcDigit
        {
            get { return _ExcDigit; }
            set { _ExcDigit = value; }
        }

        private static int _PriceDigit = 5;
        public static int PriceDigit
        {
            get { return _PriceDigit; }
            set { _PriceDigit = value; }
        }

        private static int _QtyDigit = 4;
        public static int QtyDigit
        {
            get { return _QtyDigit; }
            set { _QtyDigit = value; }
        }

        private static int _AmtDigit = 2;
        public static int AmtDigit
        {
            get { return _AmtDigit; }
            set { _AmtDigit = value; }
        }

        private static int _PercentDigit = 2;
        public static int PercentDigit
        {
            get { return _PercentDigit; }
            set { _PercentDigit = value; }
        }

        public static bool StateNotShowEmpLeave{get;set;}

    }
}

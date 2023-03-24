using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace HI.UL
{
    public static class ULDate
    {
        public const string FormatDateDB = "Convert(varchar(10),Getdate(),111)";
        public const string FormatTimeDB = "Convert(varchar(8),Getdate(),114)";

        public static DateTime GetOnServer(HI.Conn.DB.DataBaseName DbName)
        {
            System.DateTime Current_Date = System.DateTime.Now;

            System.Data.SqlClient.SqlConnection _Cnn = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlCommand _Cmd = new System.Data.SqlClient.SqlCommand();

            try
            {

                if (_Cnn.State == ConnectionState.Open) { _Cnn.Close(); };
                _Cnn.ConnectionString = HI.Conn.DB.ConnectionString(DbName);
                _Cnn.Open();
                _Cmd = _Cnn.CreateCommand();

                _Cmd.CommandType = CommandType.Text;
                _Cmd.CommandText = "Select GetDate()";
                Current_Date = Convert.ToDateTime(_Cmd.ExecuteScalar());

                HI.Conn.SQLConn.DisposeSqlConnection(_Cmd);
                HI.Conn.SQLConn.DisposeSqlConnection(_Cnn);

            }
            catch
            {
                HI.Conn.SQLConn.DisposeSqlConnection(_Cmd);
                HI.Conn.SQLConn.DisposeSqlConnection(_Cnn);
               
            }

            return Current_Date;

        }

        public static string ConvertEN(object DataDate)
        {

            string strDate = "";


            try
            {
                strDate = CheckDate(DataDate);

                if ((Convert.ToInt32(Strings.Mid(strDate, 7, 4)) > 0) & (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) > (2300)))
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) - 543).ToString("0000");
                }
                else
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Strings.Mid(strDate, 7, 4));
                }
            }
            catch// (Exception ex)
            {
                strDate = "";
            }
            return strDate;

        }

        public static string ConvertTH(object DataDate)
        {

            string strDate = "";


            try
            {
                strDate = CheckDate(DataDate);

                if ((Convert.ToInt32(Strings.Mid(strDate, 7, 4)) > 0) & (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) < (2300)))
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) + 543).ToString("0000");
                }
                else
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Strings.Mid(strDate, 7, 4));
                }
            }
            catch //(Exception ex)
            {
                strDate = "";
            }

            return strDate;

        }


        public static string ConvertEnDB(object DataDate)
        {
            string strDate = "";

            try
            {
                strDate = CheckDate(DataDate);

                if ((Convert.ToInt32(Strings.Mid(strDate, 7, 4)) > 0) & (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) > (2300)))
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Convert.ToInt32(Strings.Mid(strDate, 7, 4)) - 543).ToString("0000");
                }
                else
                {
                    strDate = (Strings.Mid(strDate, 1, 2)) + "/" + (Strings.Mid(strDate, 4, 2)) + "/" + (Strings.Mid(strDate, 7, 4));
                }

                strDate = Strings.Mid(strDate, 7, 4) + "/" + Strings.Mid(strDate, 4, 2) + "/" + Strings.Mid(strDate, 1, 2);
            }
            catch //(Exception ex)
            {
                strDate = "";
            }


            return strDate;

        }

        public static string CheckDate(object Obj)
        {
            try
            {
                CultureInfo _Culture = new CultureInfo("en-US", true);
                _Culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                _Culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

                System.Threading.Thread.CurrentThread.CurrentCulture = _Culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = _Culture;



                string _Date = "";
                _Date = Strings.Format(Convert.ToDateTime(Obj), "dd/MM/yyyy");

                return _Date;
            }
            catch //(Exception ex)
            {
                return "";
            }
        }

        public static int CheckHoliday(string StrDate, string ToDate)
        {
            try
            {
                int K = 0;

                if (string.IsNullOrEmpty(HI.UL.ULDate.ConvertEnDB(StrDate)) | string.IsNullOrEmpty(HI.UL.ULDate.ConvertEnDB(ToDate)))
                {
                    return 0;
                }

                if (HI.UL.ULDate.ConvertEnDB(StrDate) == HI.UL.ULDate.ConvertEnDB(ToDate))
                {
                    return 0;

                }
                else
                {
                    do
                    {
                        if (Convert.ToDateTime(StrDate).DayOfWeek == DayOfWeek.Sunday | Convert.ToDateTime(StrDate).DayOfWeek == DayOfWeek.Saturday)
                        {
                            StrDate = HI.UL.ULDate.ConvertEnDB(Convert.ToDateTime(StrDate).AddDays(1));
                        }
                        else
                        {
                            K = K + 1;
                            StrDate = HI.UL.ULDate.ConvertEnDB(Convert.ToDateTime(StrDate).AddDays(1));
                        }

                    } while (!(Convert.ToDateTime(HI.UL.ULDate.ConvertEnDB(StrDate)) >= Convert.ToDateTime(HI.UL.ULDate.ConvertEnDB(ToDate))));
                }


                return K;

            }
            catch
            {
                return 0;
            }

        }

        public static string AddDay(string StrDate, int ToTalDay)
        {
            string ToDate = StrDate;


            try
            {
                if (string.IsNullOrEmpty(HI.UL.ULDate.ConvertEnDB(StrDate)))
                {
                    return "";
                }

                int K = ToTalDay;

                if (ToTalDay == 0)
                {
                    return ToDate;
                }
                else
                {
                    ToDate = HI.UL.ULDate.ConvertEnDB(Convert.ToDateTime(ToDate).AddDays(K));
                }


                return ToDate;

            }
            catch
            {
                return ToDate;
            }

        }

        public static string AddMonth(object Obj, int Amount)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

                string _Date = "";
                _Date = Strings.Format(Convert.ToDateTime(Obj).AddMonths(Amount), "dd/MM/yyyy");

                return _Date;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string AddYear(string otmpDate, int tmpToTalYear)
        {
            string _Date = otmpDate;


            try
            {
                int K = tmpToTalYear;

                if (tmpToTalYear == 0)
                {
                    return _Date;
                }
                else
                {
                    _Date = HI.UL.ULDate.ConvertEnDB(Convert.ToDateTime(_Date).AddYears(K));
                }


            }
            catch
            {
            }
            return _Date;
        }

        public static string GetMonthNameEN(string objDate)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

                string _Month = "";
                _Month = Strings.Format(Convert.ToDateTime(objDate), "MMMM");

                return _Month;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string GetMonthNameTH(string objDate)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US", true);
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

                string _Month = "";
                _Month = Strings.Format(Convert.ToDateTime(objDate), "MMMM");

                return objDate;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static long UDayDiff(DateInterval _Type, string ObjStartDate, string ObjEndDate)
        {

            try
            {
                string _SDate = HI.UL.ULDate.ConvertEnDB(ObjStartDate);
                string _EDate = HI.UL.ULDate.ConvertEnDB(ObjEndDate);

                if (!string.IsNullOrEmpty(_SDate) & !string.IsNullOrEmpty(_EDate))
                {
                    return DateAndTime.DateDiff(_Type, Convert.ToDateTime(_SDate), Convert.ToDateTime(_EDate));
                }
                else
                {
                    return 0;
                }

            }
            catch
            {
                return 0;
            }

        }

        public static int HHMMtoMin(string HHMM)
        {
            string[] _Arr = null;
            int _Time = 0;

            _Arr = HHMM.Split(Convert.ToChar('.'));
            if (_Arr.Length > 0)
            {
                _Time = Convert.ToInt32(Conversion.Val(_Arr[0]) * 60);
                if (_Arr.Length > 1)
                {
                    _Time = _Time + Convert.ToInt32(Conversion.Val(_Arr[1]));
                }
            }
            return _Time;
        }

    }
}

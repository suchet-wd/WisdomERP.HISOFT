using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Windows;
using System.Drawing;

namespace HI.UL
{
    public static class ULF
    {
        public static string rpQuoted(string tmpStr)
        {

           string DataStr = tmpStr.Trim();

            if (!string.IsNullOrEmpty(tmpStr))
            {
                return Strings.Replace(DataStr, (Strings.Chr(39)).ToString(), (Strings.Chr(39)).ToString() + (Strings.Chr(39)).ToString());
            }
            else
            {
                return DataStr;
            }
      
        }

        public static double ChkNumeric(object tmpObj, double pnDefault = 0)
        {
            try
            {
                if ((tmpObj == null))
                    return pnDefault;
                if (Information.IsDBNull(tmpObj))
                    return pnDefault;
                if (!Information.IsNumeric(tmpObj))
                    return pnDefault;
                return Double.Parse(tmpObj.ToString());
            }
            catch (Exception ex)
            {
                return pnDefault;
            }
        }

        public static string Convert_Bath_TH(double nAmount)
        {
            string Tep = null;
            string cTep = null;
            string cNum = null;
            string cString = null;
            int Counter = 0;
            int Ch = 0;
            int Back_Counter = 0;
            string CDecs = null;
        
            string[] xlc = new string[7];
            string[] Anum = new string[11];
            string[] Milanum = new string[11];
            cString = "";

            try
            {

                nAmount = Convert.ToDouble(Strings.Format(nAmount, "########.##"));
                xlc[1] = "สิบ";
                xlc[2] = "ร้อย";
                xlc[3] = "พัน";
                xlc[4] = "หมื่น";
                xlc[5] = "แสน";
                xlc[6] = "ล้าน";
                Anum[1] = "หนึ่ง";
                Anum[2] = "สอง";
                Anum[3] = "สาม";
                Anum[4] = "สี่";
                Anum[5] = "ห้า";
                Anum[6] = "หก";
                Anum[7] = "เจ็ด";
                Anum[8] = "แปด";
                Anum[9] = "เก้า";
                Anum[10] = "สิบ";
                Milanum[1] = "สิบ";
                Milanum[2] = "ยี่สิบ";
                Milanum[3] = "สามสิบ";
                Milanum[4] = "สี่สิบ";
                Milanum[5] = "ห้าสิบ";
                Milanum[6] = "หกสิบ";
                Milanum[7] = "เจ็ดสิบ";
                Milanum[8] = "แปดสิบ";
                Milanum[9] = "เก้าสิบ";
                Milanum[10] = "หนึ่งร้อย";
               
                cNum = Strings.Trim(Conversion.Str(nAmount));
                if (Strings.InStr(1, cNum, ".") <= 0)
                {
                    cNum = cNum + ".00";
                };

                Back_Counter = 1;
                CDecs = Strings.Mid(cNum, Strings.InStr(1, cNum, ".") + 1, 2);
                Tep = "บาท";
                cTep = "ถ้วน";
                for (Counter = (Strings.InStr(1, cNum, ".") - 1); Counter >= 1; Counter += -1)
                {
                    Ch = Convert.ToInt32(Strings.Mid(cNum, Back_Counter, 1));
                    if (Convert.ToString(Ch) != "0" | Information.IsDBNull(Ch))
                    {

                        switch (Counter)
                        {
                            case 1:
                                if (Strings.InStr(1, cNum, ".") - 2 != 0)
                                {
                                    if (!Information.IsDBNull(Conversion.Val(Strings.Mid(cNum, Strings.InStr(1, cNum, ".") - 2, 1))))
                                    {
                                        if (Ch == 1)
                                        {
                                            cString = cString + "เอ็ด";
                                        }
                                        else
                                        {
                                            cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))];
                                        }
                                    }
                                    else
                                    {
                                        cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))];
                                    }
                                }
                                else
                                {
                                    cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))];
                                }
                                break;
                            case 2:
                                if (Convert.ToString(Ch) == "2")
                                {
                                    cString = cString + "ยี่" + xlc[Counter - 1];
                                }
                                else
                                {
                                    if (Ch == 1)
                                    {
                                        cString = cString + xlc[Counter - 1];
                                    }
                                    else
                                    {
                                        cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                    }
                                }
                                break;
                            case 3:
                                cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                break;
                            case 4:
                                cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                break;
                            case 5:
                                cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                break;
                            case 6:
                                cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                break;
                            case 7:
                                if (Strings.Len(cNum) <= 10)
                                {
                                    cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                }
                                else
                                {
                                    if (Ch == 1)
                                    {
                                        cString = cString + "เอ็ด" + xlc[Counter - 1];
                                    }
                                    else
                                    {
                                        cString = cString + Anum[Convert.ToInt32(Conversion.Val(Ch))] + xlc[Counter - 1];
                                    }
                                }
                                break;
                            default:

                                if (Counter >= 8)
                                {
                                    if (Strings.Mid(cNum, Back_Counter + 1, 1) == "0")
                                    {
                                        cString = cString + Milanum[Convert.ToInt32(Conversion.Val(Ch))] + "ล้าน";
                                    }
                                    else
                                    {
                                        cString = cString + Milanum[Convert.ToInt32(Conversion.Val(Ch))];
                                    }
                                };
                                break;
                        }
                        
                    }
                    
                    Back_Counter = Back_Counter + 1;
                }
                if (Conversion.Val(CDecs) > 0)
                {
                    if (Convert.ToBoolean(Strings.StrComp(cString, "")))
                    {
                        cString = cString + "บาท";
                    }
                    Tep = " ";
                    switch (Strings.Mid(CDecs, 1, 1))
                    {
                        case "1":
                            Tep = "สิบ";
                            break;
                        case "2":
                            Tep = "ยี่สิบ";
                            break;
                        case "3":
                            Tep = "สามสิบ";
                            break;
                        case "4":
                            Tep = "สี่สิบ";
                            break;
                        case "5":
                            Tep = "ห้าสิบ";
                            break;
                        case "6":
                            Tep = "หกสิบ";
                            break;
                        case "7":
                            Tep = "เจ็ดสิบ";
                            break;
                        case "8":
                            Tep = "แปดสิบ";
                            break;
                        case "9":
                            Tep = "เก้าสิบ";
                            break;
                    }
                    cString = cString + Tep;
                    if (!string.IsNullOrEmpty(Strings.Mid(CDecs, 2, 1)))
                    {
                        if (Strings.Mid(CDecs, 2, 1) == "1" & Strings.Mid(CDecs, 1, 1) != "0")
                        {
                            cString = cString + "เอ็ดสตางค์";
                        }
                        else
                        {
                            cString = cString + Anum[Convert.ToInt32(Conversion.Val(Strings.Mid(CDecs, 2, 1)))];
                            cString = cString + "สตางค์";
                        }
                    }
                    else
                    {
                        cString = cString + "สตางค์";
                    }
                }
                else
                {
                    cString = cString + (Tep + cTep);
                }
            }
            catch (Exception ex)
            {
                cString = "";
            }
            return cString;
        }


        public static string Convert_Bath_EN(double n)
        {
            dynamic NSTR = null;
            dynamic n2 = null;
            dynamic i = null;
            dynamic P = null;
            dynamic Len_n = null;
            bool FlagBill = false;
            bool FlagMill = false;
            bool FlagThou = false;
            string[] E0_9 = {
			"",
			"ONE",
			"TWO",
			"THREE",
			"FOUR",
			"FIVE",
			"SIX",
			"SEVEN",
			"EIGHT",
			"NINE"
		};
            string[] E10_90 = {
			"",
			"TEN",
			"TWENTY",
			"THIRTY",
			"FORTY",
			"FIFTY",
			"SIXTY",
			"SEVENTY",
			"EIGHTY",
			"NINETY"
		};
            string[] E11_19 = {
			"",
			"ELEVEN",
			"TWELVE",
			"THIRTEEN",
			"FOURTEEN",
			"FIFTEEN",
			"SIXTEEN",
			"SEVENTEEN",
			"EIGHTEEN",
			"NINETEEN"
		};


            const string E0 = "ZERO";
            const string E100 = "HUNDRED";
            const string E1000 = "THOUSAND";
            const string E10000 = "TEN THOUSAND";
            const string E100000 = "HUNDRED THOUSAND";
            const string E10X6 = "MILLION";
            const string E10X12 = "BILLION";

            string cNum = null;
            string CDecs = null;
            double nAmount = 0;
            double nAmountDif = 0;

            try
            {
                NSTR = "";
                if (Information.IsNumeric(n) == false)
                {
                }
                else
                {
                    nAmount = Convert.ToDouble(Strings.Format(n, "0.00"));
                    nAmountDif = Convert.ToDouble(Strings.Format(((Convert.ToDouble(Strings.Format((nAmount % 1), "0.00")))) * 100, "#0"));

                    CDecs = nAmountDif.ToString("00");
                 
                    FlagBill = false;
                    FlagMill = false;
                    FlagThou = false;
                    //Convert Veriable n to n2 is string
                   
                    n2 = Strings.Format(nAmount - ((Convert.ToDouble(Strings.Format((nAmount % 1), "0.00")))), "#0");
                    //Save length of n2
                    Len_n = Strings.Len(n2);
                    if (Len_n > 13)
                    {
                        NSTR = "";

                    }
                    else
                    {
                        //Put length to i --> use loop
                        i = Len_n;

                        //Clear p --> use get number (1 digit)
                        P = 0;

                        if (Convert.ToDouble(Strings.Format(Convert.ToDouble(n2), "#0")) == 0)
                        {
                            //ใส่ "ZERO" ถ้ามีค่า 0
                            NSTR = E0;
                        }
                        else
                        {
                            //วน loop ใส่ข้อมูล
                            //===================  Loop have 6 case ============================
                            //  แต่ละกรณีเป็นการหาตำแหน่งของตัวเลขที่อยู่ โดยจะนับจากตัวแรกสุดไปยังตัวสุดท้าย
                            //  ตำแหน่งที่นับนั้นเป็น ตำแหน่งที่นับทีละ 3 จึง ใช้วิธีหาเศษ (MOD)
                            //  ตำแหน่งนั้นจะมีตั้งแต่หลัก หน่วย, สิบ,ร้อย, (พัน, ล้าน, ล้านล้าน เป็นกรณีพิเศษ ที่ต้องใส่เพิ่มเข้ามา)
                            //  ส่วนในหลัก ล้านนั้นจะดูจากตำแหน่ง หลักหน่วย( MOD ได้ 1) โดยจะมีการดักที่ i (pointer) โดยดูตำแหน่ง 13, 7
                            //================================================================
                            while (i > 0)
                            {
                                //เก็บตัวเลข
                                P = Convert.ToInt32(Strings.Mid(n2, (Len_n - i) + 1, 1));
                                if (P != 0)
                                {
                                    FlagBill = false;
                                    FlagMill = false;
                                    FlagThou = false;
                                }
                                switch ((int)(i % 3))
                                {
                                    //ตรวจสอบตำแหน่งที่อยู่
                                    case 1:
                                        if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                        {
                                            NSTR = NSTR + E0_9[P] + " ";
                                        }
                                        break;
                                    case 2:
                                        //ตรวจจสอบหลักสิบว่า เป็น Teen หรือไม่
                                        if (P == 1)
                                        {
                                            i = i - 1;
                                            P = Convert.ToInt32(Strings.Mid(n2, (Len_n - i) + 1, 1));
                                            if (P == 0)
                                            {
                                                //TEN
                                                NSTR = NSTR + E10_90[1] + " ";
                                            }
                                            else
                                            {
                                                //TEEN
                                                NSTR = NSTR + E11_19[P] + " ";
                                            }
                                        }
                                        else
                                        {
                                            //ใส่หลักสิบ
                                            if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                            {
                                                NSTR = NSTR + E10_90[P] + " ";
                                            }
                                        }
                                        break;
                                    case 0:
                                        //3,6,9...
                                        if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                        {
                                            NSTR = NSTR + E0_9[P] + " " + E100 + " ";
                                        }
                                        break;
                                }

                                //ใส่หน่วยพิเศษ (พัน, ล้าน, ล้านล้าน)
                                if ((i >= 12) & ((i - 1) % 12) == 0)
                                {
                                    NSTR = NSTR + E10X12 + " ";
                                    FlagBill = true;
                                }
                                else if ((i >= 6) & ((i - 1) % 6) == 0)
                                {
                                    if (!FlagBill)
                                    {
                                        NSTR = NSTR + E10X6 + " ";
                                        FlagMill = true;
                                    }
                                }
                                else if ((i >= 4) & ((i % 6) == 4))
                                {
                                    if (!(FlagBill | FlagMill))
                                    {
                                        NSTR = NSTR + E1000 + " ";
                                        FlagThou = true;
                                    }
                                }

                                i = i - 1;
                            }
                        }
                    }

                    if (Conversion.Val(CDecs) > 0)
                    {
                        NSTR = NSTR + " AND " + Strings.Right("00" + CDecs.ToString(), 2) + "/100";
                    }

                    NSTR = NSTR + " ONLY ";

                }
            }
            catch (Exception ex)
            {
                NSTR = "";
            }
            return NSTR;
        }


        public static string Convert_Bath_LA(double n)
        {
            dynamic NSTR = null;
            dynamic n2 = null;
            dynamic i = null;
            dynamic P = null;
            dynamic Len_n = null;
            bool FlagBill = false;
            bool FlagMill = false;
            bool FlagThou = false;
            string[] E0_9 = {
            "",
            "ໜຶ່ງ",
            "ສອງ",
            "ສາມ",
            "ສີ່",
            "ຫ້າ",
            "ຫົກ",
            "ເຈັດ",
            "ແປດ",
            "ເກົ້າ"
        };
            string[] E10_90 = {
            "",
            "ສິບ",
            "ຊາວ",
            "ສາມສິບ",
            "ສີ່ສິບ",
            "ຫ້າສິບ",
            "ຫົກສິບ",
            "ເຈັດສິບ",
            "ແປດສິບ",
            "ເກົ້າສິບ"
        };
            string[] E11_19 = {
            "",
            "ສິບເອັດ",
            "ສິບສອງ",
            "ສິບສາມ",
            "ສິບສີ່",
            "ສິບຫ້າ",
            "ສິບຫົກ",
            "ສິບເຈັດ",
            "ສິບແປດ",
            "ສິບເກົ້າ"
        };


            const string E0 = "ສູນ";
            const string E100 = "ຮ້ອຍ";
            const string E1000 = "ພັນ";
            const string E10000 = "ແສນ";
            const string E100000 = "ແສນ";
            const string E10X6 = "ລ້ານ";
            const string E10X12 = "ສິບລ້ານ";

            string cNum = null;
            string CDecs = null;
            double nAmount = 0;
            double nAmountDif = 0;

            try
            {
                NSTR = "";
                if (Information.IsNumeric(n) == false)
                {
                }
                else
                {
                    nAmount = Convert.ToDouble(Strings.Format(n, "0.00"));
                    nAmountDif = Convert.ToDouble(Strings.Format(((Convert.ToDouble(Strings.Format((nAmount % 1), "0.00")))) * 100, "#0"));

                    CDecs = nAmountDif.ToString("00");

                    FlagBill = false;
                    FlagMill = false;
                    FlagThou = false;
                    //Convert Veriable n to n2 is string

                    n2 = Strings.Format(nAmount - ((Convert.ToDouble(Strings.Format((nAmount % 1), "0.00")))), "#0");
                    //Save length of n2
                    Len_n = Strings.Len(n2);
                    if (Len_n > 13)
                    {
                        NSTR = "";

                    }
                    else
                    {
                        //Put length to i --> use loop
                        i = Len_n;

                        //Clear p --> use get number (1 digit)
                        P = 0;

                        if (Convert.ToDouble(Strings.Format(Convert.ToDouble(n2), "#0")) == 0)
                        {
                            //ใส่ "ZERO" ถ้ามีค่า 0
                            NSTR = E0;
                        }
                        else
                        {
                            //วน loop ใส่ข้อมูล
                            //===================  Loop have 6 case ============================
                            //  แต่ละกรณีเป็นการหาตำแหน่งของตัวเลขที่อยู่ โดยจะนับจากตัวแรกสุดไปยังตัวสุดท้าย
                            //  ตำแหน่งที่นับนั้นเป็น ตำแหน่งที่นับทีละ 3 จึง ใช้วิธีหาเศษ (MOD)
                            //  ตำแหน่งนั้นจะมีตั้งแต่หลัก หน่วย, สิบ,ร้อย, (พัน, ล้าน, ล้านล้าน เป็นกรณีพิเศษ ที่ต้องใส่เพิ่มเข้ามา)
                            //  ส่วนในหลัก ล้านนั้นจะดูจากตำแหน่ง หลักหน่วย( MOD ได้ 1) โดยจะมีการดักที่ i (pointer) โดยดูตำแหน่ง 13, 7
                            //================================================================
                            while (i > 0)
                            {
                                //เก็บตัวเลข
                                P = Convert.ToInt32(Strings.Mid(n2, (Len_n - i) + 1, 1));
                                if (P != 0)
                                {
                                    FlagBill = false;
                                    FlagMill = false;
                                    FlagThou = false;
                                }
                                switch ((int)(i % 3))
                                {
                                    //ตรวจสอบตำแหน่งที่อยู่
                                    case 1:
                                        if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                        {
                                            NSTR = NSTR + E0_9[P] + " ";
                                        }
                                        break;
                                    case 2:
                                        //ตรวจจสอบหลักสิบว่า เป็น Teen หรือไม่
                                        if (P == 1)
                                        {
                                            i = i - 1;
                                            P = Convert.ToInt32(Strings.Mid(n2, (Len_n - i) + 1, 1));
                                            if (P == 0)
                                            {
                                                //TEN
                                                NSTR = NSTR + E10_90[1] + " ";
                                            }
                                            else
                                            {
                                                //TEEN
                                                NSTR = NSTR + E11_19[P] + " ";
                                            }
                                        }
                                        else
                                        {
                                            //ใส่หลักสิบ
                                            if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                            {
                                                NSTR = NSTR + E10_90[P] + " ";
                                            }
                                        }
                                        break;
                                    case 0:
                                        //3,6,9...
                                        if (i == 6)
                                        {
                                            if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                            {
                                                NSTR = NSTR + E0_9[P];
                                            }
                                        }
                                        else
                                        {
                                            if ((!(FlagBill | FlagMill | FlagThou)) & (P != 0))
                                            {
                                                NSTR = NSTR + E0_9[P] + " " + E100 + " ";
                                            }
                                        }

                                        break;
                                }

                                //ใส่หน่วยพิเศษ (พัน, ล้าน, ล้านล้าน)
                                if ((i >= 12) & ((i - 1) % 12) == 0)
                                {
                                    NSTR = NSTR + E10X12 + " ";
                                    FlagBill = true;
                                }
                                else if ((i >= 6) & ((i - 1) % 6) == 0)
                                {
                                    if (!FlagBill)
                                    {
                                        NSTR = NSTR + E10X6 + " ";
                                        FlagMill = true;
                                    }
                                }
                                else if ((i >= 5) & ((i - 1) % 5) == 0)
                                {
                                    if (!FlagBill)
                                    {
                                        NSTR = NSTR + E100000 + " ";
                                        FlagMill = true;
                                    }
                                }
                                else if ((i >= 4) & ((i % 6) == 4))
                                {
                                    if (!(FlagBill | FlagMill))
                                    {
                                        NSTR = NSTR + E1000 + " ";
                                        FlagThou = true;
                                    }
                                }

                                i = i - 1;
                            }
                        }
                    }

                    if (Conversion.Val(CDecs) > 0)
                    {
                        //NSTR = NSTR + " AND " + Strings.Right("00" + CDecs.ToString(), 2) + "/100";
                    }

                    //NSTR = NSTR + " ONLY ";

                }
            }
            catch (Exception ex)
            {
                NSTR = "";
            }
            return NSTR;
        }

        public static bool ValidEmail(string strCheck)
        {
            try
            {
                bool bCK = false;
                string strDomainType = null;

                const string sInvalidChars = "!#$%^&*()=+{}[]|\\;:'/?>,< ";
                int i = 0;

                //Check to see if there is a double quote
                bCK = !(Strings.InStr(1, strCheck, (Strings.Chr(34)).ToString()) > 0);
                if (!bCK)
                    return false;

                //Check to see if there are consecutive dots
                bCK = !(Strings.InStr(1, strCheck, "..") > 0);
                if (!bCK)
                    return false;

                // Check for invalid characters.
                if (Strings.Len(strCheck) > Strings.Len(sInvalidChars))
                {
                    for (i = 1; i <= Strings.Len(sInvalidChars); i++)
                    {
                        if (Strings.InStr(strCheck, Strings.Mid(sInvalidChars, i, 1)) > 0)
                        {
                            bCK = false;
                            return false;
                        }
                    }
                }
                else
                {
                    for (i = 1; i <= Strings.Len(strCheck); i++)
                    {
                        if (Strings.InStr(sInvalidChars, Strings.Mid(strCheck, i, 1)) > 0)
                        {
                            bCK = false;
                            return false;
                        }
                    }
                }


                if (Strings.InStr(1, strCheck, "@") > 1)
                {
                    bCK = Strings.Len(Strings.Left(strCheck, Strings.InStr(1, strCheck, "@") - 1)) > 0;
                }
                else
                {
                    bCK = false;
                }
                if (!bCK)
                    return false;

                strCheck = Strings.Right(strCheck, Strings.Len(strCheck) - Strings.InStr(1, strCheck, "@"));
                bCK = !(Strings.InStr(1, strCheck, "@") > 0);

                if (!bCK)
                    return false;

                strDomainType = Strings.Right(strCheck, Strings.Len(strCheck) - Strings.InStr(1, strCheck, "."));
                bCK = Strings.Len(strDomainType) > 0 & Strings.InStr(1, strCheck, ".") < Strings.Len(strCheck);
                if (!bCK)
                    return false;

                strCheck = Strings.Left(strCheck, Strings.Len(strCheck) - Strings.Len(strDomainType) - 1);
                while (!(Strings.InStr(1, strCheck, ".") <= 1))
                {
                    if (Strings.Len(strCheck) >= Strings.InStr(1, strCheck, "."))
                    {
                        strCheck = Strings.Left(strCheck, Strings.Len(strCheck) - (Strings.InStr(1, strCheck, ".") - 1));
                    }
                    else
                    {
                        bCK = false;
                        return false;
                    }
                }
                if (strCheck == "." | Strings.Len(strCheck) == 0)
                {
                    bCK = false;
                    return false;
                }

                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }

        }
    }
}

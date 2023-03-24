using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HI.MG
{
    public static class Msg
    {
        private static string _CopyRight = "|Copyright © 2015, Wisdom ERP Co., Ltd. All rights reserved. |Copyright © 2015, Wisdom ERP Co., Ltd. All rights reserved. ";
        private static string _SaveData = "|Would You want to save the data ? | คุณต้องการทำการบันทึกข้อมูลใช่หรือไม่ ? ";
        private static string _DeleteData = "|Would You want to delete the data ? | คุณต้องการทำการลบข้อมูลใช่หรือไม่ ? ";
        private static string _SaveDataComplete = "| Save Data Complete !!! | ระบบทำการบันทึกข้อมูลเรียบร้อยแล้ว !!! ";
        private static string _DeleteDataComplete = "| Delete Data Complete !!! | ระบบทำการลบข้อมูลเรียบร้อยแล้ว !!! ";
        private static string _SaveDataNotComplete = "| Save Data Not Complete !!! | ระบบไม่สามารถทำการบันทึกข้อมูลได้  เนื่องจากเกิดข้อผิดพลาดบางประการ !!! ";
        private static string _DeleteDataNotComplete = "| Delete Data Not Complete !!! | ระบบไม่สามารถทำการลบข้อมูลได้  เนื่องจากเกิดข้อผิดพลาดบางประการ !!! ";
        private static string _SelectData = "| Please Select Data | กรุณาทำการเลือกข้อมูล ";
        private static string _InputData = "| Please Input Data | กรุณาทำการระบุข้อมูล ";
        private static string _PleaseWait = "| Please Waiting.... System Processing | กรุณารอสักครู่ระบบกำลังทำการประมวลผลข้อมูล.... ";
        private static string _ExiHSystem = "| Exit System  ? | คุณต้องการออกจากระบบใช่หรือไม่  ? ";
        private static string _GenDocument = "| System Auto Generate | ระบบสร้างให้อัตโนมัติ  ";

        public static  string CopyRight
        {

            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _CopyRight.Split('|')[2].ToString();
                }
                else
                {
                    return _CopyRight.Split('|')[1].ToString();
                }

            }
            set { _CopyRight = value; }

        }

        public static  string SaveData
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _SaveData.Split('|')[2].ToString();
                }
                else
                {
                    return _SaveData.Split('|')[1].ToString();
                }
            }
            set { _SaveData = value; }
        }

        public static  string DeleteData
        {

            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _DeleteData.Split('|')[2].ToString();
                }
                else
                {
                    return _DeleteData.Split('|')[1].ToString();
                }
            }
            set { _DeleteData = value; }
        }

        public static   string SaveDataComplete
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _SaveDataComplete.Split('|')[2].ToString();
                }
                else
                {
                    return _SaveDataComplete.Split('|')[1].ToString();
                }
            }
            set { _SaveDataComplete = value; }
          
        }

        public static  string DeleteDataComplete
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _DeleteDataComplete.Split('|')[2].ToString();
                }
                else
                {
                    return _DeleteDataComplete.Split('|')[1].ToString();
                }
            }
            set { _DeleteDataComplete = value; }
            
        }

        public static  string SaveDataNotComplete
        {

            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _SaveDataNotComplete.Split('|')[2].ToString();
                }
                else
                {
                    return _SaveDataNotComplete.Split('|')[1].ToString();
                }
            }
            set { _SaveDataNotComplete = value; }
        }

        public static  string DeleteDataNotComplete
        {

            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _DeleteDataNotComplete.Split('|')[2].ToString();
                }
                else
                {
                    return _DeleteDataNotComplete.Split('|')[1].ToString();
                }
            }
            set { _DeleteDataNotComplete = value; }
        }

        public static  string MSelect
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _SelectData.Split('|')[2].ToString();
                }
                else
                {
                    return _SelectData.Split('|')[1].ToString();
                }
            }
            set { _SelectData = value; }
        }

        public static  string Input
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _InputData.Split('|')[2].ToString();
                }
                else
                {
                    return _InputData.Split('|')[1].ToString();
                }
            }
            set { _InputData = value; }
        }

        public static   string PleaseWait
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _PleaseWait.Split('|')[2].ToString();
                }
                else
                {
                    return _PleaseWait.Split('|')[1].ToString();
                }

            }
            set { _PleaseWait = value; }
        }

        public static  string ExiHSystem
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return _ExiHSystem.Split('|')[2].ToString();
                }
                else
                {
                    return _ExiHSystem.Split('|')[1].ToString();
                }
            }
            set { _ExiHSystem = value; }
        }

        public static   string GenDocument
        {
            get
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    return  _GenDocument.Split('|')[2].ToString();
                }
                else
                {
                    return _GenDocument.Split('|')[1].ToString();
                }
            }
            set { _GenDocument = value; }
        }

        public static System.Windows.Forms.DialogResult ShowError(string Prompt)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                return System.Windows.Forms.MessageBox.Show(Prompt, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static System.Windows.Forms.DialogResult Show(string Prompt)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt);
            }
            catch (Exception )
            {
                return System.Windows.Forms.MessageBox.Show(Prompt);
            }
        }

        public static System.Windows.Forms.DialogResult Show(string Prompt, string Title)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt, Title);
            }
            catch (Exception ex)
            {
                return System.Windows.Forms.MessageBox.Show(Prompt, Title);
            }
        }

        public static System.Windows.Forms.DialogResult Show(string Prompt, string Title, System.Windows.Forms.MessageBoxButtons Buttons)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt, Title, Buttons);
            }
            catch (Exception ex)
            {
                return System.Windows.Forms.MessageBox.Show(Prompt, Title, Buttons);
            }
        }

        public static System.Windows.Forms.DialogResult Show(string Prompt, string Title, System.Windows.Forms.MessageBoxButtons Buttons, System.Windows.Forms.MessageBoxIcon Icon)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt, Title, Buttons, Icon);
            }
            catch (Exception ex)
            {
                return System.Windows.Forms.MessageBox.Show(Prompt, Title, Buttons, Icon);
            }
        }

        public static System.Windows.Forms.DialogResult Show(string Prompt, string Title, System.Windows.Forms.MessageBoxButtons Buttons, System.Windows.Forms.MessageBoxIcon Icon, System.Windows.Forms.MessageBoxDefaultButton DefaultButton)
        {
            try
            {
                return DevExpress.XtraEditors.XtraMessageBox.Show(Prompt, Title, Buttons, Icon, DefaultButton);
            }
            catch (Exception ex)
            {
                return System.Windows.Forms.MessageBox.Show(Prompt, Title, Buttons, Icon, DefaultButton);
            }
        }
    }
}

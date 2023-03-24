using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace HI.MG
{
    public static class ShowMsg
    {
        public enum ProcessType : int
        {
            mSave = 0,
            mDelete = 1
        }

        public enum InvalidType : int
        {
            SelectData = 0,
            InputData = 1
        }

        public static void mProcessComplete(ProcessType _ProcType, string _MessageTile)
        {
            switch (_ProcType)
            {
                case ProcessType.mSave:
                    HI.MG.Msg.Show(HI.MG.Msg.SaveDataComplete, _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ProcessType.mDelete:
                    HI.MG.Msg.Show(HI.MG.Msg.DeleteDataComplete, _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        public static void mProcessNotComplete(ProcessType _ProcType, string _MessageTile)
        {
            switch (_ProcType)
            {
                case ProcessType.mSave:
                    HI.MG.Msg.Show(HI.MG.Msg.SaveDataNotComplete, _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case ProcessType.mDelete:
                    HI.MG.Msg.Show(HI.MG.Msg.DeleteDataNotComplete, _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        public static bool mConfirmProcess(ProcessType _ProcType, string _MessageSpeacial = "", string _MessageTile = "|! Please Confirm | กรุณายืนยัน |! Please Confirm |! Please Confirm ")
        {
            string _Data = _MessageSpeacial;
            string _Title = _MessageTile;
            if (_Title.Split('|').Length < 3)
            {
                for (int I = _Title.Split('|').Length; I <= 3; I++)
                {
                    _Title = _Title + "|" + _Title;
                }
            }

            switch (_ProcType)
            {
                case ProcessType.mSave:
                    if (HI.MG.Msg.Show(HI.MG.Msg.SaveData + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _Title.Split('|')[(int)HI.ST.Lang.Language].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ProcessType.mDelete:
                    if (HI.MG.Msg.Show(HI.MG.Msg.DeleteData + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _Title.Split('|')[(int)HI.ST.Lang.Language].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
        }


        public static bool mConfirmProcess(string _Message, long _MessageID, string _MessageSpeacial = "", string _MessageTile = "|! Please Confirm | กรุณายืนยัน |! Please Confirm |! Please Confirm ")
        {
            string Strsql = null;
            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;

            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + " VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);

            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }

            }


            if (HI.MG.Msg.Show(_Msg + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile.Split('|')[(int)HI.ST.Lang.Language].ToString() + " (" + _MessageID.ToString() + ")  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public static bool mConfirmProcessDefaultNo(ProcessType _ProcType, string _MessageSpeacial = "", string _MessageTile = "|! Please Confirm | กรุณายืนยัน |! Please Confirm |! Please Confirm ")
        {
            string _Data = _MessageSpeacial;
            string _Title = _MessageTile;
            if (_Title.Split('|').Length < 3)
            {
                for (int I = _Title.Split('|').Length; I <= 3; I++)
                {
                    _Title = _Title + "|" + _Title;
                }
            }

            switch (_ProcType)
            {
                case ProcessType.mSave:
                    if (HI.MG.Msg.Show(HI.MG.Msg.SaveData + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _Title.Split('|')[(int)HI.ST.Lang.Language].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ProcessType.mDelete:
                    if (HI.MG.Msg.Show(HI.MG.Msg.DeleteData + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _Title.Split('|')[(int)HI.ST.Lang.Language].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
        }

        public static bool mConfirmProcessDefaultNo(string _Message, long _MessageID, string _MessageSpeacial = "", string _MessageTile = "|! Please Confirm | กรุณายืนยัน |! Please Confirm |! Please Confirm ")
        {
            string Strsql = null;
            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;

            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + " VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);

            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }

            }

            if (HI.MG.Msg.Show(_Msg + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile.Split('|')[(int)HI.ST.Lang.Language].ToString() + " (" + _MessageID.ToString() + ")  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void mInvalidData(InvalidType _Invalid, string _MessageTile, string _MessageSpeacial = "")
        {
            switch (_Invalid)
            {
                case InvalidType.SelectData:
                    HI.MG.Msg.Show(HI.MG.Msg.MSelect + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case InvalidType.InputData:
                    HI.MG.Msg.Show(HI.MG.Msg.Input + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    HI.MG.Msg.Show(HI.MG.Msg.Input + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

        }

        public static void mInvalidData(string _Message, long _MessageID, string _MessageTile, string _MessageSpeacial = "")
        {
            string Strsql = null;
            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;

            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + " VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }
            }

            HI.MG.Msg.Show(_Msg + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile + " (" + _MessageID.ToString() + ")  ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void mInfo(string _Message, long _MessageID, string _MessageTile, string _MessageSpeacial = "", MessageBoxIcon MsgIcon = MessageBoxIcon.Information)
        {
            string Strsql = null;
            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;


            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + " VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_SYSTEM);


            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }

            }

            HI.MG.Msg.Show(_Msg + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile + " (" + _MessageID.ToString() + ")  ", MessageBoxButtons.OK, MsgIcon);
        }


        public static void mProcessError(long _MessageID, string _Message = "", string _MessageTile = "System Alert", System.Windows.Forms.MessageBoxIcon oIcon = MessageBoxIcon.Error, string _MessageSpeacial = "")
        {
            string Strsql = null;

            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;

            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + " VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_SYSTEM);

            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }
            }

            HI.MG.Msg.Show(_Msg + ((!string.IsNullOrEmpty(_MessageSpeacial) ? "   ( " + _MessageSpeacial + ")" : "")).ToString(), _MessageTile + " (" + _MessageID.ToString() + ")  ", MessageBoxButtons.OK, oIcon);

        }


        public static bool mProcessExiHSystem(string _MessageTile)
        {
            if (HI.MG.Msg.Show(HI.MG.Msg.ExiHSystem, _MessageTile, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetMessage(string _Message, long _MessageID)
        {
            string Strsql = null;
            Strsql = "SELECT TOP 1 FTMessageEN,FTMessageTH  ";
            Strsql += Environment.NewLine + "  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge WITH(NOLOCK) ";
            Strsql += Environment.NewLine + " WHERE  FNHSysMessageID=" + _MessageID + "";

            DataTable _DT = HI.Conn.SQLConn.GetDataTable(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);
            string _Msg = _Message;

            if (_DT.Rows.Count <= 0)
            {
                Strsql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSystemMessasge(FNHSysMessageID,FTMessageEN,FTMessageTH)";
                Strsql += Environment.NewLine + "  VALUES(" + _MessageID + ",'" + HI.UL.ULF.rpQuoted(_Msg) + "','" + HI.UL.ULF.rpQuoted(_Msg) + "') ";
                HI.Conn.SQLConn.ExecuteOnly(Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM);

            }
            else
            {
                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                    _Msg = _DT.Rows[0][1].ToString();
                }
                else
                {
                    _Msg = _DT.Rows[0][0].ToString();
                }

            }

            if ((HI.ST.SysInfo.Admin))
            {
                _Msg = _Msg + " (" + _MessageID.ToString() + ")";
            }

            return _Msg;

        }
    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace HI.ST
{
    public static class Lang
    {
        public enum eLang : int
        {
            EN = 1,
            TH = 2,
            VT = 3,
            KM = 4,
            BM = 5,
            LAO = 6,
            CH = 7
        }

        public static bool StateInsertLang{get; set; }
        public static eLang Language { get; set; }

        public static void ActiveLangChange(object MainObj, string _ModuleName)
        {
        }

        public static void InsertLanguage(object oForm)
        {
            SysLanguage oSysLang = default(SysLanguage);
            string _formName = oForm.GetType().GetProperty("Name").GetValue(oForm, null).ToString().Trim();

            try
            {
                string _Str = "";

                _Str = "SELECT TOP 1  FTFormName  ";
                _Str += Constants.vbCrLf + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSysLanguage WITH(Nolock) ";
                _Str += Constants.vbCrLf + " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(_formName) + "'";

                if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "")))
                {
                    oSysLang = new SysLanguage();


                    if (oForm is DevExpress.XtraEditors.XtraForm)
                    {
                        try
                        {
                            oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleID, _formName, oForm);


                        }
                        catch (Exception ex2)
                        {
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex);
            }
            finally
            {
                oSysLang = null;
            }
        }


        public static void SP_SETxLanguage(Control pFrom)
        {


            if (pFrom.Controls.Count > 0)
            {
                foreach (Control ObjSub in pFrom.Controls)
                {
                    SP_SETxLanguageSub(ObjSub);
                }
            }

            SP_SETxLanguageControl(pFrom, true);

        }

        private static void SP_SETxLanguageSubGridBand(DevExpress.XtraGrid.Views.BandedGrid.GridBand oMBand) {
            try {
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in oMBand.Children  )
                         {
                                    SP_SETxLanguageControl(oBand);

                                    if (oBand.HasChildren ==true) { SP_SETxLanguageSubGridBand(oBand); };

                            }
            }

            catch { }
        }

        private static void SP_SETxLanguageSub(Control pObj)
        {

            try
            {
                switch (HI.ENM.Control.GeTypeControl(pObj))
                {

                    case (ENM.Control.ControlType.LabelControl):
                    case (ENM.Control.ControlType.GroupControl):
                    case (ENM.Control.ControlType.CheckEdit):
                    case (ENM.Control.ControlType.RadioButton):
                    case (ENM.Control.ControlType.SimpleButton):
                    case (ENM.Control.ControlType.DockPanel):

                        SP_SETxLanguageControl(pObj);

                        break;
                    case (ENM.Control.ControlType.LayoutControl):
                        foreach (DevExpress.XtraLayout.LayoutControlItem ObjItem in ((DevExpress.XtraLayout.LayoutControl)pObj).Root.Items)
                        {   
                                    SP_SETxLanguageControl(ObjItem);
                        }
                        break;

                    case (ENM.Control.ControlType.GridControl):

                        object ogvGrid = null;

                        if (((DevExpress.XtraGrid.GridControl )pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl )pObj).MainView;
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Columns)
                            {
                                    SP_SETxLanguageControl(oGridCol);                              
                            }
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Bands)
                            {
                                    SP_SETxLanguageControl(oBand);


                                    if (oBand.Collection.Count > 0) { SP_SETxLanguageSubGridBand(oBand); };

                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Columns)
                            {
                                SP_SETxLanguageControl(oGridCol);
                            }
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Bands)
                            {
                                SP_SETxLanguageControl(oBand);


                                if (oBand.Collection.Count > 0) { SP_SETxLanguageSubGridBand(oBand); };

                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (object oGridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)ogvGrid).Columns)
                            {
                                if (oGridCol is DevExpress.XtraGrid.Columns.GridColumn)
                                {
                                    SP_SETxLanguageControl(oGridCol);
                                }
                            }
                        }
                        break;

                    case (ENM.Control.ControlType.PivotGridControl):
                        foreach (DevExpress.XtraPivotGrid.PivotGridField ObjItem in ((DevExpress.XtraPivotGrid.PivotGridControl)pObj).Fields)
                        {
                            try
                            {
                                SP_SETxLanguageControl(ObjItem);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        break;

                    case (ENM.Control.ControlType.XtraTabControl):
                        foreach (DevExpress.XtraTab.XtraTabPage oTabPage in ((DevExpress.XtraTab.XtraTabControl)pObj).TabPages)
                        {
                            SP_SETxLanguageControl(oTabPage);
                        }

                        break;

                    case ENM.Control.ControlType.TreeList:
                        foreach (DevExpress.XtraTreeList.Columns.TreeListColumn ObjItem in ((DevExpress.XtraTreeList.TreeList)pObj).Columns)
                        {
                           
                                    SP_SETxLanguageControl(ObjItem);
                        
                        }

                        break;
                    case ENM.Control.ControlType.LookUpEdit:

                        string objectnamenew = ((DevExpress.XtraEditors.LookUpEdit)pObj).Name.ToString();

                        foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.LookUpEdit)pObj).Properties.Columns)
                        {
                            string K = Col.FieldName.ToString();
                            if (Col.Visible)
                            {
                               
                            };
                        }

                        break;

                    case ENM.Control.ControlType.RepositoryItemLookUpEdit:

                        //string objectname = ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Name.ToString();

                        //foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Columns)
                        //{
                        //    if (Col.Visible)
                        //    {                              
                        //    };
                        //}

                        break;
                    default:

                        break;
                }

                if (pObj.Controls.Count > 0)
                {
                    foreach (Control Obj in pObj.Controls)
                    {
                        SP_SETxLanguageSub(Obj);
                    }
                }


            }
            catch (Exception ex)
            {
            }

        }


        public static void SP_SETxLanguageControl(object pObj, bool StateForm = false)
        {
            try {


                int nCut = (int)HI.ST.Lang.Language;
                string tTag = null;
                string[] atTag = null;

                atTag = ("" + pObj.GetType().GetProperty("Tag").GetValue(pObj, null).ToString()).Split('|');

                try
                {
                    if (atTag.Length <= 3)
                        return;
                }
                catch (Exception ex)
                {
                }

                try
                {
                    tTag = atTag[nCut];
                }
                catch (Exception ex)
                {
                    tTag = "";
                    return;
                }

                if (tTag == "") { return; };

                try
                {
                    pObj.GetType().GetProperty("Text").SetValue(pObj, tTag, null);
                    return;

                }
                catch (Exception ex)
                {
                    try
                    {
                        pObj.GetType().GetProperty("Caption").SetValue(pObj, tTag, null);
                        return;
                    }
                    catch (Exception ex3)
                    {
                    }
                }

            } catch { }
           

        }

        private static void SP_SETxLanguageControlTreeList(Control pObj)
        {
            int nCut = (int)HI.ST.Lang.Language;
            string tTag = null;
            string[] atTag = null;

            foreach (Control Obj in pObj.Controls)
            {
                try
                {
                    atTag = ("" + Obj.GetType().GetProperty("Tag").GetValue(Obj, null).ToString()).Split('|');
                    if (atTag[0].Trim() == "1")
                    {
                        Obj.GetType().GetProperty("Caption").SetValue(Obj, atTag[nCut], null);
                    }
                    return;
                }
                catch (Exception ex)
                {
                }
                SP_SETxLanguageControlTreeList(Obj);

            }
        }

        private static void SP_SETxLanguageControlNvarbar(Control pObj)
        {
            int nCut = (int)HI.ST.Lang.Language;
            string tTag = null;
            string[] atTag = null;

            foreach (Control Obj in pObj.Controls)
            {
           
                try
                {
                    atTag = ("" + Obj.GetType().GetProperty("Tag").GetValue(Obj, null).ToString()).Split('|');
                    if (atTag[0].Trim() == "1")
                    {
                        Obj.GetType().GetProperty("Caption").SetValue(Obj, atTag[nCut], null);
                    }
                    return;
                }
                catch (Exception ex)
                {
                }

                SP_SETxLanguageControlNvarbar(Obj);

            }
        }
    }
}

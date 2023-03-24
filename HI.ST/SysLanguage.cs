using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DevExpress.XtraLayout;
using System.Windows.Forms;

namespace HI.ST
{
     public class SysLanguage
    {

        public SysLanguage()
        {
        }

        private DataTable _DtObject;
        private string GetObjLang(string pModuleName, string pFormName, string pObjectName, string _ObjTag)
        {
            string tSql = null;
            string _Tag = "1";

            if (!string.IsNullOrEmpty(_ObjTag))
            {
                _Tag = _ObjTag;
            }

            if (_DtObject == null)
            {
                tSql = "SELECT '|'  + ISNULL(FTLangEN,'')  +'|'+ ISNULL(FTLangTH,'') + '|'+ ISNULL(FTLangVT,'') +'|'+ ISNULL(FTLangKM,'') +'|'+ ISNULL(FTLangBM,'') + '|'+ ISNULL(FTLangLAO,'') +'|'+ ISNULL(FTLangCH,'') AS LangT,FTObjectName ";
                tSql += Constants.vbCrLf + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSysLanguage WITH(Nolock) ";
                tSql += Constants.vbCrLf + " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(pFormName) + "' ";

                _DtObject = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_LANG);

            }

            try
            {
                foreach (DataRow R in _DtObject.Select(" FTObjectName='" + HI.UL.ULF.rpQuoted(pObjectName.Trim()) + "'"))
                {
                    return _Tag + (R["LangT"]).ToString();
                }

                return "";
                //

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return "";
        }

        public bool LoadObjectLanguage(string pModuleName, string pFormName, object pObj, bool StateLoop = false)
        {
            try
            {  
                string oItem = null;
                string _Tag = "";
                string _ModuleName = "";
                string _ModuleID = "";
                string _MenuName = "";
                string _FTOptionMouseScoll = "";

                if (!(StateLoop))
                {
                    if ((HI.ST.SysInfo.Admin))
                    {
                        HI.ST.Lang.InsertLanguage(pObj);
                    }

                    string _Qry = null;
                    _Qry = "SELECT '|'  + ISNULL(FTLangEN,'')  +'|'+ ISNULL(FTLangTH,'') + '|'+ ISNULL(FTLangVT,'') +'|'+ ISNULL(FTLangKM,'') +'|'+ ISNULL(FTLangBM,'') + '|'+ ISNULL(FTLangLAO,'') +'|'+ ISNULL(FTLangCH,'') AS LangT,FTObjectName ";
                    _Qry += Constants.vbCrLf + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSysLanguage WITH(Nolock) ";
                    _Qry += Constants.vbCrLf + " WHERE FTFormName='" + HI.UL.ULF.rpQuoted(pFormName) + "' ";

                    _DtObject = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM);

                }
                string _pObjName = "";
                try { _pObjName = pObj.GetType().GetProperty("Name").GetValue(pObj, null).ToString().Trim(); }
                catch { };
              
                switch (HI.ENM.Control.GeTypeControl(pObj))
                {

                    case ENM.Control.ControlType.XtraForm:
                        try
                        {
                            _ModuleName = HI.ST.SysInfo.ModuleName;
                            _ModuleID = HI.ST.SysInfo.ModuleID;
                            _MenuName = HI.ST.SysInfo.MenuName;
                            _FTOptionMouseScoll = HI.ST.SysInfo.FTOptionMouseScoll ;
                        }
                        catch (Exception ex)
                        {
                        }

                        DevExpress.XtraEditors.XtraForm mObj = (DevExpress.XtraEditors.XtraForm)pObj;

                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, "");
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            mObj.Tag = _ModuleName + "|" + oItem.Split('|')[1] + "|" + oItem.Split('|')[2] + "|" + oItem.Split('|')[3] + "|" + oItem.Split('|')[4] + "|" + oItem.Split('|')[5] + "|" + oItem.Split('|')[6] + "|" + oItem.Split('|')[7] + "|" + _ModuleID + "|" + _MenuName + "|" + _FTOptionMouseScoll;
                        }
                        else
                        {
                            mObj.Tag = _ModuleName + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + mObj.Text.ToString() + "|" + _ModuleID + "|" + _MenuName + "|" + _FTOptionMouseScoll;
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.LabelControl:
                        
                        _Tag = "1";

                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            ((Control)pObj).Tag = oItem;
                        }
                        else
                        {
                            ((Control)pObj).Tag = _Tag + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString();
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;

                    case ENM.Control.ControlType.DockPanel:

                        _Tag = "1";

                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            ((Control)pObj).Tag = oItem;
                        }
                        else
                        {
                            ((Control)pObj).Tag = _Tag + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString();
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.SimpleButton:

                        _Tag = "1";

                        DevExpress.XtraEditors.SimpleButton mSmbObj = (DevExpress.XtraEditors.SimpleButton)pObj;
                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            mSmbObj.Tag = oItem;
                        }
                        else
                        {
                            mSmbObj.Tag = _Tag + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString() + "|" + mSmbObj.Text.ToString();
                        }
                       
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.CheckEdit:

                        DevExpress.XtraEditors.CheckEdit mChkEObj = (DevExpress.XtraEditors.CheckEdit)pObj;

                        try
                        {
                            _Tag = ("" + (mChkEObj.Tag.ToString()).Split('|')[0]);
                        }
                        catch (Exception ex)
                        {
                            _Tag = "1";
                        }

                        if (string.IsNullOrEmpty(_Tag))
                        {
                            _Tag = "1";
                        }

                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            mChkEObj.Tag = oItem;
                        }
                        else
                        {
                            mChkEObj.Tag = _Tag + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString() + "|" + mChkEObj.Text.ToString();
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.GroupControl:

                        _Tag = "1";

                        DevExpress.XtraEditors.GroupControl mGrpObj = (DevExpress.XtraEditors.GroupControl)pObj;

                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            mGrpObj.Tag = oItem;
                        }
                        else
                        {
                            mGrpObj.Tag = _Tag + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString() + "|" + mGrpObj.Text.ToString();
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.XtraTabControl:
                        foreach (DevExpress.XtraTab.XtraTabPage oTabPage in ((DevExpress.XtraTab.XtraTabControl)pObj).TabPages)
                        {
                            oItem = GetObjLang(pModuleName, pFormName, oTabPage.Name.ToString().Trim()  ,"");
                            if (!string.IsNullOrEmpty(oItem))
                            {
                                oTabPage.Tag = oItem;
                            }
                            else
                            {
                                oTabPage.Tag = "1|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString() + "|" + oTabPage.Text.ToString();
                            }
                            Lang.SP_SETxLanguageControl(oTabPage);
                        }

                        break;
                    case ENM.Control.ControlType.RadioButton:

                        _Tag = "1";

                        string _text = pObj.GetType().GetProperty("Text").GetValue(pObj, null).ToString().Trim();
                        oItem = GetObjLang(pModuleName, pFormName, _pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                         
                            pObj.GetType().GetProperty("Tag").SetValue(pObj, oItem, null);
                        }
                        else
                        {
                            pObj.GetType().GetProperty("Tag").SetValue(pObj, (_Tag + "|" + _text + "|" + _text + "|" + _text + "|" + _text + "|" + _text + "|" + _text + "|" + _text), null);
                           
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.XtraTabPage:

                        _Tag = "1";

                        DevExpress.XtraTab.XtraTabPage _xTabPageObj = (DevExpress.XtraTab.XtraTabPage)pObj;
                        oItem = GetObjLang(pModuleName, pFormName,_pObjName, _Tag);
                        if (!string.IsNullOrEmpty(oItem))
                        {
                            _xTabPageObj.Tag = oItem;
                        }
                        else
                        {
                            _xTabPageObj.Tag = _Tag + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString() + "|" + _xTabPageObj.Text.ToString();
                        }
                        Lang.SP_SETxLanguageControl(pObj);
                        break;
                    case ENM.Control.ControlType.GridControl:
                        object ogvGrid = null;

                       

                        if (((DevExpress.XtraGrid.GridControl )pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;

                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Columns)
                            {
                               
                                   oItem = GetObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), "");
                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        oGridCol.Tag = oItem;
                                    }
                                    else
                                    {
                                        oGridCol.Tag = "1|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString();
                                    }
                                    Lang.SP_SETxLanguageControl(oGridCol);
                                
                            }

                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Bands)
                            {
                                
                                    oItem = GetObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), "");
                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        oBand.Tag = oItem;
                                    }
                                    else
                                    {
                                        oBand.Tag = "1|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString();
                                    }
                                    Lang.SP_SETxLanguageControl(oBand);

                                    if (oBand.HasChildren ==true) { LoadObjectLanguageBand(pModuleName, pFormName, oBand); };
                               
                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;

                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Columns)
                            {

                                oItem = GetObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), "");
                                if (!string.IsNullOrEmpty(oItem))
                                {
                                    oGridCol.Tag = oItem;
                                }
                                else
                                {
                                    oGridCol.Tag = "1|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString();
                                }
                                Lang.SP_SETxLanguageControl(oGridCol);

                            }

                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Bands)
                            {

                                oItem = GetObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), "");
                                if (!string.IsNullOrEmpty(oItem))
                                {
                                    oBand.Tag = oItem;
                                }
                                else
                                {
                                    oBand.Tag = "1|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString();
                                }
                                Lang.SP_SETxLanguageControl(oBand);

                                if (oBand.HasChildren == true) { LoadObjectLanguageBand(pModuleName, pFormName, oBand); };

                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (DevExpress.XtraGrid.Columns.GridColumn oGridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)ogvGrid).Columns)
                            {
                                    oItem = GetObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), "");
                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        oGridCol.Tag = oItem;
                                    }
                                    else
                                    {
                                        oGridCol.Tag = "1|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString() + "|" + oGridCol.Caption.ToString();
                                    }

                                    Lang.SP_SETxLanguageControl(oGridCol);
                                    if (oGridCol.ColumnEdit != null) {
                                        LoadObjectLanguage(pModuleName, pFormName, oGridCol.ColumnEdit, true);
                                    };

                            }
                        }

                        break;
                    case ENM.Control.ControlType.Bar:

                        foreach (DevExpress.XtraBars.BarSubItem oBarItem in ((DevExpress.XtraBars.Bar)pObj).ItemLinks)
                        {
                            oItem = GetObjLang(pModuleName, pFormName, oBarItem.Name.ToString().Trim(), "");
                            if (!string.IsNullOrEmpty(oItem))
                            {
                                oBarItem.Tag = oItem;
                            }
                            else
                            {
                                oBarItem.Tag = "1|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString() + "|" + oBarItem.Caption.ToString();
                            }
                            Lang.SP_SETxLanguageControl(oBarItem);
                        }

                        break;
                    case ENM.Control.ControlType.LayoutControl:
                        foreach (DevExpress.XtraLayout.LayoutControlItem ObjItem in ((DevExpress.XtraLayout.LayoutControl)pObj).Root.Items)
                        {
                            
                                    _Tag = "1";

                                    oItem = GetObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), _Tag);

                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        ObjItem.Tag = oItem;
                                    }
                                    else
                                    {
                                        ObjItem.Tag = _Tag + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString() + "|" + ObjItem.Text.ToString();
                                    }
                                    Lang.SP_SETxLanguageControl(ObjItem);

                                
                           
                        }

                        break;
                    case ENM.Control.ControlType.TreeList:
                        foreach (DevExpress.XtraTreeList.Columns.TreeListColumn ObjItem in ((DevExpress.XtraTreeList.TreeList)pObj).Columns)
                        {
                       
                                    _Tag = "1";


                                    oItem = GetObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), _Tag);

                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        ObjItem.Tag = oItem;
                                    }
                                    else
                                    {
                                        ObjItem.Tag = _Tag + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString();
                                    }

                                    Lang.SP_SETxLanguageControl(ObjItem);

                                  
                            
                        }

                        break;
                    case (ENM.Control.ControlType.PivotGridControl):
                        foreach (DevExpress.XtraPivotGrid.PivotGridField ObjItem in ((DevExpress.XtraPivotGrid.PivotGridControl)pObj).Fields)
                        {
                            try
                            {
                                oItem = GetObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), "");
                                if (!string.IsNullOrEmpty(oItem))
                                {
                                    ObjItem.Tag = oItem;
                                }
                                else
                                {
                                    ObjItem.Tag = "1|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString() + "|" + ObjItem.Caption.ToString();
                                }

                                Lang.SP_SETxLanguageControl(ObjItem);
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        break;
                    case ENM.Control.ControlType.LookUpEdit:
                        string objectnamenew = ((DevExpress.XtraEditors.LookUpEdit)pObj).Name.ToString();
                       
                        foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.LookUpEdit)pObj).Properties.Columns)
                        {
                            string K = Col.FieldName.ToString();
                            if (Col.Visible)
                            {
                                oItem = GetObjLang(pModuleName, pFormName, objectnamenew + "__" + Col.FieldName.ToString().Trim(), "");
                                if (!string.IsNullOrEmpty(oItem))
                                {
                                    int nCut = (int)HI.ST.Lang.Language;
                                    string tTag = null;
                                    string[] atTag = null;

                                    atTag = ("" + oItem.ToString()).Split('|');

                                    try
                                    {
                                        if (atTag.Length <= 3) { break; };
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
                                    }

                                    Col.Caption = tTag;
                                }
                                else
                                {
                                    Col.Caption = Col.Caption.ToString();
                                }

                            };
                        }

                        break;
                    case ENM.Control.ControlType.RepositoryItemLookUpEdit:
                        string objectname = ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Name.ToString();

                        foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Columns)
                        {
                            string K = Col.FieldName.ToString();
                            try
                            {
                                if (Col.Visible)
                                {
                                    oItem = GetObjLang(pModuleName, pFormName, objectname + "__" + Col.FieldName.ToString().Trim(), "");
                                    if (!string.IsNullOrEmpty(oItem))
                                    {
                                        int nCut = (int)HI.ST.Lang.Language;
                                        string tTag = null;
                                        string[] atTag = null;

                                        atTag = ("" + oItem.ToString()).Split('|');

                                        try
                                        {
                                            if (atTag.Length <= 3) { break; };
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
                                        }

                                        Col.Caption = tTag;
                                    }
                                    else
                                    {
                                        Col.Caption = Col.Caption.ToString();
                                    }

                                };
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        break;
                    default:
                        if (!(StateLoop))
                        {
   
                            try
                            {
                                _ModuleName = HI.ST.SysInfo.ModuleName;
                                _ModuleID = HI.ST.SysInfo.ModuleID;
                                _MenuName = HI.ST.SysInfo.MenuName;
                            }
                            catch (Exception ex)
                            {
                            }

                            oItem = GetObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), "");
                            if (!string.IsNullOrEmpty(oItem))
                            {
                                ((Control)pObj).Tag = _ModuleName + "|" + oItem.Split('|')[1] + "|" + oItem.Split('|')[2] + "|" + oItem.Split('|')[3] + "|" + oItem.Split('|')[4] + "|" + oItem.Split('|')[5] + "|" + oItem.Split('|')[6] + "|" + oItem.Split('|')[7] + "|" + _ModuleID + "|" + _MenuName;
                            }
                            else
                            {
                                ((Control)pObj).Tag = _ModuleName + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + ((Control)pObj).Text.ToString() + "|" + _ModuleID + "|" + _MenuName;
                            }

                            Lang.SP_SETxLanguageControl(pObj);
                        }
                        break;
                }

                try {  if (((Control)pObj).Controls.Count > 0)
                {
                    foreach (Control Obj in ((Control)pObj).Controls)
                    {
                        LoadObjectLanguage(pModuleName, pFormName, Obj, true);
                    }
                }
                }
                catch { }
               

                if (!(StateLoop))
                {
                    try { HI.ST.Security.PermissionObject( HI.ST.SysInfo.MenuName, (Control)pObj);}
                    catch { };
                    
                }

                return true;
            }
            catch (Exception ex)
            {
               
                if (!(StateLoop))
                {
                    try {  HI.ST.Security.PermissionObject(HI.ST.SysInfo.MenuName, (Control)pObj);}
                    catch { };
                   
                }

                return false;
            }
        }

        private void  LoadObjectLanguageBand(string pModuleName, string pFormName, DevExpress.XtraGrid.Views.BandedGrid.GridBand ogvMBand)
        {

            try {

                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ogvMBand.Children  )
                {

                    string oItem = GetObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), "");
                    if (!string.IsNullOrEmpty(oItem))
                    {
                        oBand.Tag = oItem;
                    }
                    else
                    {
                        oBand.Tag = "1|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString() + "|" + oBand.Caption.ToString();
                    }
                    Lang.SP_SETxLanguageControl(oBand);


                    if (oBand.HasChildren ==true) { LoadObjectLanguageBand(pModuleName, pFormName, oBand); };

                }
            
            }
            catch { 
            
            }
           
        
        }

        private bool InsObjLang(string pModuleName, string pFormName, string pObjectName, string pText)
        {
            string tSql = null;
            try
            {
                try
                {
                    tSql = "SELECT TOP 1  FTFormName,FTObjectName ";
                    tSql += Constants.vbCrLf + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSysLanguage WITH(Nolock) ";
                    tSql += Constants.vbCrLf + " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(pFormName) + "'";
                    tSql += Constants.vbCrLf + " AND FTObjectName='" + HI.UL.ULF.rpQuoted(pObjectName.Trim()) + "'";


                    if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(tSql, Conn.DB.DataBaseName.DB_SYSTEM, "")))
                    {
                        tSql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) + "].dbo.HSysLanguage (FTFormName,FTObjectName,FTLangEN,FTLangTH,FTLangVT,FTLangKM,FTLangBM,FTLangLAO,FTLangCH) VALUES (";
                        tSql += Constants.vbCrLf + "'" + HI.UL.ULF.rpQuoted(pFormName.Trim()) + "'";
                        tSql += Constants.vbCrLf + ",'" + HI.UL.ULF.rpQuoted(pObjectName.Trim()) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "'";
                        tSql += Constants.vbCrLf + ",N'" + HI.UL.ULF.rpQuoted(pText) + "')";

                        HI.Conn.SQLConn.ExecuteOnly(tSql, Conn.DB.DataBaseName.DB_SYSTEM);

                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void InsertObjectLanguageBanded(string pModuleName, string pFormName, DevExpress.XtraGrid.Views.BandedGrid.GridBand oMBand)
        {

            try {
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in oMBand.Children )
                {

                    InsObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), oBand.Caption);

                    if (oBand.HasChildren ==true) { InsertObjectLanguageBanded(pModuleName, pFormName, oBand); };

                }
            }
            catch { }
        }

        public bool InsertObjectLanguage(string pModuleName, string pFormName, object pObj)
        {
            try
            {

                switch (HI.ENM.Control.GeTypeControl(pObj))
                {

                    case ENM.Control.ControlType.XtraForm:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.LabelControl:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);
                        break;
                    case ENM.Control.ControlType.DockPanel:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.SimpleButton:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.CheckEdit:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.GroupControl:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.XtraTabControl:
                        foreach (DevExpress.XtraTab.XtraTabPage oTabPage in ((DevExpress.XtraTab.XtraTabControl)pObj).TabPages)
                        {
                            InsObjLang(pModuleName, pFormName, oTabPage.Name.ToString().Trim(), oTabPage.Text);
                        }

                        break;
                    case ENM.Control.ControlType.RadioButton:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.XtraTabPage:
                        InsObjLang(pModuleName, pFormName, ((Control)pObj).Name.ToString().Trim(), ((Control)pObj).Text);

                        break;
                    case ENM.Control.ControlType.GridControl:
                        object ogvGrid = null;
                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Columns)
                            {
                               
                                   InsObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), oGridCol.Caption);
                               
                            }
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)ogvGrid).Bands)
                            {
                               
                                   InsObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), oBand.Caption);

                                   if (oBand.HasChildren == true) { InsertObjectLanguageBanded(pModuleName, pFormName, oBand); };
                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn oGridCol in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Columns)
                            {

                                InsObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), oGridCol.Caption);

                            }
                            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand oBand in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)ogvGrid).Bands)
                            {

                                InsObjLang(pModuleName, pFormName, oBand.Name.ToString().Trim(), oBand.Caption);

                                if (oBand.HasChildren == true) { InsertObjectLanguageBanded(pModuleName, pFormName, oBand); };
                            }
                        }

                        if (((DevExpress.XtraGrid.GridControl)pObj).MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            ogvGrid = ((DevExpress.XtraGrid.GridControl)pObj).MainView;
                            foreach (DevExpress.XtraGrid.Columns.GridColumn oGridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)ogvGrid).Columns)
                            {    
                                    InsObjLang(pModuleName, pFormName, oGridCol.Name.ToString().Trim(), oGridCol.Caption);

                                    if (oGridCol.ColumnEdit != null)
                                    {
                                        InsertObjectLanguage(pModuleName, pFormName, oGridCol.ColumnEdit);
                                    };

                            }
                        }
                        break;
                    case (ENM.Control.ControlType.PivotGridControl):
                        foreach (DevExpress.XtraPivotGrid.PivotGridField ObjItem in ((DevExpress.XtraPivotGrid.PivotGridControl)pObj).Fields)
                        {
                            try
                            {
                                InsObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), ObjItem.Caption);
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        break;
                    case ENM.Control.ControlType.Bar:
                        foreach (DevExpress.XtraBars.BarSubItem oBarItem in ((DevExpress.XtraBars.Bar)pObj).ItemLinks)
                        {
                            InsObjLang(pModuleName, pFormName, oBarItem.Name.ToString().Trim(), oBarItem.Caption);
                        }

                        break;
                    case ENM.Control.ControlType.LayoutControl:
                        foreach (DevExpress.XtraLayout.LayoutControlItem ObjItem in ((DevExpress.XtraLayout.LayoutControl)pObj).Root.Items)
                        {
              
                                    InsObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), ObjItem.Text);

                        }

                        break;
                    case ENM.Control.ControlType.TreeList:
                        foreach (DevExpress.XtraTreeList.Columns.TreeListColumn ObjItem in ((DevExpress.XtraTreeList.TreeList)pObj).Columns)
                        {
                                    InsObjLang(pModuleName, pFormName, ObjItem.Name.ToString().Trim(), ObjItem.Caption);
                        }

                        break;
                    case ENM.Control.ControlType.LookUpEdit:
                       
                        string objectnamenew = ((DevExpress.XtraEditors.LookUpEdit)pObj).Name.ToString();
                       
                        foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.LookUpEdit)pObj).Properties.Columns)
                        {
                            string K = Col.FieldName.ToString();
                            if (Col.Visible)
                            {
                                InsObjLang(pModuleName, pFormName, objectnamenew + "__" + Col.FieldName.ToString().Trim(), Col.Caption);
                            };
                        }

                        break;
                    case ENM.Control.ControlType.RepositoryItemLookUpEdit:

                        string objectname = ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Name.ToString();

                        foreach (DevExpress.XtraEditors.Controls.LookUpColumnInfo Col in ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)pObj).Columns)
                        {
                            if (Col.Visible)
                            {
                                InsObjLang(pModuleName, pFormName, objectname + "__" + Col.FieldName.ToString().Trim(), Col.Caption);
                            };
                        }

                        break;
                    default:

                        break;
                }

                try {  if (((Control)pObj).Controls.Count > 0)
                {
                    foreach (object Obj in ((Control)pObj).Controls)
                    {
                        InsertObjectLanguage(pModuleName, pFormName, Obj);
                    }
                }
                }
                catch { }

                return true;
            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
                return false;
            }
        }



    }
}

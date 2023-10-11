using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace HI.Track
{
    public partial class wProdLTTracking : DevExpress.XtraEditors.XtraForm
    {
        public wProdLTTracking()
        {
            InitializeComponent();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            if (VerifyField())
            {
                String _Qry = "";
                int _PoTime = 3; //PO Times 3 Days
                int _RawMatfromTH = 3; //Raw Material form TH Times 3 Days
                int _RawMatfromOversea = 15; //Raw Material form Oversea Times 3 Days
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    //" CASE WHEN CS.FNISTeamMulti = 'N' THEN '20' + RIGHT ( Sea.FTSeasonCode, 2 ) + LEFT ( Sea.FTSeasonCode, 2 ) " +
                    //" ELSE '20' + RIGHT(TM.FTSeason, 2) + LEFT(TM.FTSeason, 2) END AS FTDmndSesn1, " +
                    //" CASE WHEN CS.FNISTeamMulti = 'N' THEN S.FTStyleCode + '20' + RIGHT(TM.FTSeason, 2) + LEFT(TM.FTSeason, 2) " +
                    //" ELSE S.FTStyleCode + '20' + RIGHT(TM.FTSeason, 2) + LEFT(TM.FTSeason, 2) END AS  FTSeason1," +
                    //" row_number ( ) OVER ( partition BY Sea.FTSeasonCode, FTUse, RMDS.FTMATERIALTYPE ORDER BY FNPurchasingLT DESC ) AS Seqnum, " +
                    //" row_number ( ) OVER ( partition BY Sea.FTSeasonCode,S.FTStyleCode ORDER BY FNPurchasingLT DESC ) AS Seqnum, " +
                    ogcDetail.DataSource = null;
                    //DataTable dt = null;
                    _Qry = "SELECT DISTINCT " +
                        " CONVERT(DATETIME, CS.FDInsDate+' '+ CS.FTInsTime) as DateCostSheet," +
                        " '20' + RIGHT ( Sea.FTSeasonCode, 2 ) + LEFT ( Sea.FTSeasonCode, 2 ) AS FTDmndSesn," +
                        " S.FTStyleCode + '20' + RIGHT(Sea.FTSeasonCode, 2) + LEFT(Sea.FTSeasonCode, 2) AS FTSeason, " +
                        //" CASE WHEN CS.FNISTeamMulti = 'Y' AND TM.FTSeason <> null THEN CS.FNISTeamMulti ELSE  'N' END AS TeamMulti," +
                        " CS.FNISTeamMulti  AS TeamMulti," +
                        //AND TM.FTSeason <> null
                        " CASE WHEN CS.FNISTeamMulti = 'Y'  THEN ISNULL( TM.FTSeason, ISNULL( Sea.FTSeasonCode, '')) ELSE ISNULL(Sea.FTSeasonCode, '') END AS SeasonParent, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL( TM.FTStyleCode, ISNULL( S.FTStyleCode, '')) ELSE ISNULL(S.FTStyleCode, '') END AS FTStyleCode, " +
                        " ISNULL(S.FTStyleCode,'') as StyleParent, " +
                        " ISNULL(FTUse,'') as FTProdCat, ISNULL(CS.FTMSC,'') as FTLeague,  " +
                        " row_number ( ) OVER ( partition BY Sea.FTSeasonCode,S.FTStyleCode ORDER BY " +
                        " (CASE WHEN RMDS.FTLiaisonOfficeCode = 'TH' THEN ISNULL(MAX(RMDS.FNPurchasingLT),0) + 6 " +
                        " ELSE ISNULL(MAX(RMDS.FNPurchasingLT),0) + 18 END) DESC, " +
                        //AND TM.FTSeason <> null
                        " (CASE WHEN CS.FNISTeamMulti = 'Y'  THEN ISNULL( TM.FTStyleCode, '' ) ELSE ISNULL( S.FTStyleCode, '' ) END) ," +
                        " CONVERT( DATETIME, CS.FDInsDate + ' ' + CS.FTInsTime )  DESC ) AS Seqnum, " +

                        " CASE WHEN RMDS.FTLiaisonOfficeCode = 'TH' THEN ISNULL(MAX(RMDS.FNPurchasingLT),0) + " + (_PoTime + _RawMatfromTH) +
                        " ELSE ISNULL(MAX(RMDS.FNPurchasingLT),0) + " + (_PoTime + _RawMatfromOversea) + " END AS FTMerPLT, " +
                        " ISNULL( CAST(CS.FNLeadtime as Int), 0 ) AS FTMfgLT, " +
                        " RMDS.FTMATERIALTYPE as FTGrpDesc, RMDS.FTSMState as FTStatus ,  " +
                        " ISNULL(FTCostSheetBy,'') AS MerCode,  ISNULL(CS.FNCostSheetQuotedType,'') AS CBDQuoteStatus, CAST(CS.FNCMP AS DECIMAL(5, 2)) AS CMP, " +
                        " ISNULL(vp.FTVenderPramCode,'') AS Factory, ISNULL(CSD.TTLG,'') AS Countryoforigin ," +

                        " ISNULL(CS.FNL4Country1Exc,0) AS FNL4Country1Exc, " +
                        " ISNULL(CS.FNL4Country2Exc,0) AS FNL4Country2Exc, " +
                        " ISNULL(CS.FNL4Country3Exc,0) AS FNL4Country3Exc, " +

                        " ISNULL(CS.FNL4Country1Cur,'') AS FNL4Country1Cur, " +
                        " ISNULL(CS.FNL4Country2Cur,'') AS FNL4Country2Cur, " +
                        " ISNULL(CS.FNL4Country3Cur,'') AS FNL4Country3Cur," +
                        //AND TM.FTSeason <> null
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L1, ISNULL(CS.FNL4Country1Extended,0)) ELSE ISNULL(CS.FNL4Country1Extended,0) END AS FNL4Country1Extended, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L2, ISNULL(CS.FNL4Country2Extended,0)) ELSE ISNULL(CS.FNL4Country2Extended,0) END AS FNL4Country2Extended, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L3, ISNULL(CS.FNL4Country3Extended,0)) ELSE ISNULL(CS.FNL4Country3Extended,0) END AS FNL4Country3Extended, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB1, ISNULL(CS.FNL4Country1Final,0)) ELSE ISNULL(CS.FNL4Country1Final,0) END AS FNL4Country1Final, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB2, ISNULL(CS.FNL4Country2Final,0)) ELSE ISNULL(CS.FNL4Country2Final,0) END AS FNL4Country2Final, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB3, ISNULL(CS.FNL4Country3Final,0)) ELSE ISNULL(CS.FNL4Country3Final,0) END AS FNL4Country3Final, " +
                        " (CSD.FTMainMatCode + ' ' + CSD.FTSuplCode) AS ItemVender, ISNULL(CS.FTCostSheetNo,'') AS FTCostSheetNo, ISNULL(FTMSCCode,'') AS FTMSCCode," +

                        //AND TM.FTSeason <> null 
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDEDSIZEFOB, ISNULL(CS.FNExtendedFOB,0)) ELSE ISNULL(CS.FNExtendedFOB,0) END AS FNExtendedFOB, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNFINALFOB, ISNULL(CS.FNGrandTotal,0)) ELSE ISNULL(CS.FNGrandTotal,0) END AS FinalFOB, " +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY1, ISNULL(CS.FNL4Country1,0)) ELSE ISNULL(CS.FNL4Country1,0) END AS FNL4Country1," +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY2, ISNULL(CS.FNL4Country2,0)) ELSE ISNULL(CS.FNL4Country2,0) END AS FNL4Country2," +
                        " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY3, ISNULL(CS.FNL4Country3,0)) ELSE ISNULL(CS.FNL4Country3,0) END AS FNL4Country3";


                    if ((HI.ST.Lang.Language).ToString() == "TH")
                    {
                        _Qry += " ,ISNULL(S.FTStyleNameTH,'') as FTProdDesc, ISNULL(msc.FTMSCNameTH,'') AS FTMSCName, " +
                            "ISNULL(ul.FTUserDescriptionTH,'') AS FTUserDescription, ISNULL(mer.FTMerTeamNameTH,'') AS FTMerTeamName, " +
                            "ISNULL(vp.FTVenderPramNameTH,'') AS FTVenderPramName ";
                    }
                    else
                    {
                        _Qry += " ,ISNULL(S.FTStyleNameEN,'') as FTProdDesc, ISNULL(msc.FTMSCNameEN,'') AS FTMSCName, " +
                            "ISNULL(ul.FTUserDescriptionEN,'') AS FTUserDescription, ISNULL(mer.FTMerTeamNameEN,'') AS FTMerTeamName , " +
                            "ISNULL(vp.FTVenderPramNameEN,'') AS FTVenderPramName ";
                    }

                    _Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS CS WITH(NOLOCK) " +

                    " OUTER APPLY (SELECT FTUse,FTMainMatCode, FTSuplCode, TTLG, FNFINALFOB FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail WITH ( NOLOCK ) " +
                    " WHERE CS.FTCostSheetNo = FTCostSheetNo ) AS CSD " +

                    " OUTER APPLY (SELECT FTSeason, FTStyleCode, FNFINALFOB,FNEXTENDEDSIZEFOB,FNExtendedFOB, FNEXTENDSIZEFOBL4L1, FNEXTENDSIZEFOBL4L2, FNEXTENDSIZEFOBL4L3, " +
                    " FTL4LCURRENCYFOB1,FTL4LCURRENCYFOB2,FTL4LCURRENCYFOB3, FTL4LORDERCNTY1, FTL4LORDERCNTY2, FTL4LORDERCNTY3, FNVersion " +
                    " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail_TeamMulti WITH ( NOLOCK ) " +
                    " WHERE CS.FTCostSheetNo = FTCostSheetNo ) AS TM " +

                    " OUTER APPLY(SELECT FTStyleCode, FTStyleNameEN, FTStyleNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle WITH (NOLOCK) " +
                    " WHERE CS.FNHSysStyleId = FNHSysStyleId) AS S " +

                    " OUTER APPLY(SELECT FTSeasonCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason WITH (NOLOCK ) " +
                    " WHERE CS.FNHSysSeasonId = FNHSysSeasonId) AS Sea " +

                    " OUTER APPLY(SELECT FTMSCCode, FTMSCNameEN, FTMSCNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMSC WITH (NOLOCK ) " +
                    " WHERE CS.FTMSC = FTMSCCode) AS msc " +

                    " OUTER APPLY(SELECT FTUserDescriptionEN,FTUserDescriptionTH,FNHSysMerTeamId FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLogin WITH (NOLOCK ) " +
                    " WHERE CS.FTCostSheetBy = FTUserName) AS ul " +

                    " OUTER APPLY(SELECT FTMerTeamNameEN, FTMerTeamNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMerTeam WITH (NOLOCK ) " +
                    " WHERE ul.FNHSysMerTeamId = FNHSysMerTeamId) AS mer " +

                    " OUTER APPLY(SELECT FTVenderPramCode,FTVenderPramNameEN, FTVenderPramNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMVenderPram WITH (NOLOCK ) " +
                    " WHERE FNHSysVenderPramId = cs.FNHSysVenderPramId) AS vp " +

                    " INNER jOIN (SELECT DISTINCT FTMat, FTRMDSSESNCD,FTLiaisonOfficeCode, FTSupplierLocationCode, FNPurchasingLT, FTMATERIALTYPE,FTSMState, FTSMStatus, FTMANUFACTURINGCRTYOFORIGIN AS Countryoforigin " +
                    " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.THITRMDSMasterFile WITH ( NOLOCK ))  RMDS " +
                    " ON CSD.FTMainMatCode = RMDS.FTMat AND CSD.FTSuplCode = RMDS.FTSupplierLocationCode ";
                    if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += " AND RMDS.FTRMDSSESNCD = '" + FNHSysSeasonId.Text + "' ";
                    }
                    _Qry += " WHERE CS.FNHSysSeasonId > 0 AND CS.FNHSysStyleId > 0 AND CS.FNCostSheetQuotedType = 'Q-CRMDS' ";


                    if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += " AND (Sea.FTSeasonCode BETWEEN '" + FNHSysSeasonId.Text + "' AND '" + FNHSysSeasonIdTo.Text + "')";
                    }
                    if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += " AND (S.FTStyleCode BETWEEN '" + FNHSysStyleId.Text + "' AND '" + FNHSysStyleIdTo.Text + "')";
                    }

                    _Qry += "GROUP BY  Sea.FTSeasonCode,S.FTStyleCode,CS.FTMSC,FTMSCCode, RMDS.FTLiaisonOfficeCode,RMDS.FNPurchasingLT,CS.FNLeadtime, " +
                        "RMDS.FTMATERIALTYPE ,RMDS.FTSMState, FTCostSheetBy,CS.FNISTeamMulti ,CS.FNCostSheetQuotedType ,CS.FNCMP , " +
                        "CS.FNExtendedFOB,vp.FTVenderPramCode ,CSD.TTLG ,CS.FNL4Country1Exc ,CS.FNL4Country2Exc , " +
                        "CS.FNL4Country3Exc , CS.FNL4Country1 ,CS.FNL4Country2 ,CS.FNL4Country3 , CS.FNL4Country1Cur ,CS.FNL4Country2Cur ," +
                        "CS.FNL4Country3Cur ,CS.FNL4Country1Extended ,CS.FNL4Country2Extended ,CS.FNL4Country3Extended ,CS.FNL4Country1Final , " +
                        "CS.FNL4Country2Final ,CS.FNL4Country3Final ,TM.FTSeason,TM.FTStyleCode,CSD.FTMainMatCode , CSD.FTSuplCode, " +
                        "S.FTStyleNameEN ,msc.FTMSCNameEN ,ul.FTUserDescriptionEN ,mer.FTMerTeamNameEN ,vp.FTVenderPramNameEN,CSD.FTUse" +
                        ",msc.FTMSCNameTH,S.FTStyleNameTH,ul.FTUserDescriptionTH, mer.FTMerTeamNameTH,vp.FTVenderPramNameTH " +
                        ",CS.FTCostSheetNo ,CS.FNGrandTotal, TM.FNFINALFOB, TM.FTL4LORDERCNTY1, TM.FTL4LORDERCNTY2, TM.FTL4LORDERCNTY3 " +
                        ",TM.FNEXTENDSIZEFOBL4L1,TM.FNEXTENDSIZEFOBL4L2,TM.FNEXTENDSIZEFOBL4L3,TM.FNExtendedFOB, TM.FTL4LCURRENCYFOB1, " +
                        "TM.FTL4LCURRENCYFOB2, TM.FTL4LCURRENCYFOB3,TM.FNEXTENDEDSIZEFOB, CS.FDInsDate,CS.FTInsTime ";
                    _Qry += " ORDER BY  StyleParent, Seqnum "; //,
                    //"ORDER BY FTDmndSesn,FTSeason,FTStyleCode,FTMerPLT DESC";
                    // "ORDER BY FTSeason , FTMerPLT DESC";
                    //_Qry += " ORDER BY FTSeason, FTUse, RMDS.FTMATERIALTYPE, Seqnum ";

                    // ----- Remove Rows -----

                    DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);
                    String _style = "";
                    int _FTMerPLT = 0;

                    //Delete Cost sheet old version
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if (string.Compare(dt.Rows[i]["FTDmndSesn"].ToString(), dt.Rows[i + 1]["FTDmndSesn"].ToString()) == 0 && string.Compare(dt.Rows[i]["FTSeason"].ToString(), dt.Rows[i + 1]["FTSeason"].ToString()) == 0)
                        {
                            if (dt.Rows[i]["TeamMulti"].ToString() == "N")
                            {
                                if (DateTime.Parse(dt.Rows[i]["DateCostSheet"].ToString()) > DateTime.Parse(dt.Rows[i + 1]["DateCostSheet"].ToString()))
                                {
                                    dt.Rows[i + 1].Delete();
                                    i++;
                                }
                                else if (DateTime.Parse(dt.Rows[i]["DateCostSheet"].ToString()) < DateTime.Parse(dt.Rows[i + 1]["DateCostSheet"].ToString()))
                                {
                                    dt.Rows[i].Delete();
                                }
                                else
                                {
                                    i++;
                                }
                            }
                        }
                    }
                    dt.AcceptChanges();
                    //End Delete Cost sheet old version

                    int _i = 0;
                    //int _j = 0;
                    ArrayList iv = new ArrayList();
                    ArrayList ivChild = new ArrayList();
                    ArrayList styleChild = new ArrayList();
                    String _itemVender = "";
                    String _itemVenderChild = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((dt.Rows[i]["Seqnum"].ToString()) != "1")
                        {
                            if (_style == dt.Rows[i]["StyleParent"].ToString())
                            {
                                if (_FTMerPLT > int.Parse(dt.Rows[i]["FTMerPLT"].ToString())) // Delete Leadtime < 
                                {
                                    dt.Rows[i].Delete();
                                }
                                else // Filter only Max Leadtime
                                {
                                    if (dt.Rows[i]["ItemVender"].ToString() != _itemVender)
                                    {
                                        bool isDuplicate = false;
                                        foreach (String _iv in iv)
                                        {
                                            if (_iv == dt.Rows[i]["ItemVender"].ToString())
                                            {
                                                if (dt.Rows[i]["StyleParent"].ToString() == dt.Rows[i]["FTStyleCode"].ToString())
                                                {
                                                    _itemVenderChild += "/" + dt.Rows[i]["ItemVender"];
                                                }
                                                isDuplicate = true;
                                                break;
                                            }
                                        }
                                        if (isDuplicate == false)
                                        {
                                            iv.Add(dt.Rows[i]["ItemVender"].ToString());
                                            dt.Rows[_i]["ItemVender"] = (dt.Rows[_i]["ItemVender"] + "/" + dt.Rows[i]["ItemVender"]);
                                            dt.Rows[i].Delete();
                                        }
                                        else
                                        {
                                            dt.Rows[i].Delete();
                                        }
                                    }
                                    else
                                    {
                                        if (dt.Rows[i]["TeamMulti"].ToString() == "Y")
                                        {
                                            if (dt.Rows[i]["StyleParent"].ToString() == dt.Rows[i]["FTStyleCode"].ToString())
                                            {
                                                dt.Rows[i]["StyleParent"] = "";
                                                //dt.Rows[i].Delete();
                                            }
                                            else
                                            {
                                                bool isNewChild = true;
                                                foreach (string s in styleChild)
                                                {
                                                    if (s == dt.Rows[i]["FTStyleCode"].ToString())
                                                    {
                                                        isNewChild = false;
                                                        break;
                                                    }
                                                }

                                                if (isNewChild)// (ivChild.Count == 0)
                                                {
                                                    ivChild.Add(dt.Rows[i]["ItemVender"]);
                                                    dt.Rows[i]["ItemVender"] = dt.Rows[_i]["ItemVender"];
                                                }
                                                else
                                                {
                                                    foreach (String _iv in ivChild)
                                                    {
                                                        if (_iv == dt.Rows[i]["ItemVender"].ToString())
                                                        {
                                                            dt.Rows[i].Delete();
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            dt.Rows[i].Delete();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ivChild.Clear();
                            iv.Clear();
                            iv.Add(dt.Rows[i]["ItemVender"].ToString());
                            styleChild.Clear();
                            styleChild.Add(dt.Rows[i]["FTStyleCode"]);
                            _i = i;
                            _itemVender = dt.Rows[i]["ItemVender"].ToString();
                            _itemVenderChild = dt.Rows[i]["ItemVender"].ToString();
                            _style = dt.Rows[i]["StyleParent"].ToString();
                            _FTMerPLT = int.Parse(dt.Rows[i]["FTMerPLT"].ToString());
                            if ((dt.Rows[i]["TeamMulti"].ToString()) == "Y" && dt.Rows[i]["StyleParent"] == dt.Rows[i]["FTStyleCode"])
                            {
                                dt.Rows[i]["StyleParent"] = "";
                            }
                        }
                    }
                    dt.AcceptChanges();

                    ogcDetail.DataSource = dt;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _Spls.Close();
            }
            else
            {
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }

        private bool VerifyField()
        {
            bool checkfield = false;
            if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
            {
                checkfield = true;
            }
            if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
            {
                checkfield = true;
            }
            if (FNHSysStyleId.Text == "" && FNHSysStyleIdTo.Text != "")
            {
                checkfield = false;
            }
            if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text == "")
            {
                checkfield = false;
            }
            if (FNHSysSeasonId.Text == "" && FNHSysSeasonIdTo.Text != "")
            {
                checkfield = false;
            }
            if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text == "")
            {
                checkfield = false;
            }
            return checkfield;
        }

    }
}
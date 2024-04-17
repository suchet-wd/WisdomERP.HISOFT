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
                //int _PoTime = 3; //PO Times 3 Days
                //int _RawMatfromTH = 3; //Raw Material form TH Times 3 Days
                //int _RawMatfromOversea = 15; //Raw Material form Oversea Times 3 Days
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
                    ////DataTable dt = null;
                    //_Qry = "SELECT DISTINCT \n";

                    //// 06-Mar-2024 Modify Case Costsheet Header and Team multi Diff Style & Season
                    //_Qry += "CASE WHEN CS.FNISTeamMulti = 'Y' THEN  '20' + RIGHT(TM.FTSeason, 2) + LEFT(TM.FTSeason, 2) ELSE ";
                    //_Qry += "'20' + RIGHT(Sea.FTSeasonCode, 2) + LEFT(Sea.FTSeasonCode, 2) END AS 'FTDmndSesn', \n";
                    ////" '20' + RIGHT ( Sea.FTSeasonCode, 2 ) + LEFT ( Sea.FTSeasonCode, 2 ) AS FTDmndSesn, \n" +

                    //_Qry += "CASE WHEN CS.FNISTeamMulti = 'Y' THEN TM.FTStyleCode + '20' + RIGHT(TM.FTSeason, 2) + LEFT(TM.FTSeason, 2) \n ";
                    //_Qry += "ELSE S.FTStyleCode + '20' + RIGHT(Sea.FTSeasonCode, 2) + LEFT(Sea.FTSeasonCode, 2) END AS 'FTSeason', \n";
                    ////" S.FTStyleCode + '20' + RIGHT(Sea.FTSeasonCode, 2) + LEFT(Sea.FTSeasonCode, 2) AS FTSeason, \n" +

                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y'  THEN ISNULL( TM.FTSeason, ISNULL( Sea.FTSeasonCode, '')) ELSE ISNULL(Sea.FTSeasonCode, '') END AS 'SeasonParent', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL( TM.FTStyleCode, ISNULL( S.FTStyleCode, '')) ELSE ISNULL(S.FTStyleCode, '') END AS 'FTStyleCode', \n";
                    //_Qry += " ISNULL(S.FTStyleCode,'') as 'StyleParent', \n";

                    ////" CASE WHEN CS.FNISTeamMulti = 'Y' AND TM.FTSeason <> null THEN CS.FNISTeamMulti ELSE  'N' END AS TeamMulti," +
                    //_Qry += " CS.FNISTeamMulti  AS 'TeamMulti', \n";
                    ////AND TM.FTSeason <> null

                    //_Qry += " ISNULL(FTUse,'') as FTProdCat, ISNULL(CS.FTMSC,'') as 'FTLeague',  \n";

                    //// Running Number 
                    //_Qry += " row_number ( ) OVER ( partition BY ISNULL(TM.FTSeason,Sea.FTSeasonCode), ISNULL(tm.FTStyleCode,S.FTStyleCode) ORDER BY \n";
                    //_Qry += " (CASE WHEN RMDS.FTLiaisonOfficeCode = 'TH' THEN ISNULL(MAX(RMDS.FNPurchasingLT),0) + 6 \n";
                    //_Qry += " ELSE ISNULL(MAX(RMDS.FNPurchasingLT),0) + 18 END) DESC, \n";
                    ////AND TM.FTSeason <> null
                    //_Qry += " (CASE WHEN CS.FNISTeamMulti = 'Y'  THEN ISNULL( TM.FTStyleCode, '' ) ELSE ISNULL( S.FTStyleCode, '' ) END) , \n";
                    //_Qry += " CONVERT( DATETIME, CS.FDInsDate + ' ' + CS.FTInsTime )  DESC ) AS 'Seqnum', \n";

                    //_Qry += " CASE WHEN RMDS.FTLiaisonOfficeCode = 'TH' THEN ISNULL(MAX(RMDS.FNPurchasingLT),0) + " + (_PoTime + _RawMatfromTH) + "\n";
                    //_Qry += " ELSE ISNULL(MAX(RMDS.FNPurchasingLT),0) + " + (_PoTime + _RawMatfromOversea) + " END AS 'FTMerPLT', \n";
                    //_Qry += " ISNULL( CAST(CS.FNLeadtime as Int), 0 ) AS 'FTMfgLT', \n";
                    //_Qry += " RMDS.FTMATERIALTYPE as FTGrpDesc, RMDS.FTSMState as 'FTStatus' ,  \n";
                    //_Qry += " ISNULL(FTCostSheetBy,'') AS MerCode,  ISNULL(CS.FNCostSheetQuotedType,'') AS CBDQuoteStatus, CAST(CS.FNCMP AS DECIMAL(5, 2)) AS CMP, \n";
                    //_Qry += " ISNULL(vp.FTVenderPramCode,'') AS Factory, ISNULL(CSD.TTLG,'') AS 'Countryoforigin' , \n";

                    //_Qry += " ISNULL(CS.FNL4Country1Exc,0) AS 'FNL4Country1Exc', \n";
                    //_Qry += " ISNULL(CS.FNL4Country2Exc,0) AS 'FNL4Country2Exc', \n";
                    //_Qry += " ISNULL(CS.FNL4Country3Exc,0) AS 'FNL4Country3Exc', \n";

                    //_Qry += " ISNULL(CS.FNL4Country1Cur,'') AS 'FNL4Country1Cur', \n";
                    //_Qry += " ISNULL(CS.FNL4Country2Cur,'') AS 'FNL4Country2Cur', \n";
                    //_Qry += " ISNULL(CS.FNL4Country3Cur,'') AS 'FNL4Country3Cur', \n";
                    ////AND TM.FTSeason <> null
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L1, ISNULL(CS.FNL4Country1Extended,0)) ELSE ISNULL(CS.FNL4Country1Extended,0) END AS 'FNL4Country1Extended', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L2, ISNULL(CS.FNL4Country2Extended,0)) ELSE ISNULL(CS.FNL4Country2Extended,0) END AS 'FNL4Country2Extended', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDSIZEFOBL4L3, ISNULL(CS.FNL4Country3Extended,0)) ELSE ISNULL(CS.FNL4Country3Extended,0) END AS 'FNL4Country3Extended', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB1, ISNULL(CS.FNL4Country1Final,0)) ELSE ISNULL(CS.FNL4Country1Final,0) END AS 'FNL4Country1Final', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB2, ISNULL(CS.FNL4Country2Final,0)) ELSE ISNULL(CS.FNL4Country2Final,0) END AS 'FNL4Country2Final', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LCURRENCYFOB3, ISNULL(CS.FNL4Country3Final,0)) ELSE ISNULL(CS.FNL4Country3Final,0) END AS 'FNL4Country3Final', \n";
                    //_Qry += " (CSD.FTMainMatCode + ' ' + CSD.FTSuplCode) AS ItemVender, ISNULL(FTMSCCode,'') AS 'FTMSCCode', \n";

                    ////AND TM.FTSeason <> null 
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNEXTENDEDSIZEFOB, ISNULL(CS.FNExtendedFOB,0)) ELSE ISNULL(CS.FNExtendedFOB,0) END AS 'FNExtendedFOB', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FNFINALFOB, ISNULL(CS.FNGrandTotal,0)) ELSE ISNULL(CS.FNGrandTotal,0) END AS 'FinalFOB', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY1, ISNULL(CS.FNL4Country1,0)) ELSE ISNULL(CS.FNL4Country1,0) END AS 'FNL4Country1', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY2, ISNULL(CS.FNL4Country2,0)) ELSE ISNULL(CS.FNL4Country2,0) END AS 'FNL4Country2', \n";
                    //_Qry += " CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTL4LORDERCNTY3, ISNULL(CS.FNL4Country3,0)) ELSE ISNULL(CS.FNL4Country3,0) END AS 'FNL4Country3' \n";


                    //if ((HI.ST.Lang.Language).ToString() == "TH")
                    //{
                    //    _Qry += " ,CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(SM.FTStyleNameEN,'') ELSE ISNULL(S.FTStyleNameEN,'') END AS 'FTProdDesc', \n";

                    //    _Qry += "  ISNULL(msc.FTMSCNameTH,'') AS 'FTMSCName', \n";
                    //    _Qry += "ISNULL(ul.FTUserDescriptionTH,'') AS 'FTUserDescription', ISNULL(mer.FTMerTeamNameTH,'') AS 'FTMerTeamName', \n";
                    //    _Qry += "ISNULL(vp.FTVenderPramNameTH,'') AS 'FTVenderPramName' \n";
                    //}
                    //else
                    //{
                    //    _Qry += " ,CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(SM.FTStyleNameEN,'') ELSE ISNULL(S.FTStyleNameEN,'') END AS 'FTProdDesc', \n";
                    //    _Qry += " ISNULL(msc.FTMSCNameEN,'') AS 'FTMSCName', \n";
                    //    _Qry += "ISNULL(ul.FTUserDescriptionEN,'') AS 'FTUserDescription', ISNULL(mer.FTMerTeamNameEN,'') AS 'FTMerTeamName' , \n";
                    //    _Qry += "ISNULL(vp.FTVenderPramNameEN,'') AS 'FTVenderPramName' \n";
                    //}

                    //_Qry += " , ISNULL(CS.FTCostSheetNo,'') AS 'FTCostSheetNo' , CONVERT(DATETIME, CS.FDInsDate+' '+ CS.FTInsTime) as 'DateCostSheet' \n";

                    //// ------------------------- End Columns -------------------------

                    //_Qry += " \n\n FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS CS WITH(NOLOCK) \n";

                    //_Qry += " OUTER APPLY (SELECT FTUse,FTMainMatCode, FTSuplCode, TTLG, FNFINALFOB \n";
                    //_Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail WITH ( NOLOCK ) \n";
                    //_Qry += " WHERE CS.FTCostSheetNo = FTCostSheetNo ) AS CSD \n\n";

                    //_Qry += " OUTER APPLY (SELECT tm.FTSeason, tm.FTStyleCode, tm.FNFINALFOB,tm.FNEXTENDEDSIZEFOB, tm.FNEXTENDSIZEFOBL4L1, \n";
                    //_Qry += " tm.FNEXTENDSIZEFOBL4L2, tm.FNEXTENDSIZEFOBL4L3, tm.FTL4LCURRENCYFOB1,tm.FTL4LCURRENCYFOB2,tm.FTL4LCURRENCYFOB3, tm.FTL4LORDERCNTY1, \n";
                    //_Qry += " tm.FTL4LORDERCNTY2, tm.FTL4LORDERCNTY3, tm.FNVersion \n";
                    //_Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail_TeamMulti AS tm WITH (NOLOCK) \n";

                    ////_Qry += " OUTER APPLY (SELECT TOP 1 csh.FNHSysSeasonId AS FTState FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS csh WITH(NOLOCK) ";
                    ////_Qry += " WHERE csh.FTCostSheetNo = TM.FTCostSheetNo AND csh.FNHSysStyleId = cs.FNHSysStyleId AND csh.FNHSysSeasonId = cs.FNHSysSeasonId ) AS csh ";
                    ////AND csh.FTState is null
                    //_Qry += " WHERE TM.FTCostSheetNo = CS.FTCostSheetNo  ) AS TM  \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1  FTStyleCode, FTStyleNameEN, FTStyleNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle WITH (NOLOCK) \n";
                    //_Qry += " WHERE CS.FNHSysStyleId = FNHSysStyleId) AS S \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1  FTStyleCode, FTStyleNameEN, FTStyleNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS st WITH (NOLOCK) ";
                    //_Qry += " WHERE TM.FTStyleCode = st.FTStyleCode) AS SM  \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1  FTSeasonCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason WITH (NOLOCK ) \n";
                    //_Qry += " WHERE CS.FNHSysSeasonId = FNHSysSeasonId) AS Sea \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1  FTMSCCode, FTMSCNameEN, FTMSCNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMSC WITH (NOLOCK ) \n";
                    //_Qry += " WHERE CS.FTMSC = FTMSCCode) AS msc \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1 FTUserDescriptionEN,FTUserDescriptionTH,FNHSysMerTeamId FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLogin WITH (NOLOCK ) \n";
                    //_Qry += " WHERE CS.FTCostSheetBy = FTUserName) AS ul \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1 FTMerTeamNameEN, FTMerTeamNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMerTeam WITH (NOLOCK ) \n";
                    //_Qry += " WHERE ul.FNHSysMerTeamId = FNHSysMerTeamId) AS mer \n\n";

                    //_Qry += " OUTER APPLY(SELECT TOP 1 FTVenderPramCode,FTVenderPramNameEN, FTVenderPramNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMVenderPram WITH (NOLOCK ) \n";
                    //_Qry += " WHERE FNHSysVenderPramId = cs.FNHSysVenderPramId) AS vp \n\n";

                    //_Qry += " INNER jOIN (SELECT DISTINCT FTMat, FTRMDSSESNCD,FTLiaisonOfficeCode, FTSupplierLocationCode, FNPurchasingLT, FTMATERIALTYPE,FTSMState, FTSMStatus, FTMANUFACTURINGCRTYOFORIGIN AS Countryoforigin \n";
                    //_Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.THITRMDSMasterFile WITH ( NOLOCK ))  RMDS \n";
                    //_Qry += " ON CSD.FTMainMatCode = RMDS.FTMat AND CSD.FTSuplCode = RMDS.FTSupplierLocationCode \n";
                    ////if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
                    ////{
                    ////    _Qry += " AND RMDS.FTRMDSSESNCD = '" + FNHSysSeasonId.Text + "' \n";
                    ////}
                    //_Qry += " \n\n WHERE CS.FNHSysSeasonId > 0 AND CS.FNHSysStyleId > 0 AND CS.FNCostSheetQuotedType = 'Q-CRMDS' \n";


                    //if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
                    //{
                    //    // 06-Mar-2024 Modify Case Costsheet Header and Team multi Diff Style & Season
                    //    //_Qry += " AND (Sea.FTSeasonCode BETWEEN '" + FNHSysSeasonId.Text + "' AND '" + FNHSysSeasonIdTo.Text + "') \n";
                    //    _Qry += " AND(ISNULL(tm.FTSeason, Sea.FTSeasonCode) BETWEEN '" + FNHSysSeasonId.Text + "' AND '" + FNHSysSeasonIdTo.Text + "') \n";

                    //}
                    //if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
                    //{
                    //    //_Qry += " AND (S.FTStyleCode BETWEEN '" + FNHSysStyleId.Text + "' AND '" + FNHSysStyleIdTo.Text + "') \n";
                    //    // 06-Mar-2024 Modify Case Costsheet Header and Team multi Diff Style & Season
                    //    _Qry += " AND (ISNULL(tm.FTStyleCode, S.FTStyleCode) BETWEEN '" + FNHSysStyleId.Text + "' AND '" + FNHSysStyleIdTo.Text + "') \n";
                    //}

                    //_Qry += "\n GROUP BY  Sea.FTSeasonCode,S.FTStyleCode,CS.FTMSC,FTMSCCode, RMDS.FTLiaisonOfficeCode,RMDS.FNPurchasingLT,CS.FNLeadtime, \n" +
                    //    "RMDS.FTMATERIALTYPE ,RMDS.FTSMState, FTCostSheetBy,CS.FNISTeamMulti ,CS.FNCostSheetQuotedType ,CS.FNCMP , \n" +
                    //    "CS.FNExtendedFOB,vp.FTVenderPramCode ,CSD.TTLG ,CS.FNL4Country1Exc ,CS.FNL4Country2Exc , \n" +
                    //    "CS.FNL4Country3Exc , CS.FNL4Country1 ,CS.FNL4Country2 ,CS.FNL4Country3 , CS.FNL4Country1Cur ,CS.FNL4Country2Cur , \n" +
                    //    "CS.FNL4Country3Cur ,CS.FNL4Country1Extended ,CS.FNL4Country2Extended ,CS.FNL4Country3Extended ,CS.FNL4Country1Final , \n" +
                    //    "CS.FNL4Country2Final ,CS.FNL4Country3Final ,TM.FTSeason,TM.FTStyleCode,CSD.FTMainMatCode , CSD.FTSuplCode, CSD.FTUse, \n";

                    //_Qry += "msc.FTMSCNameTH, S.FTStyleNameTH, SM.FTStyleNameTH, ul.FTUserDescriptionTH, mer.FTMerTeamNameTH,vp.FTVenderPramNameTH, \n";

                    //_Qry += "msc.FTMSCNameEN, S.FTStyleNameEN, SM.FTStyleNameEN, ul.FTUserDescriptionEN ,mer.FTMerTeamNameEN ,vp.FTVenderPramNameEN, \n";

                    //_Qry += "CS.FTCostSheetNo ,CS.FNGrandTotal, TM.FNFINALFOB, TM.FTL4LORDERCNTY1, TM.FTL4LORDERCNTY2, TM.FTL4LORDERCNTY3 \n" +
                    //    ",TM.FNEXTENDSIZEFOBL4L1,TM.FNEXTENDSIZEFOBL4L2,TM.FNEXTENDSIZEFOBL4L3, TM.FTL4LCURRENCYFOB1, \n" +
                    //    "TM.FTL4LCURRENCYFOB2, TM.FTL4LCURRENCYFOB3,TM.FNEXTENDEDSIZEFOB, CS.FDInsDate,CS.FTInsTime \n";




                    //_Qry += " \n ORDER BY  StyleParent, Seqnum "; //,
                    ////"ORDER BY FTDmndSesn,FTSeason,FTStyleCode,FTMerPLT DESC";
                    //// "ORDER BY FTSeason , FTMerPLT DESC";
                    ////_Qry += " ORDER BY FTSeason, FTUse, RMDS.FTMATERIALTYPE, Seqnum ";

                    //// ----- Remove Rows -----
                    _Qry = "EXEC " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".[dbo].[Get_Production_Leadtime_Tracking] ";
                    if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += "@Season = '" + FNHSysSeasonId.Text + "', @SeasonTo = '" + FNHSysSeasonIdTo.Text + "'";
                    }
                    if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "" && FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += ", ";
                    }
                    if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += "@Style = '" + FNHSysStyleId.Text + "', @StyleTo = '" + FNHSysStyleIdTo.Text + "'";
                    }

                    _Qry += ", @LangShow = '" + (HI.ST.Lang.Language).ToString() + "'";

                    DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);
                    DataTable dtResult = new DataTable();

                    String _style = "";
                    int _FTMerPLT = 0;

                    //Delete Cost sheet old version
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if (string.Compare(dt.Rows[i]["FTDmndSesn"].ToString(), dt.Rows[i + 1]["FTDmndSesn"].ToString()) == 0 &&
                            string.Compare(dt.Rows[i]["FTSeason"].ToString(), dt.Rows[i + 1]["FTSeason"].ToString()) == 0)
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

                    //ArrayList iv = new ArrayList();
                    //String _itemVender = "";

                    //foreach (DataRow dr1 in dt.Select("Seqnum <> 0"))
                    //{

                    //    //foreach (DataRow dr2 in dt.Select("Seqnum = 0 AND FTMerPLT = '" + dr1["FTMerPLT"].ToString() + "'"))
                    //    //{
                    //    //    //dtResult.ImportRow(dr2);
                    //    //    Console.WriteLine(dr2["ItemVender"].ToString());
                    //    //}
                    //    //iv.Add(dr["ItemVender"].ToString());
                    //    //string s = dr["FTMerPLT"].ToString();
                    //}

                    //foreach (DataRow dr1 in dt.Select("Seqnum = 1"))
                    //{
                    //    foreach (DataRow dr2 in dt.Select("Seqnum > 1 AND FTMerPLT = '" + dr1["FTMerPLT"].ToString() + "'"))
                    //    {
                    //        dtResult.ImportRow(dr2);
                    //        //Console.WriteLine(dr2["ItemVender"].ToString());
                    //    }
                    //    //iv.Add(dr["ItemVender"].ToString());
                    //    //string s = dr["FTMerPLT"].ToString();
                    //}

                    int _i = 0;
                    //int _j = 0;
                    ArrayList iv = new ArrayList();
                    ArrayList ivChild = new ArrayList();
                    ArrayList styleChild = new ArrayList();
                    String _itemVender = "";
                    String _itemVenderChild = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if ((dt.Rows[i]["Seqnum"].ToString()) != "1" && (dt.Rows[i]["Seqnum"].ToString()) != "0")
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
                                                //dt.Rows[i]["StyleParent"] = "";
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
                            if ((dt.Rows[i]["Seqnum"].ToString()) == "1")
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
                    }
                    dt.AcceptChanges();


                    // Load Data to Grid
                    ogcDetail.DataSource = dtResult; // dt;

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

        private void ogvDetail_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (ogvDetail.GetRowCellValue(e.RowHandle, "Seqnum").ToString() == "0")
                {
                    e.Appearance.BackColor = System.Drawing.Color.LightGray;
                }
                if (ogvDetail.GetRowCellValue(e.RowHandle, "TeamMulti").ToString() == "Y")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
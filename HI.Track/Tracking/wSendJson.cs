using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HI.MER;
using System.Windows.Forms;

namespace HI.Track
{
    public partial class wSendJson : DevExpress.XtraEditors.XtraForm
    {
        private static string _SysPath = Application.StartupPath + "\\";
        private static string tW_SysPath = Application.StartupPath + "\\Images";
        private static string _AppCBDJsonPath = Application.StartupPath + "\\CBDJson";

        public wSendJson()
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
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    ogcDetail.DataSource = null;
                    _Qry = "SELECT DISTINCT '0' AS CBD, '0' AS Picture, '0' AS Mark, CS.FTCostSheetNo, CS.FDCostSheetDate, CS.FTCostSheetBy, CS.FNCostSheetColor, CS.FNVersion, CS.FNCostSheetQuotedType , " +
                        " CS.FNCostSheetSampleRound, CS.FTCostSheetBy, CS.FTLOProductDeveloper, CS.FNISTeamMulti, CS.FTMSC, CS.FNCostSheetColor, " +
                        " CS.FNCostSheetSize, S.FTStyleCode, Sea.FTSeasonCode, mer.FTMerTeamNameEN, mer.FTMerTeamNameTH,  S.FTStyleCode ";

                    _Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS CS WITH ( NOLOCK ) ";

                    _Qry += " OUTER APPLY (SELECT FTUse,FTMainMatCode, FTSuplCode, TTLG, FNFINALFOB, FTRMDSSeason FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail WITH ( NOLOCK ) " +
                    " WHERE CS.FTCostSheetNo = FTCostSheetNo ) AS CSD ";

                    _Qry += " OUTER APPLY(SELECT FTStyleCode, FTStyleNameEN, FTStyleNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle WITH (NOLOCK) " +
                    " WHERE CS.FNHSysStyleId = FNHSysStyleId) AS S ";

                    _Qry += " OUTER APPLY(SELECT FTSeasonCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason WITH (NOLOCK ) " +
                    " WHERE CS.FNHSysSeasonId = FNHSysSeasonId) AS Sea ";

                    _Qry += " OUTER APPLY(SELECT FTUserDescriptionEN,FTUserDescriptionTH,FNHSysMerTeamId FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLogin WITH (NOLOCK ) " +
                    " WHERE CS.FTCostSheetBy = FTUserName) AS ul ";

                    _Qry += " OUTER APPLY(SELECT FTMerTeamNameEN, FTMerTeamNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMerTeam WITH (NOLOCK ) " +
                    " WHERE ul.FNHSysMerTeamId = FNHSysMerTeamId) AS mer ";

                    _Qry += " WHERE CS.FTCostSheetBy = 'mlkanyarat' " +
                    //_Qry += " WHERE CS.FTCostSheetBy = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' " +
                        "AND FTSeasonCode BETWEEN '" + FNHSysSeasonId.Text.Trim() + "' AND '" + FNHSysSeasonIdTo.Text.Trim() + "'";

                    DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                    if (dt.Rows.Count > 0)
                    {
                        txtmail.Enabled = true;
                        txtpassword.Enabled = true;
                        ocmpostdatajson2.Enabled = true;
                    }
                    else
                    {
                        txtmail.Enabled = false;
                        txtpassword.Enabled = false;
                        ocmpostdatajson2.Enabled = false;
                    }

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
            if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
            {
                checkfield = true;
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

        private void ocmpostdatajson_Click(object sender, EventArgs e)
        {

        }

        private void ogvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void FTStateReserve_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void ocmpostdatajson2_Click(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            if (txtmail.Text != "" && txtpassword.Text != "")
            {

                string pMail = txtmail.Text;
                string pMailPassword = txtpassword.Text;
                string Qry = "";
                DataTable dt = new DataTable();
                //"select top 1 FTUserName, FTEmailConnectNike, FTPasswordConnectNike from [" +
                //HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin WITH(NOLOCK) " +
                //" WHERE (FTUserName = N'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ";

                //DataTable dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SECURITY);
                //foreach (DataRow r in dt.Rows)
                //{
                //    pMail = r["FTEmailConnectNike"].ToString().Trim();
                //    pMailPassword = HI.Conn.DB.FuncDecryptData(r["FTEmailConnectNike"].ToString());
                //}


                for (int i = 0; i < ogvDetail.RowCount; i++)
                {
                    Boolean StateSendData = false;
                    Boolean StateSendCBD = false;
                    Boolean StateSendPicture = false;
                    Boolean StateSendMarkD = false;
                    StateSendCBD = ogvDetail.GetRowCellValue(i, "CBD").Equals("1") ? true : false;
                    StateSendPicture = ogvDetail.GetRowCellValue(i, "Picture").Equals("1") ? true : false;
                    StateSendMarkD = ogvDetail.GetRowCellValue(i, "Mark").Equals("1") ? true : false;

                    if (StateSendCBD || StateSendPicture || StateSendMarkD)
                    {
                        Qry = "select TOP 1  '" + ogvDetail.GetRowCellValue(i, "CBD") + "' AS FTSelect, 1 FNSeq, FTStateExportUser, FDStateExportDate, FTStateExportTime,";
                        Qry += ogvDetail.GetRowCellValue(i, "FNISTeamMulti").Equals("Y") ? "'CBD Json Team Multi'" : "'CBD Json Standard'";
                        Qry += " As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + ogvDetail.GetRowCellValue(i, "FTCostSheetNo") + "'" +
                            " UNION " +
                            " select TOP 1  '" + ogvDetail.GetRowCellValue(i, "Picture") + "' AS FTSelect, 2 FNSeq, FTStateImageExportUser, FDStateImageExportDate, FTStateImageExportTime,'Picture' As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription  " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_File WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + ogvDetail.GetRowCellValue(i, "FTCostSheetNo") + "'" +
                            " UNION " +
                            " select TOP 1  '" + ogvDetail.GetRowCellValue(i, "Mark") + "' AS FTSelect, 3 FNSeq, FTStateMarkExportUser, FDStateMarkExportDate, FTStateMarkExportTime,'Mark' As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription  " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_File WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + ogvDetail.GetRowCellValue(i, "FTCostSheetNo") + "'";

                        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                        bool teamMulti = ogvDetail.GetRowCellValue(i, "FNISTeamMulti").ToString() == "Y" ? true : false;

                        SetdataJSON(ogvDetail.GetRowCellValue(i, "FTCostSheetNo").ToString(), pMail, pMailPassword, ref dt, teamMulti, StateSendCBD, StateSendPicture, StateSendMarkD);
                    }

                    //Console.WriteLine(ogvDetail.GetRowCellValue(i, "FTCostSheetNo").ToString(), pMail, pMailPassword, dt, StateSendCBD, StateSendPicture, StateSendMarkD);
                    //HI.MER.wCostSheet cs = new HI.MER.wCostSheet();
                }
            }
            else
            {
                if (txtmail.Text.Length == 0)
                {
                    txtmail.Focus();
                }
                else
                {
                    if (txtpassword.Text.Length == 0)
                    {
                        txtpassword.Focus();
                    }
                }
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            }
            _Spls.Close();
        }


        static void SetdataJSON(string Docno, string pMail, string pMailPassword, ref DataTable dtdata, bool teamMulti, bool SateCBD, bool StatePicture, bool StateMark)
        {
            string Qry = "";
            DataTable dt = new DataTable();
            //string VenderPramCode = "";
            //string Material = "";
            //string FTCurCode = "";
            //int RIndx = 0;
            //Object JSonHeadcer;
            //decimal InvAmt = 0;
            //decimal GInvAmt = 0;

            CBDJson_Tbl_Imp_FOBSummary Tbl_Imp_FOBSummary = new CBDJson_Tbl_Imp_FOBSummary();
            List<CBDJson_Tbl_Imp_CMP> Tbl_Imp_CMP = new List<CBDJson_Tbl_Imp_CMP>();
            CBDJson_Tbl_Imp_L4L1 Tbl_Imp_L4L1 = new CBDJson_Tbl_Imp_L4L1();
            CBDJson_Tbl_Imp_L4L2 Tbl_Imp_L4L2 = new CBDJson_Tbl_Imp_L4L2();
            CBDJson_Tbl_Imp_L4L3 Tbl_Imp_L4L3 = new CBDJson_Tbl_Imp_L4L3();
            List<CBDJson_Tbl_Imp_Fabric> Tbl_Imp_Fabric = new List<CBDJson_Tbl_Imp_Fabric>();
            List<CBDJson_Tbl_Imp_Trims> Tbl_Imp_Trims = new List<CBDJson_Tbl_Imp_Trims>();
            List<CBDJson_Tbl_Imp_Process_Mtrl> Tbl_Imp_Process_Mtrl = new List<CBDJson_Tbl_Imp_Process_Mtrl>();
            List<CBDJson_Tbl_Imp_Process_Labor> Tbl_Imp_Process_Labor = new List<CBDJson_Tbl_Imp_Process_Labor>();
            List<CBDJson_Tbl_Imp_Packaging> Tbl_Imp_Packaging = new List<CBDJson_Tbl_Imp_Packaging>();
            List<CBDJson_Tbl_Imp_BEMIS> Tbl_Imp_BEMIS = new List<CBDJson_Tbl_Imp_BEMIS>();
            List<CBDJson_ChildCbds> ChildCbds = new List<CBDJson_ChildCbds>();


            //Qry = "  Select C.* ,  ISNULL(C.FTStyleCode,'') + '|' +   ISNULL(C.FTMainMatColorCode,'') + '|' +  ISNULL(C.FTTeamName,'')  AS FTTeam";
            //Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  ";
            //Qry += " WHERE C.FTCostSheetNo='" + Docno + "' ";

            //dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);


            Qry = "SELECT DISTINCT CS.*, " +
                "CASE WHEN CS.FNISTeamMulti = 'Y' THEN ISNULL(TM.FTSeason, ISNULL(Sea.FTSeasonCode, '' )) ELSE ISNULL(Sea.FTSeasonCode, '' ) END AS SeasonParent, " +
                "Sea.FTSeasonCode AS 'season',cmp.FTCmpCode AS 'factory_code'," +
                "CASE WHEN CS.FNISTeamMulti = 'Y'  THEN ISNULL(TM.FTStyleCode, '' ) ELSE ISNULL(S.FTStyleCode, '' ) END AS 'style_no', " +
                "S.FTStyleNameEN AS 'style_name', CS.FNISTeamMulti AS 'is_team_cbd', CS.FNCostSheetColor AS 'color', CS.FNCostSheetSize AS 'size', " +
                "CS.FNCostSheetBuyType AS 'buy_type', CS.FNVersion AS 'version', CS.FTMSC AS 'msc', CS.FTLOProductDeveloper AS 'developer', " +
                "CS.FNCostSheetQuotedType AS 'cbd_quote_status', CS.FNCostSheetSampleRound AS 'sample_round', CS.FNHSysStyleIdTo AS 'base_style_no', " +
                "CS.FTDateQuoted AS 'quoted_date', CS.FTQuotedLog AS 'quote_log', CS.FTFileName AS 'comment', CS.FNTotalFabAmt AS 'total_fabric', " +
                "CS.FNTotalAccAmt AS 'FNTotalAccAmt', CS.FNChargeFabAmt AS 'charge_fabric', CS.FNChargeAccAmt AS 'FNChargeAccAmt', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country1Cur,'') END AS 'currency1', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country1Exc,'') END AS 'exchange_rate1', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country2Cur,'') END AS 'currency2', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country2Exc,'') END AS 'exchange_rate2', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country3Cur,'') END AS 'currency3', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country3Exc,'') END AS 'exchange_rate3', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country1Extended,0) ELSE ISNULL(TM.FNEXTENDSIZEFOBL4L1, '') END AS 'extended_size_fob1', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country2Extended,0) ELSE ISNULL(TM.FNEXTENDSIZEFOBL4L2, '') END AS 'extended_size_fob2', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country3Extended,0) ELSE ISNULL(TM.FNEXTENDSIZEFOBL4L3, '') END AS 'extended_size_fob3', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country1Final,0) ELSE ISNULL(TM.FTL4LCURRENCYFOB1, '') END AS 'local_currency_fob1', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country2Final,0) ELSE ISNULL(TM.FTL4LCURRENCYFOB2, '') END AS 'local_currency_fob2', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country3Final,0) ELSE ISNULL(TM.FTL4LCURRENCYFOB3, '') END AS 'local_currency_fob3', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNExtendedFOB,0) ELSE ISNULL(TM.FNEXTENDEDSIZEFOB, '') END AS 'FNExtendedFOB', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNGrandTotal,0) ELSE ISNULL(TM.FNFINALFOB, '') END AS 'FinalFOB', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country1,0) ELSE ISNULL(TM.FTL4LORDERCNTY1, '') END AS 'country1', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country2,0) ELSE ISNULL(TM.FTL4LORDERCNTY2, '') END AS 'country2', " +
                "CASE WHEN CS.FNISTeamMulti = 'N' THEN ISNULL(CS.FNL4Country3,0) ELSE ISNULL(TM.FTL4LORDERCNTY3, '') END AS 'country3' ";

            Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS CS With(NOLOCK) ";

            Qry += " OUTER APPLY(SELECT FTUse, FTMainMatCode, FTSuplCode, TTLG, FNFINALFOB, FTRMDSSeason " +
                " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail WITH (NOLOCK ) WHERE CS.FTCostSheetNo = FTCostSheetNo) AS CSD";

            Qry += " OUTER APPLY (SELECT FTSeason, FTStyleCode, FNFINALFOB,FNEXTENDEDSIZEFOB,FNExtendedFOB, FNEXTENDSIZEFOBL4L1, FNEXTENDSIZEFOBL4L2, FNEXTENDSIZEFOBL4L3, " +
            " FTL4LCURRENCYFOB1,FTL4LCURRENCYFOB2,FTL4LCURRENCYFOB3, FTL4LORDERCNTY1, FTL4LORDERCNTY2, FTL4LORDERCNTY3, FNVersion " +
            " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_Detail_TeamMulti WITH ( NOLOCK ) " +
            " WHERE CS.FTCostSheetNo = FTCostSheetNo ) AS TM ";

            Qry += " OUTER APPLY(SELECT FTStyleCode, FTStyleNameEN, FTStyleNameTH " +
                " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle WITH (NOLOCK) WHERE CS.FNHSysStyleId = FNHSysStyleId) AS S ";

            Qry += " OUTER APPLY(SELECT FTSeasonCode FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason WITH (NOLOCK ) " +
                " WHERE CS.FNHSysSeasonId = FNHSysSeasonId) AS Sea ";

            Qry += " OUTER APPLY(SELECT FTUserDescriptionEN, FTUserDescriptionTH, FNHSysMerTeamId " +
                " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLogin WITH(NOLOCK ) WHERE CS.FTCostSheetBy = FTUserName) AS ul ";

            Qry += " OUTER APPLY(SELECT FTMerTeamNameEN, FTMerTeamNameTH " +
                " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMMerTeam WITH (NOLOCK ) WHERE ul.FNHSysMerTeamId = FNHSysMerTeamId) AS mer ";

            Qry += " OUTER APPLY(SELECT* FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp WITH (NOLOCK ) WHERE FNHSysCmpId= CS.FNHSysCmpId) AS Cmp ";

            Qry += " WHERE CS.FTCostSheetNo = '" + Docno + "'";


            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Tbl_Imp_FOBSummary.total_fabric = decimal.Parse(r["FNTotalFabAmt"].ToString());
                    Tbl_Imp_FOBSummary.total_trim = decimal.Parse(r["FNTotalAccAmt"].ToString());
                    Tbl_Imp_FOBSummary.charge_fabric = decimal.Parse(r["FNChargeFabAmt"].ToString());
                    Tbl_Imp_FOBSummary.charge_trim = decimal.Parse(r["FNChargeAccAmt"].ToString());
                    Tbl_Imp_FOBSummary.process_material_cost = decimal.Parse(r["FNProcessMatCost"].ToString());
                    Tbl_Imp_FOBSummary.process_labor_cost = decimal.Parse(r["FNProcessLaborCost"].ToString());
                    Tbl_Imp_FOBSummary.packaging = decimal.Parse(r["FNPackagingAmt"].ToString());
                    Tbl_Imp_FOBSummary.other_cost = decimal.Parse(r["FNOtherCostAmt"].ToString());
                    Tbl_Imp_FOBSummary.cmp = decimal.Parse(r["FNCMP"].ToString());
                    Tbl_Imp_FOBSummary.extended_size_adjustment = decimal.Parse(r["FNExtendedPer"].ToString()) / 100; // "0.0000"
                    Tbl_Imp_FOBSummary.final_fob = decimal.Parse(r["FNGrandTotal"].ToString());
                    Tbl_Imp_FOBSummary.extended_size_fob = decimal.Parse(r["FNExtendedFOB"].ToString());
                    Tbl_Imp_FOBSummary.trim_usage_allowance = decimal.Parse(r["FNTrinUsageAllowPer"].ToString()) / 100; //  "0.0000"

                    Tbl_Imp_L4L1.country = r["country1"].ToString();
                    Tbl_Imp_L4L1.currency = r["currency1"].ToString();
                    Tbl_Imp_L4L1.exchange_rate = decimal.Parse(r["exchange_rate1"].ToString());
                    Tbl_Imp_L4L1.local_currency_fob = decimal.Parse(r["local_currency_fob1"].ToString());
                    Tbl_Imp_L4L1.extended_size_fob = decimal.Parse(r["extended_size_fob1"].ToString());

                    Tbl_Imp_L4L2.country = r["country2"].ToString();
                    Tbl_Imp_L4L2.currency = r["currency2"].ToString();
                    Tbl_Imp_L4L2.exchange_rate = decimal.Parse(r["exchange_rate2"].ToString());
                    Tbl_Imp_L4L2.local_currency_fob = decimal.Parse(r["local_currency_fob2"].ToString());
                    Tbl_Imp_L4L2.extended_size_fob = decimal.Parse(r["extended_size_fob2"].ToString());

                    Tbl_Imp_L4L3.country = r["country3"].ToString();
                    Tbl_Imp_L4L3.currency = r["currency3"].ToString();
                    Tbl_Imp_L4L3.exchange_rate = decimal.Parse(r["exchange_rate3"].ToString());
                    Tbl_Imp_L4L3.local_currency_fob = decimal.Parse(r["local_currency_fob3"].ToString());
                    Tbl_Imp_L4L3.extended_size_fob = decimal.Parse(r["extended_size_fob3"].ToString());

                    Qry = " SELECT * FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail AS CSD With(NOLOCK) " +
                    " WHERE CSD.FTCostSheetNo = '" + Docno + "'";

                    DataTable _dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                    //DataRow[] result = dt.Select("FNCostType = '6'");
                    foreach (DataRow _r in _dt.Select("FNCostType = '6'"))
                    {
                        CBDJson_Tbl_Imp_CMP xPO = new CBDJson_Tbl_Imp_CMP();
                        xPO.bmccode = _r["FTBMCCODE"].ToString();
                        xPO.cost_per_minute = Decimal.Parse(_r["FNExtenPer"].ToString());
                        xPO.standard_allowed_minute = Decimal.Parse(_r["FNSTANDARDALLOWEDMINUTES"].ToString());
                        xPO.efficiency = Decimal.Parse(_r["FNEFFICIENCYPERCENT"].ToString());
                        xPO.profit = Decimal.Parse(_r["FNPROFITPERCENT"].ToString());
                        xPO.cmpcost = Decimal.Parse(_r["FNCMPCOST"].ToString());
                        Tbl_Imp_CMP.Add(xPO);
                    }

                    string pWith = "";
                    foreach (DataRow _r in _dt.Select("FNCostType = '1'"))
                    {
                        CBDJson_Tbl_Imp_Fabric xPO = new CBDJson_Tbl_Imp_Fabric();

                        pWith = "";
                        if (_r["FNWidth"].ToString().Length > 0)
                        {

                            pWith = _r["FNWidth"].ToString(); //  "0.00"

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == ".")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }
                        }

                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.material_color = _r["FTMainMatColorCode"].ToString();
                        xPO.description = _r["FTMainMatName"].ToString();
                        xPO.vendor = _r["FTSuplCode"].ToString();
                        xPO.country_origin = _r["TTLG"].ToString();
                        xPO.use_location = _r["FTUse"].ToString();
                        xPO.weight = Decimal.Parse(_r["FNWeight"].ToString());  //  "0.00"
                        xPO.width = pWith;
                        xPO.width_unit = _r["FTWidthUnit"].ToString();
                        xPO.marker_efficiency_percentage = Decimal.Parse(_r["FNMarkerEff"].ToString());  // "0.0000"
                        xPO.net_usage = Decimal.Parse(_r["FNMarkerUsed"].ToString());  // "0.0000"
                        xPO.allowance_percentage = Decimal.Parse(_r["FNAllowancePer"].ToString());  //  "0.0000"
                        xPO.gross_usage = Decimal.Parse(_r["FNTotalUsed"].ToString());  //  "0.0000"
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(_r["FNCostPerUOM"].ToString());  // "0.000"
                        xPO.cost_insurance_freight = Decimal.Parse(_r["FNCIF"].ToString());  // "0.0000"
                        xPO.usage_cost = Decimal.Parse(_r["FNUSAGECOST"].ToString());  //  "0.0000"
                        xPO.handling_charge_percentage = Decimal.Parse(_r["FNHANDLINGCHARGEPERCENT"].ToString()) / 100;  //  "0.0000"
                        xPO.handling_charge_cost = Decimal.Parse(_r["FNHANDLINGCHARGECOST"].ToString());  // "0.0000"
                        xPO.import_duty = Decimal.Parse(_r["FNIMPORTDUTYPERCENT"].ToString()) / 100;  // "0.0000"

                        Tbl_Imp_Fabric.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '2'"))
                    {
                        CBDJson_Tbl_Imp_Trims xPO = new CBDJson_Tbl_Imp_Trims();

                        pWith = "";

                        if (_r["FNWidth"].ToString().Length > 0)
                        {
                            pWith = _r["FNWidth"].ToString(); // "0.00"

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == ".")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }
                        }

                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.material_color = _r["FTMainMatColorCode"].ToString();
                        xPO.description = _r["FTMainMatName"].ToString();
                        xPO.vendor = _r["FTSuplCode"].ToString();
                        xPO.country_origin = _r["TTLG"].ToString();
                        xPO.use_location = _r["FTUse"].ToString();
                        xPO.width = pWith;
                        xPO.width_unit = _r["FTWidthUnit"].ToString();
                        xPO.net_usage = Decimal.Parse(_r["FNMarkerUsed"].ToString());  // "0.0000"
                        xPO.allowance_percentage = Decimal.Parse(_r["FNAllowancePer"].ToString()) / 100;  // "0.0000"
                        xPO.gross_usage = Decimal.Parse(_r["FNTotalUsed"].ToString());  // "0.0000"
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(_r["FNCostPerUOM"].ToString());  //  "0.000"
                        xPO.cost_insurance_freight = Decimal.Parse(_r["FNCIF"].ToString());  //  "0.0000"
                        xPO.usage_cost = Decimal.Parse(_r["FNUSAGECOST"].ToString());  // "0.0000"
                        xPO.handling_charge_percentage = Decimal.Parse(_r["FNHANDLINGCHARGEPERCENT"].ToString()) / 100;  // "0.0000"
                        xPO.handling_charge_cost = Decimal.Parse(_r["FNHANDLINGCHARGECOST"].ToString());  //  "0.0000"
                        xPO.import_duty = Decimal.Parse(_r["FNIMPORTDUTYPERCENT"].ToString()) / 100;  // "0.0000"

                        Tbl_Imp_Trims.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '3'"))
                    {
                        CBDJson_Tbl_Imp_Process_Mtrl xPO = new CBDJson_Tbl_Imp_Process_Mtrl();
                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.description = _r["FTMainMatName"].ToString();
                        xPO.process_subtype = _r["FTPROCESSSUBTYPE"].ToString();
                        xPO.vendor = _r["FTSuplCode"].ToString();
                        xPO.country_origin = _r["TTLG"].ToString();
                        xPO.use_location = _r["FTUse"].ToString();
                        xPO.net_usage = Decimal.Parse(_r["FNMarkerUsed"].ToString());  // "0.0000"
                        xPO.allowance_percentage = Decimal.Parse(_r["FNAllowancePer"].ToString()) / 100;  // "0.0000"
                        xPO.gross_usage = Decimal.Parse(_r["FNTotalUsed"].ToString());  //  "0.0000"
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(_r["FNCostPerUOM"].ToString());  //  "0.000"
                        xPO.usage_cost = Decimal.Parse(_r["FNUSAGECOST"].ToString());  // "0.0000"
                        xPO.import_duty = Decimal.Parse(_r["FNIMPORTDUTYPERCENT"].ToString()) / 100;  // "0.0000"

                        Tbl_Imp_Process_Mtrl.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '4'"))
                    {
                        CBDJson_Tbl_Imp_Process_Labor xPO = new CBDJson_Tbl_Imp_Process_Labor();
                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.process_subtype = _r["FTPROCESSSUBTYPE"].ToString();
                        xPO.description = _r["FTMainMatName"].ToString();
                        xPO.vendor = _r["FTSuplCode"].ToString();
                        xPO.country_origin = _r["TTLG"].ToString();
                        xPO.use_location = _r["FTUse"].ToString();
                        xPO.gross_usage = Decimal.Parse(_r["FNTotalUsed"].ToString()); // "0.0000"
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(_r["FNCostPerUOM"].ToString());  //  "0.000"
                        xPO.usage_cost = Decimal.Parse(_r["FNUSAGECOST"].ToString());  // "0.0000"
                        xPO.import_duty = Decimal.Parse(_r["FNIMPORTDUTYPERCENT"].ToString()) / 100;  // "0.0000"

                        Tbl_Imp_Process_Labor.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '5'"))
                    {
                        CBDJson_Tbl_Imp_Packaging xPO = new CBDJson_Tbl_Imp_Packaging();
                        pWith = "";
                        if (_r["FNWidth"].ToString().Length > 0)
                        {
                            pWith = _r["FNWidth"].ToString(); //  "0.00"

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == "0")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }

                            if (Microsoft.VisualBasic.Strings.Right(pWith, 1) == ".")
                            {
                                pWith = Microsoft.VisualBasic.Strings.Left(pWith, pWith.Length - 1);
                            }
                        }

                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.description = _r["FTMainMatName"].ToString();
                        xPO.vendor = _r["FTSuplCode"].ToString();
                        xPO.country_origin = _r["TTLG"].ToString();
                        xPO.use_location = _r["FTUse"].ToString();
                        xPO.width = pWith;
                        xPO.width_unit = _r["FTWidthUnit"].ToString();

                        xPO.net_usage = Decimal.Parse(_r["FNMarkerUsed"].ToString());  //  "0.0000"
                        xPO.allowance_percentage = Decimal.Parse(_r["FNAllowancePer"].ToString()) / 100;  //  "0.0000"
                        xPO.gross_usage = Decimal.Parse(_r["FNTotalUsed"].ToString());  //  "0.0000"
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(_r["FNCostPerUOM"].ToString());  //  "0.000"
                        xPO.cost_insurance_freight = Decimal.Parse(_r["FNCIF"].ToString());  //  "0.0000"
                        xPO.usage_cost = Decimal.Parse(_r["FNUSAGECOST"].ToString());  // "0.0000"
                        xPO.handling_charge_percentage = Decimal.Parse(_r["FNHANDLINGCHARGEPERCENT"].ToString()) / 100;  // "0.00"
                        xPO.handling_charge_cost = Decimal.Parse(_r["FNHANDLINGCHARGECOST"].ToString());  // "0.0000"
                        xPO.import_duty = Decimal.Parse(_r["FNIMPORTDUTYPERCENT"].ToString()) / 100;  //  "0.00"

                        Tbl_Imp_Packaging.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '7'"))
                    {
                        CBDJson_Tbl_Imp_BEMIS xPO = new CBDJson_Tbl_Imp_BEMIS();

                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.full_width = Decimal.Parse(_r["FNFULLWIDTH"].ToString());  // "0.0000"
                        xPO.slitting_width = Decimal.Parse(_r["FNSLITTINGWIDTH"].ToString());  //  "0.0000"
                        xPO.required_length = Decimal.Parse(_r["FNREQUIREDLENGTH"].ToString());  //  "0.0000"
                        xPO.usage_full_width = Decimal.Parse(_r["FNNETUSAGEINFULLWIDTH"].ToString());  //  "0.0000"
                        xPO.price_meter = Decimal.Parse(_r["FNPRICEINMETER"].ToString());  //  "0.0000"
                        xPO.bemis_slitting_percentage = Decimal.Parse(_r["FNBEMISSLITTINGUPCHARGE"].ToString());  // "0.0000"
                        xPO.price_slitting_width = Decimal.Parse(_r["FNPRICEPERSLITTINGWITDH"].ToString());  // "0.0000"
                        Tbl_Imp_BEMIS.Add(xPO);
                    }

                    if (teamMulti)
                    {
                        CBDMultiJson JSonHeadcer = new CBDMultiJson();
                        JSonHeadcer.CBDID = r["FTFileName"].ToString();
                        JSonHeadcer.comment = r["FTRemark"].ToString();
                        JSonHeadcer.season = r["season"].ToString();
                        JSonHeadcer.factory_code = r["factory_code"].ToString();
                        JSonHeadcer.style_no = r["style_no"].ToString();
                        JSonHeadcer.style_name = r["style_name"].ToString();
                        JSonHeadcer.is_team_cbd = r["is_team_cbd"].ToString();
                        JSonHeadcer.color = r["color"].ToString();
                        JSonHeadcer.size = r["size"].ToString();
                        JSonHeadcer.buy_type = r["buy_type"].ToString();
                        JSonHeadcer.version = int.Parse(r["version"].ToString());
                        JSonHeadcer.msc = r["msc"].ToString();
                        JSonHeadcer.developer = r["developer"].ToString();
                        JSonHeadcer.cbd_quote_status = r["cbd_quote_status"].ToString();
                        JSonHeadcer.sample_round = r["sample_round"].ToString();
                        JSonHeadcer.base_style_no = r["base_style_no"].ToString();
                        if (r["quoted_date"].ToString() != "")
                        {
                            JSonHeadcer.quoted_date = Convert.ToDateTime(r["quoted_date"].ToString());
                        }
                        JSonHeadcer.quote_log = r["quote_log"].ToString();
                        JSonHeadcer.comment = r["comment"].ToString();

                        // Team Multi Process

                        Qry = "  Select C.* ";
                        Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail_TeamMulti As C With (NOLOCK)  ";
                        Qry += " WHERE C.FTCostSheetNo='" + Docno + "' ";
                        Qry += " ORDER BY FNSeq ";
                        _dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                        //    For Each R As DataRow In dt.Select("FTCostSheetNo<>''", "FNSeq")
                        foreach (DataRow _r in _dt.Select("FTCostSheetNo <> ''", "FNSeq"))
                        {

                            CBDJson_ChildCbds xChildCbds = new CBDJson_ChildCbds();
                            List<CBDJson_embellishments> embellishments = new List<CBDJson_embellishments>();
                            List<CBDJson_child_l4ls> child_l4ls = new List<CBDJson_child_l4ls>();

                            for (int i = 1; i <= 15; i++)
                            {
                                if (r["FTItem" + i].ToString().Length > 0)
                                {
                                    CBDJson_embellishments pembellishments = new CBDJson_embellishments();

                                    pembellishments.emb_pps_item_number = r["FTItem" + i].ToString();
                                    pembellishments.process_type = r["FTProcesssubType" + i].ToString();
                                    pembellishments.emb_description = r["FTDescription" + i].ToString();
                                    pembellishments.emb_vendor = r["FTSuplCode" + i].ToString();
                                    pembellishments.emb_unit_price = Decimal.Parse(r["FNUnitPrice" + i].ToString());
                                    pembellishments.emb_cost_insurance_freight = Decimal.Parse(r["FNCIF" + i].ToString());
                                    //Decimal.Parse(Format(Val(R.Item("" & I.ToString).ToString), "0.00")) 'Decimal.Parse(Format(Val(R.Item("FNCIF" & I.ToString).ToString) / 100.0, "0.0000")) 'Val(R.Item("FNCIF" & I.ToString).ToString)
                                    pembellishments.emb_usage_cost = Decimal.Parse(r["FNUSAGECOST" + i].ToString());
                                    pembellishments.emb_handling_percentage = Decimal.Parse(r["FNHandlingChargePercent" + i].ToString()); //Decimal.Parse(Format(Val(R.Item("" & I.ToString).ToString) / 100.0, "0.0000")) 'Val(R.Item("FNHandlingChargePercent" & I.ToString).ToString)
                                    pembellishments.emb_handling_cost = Decimal.Parse(r["FNHandlingChargeCost" + i].ToString());
                                    pembellishments.emb_total_trim_cost = Decimal.Parse(r["FNTotalCost" + i].ToString());
                                    pembellishments.import_duty = Decimal.Parse(r["FNImportDutyPecent" + i].ToString());
                                    //Decimal.Parse(Format(Val(R.Item("" & I.ToString).ToString) / 100.0, "0.0000")) 'Val(R.Item("FNImportDutyPecent" & I.ToString).ToString)
                                    embellishments.Add(pembellishments);
                                }
                            }

                            CBDJson_child_l4ls pchild_l4ls = new CBDJson_child_l4ls();
                            pchild_l4ls.l4l_fob = Decimal.Parse(r["FTL4LCURRENCYFOB1"].ToString());
                            pchild_l4ls.l4l_extended_fob = Decimal.Parse(r["FNEXTENDSIZEFOBL4L1"].ToString());
                            pchild_l4ls.l4l_country = r["FTL4LORDERCNTY1"].ToString();
                            child_l4ls.Add(pchild_l4ls);

                            pchild_l4ls.l4l_fob = Decimal.Parse(r["FTL4LCURRENCYFOB2"].ToString());
                            pchild_l4ls.l4l_extended_fob = Decimal.Parse(r["FNEXTENDSIZEFOBL4L2"].ToString());
                            pchild_l4ls.l4l_country = r["FTL4LORDERCNTY2"].ToString();
                            child_l4ls.Add(pchild_l4ls);

                            pchild_l4ls.l4l_fob = Decimal.Parse(r["FTL4LCURRENCYFOB3"].ToString());
                            pchild_l4ls.l4l_extended_fob = Decimal.Parse(r["FNEXTENDSIZEFOBL4L3"].ToString());
                            pchild_l4ls.l4l_country = r["FTL4LORDERCNTY3"].ToString();
                            child_l4ls.Add(pchild_l4ls);

                            xChildCbds.msc = r["FTMSC"].ToString();
                            xChildCbds.season = r["FTSeason"].ToString();
                            xChildCbds.style_no = r["FTStyleCode"].ToString();
                            xChildCbds.style_name = r["FTTeamName"].ToString();
                            xChildCbds.color = r["FTColorway"].ToString();
                            xChildCbds.embellishments = embellishments;
                            xChildCbds.child_l4ls = child_l4ls;
                            xChildCbds.developer = r["FTPRODUCTDEVELOPER"].ToString();
                            xChildCbds.comment = r["FTRemark"].ToString();

                            ChildCbds.Add(xChildCbds);
                        }


                        JSonHeadcer.Tbl_Imp_FOBSummary = Tbl_Imp_FOBSummary;
                        JSonHeadcer.Tbl_Imp_CMP = Tbl_Imp_CMP;
                        JSonHeadcer.Tbl_Imp_L4L1 = Tbl_Imp_L4L1;
                        JSonHeadcer.Tbl_Imp_L4L2 = Tbl_Imp_L4L2;
                        JSonHeadcer.Tbl_Imp_L4L3 = Tbl_Imp_L4L3;
                        JSonHeadcer.Tbl_Imp_Fabric = Tbl_Imp_Fabric;
                        JSonHeadcer.Tbl_Imp_Trims = Tbl_Imp_Trims;
                        JSonHeadcer.Tbl_Imp_Process_Mtrl = Tbl_Imp_Process_Mtrl;
                        JSonHeadcer.Tbl_Imp_Process_Labor = Tbl_Imp_Process_Labor;
                        JSonHeadcer.Tbl_Imp_Packaging = Tbl_Imp_Packaging;
                        JSonHeadcer.Tbl_Imp_BEMIS = Tbl_Imp_BEMIS;
                        JSonHeadcer.ChildCbds = ChildCbds;

                        updateStateJSON(Docno);
                    }
                    else
                    {
                        CBDJson JSonHeadcer = new CBDJson();
                        JSonHeadcer.CBDID = r["FTFileName"].ToString();
                        JSonHeadcer.comment = r["FTRemark"].ToString();
                        JSonHeadcer.season = r["season"].ToString();
                        JSonHeadcer.factory_code = r["factory_code"].ToString();
                        JSonHeadcer.style_no = r["style_no"].ToString();
                        JSonHeadcer.style_name = r["style_name"].ToString();
                        JSonHeadcer.is_team_cbd = r["is_team_cbd"].ToString();
                        JSonHeadcer.color = r["color"].ToString();
                        JSonHeadcer.size = r["size"].ToString();
                        JSonHeadcer.buy_type = r["buy_type"].ToString();
                        JSonHeadcer.version = int.Parse(r["version"].ToString());
                        JSonHeadcer.msc = r["msc"].ToString();
                        JSonHeadcer.developer = r["developer"].ToString();
                        JSonHeadcer.cbd_quote_status = r["cbd_quote_status"].ToString();
                        JSonHeadcer.sample_round = r["sample_round"].ToString();
                        JSonHeadcer.base_style_no = r["base_style_no"].ToString();
                        if (r["quoted_date"].ToString() != "")
                        {
                            JSonHeadcer.quoted_date = Convert.ToDateTime(r["quoted_date"].ToString());
                        }
                        JSonHeadcer.quote_log = r["quote_log"].ToString();
                        JSonHeadcer.comment = r["comment"].ToString();

                        JSonHeadcer.Tbl_Imp_FOBSummary = Tbl_Imp_FOBSummary;
                        JSonHeadcer.Tbl_Imp_CMP = Tbl_Imp_CMP;
                        JSonHeadcer.Tbl_Imp_L4L1 = Tbl_Imp_L4L1;
                        JSonHeadcer.Tbl_Imp_L4L2 = Tbl_Imp_L4L2;
                        JSonHeadcer.Tbl_Imp_L4L3 = Tbl_Imp_L4L3;
                        JSonHeadcer.Tbl_Imp_Fabric = Tbl_Imp_Fabric;
                        JSonHeadcer.Tbl_Imp_Trims = Tbl_Imp_Trims;
                        JSonHeadcer.Tbl_Imp_Process_Mtrl = Tbl_Imp_Process_Mtrl;
                        JSonHeadcer.Tbl_Imp_Process_Labor = Tbl_Imp_Process_Labor;
                        JSonHeadcer.Tbl_Imp_Packaging = Tbl_Imp_Packaging;
                        JSonHeadcer.Tbl_Imp_BEMIS = Tbl_Imp_BEMIS;

                         updateStateJSON(Docno);
                    }
                }

            }
        }

        private void ocmSendJSON_Click(object sender, EventArgs e)
        {

        }

        private static void updateStateJSON(string Docno)
        {
            string Qry = "Update B SET FTStateExport='1'";
            Qry += " , FTStateExportUser ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
            Qry += " ,  FDStateExportDate=" + HI.UL.ULDate.FormatDateDB + " ";
            Qry += " ,  FTStateExportTime=" + HI.UL.ULDate.FormatTimeDB + " ";
            Qry += "   FROM            " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet As A ";
            Qry += "   WHERE   A.FTCostSheetNo='" + HI.UL.ULF.rpQuoted(Docno) + "'";

            if (HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_ACCOUNT) == false)
            {

            }

            string pFolderJson = _AppCBDJsonPath + "\\" + //FTFileName.Text.Trim.Replace(" / ", " - ").Replace("\", " - ") & " +
                " Post By  " + HI.ST.UserInfo.UserName + "  Time " + DateTime.Now.ToString("yyyyMMdd HHmmss");

            if (!System.IO.Directory.Exists(pFolderJson))
            {
                System.IO.Directory.CreateDirectory(pFolderJson);
            }

            //If SendJSONFile(IIf(StateMulti, Nothing, JSonHeadcer), Docno, pFolderJson, pMail, pMailPassword, dtdata, IIf(StateMulti, JSonHeadcer, Nothing), SateCBD, StatePicture, StateMark) Then
            
            //return (true) ? true : false;
        }
    }
}
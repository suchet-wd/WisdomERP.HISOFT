﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HI.MER;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using DevExpress.XtraTab;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;

namespace HI.Track
{
    public partial class wSendJson : DevExpress.XtraEditors.XtraForm
    {
        private static string _SysPath = Application.StartupPath + "\\";
        private static string tW_SysPath = Application.StartupPath + "\\Images";
        private static string _AppCBDJsonPath = Application.StartupPath + "\\CBDJson";
        private static List<HI.Track.Class.listJSONS> listJSONs = new List<HI.Track.Class.listJSONS>();

        public wSendJson()
        {
            InitializeComponent();

            //List<string, bool, bool, bool> _listSelected = new List<string, bool, bool, bool>();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            if (VerifyField())
            {
                String _Qry = "";
                try
                {
                    ogcDetail.DataSource = null;
                    _Qry = "SELECT DISTINCT '0' AS CBD, '0' AS Picture, '0' AS Mark ";
                    _Qry += "  , (SELECT top 1 JH.FNSeq FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'CBD Json Standard' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendCBD' ";

                    _Qry += "  , (SELECT top 1 CONVERT(varchar(10),CONVERT(datetime,JH.FDSendDate), 103) FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'CBD Json Standard' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendCBDDate' ";

                    _Qry += "  , (SELECT top 1 JH.FNSeq FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'Picture' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendPicture' ";

                    _Qry += "  , (SELECT top 1 CONVERT(varchar(10),CONVERT(datetime,JH.FDSendDate), 103) FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'Picture' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendPictureDate' ";

                    _Qry += "  , (SELECT top 1 JH.FNSeq FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'Mark' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendMark' ";

                    _Qry += "  , (SELECT top 1 CONVERT(varchar(10),CONVERT(datetime,JH.FDSendDate), 103) FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_JsonHistory AS JH WITH(NOLOCK) ";
                    _Qry += " WHERE  JH.FTCostSheetNo = CS.FTCostSheetNo AND JH.FTSendType = 'Mark' ";
                    _Qry += " ORDER BY JH.FNSeq DESC) AS 'SendMarkDate' ";
                    _Qry += ", CS.FTCostSheetNo, CONVERT(varchar(10),CONVERT(datetime,CS.FDCostSheetDate), 103) AS 'FDCostSheetDate', CS.FTCostSheetBy, CS.FNCostSheetColor, CS.FNVersion, CS.FNCostSheetQuotedType , " +
                     " CS.FNCostSheetSampleRound, CS.FTCostSheetBy, CS.FTLOProductDeveloper, CS.FNISTeamMulti, CS.FTMSC, CS.FNCostSheetColor, " +
                     " CS.FNCostSheetSize, S.FTStyleCode, Sea.FTSeasonCode, S.FTStyleCode ";

                    if ((HI.ST.Lang.Language).ToString() == "TH")
                    {
                        _Qry += " , mer.FTMerTeamNameTH AS FTMerTeamName ";
                    }
                    else
                    {
                        _Qry += ", mer.FTMerTeamNameEN AS FTMerTeamName ";
                    }

                    _Qry += " , CS.FTStateActive, C.FTCmpCode, CS.FNCostSheetBuyType, CS.FNCostSheetQuotedType ";

                    _Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet AS CS WITH ( NOLOCK ) ";

                    _Qry += "OUTER APPLY (SELECT C.FTCmpCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) +
                        ".dbo.TCNMCmp AS C WITH (NOLOCK) WHERE CS.FNHSysCmpId = C.FNHSysCmpId) AS C";

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
                         // _Qry += " WHERE CS.FTCostSheetBy = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "' " +
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
            }
            else
            {
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            }
            _Spls.Close();
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



        private void ocmpostdatajson2_Click(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            if (listJSONs.Count > 0)
            {
                foreach (HI.Track.Class.listJSONS _l in listJSONs)
                {
                    Console.WriteLine(_l.CostsheetNo + "-> CBD = " + _l.CBD + " Picture = " + _l.Picture + " Mark = " + _l.Mark + " ");
                }
            }
            else
            {
                Console.WriteLine("No Costsheet Selected!!!");
            }

            if (txtmail.Text != "" && txtpassword.Text != "" && listJSONs.Count > 0)
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


                //for (int i = 0; i < ogvDetail.RowCount; i++)
                //for (int i = 0; i < listJSONs.Count; i++)
                foreach (HI.Track.Class.listJSONS _l in listJSONs)
                {
                    //Boolean StateSendData = false;
                    //Boolean StateSendCBD = false;
                    //Boolean StateSendPicture = false;
                    //Boolean StateSendMarkD = false;
                    //StateSendCBD = _l.CBD.Equals("1") ? true : false;
                    //StateSendPicture = _l.Picture.Equals("1") ? true : false;
                    //StateSendMarkD = _l.Mark.Equals("1") ? true : false;

                    //if (StateSendCBD || StateSendPicture || StateSendMarkD)
                    if (_l.CBD || _l.Picture || _l.Mark)
                    {
                        Qry = "select TOP 1  '" + _l.CBD + "' AS FTSelect, 1 FNSeq, FTStateExportUser, FDStateExportDate, FTStateExportTime,";
                        Qry += _l.TeamMulti.Equals("Y") ? "'CBD Json Team Multi'" : "'CBD Json Standard'";
                        Qry += " As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + _l.CostsheetNo + "'" +
                            " UNION " +
                            " select TOP 1  '" + _l.Picture + "' AS FTSelect, 2 FNSeq, FTStateImageExportUser, FDStateImageExportDate, FTStateImageExportTime,'Picture' As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription  " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_File WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + _l.CostsheetNo + "'" +
                            " UNION " +
                            " select TOP 1  '" + _l.Mark + "' AS FTSelect, 3 FNSeq, FTStateMarkExportUser, FDStateMarkExportDate, FTStateMarkExportTime,'Mark' As FTSendType,'' AS FTSendStatus ,'' AS FTSendStatusDescription  " +
                            " from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_File WITH(NOLOCK)  " +
                            " WHERE FTCostSheetNo='" + _l.CostsheetNo + "'";

                        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                        bool teamMulti = _l.TeamMulti.Equals("Y") ? true : false;
                        //r["FTFileName"].ToString(), r["version"].ToString(),
                        SetdataJSON(_l.CostsheetNo, pMail, pMailPassword, ref dt, teamMulti, _l.CBD, _l.Picture, _l.Mark);
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
            string VenderPramCode = "";
            string Material = "";
            string FTCurCode = "";
            int RIndx = 0;
            decimal InvAmt = 0;
            decimal GInvAmt = 0;

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


            Qry = "  Select C.* ,  ISNULL(C.FTStyleCode,'') + '|' +   ISNULL(C.FTMainMatColorCode,'') + '|' +  ISNULL(C.FTTeamName,'')  AS FTTeam";
            Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  ";
            Qry += " WHERE C.FTCostSheetNo='" + Docno + "' ";

            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);


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

            Qry += " OUTER APPLY(SELECT * FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp WITH (NOLOCK ) WHERE FNHSysCmpId= CS.FNHSysCmpId) AS Cmp ";

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
                    Tbl_Imp_FOBSummary.extended_size_adjustment = decimal.Parse(string.Format(r["FNExtendedPer"].ToString(), "0.0000")) / 100;
                    Tbl_Imp_FOBSummary.final_fob = decimal.Parse(r["FNGrandTotal"].ToString());
                    Tbl_Imp_FOBSummary.extended_size_fob = decimal.Parse(r["FNExtendedFOB"].ToString());
                    Tbl_Imp_FOBSummary.trim_usage_allowance = decimal.Parse(string.Format(r["FNTrinUsageAllowPer"].ToString(), "0.0000")) / 100;

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

                            pWith = string.Format(_r["FNWidth"].ToString(), "0.00");

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
                        xPO.weight = Decimal.Parse(string.Format(_r["FNWeight"].ToString(), "0.00"));
                        xPO.width = pWith;
                        xPO.width_unit = _r["FTWidthUnit"].ToString();
                        xPO.marker_efficiency_percentage = Decimal.Parse(string.Format(_r["FNMarkerEff"].ToString(), "0.0000"));
                        xPO.net_usage = Decimal.Parse(string.Format(_r["FNMarkerUsed"].ToString(), "0.0000"));
                        xPO.allowance_percentage = Decimal.Parse(string.Format(_r["FNAllowancePer"].ToString(), "0.0000"));
                        xPO.gross_usage = Decimal.Parse(string.Format(_r["FNTotalUsed"].ToString(), "0.0000"));
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(string.Format(_r["FNCostPerUOM"].ToString(), "0.000"));
                        xPO.cost_insurance_freight = Decimal.Parse(string.Format(_r["FNCIF"].ToString(), "0.0000"));
                        xPO.usage_cost = Decimal.Parse(string.Format(_r["FNUSAGECOST"].ToString(), "0.0000"));
                        xPO.handling_charge_percentage = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGEPERCENT"].ToString(), "0.0000")) / 100;
                        xPO.handling_charge_cost = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGECOST"].ToString(), "0.0000"));
                        xPO.import_duty = Decimal.Parse(string.Format(_r["FNIMPORTDUTYPERCENT"].ToString(), "0.0000")) / 100;

                        Tbl_Imp_Fabric.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '2'"))
                    {
                        CBDJson_Tbl_Imp_Trims xPO = new CBDJson_Tbl_Imp_Trims();

                        pWith = "";

                        if (_r["FNWidth"].ToString().Length > 0)
                        {
                            pWith = string.Format(_r["FNWidth"].ToString(), "0.00");

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
                        xPO.net_usage = Decimal.Parse(string.Format(_r["FNMarkerUsed"].ToString(), "0.0000"));
                        xPO.allowance_percentage = Decimal.Parse(string.Format(_r["FNAllowancePer"].ToString(), "0.0000")) / 100;
                        xPO.gross_usage = Decimal.Parse(string.Format(_r["FNTotalUsed"].ToString(), "0.0000"));
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(string.Format(_r["FNCostPerUOM"].ToString(), "0.000"));
                        xPO.cost_insurance_freight = Decimal.Parse(string.Format(_r["FNCIF"].ToString(), "0.0000"));
                        xPO.usage_cost = Decimal.Parse(string.Format(_r["FNUSAGECOST"].ToString(), "0.0000"));
                        xPO.handling_charge_percentage = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGEPERCENT"].ToString(), "0.0000")) / 100;
                        xPO.handling_charge_cost = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGECOST"].ToString(), "0.0000"));
                        xPO.import_duty = Decimal.Parse(string.Format(_r["FNIMPORTDUTYPERCENT"].ToString(), "0.0000")) / 100;

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
                        xPO.net_usage = Decimal.Parse(string.Format(_r["FNMarkerUsed"].ToString(), "0.0000"));
                        xPO.allowance_percentage = Decimal.Parse(string.Format(_r["FNAllowancePer"].ToString(), "0.0000")) / 100;
                        xPO.gross_usage = Decimal.Parse(string.Format(_r["FNTotalUsed"].ToString(), "0.0000"));
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(string.Format(_r["FNCostPerUOM"].ToString(), "0.000"));
                        xPO.usage_cost = Decimal.Parse(string.Format(_r["FNUSAGECOST"].ToString(), "0.0000"));
                        xPO.import_duty = Decimal.Parse(string.Format(_r["FNIMPORTDUTYPERCENT"].ToString(), "0.0000")) / 100;

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
                        xPO.gross_usage = Decimal.Parse(string.Format(_r["FNTotalUsed"].ToString(), "0.0000"));
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(string.Format(_r["FNCostPerUOM"].ToString(), "0.000"));
                        xPO.usage_cost = Decimal.Parse(string.Format(_r["FNUSAGECOST"].ToString(), "0.0000"));
                        xPO.import_duty = Decimal.Parse(string.Format(_r["FNIMPORTDUTYPERCENT"].ToString(), "0.0000")) / 100;

                        Tbl_Imp_Process_Labor.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '5'"))
                    {
                        CBDJson_Tbl_Imp_Packaging xPO = new CBDJson_Tbl_Imp_Packaging();
                        pWith = "";
                        if (_r["FNWidth"].ToString().Length > 0)
                        {
                            pWith = string.Format(_r["FNWidth"].ToString(), "0.00");

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

                        xPO.net_usage = Decimal.Parse(string.Format(_r["FNMarkerUsed"].ToString(), "0.0000"));
                        xPO.allowance_percentage = Decimal.Parse(string.Format(_r["FNAllowancePer"].ToString(), "0.0000")) / 100;
                        xPO.gross_usage = Decimal.Parse(string.Format(_r["FNTotalUsed"].ToString(), "0.0000"));
                        xPO.rmds_season = _r["FTRMDSSeason"].ToString();
                        xPO.rmds_status = _r["FNRMDSStatus"].ToString();
                        xPO.uom = _r["FTUnitCode"].ToString();
                        xPO.unit_price = Decimal.Parse(string.Format(_r["FNCostPerUOM"].ToString(), "0.000"));
                        xPO.cost_insurance_freight = Decimal.Parse(string.Format(_r["FNCIF"].ToString(), "0.0000"));
                        xPO.usage_cost = Decimal.Parse(string.Format(_r["FNUSAGECOST"].ToString(), "0.0000"));
                        xPO.handling_charge_percentage = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGEPERCENT"].ToString(), "0.00")) / 100;
                        xPO.handling_charge_cost = Decimal.Parse(string.Format(_r["FNHANDLINGCHARGECOST"].ToString(), "0.0000"));
                        xPO.import_duty = Decimal.Parse(string.Format(_r["FNIMPORTDUTYPERCENT"].ToString(), "0.00")) / 100;

                        Tbl_Imp_Packaging.Add(xPO);
                    }

                    foreach (DataRow _r in _dt.Select("FNCostType = '7'"))
                    {
                        CBDJson_Tbl_Imp_BEMIS xPO = new CBDJson_Tbl_Imp_BEMIS();

                        xPO.pps_item_number = _r["FTMainMatCode"].ToString();
                        xPO.full_width = Decimal.Parse(string.Format(_r["FNFULLWIDTH"].ToString(), "0.0000"));
                        xPO.slitting_width = Decimal.Parse(string.Format(_r["FNSLITTINGWIDTH"].ToString(), "0.0000"));
                        xPO.required_length = Decimal.Parse(string.Format(_r["FNREQUIREDLENGTH"].ToString(), "0.0000"));
                        xPO.usage_full_width = Decimal.Parse(string.Format(_r["FNNETUSAGEINFULLWIDTH"].ToString(), "0.0000"));
                        xPO.price_meter = Decimal.Parse(string.Format(_r["FNPRICEINMETER"].ToString(), "0.0000"));
                        xPO.bemis_slitting_percentage = Decimal.Parse(string.Format(_r["FNBEMISSLITTINGUPCHARGE"].ToString(), "0.0000"));
                        xPO.price_slitting_width = Decimal.Parse(string.Format(_r["FNPRICEPERSLITTINGWITDH"].ToString(), "0.0000"));
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
                                    pembellishments.emb_usage_cost = Decimal.Parse(r["FNUSAGECOST" + i].ToString());
                                    pembellishments.emb_handling_percentage = Decimal.Parse(r["FNHandlingChargePercent" + i].ToString());
                                    pembellishments.emb_handling_cost = Decimal.Parse(r["FNHandlingChargeCost" + i].ToString());
                                    pembellishments.emb_total_trim_cost = Decimal.Parse(r["FNTotalCost" + i].ToString());
                                    pembellishments.import_duty = Decimal.Parse(r["FNImportDutyPecent" + i].ToString());
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

                        updateStateJSON(null, Docno, pMail, pMailPassword,
                            dtdata, JSonHeadcer, SateCBD, StatePicture, StateMark);
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

                        updateStateJSON(JSonHeadcer, Docno, pMail, pMailPassword,
                            dtdata, null, SateCBD, StatePicture, StateMark);
                    }
                }

            }
        }

        private void ocmSendJSON_Click(object sender, EventArgs e)
        {

        }

        private static void updateStateJSON(CBDJson EFSData, string DocNo,
            string pMail, string pMailPassword, DataTable dtdata,
            CBDMultiJson EFSMulti, bool SateCBD, bool StatePicture, bool StateMark)
        {
            string Qry = "";
            /*
            string Qry = "Update A SET FTStateExport='1'";
            Qry += " , FTStateExportUser ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
            Qry += " ,  FDStateExportDate=" + HI.UL.ULDate.FormatDateDB + " ";
            Qry += " ,  FTStateExportTime=" + HI.UL.ULDate.FormatTimeDB + " ";
            Qry += " FROM  " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet As A ";
            Qry += " WHERE A.FTCostSheetNo='" + DocNo + "'";

            if (HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_ACCOUNT) == false)
            {
                Console.WriteLine("Cannot update FTStateExport!!!");
            }
            */

            string pFolderJson = _AppCBDJsonPath + "\\" + EFSData.CBDID.Trim().Replace(" / ", " - ").Replace("\\", " - ") +
                " Post By  " + HI.ST.UserInfo.UserName + "  Time " + DateTime.Now.ToString("yyyyMMdd HHmmss");

            if (!System.IO.Directory.Exists(pFolderJson))
            {
                System.IO.Directory.CreateDirectory(pFolderJson);
            }

            int PageCount = 0;
            bool StateTeamMulti = (EFSMulti == null) ? true : false;
            bool postcbdjsonSsatus = false;
            string postcbdjsommessage = "";
            bool postimagejsonSsatus = false;
            string postimagejsommessage = "";
            bool postmarkjsonSsatus = false;
            string postmarkjsommessage = "";
            string OktaurlEndPoint = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token";
            string EFSurlEndPoint = "https://apcm-apim-qa.partner.nike-cloud.com/service-api/post_AddStandardCBD";

            if (StateTeamMulti)
            {
                EFSurlEndPoint = "https://apcm-apim-qa.partner.nike-cloud.com/service-api/post_AddTeamMultiCBD";
            }

            string clientid = "nike.niketech.gsmapcm-service";
            string clientsecret = "zVQ8JMsMuEmlOIpdz7o79CMqjsSdteFE6G7Eay2Tl4iP5QJBoCxV29exV11kCwJo";
            string granttype = "client_credentials";
            // Refer to the documentation for more information on how to get the tokens
            string accessToken = "";
            string accessToken_id = "";
            string EFSjson_data = "";
            // -- Refresh the access token
            System.Net.WebRequest request = System.Net.HttpWebRequest.Create(OktaurlEndPoint);
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            string svcCredentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(clientid + ":" + clientsecret));
            request.Headers.Add("Authorization", "Basic " + svcCredentials);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string json_data = string.Format("grant_type=password&scope=iam.okta.factoryaffiliations.read iam.okta.factorygroups.read iam.okta.job.read iam.okta.address.read iam.okta.location.read openid email&username={0}&password={1}", (pMail), (pMailPassword));

            Byte[] postBytes = System.Text.Encoding.ASCII.GetBytes(json_data);


            //ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Stream postStream = request.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Flush();
            postStream.Close();

            bool StateAppcept = false;
            accessToken = "";
            accessToken_id = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  
                //DirectCast(3072, SecurityProtocolType)
                System.Net.WebResponse response = request.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream());
                // Parse the JSON the way you prefer
                string jsonResponseText = streamReader.ReadToEnd();
                RefreshTokenResultJSON jsonResult = JsonConvert.DeserializeObject<RefreshTokenResultJSON>(jsonResponseText);
                accessToken = jsonResult.access_token;
                accessToken_id = jsonResult.id_token;

                if (SateCBD)
                {
                    try
                    {
                        EFSjson_data = (StateTeamMulti == false) ? JsonConvert.SerializeObject(EFSData) : JsonConvert.SerializeObject(EFSMulti);

                        Byte[] EFSpostBytes = System.Text.Encoding.ASCII.GetBytes(EFSjson_data);

                        System.Net.WebRequest requestpost = System.Net.HttpWebRequest.Create(EFSurlEndPoint);
                        requestpost.UseDefaultCredentials = true;
                        requestpost.PreAuthenticate = true;
                        requestpost.Credentials = CredentialCache.DefaultCredentials;
                        requestpost.Method = "POST";
                        requestpost.ContentType = "application/json";
                        requestpost.Headers.Add("Authorization", "Bearer " + accessToken);
                        requestpost.Headers.Add("id_token", accessToken_id);
                        requestpost.Headers.Add("Gsmapcm-Service-Api-Subscription-Key", "b565689abdbe43b9b2ae032405d41f63");

                        Stream postStreamdata = requestpost.GetRequestStream();
                        postStreamdata.Write(EFSpostBytes, 0, EFSpostBytes.Length);
                        postStreamdata.Flush();
                        postStreamdata.Close();

                        System.Net.WebResponse responsepost = requestpost.GetResponse();
                        System.IO.StreamReader streamReaderpost = new System.IO.StreamReader(responsepost.GetResponseStream());
                        string jsonResponsePostText = streamReaderpost.ReadToEnd();
                        RefreshResultJSON jsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(jsonResponsePostText);
                        postcbdjsonSsatus = jsonPostResult.status;
                        postcbdjsommessage = jsonPostResult.message;

                        if (postcbdjsonSsatus)
                        {
                            string strFile = pFolderJson + "\\" + EFSData.CBDID.Trim().Replace(" / ", " - ").Replace("\\", " - ") +
                                " Post By  " + HI.ST.UserInfo.UserName + "  Time " + DateTime.Now.ToString("yyyyMMdd HHmmss") + "" + ".txt";

                            try
                            {
                                File.Delete(strFile);
                            }
                            catch (Exception ex) { }

                            bool fileExists = File.Exists(strFile);

                            StreamWriter sw = new StreamWriter(File.Open(strFile, FileMode.OpenOrCreate));
                            sw.WriteLine((fileExists) ? EFSjson_data : EFSjson_data);

                            Qry = "Update A SET FTStateExport='1'";
                            Qry += " , FTStateExportUser ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            Qry += " , FDStateExportDate=" + HI.UL.ULDate.FormatDateDB + " ";
                            Qry += " , FTStateExportTime=" + HI.UL.ULDate.FormatTimeDB + " ";
                            Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet As A ";
                            Qry += " WHERE A.FTCostSheetNo='" + DocNo + "'";
                            Qry += " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, " +
                                "FTCostSheetNo, FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, " +
                                "FTSendStatusDescription, FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                            Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            Qry += "," + HI.UL.ULDate.FormatDateDB;
                            Qry += "," + HI.UL.ULDate.FormatTimeDB;
                            Qry += ",'" + DocNo + "'";
                            Qry += ",'" + EFSData.version + "'";
                            Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                DocNo + "' ),0) +1 ";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                            Qry += ",'CBD Json Standard'";
                            Qry += ",'True'";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(postcbdjsommessage) + "'";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            Qry += "," + HI.UL.ULDate.FormatDateDB;
                            Qry += "," + HI.UL.ULDate.FormatTimeDB;
                            Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                            HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);
                            foreach (DataRow Rx in dtdata.Select("FNSeq=1"))
                            {
                                Rx["FTSendStatus"] = "True";
                                Rx["FTSendStatusDescription"] = postcbdjsommessage;
                            }
                            StateAppcept = true;
                        }
                        else
                        {
                            Qry = "insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                            Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            Qry += "," + HI.UL.ULDate.FormatDateDB;
                            Qry += "," + HI.UL.ULDate.FormatTimeDB;
                            Qry += ",'" + DocNo + "'";
                            Qry += ",'" + EFSData.version + "'";
                            Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                DocNo + "' ),0) +1 ";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                            Qry += (StateTeamMulti == false) ? ",'CBD Json Standard'" : ",'CBD Json Team Multi'";
                            Qry += ",'False'";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(postcbdjsommessage) + "'";
                            Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                            Qry += "," + HI.UL.ULDate.FormatDateDB;
                            Qry += "," + HI.UL.ULDate.FormatTimeDB;
                            Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                            HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                            foreach (DataRow Rx in dtdata.Select("FNSeq=1"))
                            {
                                Rx["FTSendStatus"] = "False";
                                Rx["FTSendStatusDescription"] = postcbdjsommessage;
                            }
                            StateAppcept = false;
                            //'MsgBox(postcbdjsommessage)
                        }
                    }
                    catch (WebException excbd)
                    {
                        System.Net.WebResponse exresponse = excbd.Response;

                        if (exresponse != null)
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(exresponse.GetResponseStream());
                            try
                            {
                                string exresponseText = reader.ReadToEnd();
                                RefreshResultJSON exjsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(exresponseText);

                                postcbdjsonSsatus = exjsonPostResult.status;
                                postcbdjsommessage = exjsonPostResult.message;

                                Qry = " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                Qry += "," + HI.UL.ULDate.FormatDateDB;
                                Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                Qry += ",'" + DocNo + "'";
                                Qry += ",'" + EFSData.version + "'";
                                Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                    HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                    ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                    DocNo + "' ),0) +1 ";
                                Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                Qry += (StateTeamMulti == false) ? ",'CBD Json Standard'" : ",'CBD Json Team Multi'";
                                Qry += ",'False'";
                                Qry += ",'" + HI.UL.ULF.rpQuoted(postcbdjsommessage) + "'";
                                Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                Qry += "," + HI.UL.ULDate.FormatDateDB;
                                Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                                HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                foreach (DataRow Rx in dtdata.Select("FNSeq=1"))
                                {
                                    Rx["FTSendStatus"] = "False";
                                    Rx["FTSendStatusDescription"] = postcbdjsommessage;
                                }
                            }
                            catch (Exception excbd1)
                            {
                                postcbdjsonSsatus = false;
                                postcbdjsommessage = excbd1.Message;
                            }

                            foreach (DataRow Rx in dtdata.Select("FNSeq=1"))
                            {
                                Rx["FTSendStatus"] = "";
                                Rx["FTSendStatusDescription"] = postcbdjsommessage;
                            }
                        }
                        exresponse.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (DataRow Rx in dtdata.Select("FNSeq=1"))
                {
                    Rx["FTSendStatus"] = "";
                    Rx["FTSendStatusDescription"] = ex.Message;
                }
            }
            if (StatePicture || StateMark)
            {
                string _Qry = "";

                _Qry = "select TOP 1 * ";
                _Qry += " FROM  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) +
                    "].dbo.TACCTCostSheet_File As C With (NOLOCK) ";
                _Qry += " WHERE FTCostSheetNo='" + DocNo + "' ";

                DataTable dtFile = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);
                if (dtFile.Rows.Count > 0)
                {
                    try
                    {
                        if (StatePicture && dtFile.Rows[0]["FBFileImage"].ToString() != null)
                        {
                            try
                            {
                                Byte[] data = Encoding.UTF8.GetBytes(dtFile.Rows[0]["FBFileImage"].ToString());
                                if (data.Length > 0)
                                {
                                    Image pImage = Image.FromStream(new MemoryStream(data));

                                    string strPicFile = pFolderJson + "\\" + EFSData.CBDID.Trim() + ".JPG";

                                    pImage.Save(strPicFile, System.Drawing.Imaging.ImageFormat.Jpeg);

                                    string pFileName = EFSData.CBDID.Trim() + ".JPG";
                                    string EFPictureSurlEndPoint = "https://apcm-apim-qa.partner.nike-cloud.com/service-api/post_UploadProductImage?fileName=" +
                                        pFileName + "&factoryCode=";// + FNHSysVenderPramId.Text.Trim + "&styleNumber=" + FNHSysStyleId.Text.Trim;

                                    FormUpload.FileParameter pFileData = new FormUpload.FileParameter(data, pFileName, "image/jpeg");
                                    pFileData.File = data;
                                    pFileData.FileName = pFileName;
                                    pFileData.ContentType = "image/jpeg";
                                    Dictionary<String, Object> postParameters = new Dictionary<String, Object>();

                                    postParameters.Add("file", pFileData);

                                    //'Create request and receive response

                                    System.Net.HttpWebResponse responsepost = FormUpload.MultipartFormDataPost(EFPictureSurlEndPoint, accessToken, accessToken_id, postParameters);
                                    System.IO.StreamReader streamReaderpost = new System.IO.StreamReader(responsepost.GetResponseStream());

                                    string jsonResponsePostText = streamReaderpost.ReadToEnd();
                                    RefreshResultJSON jsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(jsonResponsePostText);

                                    postimagejsonSsatus = jsonPostResult.status;
                                    postimagejsommessage = jsonPostResult.message;

                                    if (postimagejsonSsatus)
                                    {
                                        Qry = "Update A SET FTStateImageExport='1'";
                                        Qry += " , FTStateImageExportUser ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += " ,  FDStateImageExportDate=" + HI.UL.ULDate.FormatDateDB + " ";
                                        Qry += " ,  FTStateImageExportTime=" + HI.UL.ULDate.FormatTimeDB + " ";
                                        Qry += " FROM  " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_File As A ";
                                        Qry += " WHERE   A.FTCostSheetNo='" + DocNo + "'";
                                        Qry += " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq  from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTCostSheet_JsonHistory as x  " +
                                            "where x.FTCostSheetNo ='" + DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'Picture'";
                                        Qry += ",'True'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postimagejsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";


                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=2"))
                                        {
                                            Rx["FTSendStatus"] = "True";
                                            Rx["FTSendStatusDescription"] = postimagejsommessage;
                                        }
                                        StateAppcept = true;
                                    }
                                    else
                                    {
                                        Qry = " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" + DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'Picture'";
                                        Qry += ",'False'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postimagejsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";


                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=2"))
                                        {
                                            Rx["FTSendStatus"] = "False";
                                            Rx["FTSendStatusDescription"] = postimagejsommessage;
                                        }

                                        StateAppcept = false;
                                        //' MsgBox(postcbdjsommessage)
                                    }
                                }

                            }

                            catch (WebException eximage)
                            {
                                System.Net.WebResponse exresponse = eximage.Response;

                                if (exresponse != null)
                                {
                                    System.IO.StreamReader reader = new System.IO.StreamReader(exresponse.GetResponseStream());
                                    try
                                    {
                                        string exresponseText = reader.ReadToEnd();
                                        RefreshResultJSON exjsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(exresponseText);

                                        postimagejsonSsatus = exjsonPostResult.status;
                                        postimagejsommessage = exjsonPostResult.message;

                                        Qry = " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                            DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'False'";
                                        Qry += ",'True'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postimagejsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=2"))
                                        {
                                            Rx["FTSendStatus"] = "False";
                                            Rx["FTSendStatusDescription"] = postimagejsommessage;
                                        }
                                    }
                                    catch (Exception ex001)
                                    {
                                        postimagejsonSsatus = false;
                                        postimagejsommessage = ex001.Message;
                                    }
                                    foreach (DataRow Rx in dtdata.Select("FNSeq=2"))
                                    {
                                        Rx["FTSendStatus"] = "";
                                        Rx["FTSendStatusDescription"] = postimagejsommessage;
                                    }
                                }
                                exresponse.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        foreach (DataRow Rx in dtdata.Select("FNSeq=2"))
                        {
                            Rx["FTSendStatus"] = "";
                            Rx["FTSendStatusDescription"] = ex.Message;
                        }
                    }

                    try
                    {
                        if (StateMark && dtFile.Rows[0]["FBFileMark"] != null)
                        {
                            try
                            {
                                Byte[] data = Encoding.UTF8.GetBytes(dtFile.Rows[0]["FBFileMark"].ToString());

                                if (data.Length > 0)
                                {
                                    string strPicFile = pFolderJson + "\\" + EFSData.CBDID.Trim() + ".pdf";
                                    //PdfViewer1.SaveDocument(strPicFile);

                                    string pFileName = EFSData.CBDID.Trim() + ".pdf";

                                    string EFPictureSurlEndPoint = "https://apcm-apim-qa.partner.nike-cloud.com/service-api/post_UploadMinimarker" + "?fileName=" + pFileName + "&cbdId=" + EFSData.CBDID.Trim() + "";

                                    Uri pUri = new Uri(EFPictureSurlEndPoint);

                                    FormUpload.FileParameter pFileData = new FormUpload.FileParameter(data, pFileName, "application/pdf");
                                    pFileData.File = data;
                                    pFileData.FileName = pFileName;
                                    pFileData.ContentType = "application/pdf";
                                    Dictionary<String, Object> postParameters = new Dictionary<String, Object>();

                                    postParameters.Add("file", pFileData);

                                    //'Create request and receive response

                                    System.Net.HttpWebResponse responsepost = FormUpload.MultipartFormDataPost(EFPictureSurlEndPoint, accessToken, accessToken_id, postParameters);

                                    System.IO.StreamReader streamReaderpost = new System.IO.StreamReader(responsepost.GetResponseStream());

                                    string jsonResponsePostText = streamReaderpost.ReadToEnd();
                                    RefreshResultJSON jsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(jsonResponsePostText);

                                    postmarkjsonSsatus = jsonPostResult.status;
                                    postmarkjsommessage = jsonPostResult.message;

                                    if (postmarkjsonSsatus)
                                    {

                                        Qry = "Update A SET FTStateMarkExport='1'";
                                        Qry += " , FTStateMarkExportUser ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += " ,  FDStateMarkExportDate=" + HI.UL.ULDate.FormatDateDB + " ";
                                        Qry += " ,  FTStateMarkExportTime=" + HI.UL.ULDate.FormatTimeDB + " ";
                                        Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_File As A ";
                                        Qry += " WHERE A.FTCostSheetNo='" + DocNo + "'";
                                        Qry += " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                            DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'Mark'";
                                        Qry += ",'True'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postmarkjsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=3"))
                                        {
                                            Rx["FTSendStatus"] = "True";
                                            Rx["FTSendStatusDescription"] = postmarkjsommessage;
                                        }

                                        StateAppcept = true;
                                    }
                                    else
                                    {

                                        Qry = " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                            DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'Mark'";
                                        Qry += ",'False'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postmarkjsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=3"))
                                        {
                                            Rx["FTSendStatus"] = "False";
                                            Rx["FTSendStatusDescription"] = postmarkjsommessage;
                                        }
                                        StateAppcept = false;
                                        //'MsgBox(postmarkjsommessage)
                                    }
                                }
                            }
                            catch (WebException exmark)
                            {
                                System.Net.WebResponse exresponse = exmark.Response;
                                if (exresponse == null)
                                {
                                    System.IO.StreamReader reader = new System.IO.StreamReader(exresponse.GetResponseStream());
                                    try
                                    {
                                        string exresponseText = reader.ReadToEnd();
                                        RefreshResultJSON exjsonPostResult = JsonConvert.DeserializeObject<RefreshResultJSON>(exresponseText);

                                        postmarkjsonSsatus = exjsonPostResult.status;
                                        postmarkjsommessage = exjsonPostResult.message;

                                        Qry = " insert into " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory (FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, " +
                                            "FNVersion, FNSeq, FTFileName,FTSendType, FTSendStatus, FTSendStatusDescription, " +
                                            "FTSendUser, FDSendDate, FTSendTime, FTSendByMail) ";
                                        Qry += " Select '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + DocNo + "'";
                                        Qry += ",'" + EFSData.version + "'";
                                        Qry += ",ISNULL((select top 1 MAX(FNSeq) AS FNSeq from " +
                                            HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) +
                                            ".dbo.TACCTCostSheet_JsonHistory as x  where x.FTCostSheetNo ='" +
                                            DocNo + "' ),0) +1 ";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(EFSData.CBDID.Trim()) + "'";
                                        Qry += ",'Mark'";
                                        Qry += ",'False'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(postmarkjsommessage) + "'";
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                        Qry += "," + HI.UL.ULDate.FormatDateDB;
                                        Qry += "," + HI.UL.ULDate.FormatTimeDB;
                                        Qry += ",'" + HI.UL.ULF.rpQuoted(pMail) + "'";

                                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                                        foreach (DataRow Rx in dtdata.Select("FNSeq=3"))
                                        {
                                            Rx["FTSendStatus"] = "False";
                                            Rx["FTSendStatusDescription"] = postmarkjsommessage;
                                        }
                                    }
                                    catch (Exception ex003)
                                    {
                                        postmarkjsonSsatus = false;
                                        postmarkjsommessage = ex003.Message;
                                    }

                                    foreach (DataRow Rx in dtdata.Select("FNSeq=3"))
                                    {
                                        Rx["FTSendStatus"] = "";
                                        Rx["FTSendStatusDescription"] = postmarkjsommessage;
                                    }
                                    exresponse.Close();
                                }
                            }
                            catch (Exception ex)
                            {

                                foreach (DataRow Rx in dtdata.Select("FNSeq=3"))
                                {
                                    Rx["FTSendStatus"] = "";
                                    Rx["FTSendStatusDescription"] = ex.Message;
                                }
                                dtFile.Dispose();
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        System.Net.WebResponse exresponse = ex.Response;
                        if (exresponse == null)
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(exresponse.GetResponseStream());
                            try
                            {
                                string exresponseText = reader.ReadToEnd();
                                RefreshTokenResultJSON exjsonPostResult = JsonConvert.DeserializeObject<RefreshTokenResultJSON>(exresponseText);

                                //MsgBox(exjsonPostResult.error_description);

                            }
                            catch (Exception ex001)
                            {
                                postimagejsonSsatus = false;
                                //MsgBox(ex001.Message);
                            }
                            exresponse.Close();
                        }
                    }
                }
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            txtpassword.Properties.PasswordChar = (chkShowPassword.Checked == true) ? '\0' : '*';
            _Spls.Close();
        }


        static void ListJsonManage(string _csNo, string _cbd, string _picture, string _mark, string _teamMulti)
        {
            if (listJSONs.Find(x => x.CostsheetNo.Contains(_csNo)) == null)
            {
                HI.Track.Class.listJSONS _new = new HI.Track.Class.listJSONS();
                _new.CostsheetNo = _csNo;
                _new.CBD = (_cbd == "1") ? true : false;
                _new.Picture = (_picture == "1") ? true : false;
                _new.Mark = (_mark == "1") ? true : false;
                _new.TeamMulti = _teamMulti;
                listJSONs.Add(_new);
            }
            else
            {
                foreach (HI.Track.Class.listJSONS _l in listJSONs)
                {
                    if (_l.CostsheetNo == _csNo)
                    {
                        if (_cbd == "0" && _picture == "0" && _mark == "0")
                        {
                            listJSONs.Remove(_l);
                            break;
                        }
                        else
                        {
                            _l.CBD = (_cbd == "1") ? true : false;
                            _l.Picture = (_picture == "1") ? true : false;
                            _l.Mark = (_mark == "1") ? true : false;
                            _l.TeamMulti = _teamMulti;
                            break;
                        }

                    }
                    //Console.WriteLine(_l.CostsheetNo + "-> CBD = " + _l.CBD + " Picture = " + _l.Picture + " Mark = " + _l.Mark + " ");
                }
            }
        }

        private void ricCBD_CheckedChanged(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                DevExpress.XtraEditors.CheckEdit c = (DevExpress.XtraEditors.CheckEdit)sender;
                string _csNo = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FTCostSheetNo").ToString();
                string _cbd = (c.Checked) ? "1" : "0"; //ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "CBD").ToString();
                string _picture = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Picture").ToString();
                string _mark = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Mark").ToString();
                string _teamMulti = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FNISTeamMulti").ToString();
                ListJsonManage(_csNo, _cbd, _picture, _mark, _teamMulti);
            }
            catch (Exception ex)
            {

            }
            _Spls.Close();
        }

        private void ricPicture_CheckedChanged(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                DevExpress.XtraEditors.CheckEdit c = (DevExpress.XtraEditors.CheckEdit)sender;
                string _csNo = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FTCostSheetNo").ToString();
                string _cbd = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "CBD").ToString();
                string _picture = (c.Checked) ? "1" : "0"; //ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Picture").ToString();
                string _mark = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Mark").ToString();
                string _teamMulti = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FNISTeamMulti").ToString();
                ListJsonManage(_csNo, _cbd, _picture, _mark, _teamMulti);
            }
            catch (Exception ex)
            {

            }
            _Spls.Close();
        }

        private void ricMark_CheckedChanged(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                DevExpress.XtraEditors.CheckEdit c = (DevExpress.XtraEditors.CheckEdit)sender;
                string _csNo = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FTCostSheetNo").ToString();
                string _cbd = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "CBD").ToString();
                string _picture = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Picture").ToString();
                string _mark = (c.Checked) ? "1" : "0"; //ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "Mark").ToString();
                string _teamMulti = ogvDetail.GetRowCellValue(ogvDetail.FocusedRowHandle, "FNISTeamMulti").ToString();
                ListJsonManage(_csNo, _cbd, _picture, _mark, _teamMulti);
            }
            catch (Exception ex)
            {

            }
            _Spls.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                DevExpress.XtraEditors.CheckEdit c = (DevExpress.XtraEditors.CheckEdit)sender;
                string _State = (c.Checked) ? "1" : "0";

                for (int i = 0; i <= ogvDetail.RowCount; i++)
                {
                    string _csNo = ogvDetail.GetRowCellValue(i, "FTCostSheetNo").ToString();
                    string _teamMulti = ogvDetail.GetRowCellValue(i, "FNISTeamMulti").ToString();
                    ogvDetail.SetRowCellValue(i, ogvDetail.Columns.ColumnByFieldName("CBD"), _State);
                    ogvDetail.SetRowCellValue(i, ogvDetail.Columns.ColumnByFieldName("Picture"), _State);
                    ogvDetail.SetRowCellValue(i, ogvDetail.Columns.ColumnByFieldName("Mark"), _State);
                    ListJsonManage(_csNo, _State, _State, _State, _teamMulti);
                }
            }
            catch (Exception ex)
            {

            }
            _Spls.Close();
        }
    }
}
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;

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
                    _Qry += "SELECT DISTINCT '20' + RIGHT(Sea.FTSeasonCode,2) + LEFT(Sea.FTSeasonCode,2) as FTDmndSesn, " +
                        " S.FTStyleCode + '20' + RIGHT(Sea.FTSeasonCode,2) + LEFT(Sea.FTSeasonCode,2) as FTSeason, " +
                        " S.FTStyleNameEN as FTProdDesc, FTUse as FTProdCat, FTMSC as FTLeague, S.FTStyleCode as FTStyleCode, " +
                        //" row_number ( ) OVER ( partition BY Sea.FTSeasonCode, FTUse, RMDS.FTMATERIALTYPE ORDER BY FNPurchasingLT DESC ) AS Seqnum, " +
                        " row_number ( ) OVER ( partition BY Sea.FTSeasonCode,S.FTStyleCode ORDER BY FNPurchasingLT DESC ) AS Seqnum, " +
                        " CASE WHEN RMDS.FTLiaisonOfficeCode = 'TH' THEN ISNULL(RMDS.FNPurchasingLT,0) + " + (_PoTime + _RawMatfromTH) +
                        " ELSE ISNULL(RMDS.FNPurchasingLT,0) + " + (_PoTime + _RawMatfromOversea) + " END AS FTMerPLT, " +
                        " RMDS.FTMATERIALTYPE as FTGrpDesc, RMDS.FTSMState as FTStatus , ISNULL(CS.FNLeadtime, 0) as FNLeadtime" +

                        " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet AS CS WITH(NOLOCK) " +
                        " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail as CSD WITH(NOLOCK) ON CS.FTCostSheetNo = CSD.FTCostSheetNo " +
                        " INNER jOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS S WITH(NOLOCK) ON CS.FNHSysStyleId = S.FNHSysStyleId " +
                        " INNER jOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CS.FNHSysSeasonId = Sea.FNHSysSeasonId " +
                        " INNER jOIN (SELECT DISTINCT FTMat, FTRMDSSESNCD,FTLiaisonOfficeCode, FTSupplierLocationCode, FNPurchasingLT, FTMATERIALTYPE,FTSMState, FTSMStatus " +
                        " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.THITRMDSMasterFile)  RMDS " +
                        " ON CSD.FTMainMatCode = RMDS.FTMat " +
                        " WHERE CS.FNHSysSeasonId > 0 and CS.FNHSysStyleId > 0  AND CS.FNHSysSeasonId > 0  ";


                    if (FNHSysSeasonId.Text != "")
                    {
                        _Qry += " AND CS.FNHSysSeasonId>=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonId.Properties.Tag) + " ";
                    }
                    if (FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += " AND CS.FNHSysSeasonId<=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonIdTo.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleId.Text != "")
                    {
                        _Qry += " AND CS.FNHSysStyleId>=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleId.Properties.Tag) + " ";

                    }
                    if (FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += " AND CS.FNHSysStyleId<=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleIdTo.Properties.Tag) + " ";
                    }

                    _Qry += "ORDER BY FTSeason , FTMerPLT DESC";
                    //_Qry += " ORDER BY FTSeason, FTUse, RMDS.FTMATERIALTYPE, Seqnum ";

                    // ----- Remove Duplicate -----
                    DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((row["Seqnum"].ToString()) != "1")
                            row.Delete();
                    }
                    dt.AcceptChanges();
                    ogcDetail.DataSource = dt;
                    //ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

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
            return (FNHSysStyleId.Text != "" || FNHSysStyleIdTo.Text != "" || FNHSysSeasonId.Text != "" || FNHSysSeasonIdTo.Text != "") ? true : false;
        }

    }
}
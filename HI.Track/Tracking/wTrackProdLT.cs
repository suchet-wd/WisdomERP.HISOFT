using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HI.Track
{
    public partial class wTrackProdLT : Form
    {
        public wTrackProdLT()
        {
            InitializeComponent();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
            clearAllFT();
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            if (VerifyField())
            {
                String Qry = "";
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    Qry += "SELECT  DISTINCT  mas.FTSeasonCode as FTSeason,mas.FTStyleCode,isnull(CFS.FTInsUser,'') AS ImportBy,isnull(convert(varchar(10),convert(datetime,CFS.FDInsDate),103),'') AS DatImport";
                    Qry += " ,OS.FTSubOrderNo,OS.FTColorway,OS.FTSizeBreakDown,isnull(OS.FNPriceOrg,0) AS FOB";
                    Qry += " ,isnull(CFS.FNImportCMP,0) AS FNImportCMP,isnull(CFS.FNFabricAmt,0) AS FNFabricAmt,isnull(CFS.FNAccessoryAmt,0) AS FNAccessoryAmt";
                    Qry += " ,isnull(CFS.FNImportFabricAmt,0) AS FNImportFabricAmt,isnull(CFS.FNImportAccessoryAmt,0) AS FNImportAccessoryAmt";
                    Qry += " ,isnull(CFS.FNCMPOrg,isnull(CMP.FNCM,isnull(S.FNCM,0))) AS FNCM";
                    Qry += " ,ISNULL(CMP.FNCMDisPer,S.FNCMDisPer ) AS FNCMDisPer";
                    Qry += " ,ISNULL(CMP.FNCMDisAmt,S.FNCMDisAmt ) AS FNCMDisAmt";
                    Qry += " ,ISNULL(CMP.FNNetCM,ISNULL(S.FNNetCM,0) ) AS FNNetCM";
                    Qry += " ,isnull(X.FTPOref,'') AS FTCustomerPO";
                    Qry += " ,isnull(MI.FTInvoiceNo,'') AS FTInvoiceNo";
                    Qry += " FROM";
                    Qry += " (SELECT DISTINCT O.FNHSysStyleId,O.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode ";
                    Qry += " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK)INNER jOIN";
                    Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  O.FNHSysStyleId=S.FNHSysStyleId INNER jOIN";
                    Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId=Sea.FNHSysSeasonId";
                    Qry += " WHERE o.FNHSysSeasonId>0 and o.FNHSysStyleId>0";

                    if (FNHSysSeasonId.Text != "")
                    {
                        Qry += " AND o.FNHSysSeasonId>=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonId.Properties.Tag) + " ";
                    }
                    if (FNHSysSeasonIdTo.Text != "")
                    {
                        Qry += " AND o.FNHSysSeasonId<=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonIdTo.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleId.Text != "")
                    {
                        Qry += " AND o.FNHSysStyleId>=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleId.Properties.Tag) + " ";

                    }
                    if (FNHSysStyleIdTo.Text != "")
                    {
                        Qry += " AND o.FNHSysStyleId<=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleIdTo.Properties.Tag) + " ";
                    }

                    Qry += " union";
                    Qry += " SELECT CMP.FNHSysStyleId,CMP.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode ";
                    Qry += " FROM    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTSeasonCMPrice CMP WITH(NOLOCK) INNER jOIN";
                    Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  CMP.FNHSysStyleId=S.FNHSysStyleId INNER jOIN";
                    Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CMP.FNHSysSeasonId=Sea.FNHSysSeasonId";
                    Qry += " WHERE CMP.FNHSysSeasonId>0 and CMP.FNHSysStyleId>0";

                    if (FNHSysSeasonId.Text != "")
                    {
                        Qry += " AND CMP.FNHSysSeasonId>=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonId.Properties.Tag) + " ";
                    }
                    if (FNHSysSeasonIdTo.Text != "")
                    {
                        Qry += " AND CMP.FNHSysSeasonId<=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonIdTo.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleId.Text != "")
                    {
                        Qry += " AND CMP.FNHSysStyleId>=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleId.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleIdTo.Text != "")
                    {
                        Qry += " AND CMP.FNHSysStyleId<=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleIdTo.Properties.Tag) + " ";
                    }


                    Qry += " ) AS Mas LeFT OUTER JOIN";

                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK) ON Mas.FNHSysStyleId=O.FNHSysStyleId and Mas.FNHSysSeasonId=O.FNHSysSeasonId LEFT OUtER JOIN";
                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId= Sea.FNHSysSeasonId LEFT OUtER JOIN";
                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId=S.FNHSysStyleId LEFT OUTER JOIN";
                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheetFirstSale AS CFS WITH(NOLOCK) ON Mas.FTSeasonCode=CFS.FTSeason and Mas.FTStyleCode=CFS.FTStyleCode LEFT OUTER JOIN";
                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTSeasonCMPrice AS CMP WITH(NOLOCK) ON Mas.FNHSysSeasonId=CMP.FNHSysSeasonId and Mas.FNHSysStyleId=CMP.FNHSysStyleId LEFT OUTER JOIN";
                    Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrderSub_BreakDown AS OS WITH(NOLOCK) ON  O.FTOrderNo=OS.FTOrderNo LEFT OUTER JOIN";
                    Qry += " (SELECT FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo";
                    Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.V_OrderSub_BreakDown_ShipDestination";
                    Qry += " GROUP BY FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo";
                    Qry += " ) AS X ON OS.FTOrderNo = X.FTOrderNo and OS.FTSizeBreakDown=x.FTSizeBreakDown and OS.FTColorway=X.FTColorway and OS.FTSubOrderNo=X.FTSubOrderNo";
                    Qry += " LEFT OUTER JOIN  " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTFactoryCMInvoice_D AS MI WITH(NOLOCK) ON x.FTPOref=MI.FTCustomerPO and x.FTColorway=mi.FTColorway and x.FTSizeBreakDown=mi.FTSizeBreakDown";

                    //ogc.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER);
                    ogc.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _Spls.Close();
            } else
            {
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private bool VerifyField()
        {
            return (FNHSysStyleId.Text != "" || FNHSysStyleIdTo.Text != "" || FNHSysSeasonId.Text != "" || FNHSysSeasonIdTo.Text != "") ? true : false;
        }

        private void clearAllFT()
        {
            FNHSysSeasonId.Text = "";
            FNHSysSeasonIdTo.Text = "";
            FNHSysStyleId.Text = "";
            FNHSysStyleId.Text = "";
        }
    }
}

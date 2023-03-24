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
            //DataTable dt = new DataTable();
            if (VerifyField())
            {
                String _Qry = "";
                int _PoTime = 3; //PO Times 3 Days
                int _RawMatfromTH = 3; //Raw Material form TH Times 3 Days
                int _RawMatfromOversea = 15; //Raw Material form Oversea Times 3 Days
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    _Qry += "SELECT  S.FTStyleCode + Sea.FTSeasonCode as FTSeason, Sea.FTSeasonNameEN as FTDmndSesn, S.FTStyleNameEN as FTProdDesc, " +
                        "FTContentbodyfabric as FTGrpDesc, FTUse as FTProdCat, FTMSC as FTLeague, CS.FTStateActive as FTStatus, CSD.TTLG as LocalExport, " +
                        "CASE WHEN CSD.TTLG = 'TH' THEN " + (_PoTime + _RawMatfromTH) + " ELSE " + (_PoTime + _RawMatfromOversea) + " END AS FTMerPLT," +
                        "S.FTStyleCode, Sea.FTSeasonNameTH as FTSeasonNameTH, S.MSCCode, S.FTStyleNameTH as FTProdDescTH " +
                        "FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet AS CS WITH(NOLOCK) " +
                        "INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheet_Detail as CSD WITH(NOLOCK) ON CS.FTCostSheetNo = CSD.FTCostSheetNo " +
                        "INNER jOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS S WITH(NOLOCK) ON CS.FNHSysStyleId = S.FNHSysStyleId " +
                        "INNER jOIN  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CS.FNHSysSeasonId = Sea.FNHSysSeasonId " +
                        "WHERE CS.FNHSysSeasonId > 0 and CS.FNHSysStyleId > 0  AND CS.FNHSysSeasonId > 0   ";

                    /*
                    _Qry += "SELECT  FTStyleCode + FTSeasonCode as FTSeason, FTSeasonCode as FTDmndSesn, FTStyleNameEN as FTProdDesc, " +
                        "FTStyleNameTH as FTProdDescTH, FTStyleCode as FTStyleCode, O.FTStateOrderApp as FTStatus, " +
                        "O.FNHSysCustId, O.FNHSysSeasonId, FTSeasonNameEN, FTSeasonNameTH" +
                        //"--DISTINCT O.FNHSysStyleId,O.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode" +
                        " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.TMERTOrder AS O WITH(NOLOCK)" +
                        " INNER jOIN " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId" +
                        " INNER jOIN " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId = Sea.FNHSysSeasonId" +
                        " WHERE o.FNHSysSeasonId > 0 and o.FNHSysStyleId > 0  AND o.FNHSysSeasonId > 0   ";
                    */
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

                    /*
                    _Qry += "SELECT  DISTINCT  mas.FTSeasonCode as FTSeason,mas.FTStyleCode,isnull(CFS.FTInsUser,'') AS ImportBy,isnull(convert(varchar(10),convert(datetime,CFS.FDInsDate),103),'') AS DatImport";
                    _Qry += " ,OS.FTSubOrderNo,OS.FTColorway,OS.FTSizeBreakDown,isnull(OS.FNPriceOrg,0) AS FOB";
                    _Qry += " ,isnull(CFS.FNImportCMP,0) AS FNImportCMP,isnull(CFS.FNFabricAmt,0) AS FNFabricAmt,isnull(CFS.FNAccessoryAmt,0) AS FNAccessoryAmt";
                    _Qry += " ,isnull(CFS.FNImportFabricAmt,0) AS FNImportFabricAmt,isnull(CFS.FNImportAccessoryAmt,0) AS FNImportAccessoryAmt";
                    _Qry += " ,isnull(CFS.FNCMPOrg,isnull(CMP.FNCM,isnull(S.FNCM,0))) AS FNCM";
                    _Qry += " ,ISNULL(CMP.FNCMDisPer,S.FNCMDisPer ) AS FNCMDisPer";
                    _Qry += " ,ISNULL(CMP.FNCMDisAmt,S.FNCMDisAmt ) AS FNCMDisAmt";
                    _Qry += " ,ISNULL(CMP.FNNetCM,ISNULL(S.FNNetCM,0) ) AS FNNetCM";
                    _Qry += " ,isnull(X.FTPOref,'') AS FTCustomerPO";
                    _Qry += " ,isnull(MI.FTInvoiceNo,'') AS FTInvoiceNo";
                    _Qry += " FROM";
                    _Qry += " (SELECT DISTINCT O.FNHSysStyleId,O.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode ";
                    _Qry += " FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK)INNER jOIN";
                    _Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  O.FNHSysStyleId=S.FNHSysStyleId INNER jOIN";
                    _Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId=Sea.FNHSysSeasonId";
                    _Qry += " WHERE o.FNHSysSeasonId>0 and o.FNHSysStyleId>0";

                    if (FNHSysSeasonId.Text != "")
                    {
                        _Qry += " AND o.FNHSysSeasonId>=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonId.Properties.Tag) + " ";
                    }
                    if (FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += " AND o.FNHSysSeasonId<=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonIdTo.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleId.Text != "")
                    {
                        _Qry += " AND o.FNHSysStyleId>=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleId.Properties.Tag) + " ";

                    }
                    if (FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += " AND o.FNHSysStyleId<=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleIdTo.Properties.Tag) + " ";
                    }

                    _Qry += " union";
                    _Qry += " SELECT CMP.FNHSysStyleId,CMP.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode ";
                    _Qry += " FROM    [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTSeasonCMPrice CMP WITH(NOLOCK) INNER jOIN";
                    _Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  CMP.FNHSysStyleId=S.FNHSysStyleId INNER jOIN";
                    _Qry += " " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CMP.FNHSysSeasonId=Sea.FNHSysSeasonId";
                    _Qry += " WHERE CMP.FNHSysSeasonId>0 and CMP.FNHSysStyleId>0";

                    if (FNHSysSeasonId.Text != "")
                    {
                        _Qry += " AND CMP.FNHSysSeasonId>=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonId.Properties.Tag) + " ";
                    }
                    if (FNHSysSeasonIdTo.Text != "")
                    {
                        _Qry += " AND CMP.FNHSysSeasonId<=" + HI.UL.ULF.rpQuoted((string)FNHSysSeasonIdTo.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleId.Text != "")
                    {
                        _Qry += " AND CMP.FNHSysStyleId>=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleId.Properties.Tag) + " ";
                    }
                    if (FNHSysStyleIdTo.Text != "")
                    {
                        _Qry += " AND CMP.FNHSysStyleId<=" + HI.UL.ULF.rpQuoted((string)FNHSysStyleIdTo.Properties.Tag) + " ";
                    }


                    _Qry += " ) AS Mas LeFT OUTER JOIN";

                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK) ON Mas.FNHSysStyleId=O.FNHSysStyleId and Mas.FNHSysSeasonId=O.FNHSysSeasonId LEFT OUtER JOIN";
                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId= Sea.FNHSysSeasonId LEFT OUtER JOIN";
                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId=S.FNHSysStyleId LEFT OUTER JOIN";
                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + "].dbo.TACCTCostSheetFirstSale AS CFS WITH(NOLOCK) ON Mas.FTSeasonCode=CFS.FTSeason and Mas.FTStyleCode=CFS.FTStyleCode LEFT OUTER JOIN";
                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTSeasonCMPrice AS CMP WITH(NOLOCK) ON Mas.FNHSysSeasonId=CMP.FNHSysSeasonId and Mas.FNHSysStyleId=CMP.FNHSysStyleId LEFT OUTER JOIN";
                    _Qry += " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrderSub_BreakDown AS OS WITH(NOLOCK) ON  O.FTOrderNo=OS.FTOrderNo LEFT OUTER JOIN";
                    _Qry += " (SELECT FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo";
                    _Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.V_OrderSub_BreakDown_ShipDestination";
                    _Qry += " GROUP BY FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo";
                    _Qry += " ) AS X ON OS.FTOrderNo = X.FTOrderNo and OS.FTSizeBreakDown=x.FTSizeBreakDown and OS.FTColorway=X.FTColorway and OS.FTSubOrderNo=X.FTSubOrderNo";
                    _Qry += " LEFT OUTER JOIN  " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) + ".dbo.TACCTFactoryCMInvoice_D AS MI WITH(NOLOCK) ON x.FTPOref=MI.FTCustomerPO and x.FTColorway=mi.FTColorway and x.FTSizeBreakDown=mi.FTSizeBreakDown";
                    */
                    ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                    //ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER);

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
            return true; // (FNHSysStyleId.Text != "" || FNHSysStyleIdTo.Text != "" || FNHSysSeasonId.Text != "" || FNHSysSeasonIdTo.Text != "") ? true : false;
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.VisualBasic;

namespace HI.SC
{
    public partial class wSourcingFactory : DevExpress.XtraEditors.XtraForm
    {
        #region  Variables
        DataTable dtSourcing = null;
        string strSQL = string.Empty;
        string OrderNo = string.Empty;
        string SubOrderNo = string.Empty;
        string RawMatCode = string.Empty;
        string RawMatId = string.Empty;
        string ColorCode = string.Empty;
        string SizeCode = string.Empty;
        string ch = string.Empty;
        string strPurchaseNo = string.Empty;
        bool staterowclick = false; 
        int strStateChange;
        int n;
       // int m ;
        string SuplCode = string.Empty;
        Double g_From = 0.00;
        Double g_To = 0.00;
        Decimal FNTotalPurchaseQuantity = 0;
        Decimal v_FNOptiplanQuantity = 0;
        char status = '0';
        string TINVENMMaterialFNHSysUnitId = string.Empty;
        string TINVENMMaterialFNHSysUnitCode = string.Empty;
        string TMERTOrder_ResourceFNHSysUnitId = string.Empty;
        bool stateload = false;
       

        #endregion

        #region Constructor
        public wSourcingFactory()
        {

            InitializeComponent();
        }
        #endregion

        #region Methods
        private void ocmsavesc_Click(object sender, EventArgs e)
        {
            DataTable dtItemDup = new DataTable();
            DataTable dtNonSourcing = new DataTable();
            DataTable dt = new DataTable();
            string strOrderNo = string.Empty;
            string strSubOrderNo = string.Empty;
            string strRawMatId = string.Empty;
            DataTable dtNonSorcing = (DataTable)ogcsc.DataSource;

            if (FNSCQty.Value <= 0) { 
              HI.MG.ShowMsg.mInfo("ไม่พบยอดการสั่งซื้อกรุณาทำการตรวจสอบ !!!", 1409010003, this.Text);
              return;
            };

            //SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text,FNHSysSeasonId.Text.Trim());
            //setBindingData();

            dtNonSourcing = NonSourcing(RawMatId, OrderNo, SubOrderNo, strStateChange);
            dtItemDup = ItemDuplicate(RawMatCode);

            string strSQL = string.Empty;
            strSQL = "SELECT TOP 1 FNHSysCurId "
                    + "FROM "
                    + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TFINMCurrency C "
                    + "WHERE "
                    + " C.FTCurCode = '" + FNHSysCurId.Text + "'"
                    ;
            FNHSysCurId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL, Conn.DB.DataBaseName.DB_MASTER, "");
            if (RawMatId == "" | FNHSysCurId.Properties.Tag.ToString() == "")
            {
                HI.MG.ShowMsg.mProcessError(1403040003, "", this.Text, MessageBoxIcon.Warning);
            }
            else
            {
                if ((dtItemDup.Rows.Count > 0) && (strPurchaseNo == ""))
                {
                    if (HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, this.Text) == true)
                    {
                        if (dtItemDup.Rows.Count > 1)
                        {
                            if (HI.MG.ShowMsg.mConfirmProcess("", 1403040004, this.Text) != true)
                            {
                                dt = dtNonSourcing;
                                if (ch == "1")
                                {
                                    strOrderNo = dt.Rows[0]["FTOrderNo"].ToString();
                                    strSubOrderNo = dt.Rows[0]["FTSubOrderNo"].ToString();
                                    strRawMatId = dt.Rows[0]["FNHSysRawMatId"].ToString();
                                }
                                else
                                {
                                    strOrderNo = OrderNo;
                                    strSubOrderNo = SubOrderNo;
                                    strRawMatId = RawMatId;
                                }
                                dt = CalculateNonSourcing(dtItemDup, dt);
                            }
                            else
                            {
                                dt = CalculateSourcing(dtItemDup);
                            }
                        }
                        else
                        {
                            dt = dtNonSourcing;
                            if (ch == "1")
                            {
                                strOrderNo = dt.Rows[0]["FTOrderNo"].ToString();
                                strSubOrderNo = dt.Rows[0]["FTSubOrderNo"].ToString();
                                strRawMatId = dt.Rows[0]["FNHSysRawMatId"].ToString();
                            }
                            else
                            {
                                strOrderNo = OrderNo;
                                strSubOrderNo = SubOrderNo;
                                strRawMatId = RawMatId;
                            }
                            dt = CalculateNonSourcing(dtItemDup, dt);
                        }
                        //dt = SplitOptiplan(dt);
                        dt = UpdateSourcing(dt, RawMatId);
                        HI.TL.SplashScreen _Spls = new HI.TL.SplashScreen("Saving...   Please Wait   ");
                        if (this.SaveData(dt, _Spls))
                        {
                            SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text, FNHSysSeasonId.Text.Trim());
                            setBindingData();
                            _Spls.Close();

                            //SetFocusRow
                            ogvsc.FocusedRowHandle = IndexRow(RawMatId,OrderNo,SubOrderNo, dtSourcing) ;
                            ogvsc.MakeRowVisible(IndexRow(RawMatId,OrderNo,SubOrderNo, dtSourcing), true);
                            ogvsc.SelectRow(IndexRow(RawMatId, OrderNo, SubOrderNo, dtSourcing));

                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, this.Text);
                            HI.TL.HandlerControl.ClearControl(this.otbsc);
                        }
                        else
                        {
                            _Spls.Close();
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, this.Text);
                        }
                    }
                }
                else
                {
                    if (strPurchaseNo != "")//เปิด Po ไปแล้วไม่สามารถ update ได้
                    {
                        HI.MG.ShowMsg.mProcessError(1403040007, "", this.Text, MessageBoxIcon.Warning);
                    }
                }
            }

            try {
               
                dtItemDup.Dispose(); 
                dtNonSourcing.Dispose() ;
                dt.Dispose();
                dtNonSorcing.Dispose();
            }
            catch { }
            
        }
        private void ocmdelete_Click(object sender, EventArgs e)
        {
            DataTable dtItemDup = new DataTable();
            DataTable dtNonSourcing = new DataTable();
            DataTable dt = new DataTable();
            DataTable dtNonSourcingCheck = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT TOP 1 FNHSysCurId "
                    + "FROM "
                    + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TFINMCurrency C "
                    + "WHERE "
                    + " C.FTCurCode = '" + FNHSysCurId.Text + "'"
                    ;
            FNHSysCurId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL, Conn.DB.DataBaseName.DB_MASTER, "");
            strSQL = "SELECT TOP 1 FNHSysSuplId "
                 + "FROM "
                 + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier S "
                 + "WHERE "
                 + " S.FTSuplCode = '" + FNHSysSuplId.Text + "'"
                 ;
            FNHSysSuplId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL, Conn.DB.DataBaseName.DB_MASTER, "");

            dtItemDup = ItemDuplicate(RawMatCode);
            dtNonSourcing = NonSourcing(RawMatId, OrderNo, SubOrderNo, strStateChange);

            if (RawMatId == "" | FNHSysCurId.Properties.Tag.ToString() == "")
            {
                HI.MG.ShowMsg.mProcessError(1403040003, "", this.Text, MessageBoxIcon.Warning);
            }
            else
            {
                dtNonSourcingCheck = NonSourcingCheck("1");
                if (dtNonSourcingCheck != null && dtNonSourcingCheck.Rows.Count > 0)
                {
                    if ((dtItemDup.Rows.Count > 0) && (strPurchaseNo == ""))
                    {
                        if (HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล",1405250001, this.Text) == true)
                        {
                            if (dtItemDup.Rows.Count > 1)
                            {
                                DataTable dtCountSourcing = CountSourcing(dtItemDup);
                                if (dtCountSourcing.Rows.Count > 1)
                                {
                                    if (HI.MG.ShowMsg.mConfirmProcess("", 1403040005, this.Text) != true)
                                    {
                                        dt = dtNonSourcing;
                                    }
                                    else
                                    {
                                        dt = dtItemDup;
                                    }
                                }
                                else
                                {
                                    dt = dtItemDup;
                                }
                            }
                            else
                            {
                                dt = dtNonSourcing;
                            }
                            dt = UpdateSourcing(dt, RawMatId);
                            HI.TL.SplashScreen _Spls = new HI.TL.SplashScreen("Delete...   Please Wait   ");
                            if (this.DeleteData(dt, _Spls))
                            {
                                string sql;
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    sql = " Exec [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "]..SP_MERGE_RESOURCE_AFTER_CLEAR_SOURCING '" + HI.UL.ULF.rpQuoted((string)dt.Rows[i]["FTOrderNo"]) + "'," + (int)dt.Rows[i]["FNHSysRawMatId"] + " ";
                                    HI.Conn.SQLConn.ExecuteNonQuery(sql, Conn.DB.DataBaseName.DB_MERCHAN);
                                }
                                SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text, FNHSysSeasonId.Text.Trim());
                                setBindingData();
                                _Spls.Close();

                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, this.Text);
                                HI.TL.HandlerControl.ClearControl(this.otbsc);
                            }
                            else
                            {
                                _Spls.Close();
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, this.Text);
                            }
                        }
                    }
                    else
                    {
                        if (strPurchaseNo != "")
                        {
                            HI.MG.ShowMsg.mProcessError(1403040007, "", this.Text, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    HI.MG.ShowMsg.mProcessError(1403040006, "", this.Text, MessageBoxIcon.Warning);
                }
            }

            try
            {
                dtItemDup.Dispose();
                dtNonSourcing.Dispose();
                dt.Dispose();
                dtNonSourcingCheck.Dispose();
            }
            catch { }

        }

        private void refreshdata() {

            HI.TL.HandlerControl.ClearControl(opgsourcing);

            if (FNHSysBuyId.Text.Trim() != "" || FNHSysStyleId.Text.Trim() != "" || FTOrderNo.Text.Trim() != "")
            {
                HI.TL.SplashScreen _Spls = new HI.TL.SplashScreen("Loading... data  Please wait.");
                SearchData(FNHSysBuyId.Text.Trim(), FNHSysStyleId.Text.Trim(), FTOrderNo.Text.Trim(),FNHSysSeasonId.Text.Trim());
                setBindingData();
                _Spls.Close();
            }
            else {
                ogcsc.DataSource = null;
                dtSourcing = null;
                HI.TL.HandlerControl.ClearControl(this.otbsc); 
            };
        
        }

        private void ocmRefresh_Click(object sender, EventArgs e)
        {
            refreshdata();
            //if (FNHSysBuyId.Text.Trim() != "" || FNHSysStyleId.Text.Trim() != "" || FTOrderNo.Text.Trim() != "")
            //{
            //    HI.TL.SplashScreen _Spls = new HI.TL.SplashScreen("Loading... data  Please wait.");
            //    SearchData(FNHSysBuyId.Text.Trim(), FNHSysStyleId.Text.Trim(), FTOrderNo.Text.Trim());
            //    setBindingData();
            //    _Spls.Close();
            //};
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString();
        }
        private void ocmclearclsr_Click(object sender, EventArgs e)
        {
            this.FNHSysBuyId.Properties.Tag = "";
            this.FNHSysBuyId.Text = "";
            this.FNHSysBuyId_None.Text = "";

            this.FNHSysStyleId.Properties.Tag = "";
            this.FNHSysStyleId.Text = "";
            this.FNHSysStyleId_None.Text = "";

            this.ogcsc.DataSource = null;
            clearSourcing();
            this.FNHSysBuyId.Properties.Tag = "";
            this.FNHSysBuyId.Text = "";
            this.FNHSysBuyId_None.Text = "";

            this.FNHSysStyleId.Properties.Tag = "";
            this.FNHSysStyleId.Text = "";
            this.FNHSysStyleId_None.Text = "";

            this.FTOrderNo.Properties.Tag = "";
            this.FTOrderNo.Text = "";
            this.FDOrderDate.Text = "";
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString();
        }

        private bool SaveData(DataTable dtSourcing, HI.TL.SplashScreen objspl)
        {
            string strSQL = string.Empty;
            try
            {
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR);
                HI.Conn.SQLConn.SqlConnectionOpen();
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand();
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction();

                for (int i = 0; i <= dtSourcing.Rows.Count - 1; i++)
                {
                    if (dtSourcing.Rows[i]["FTPurchaseNo"].ToString() == "")
                    {
                        if (dtSourcing.Rows[i]["ch"].ToString() == "0")
                        {
                            strSQL = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].[dbo].[TPURTOrder_Sourcing] "
                             + "( "
                             + "FTInsUser,FDInsDate,FTInsTime,FTOrderNo,FTSubOrderNo,FNHSysRawMatId ,FNUsedQuantity,FNUsedPlusQuantity,FNHSysUnitId,FNPrice,FTStateNominate,FDDateSC "
                             + ",FTPurchaseNo,FNHSysSuplId,FNSCQuantity,FNSCPlusQuantity,FNTotalPurchaseQuantity,FNHSysUnitIdPurchase,FNPricePurchase "
                             + ",FNHSysCurId,FNStateChange,FTFabricFrontSize,FNReserveQuantity,FNTransferQuantity,FNOptiplanQuantity "
                             + " ) "
                             + "VALUES "
                             + "('" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'"
                             + "," + HI.UL.ULDate.FormatDateDB + ""
                             + "," + HI.UL.ULDate.FormatTimeDB + ""
                             + ",'" + dtSourcing.Rows[i]["FTOrderNo"] + "' "
                             + ",'" + dtSourcing.Rows[i]["FTSubOrderNo"] + "' "
                             + "," + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysRawMatId"].ToString()) + " "
                             + "," + Convert.ToDouble(dtSourcing.Rows[i]["FNUsedQuantity"].ToString()) + " "
                             + "," + Convert.ToDouble(dtSourcing.Rows[i]["FNUsedPlusQuantity"].ToString()) + " "
                             + "," + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysUnitId"].ToString()) + " "
                             + "," + Convert.ToDouble(dtSourcing.Rows[i]["FNPrice"].ToString()) + " "
                             + ",'" + dtSourcing.Rows[i]["FTStateNominate"] + "' "
                             + ",'" + dtSourcing.Rows[i]["FDInsDate"] + "' "
                             + ",'" + dtSourcing.Rows[i]["FTPurchaseNo"] + "' "
                             + "," + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysSuplId"].ToString()) + " "
                             + ",'" + Convert.ToDouble(dtSourcing.Rows[i]["FNSCQuantity"].ToString()) + "' "
                             + ",'" + Convert.ToDouble(dtSourcing.Rows[i]["FNSCPlusQuantity"].ToString()) + "' "
                             + ",'" + Convert.ToDouble(dtSourcing.Rows[i]["FNTotalPurchaseQuantity"].ToString()) + "' "
                             + "," + FNHSysToUnitId.Properties.Tag + " "
                             + ",'" + Convert.ToDouble(dtSourcing.Rows[i]["FNPricePurchase"].ToString()) + "' "
                             + "," + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysCurId"].ToString()) + " "
                             + "," + Convert.ToInt32(dtSourcing.Rows[i]["FNStateChange"].ToString()) + " "
                             + ",'" + dtSourcing.Rows[i]["FTFabricFrontSize"] + "' "
                             + "," + Convert.ToDouble(dtSourcing.Rows[i]["FNReserveQuantity"].ToString()) + " "
                             + "," + Convert.ToDouble(dtSourcing.Rows[i]["FNTransferQuantity"].ToString()) + " "
                             + ",'" + Convert.ToDouble(dtSourcing.Rows[i]["FNOptiplanQuantity"].ToString()) + "' "
                             + ")"
                             ;
                        }
                        else
                        {
                            strSQL = "UPDATE [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].[dbo].[TPURTOrder_Sourcing]  "
                             + " SET "
                             + "FTUpdUser = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'"
                             + ",FDUpdDate = " + HI.UL.ULDate.FormatDateDB + ""
                             + ",FTUpdTime = " + HI.UL.ULDate.FormatTimeDB + ""
                             + ",FNSCQuantity= '" + Convert.ToDouble(dtSourcing.Rows[i]["FNSCQuantity"].ToString()) + "'"
                             + ",FNSCPlusQuantity = '" + Convert.ToDouble(dtSourcing.Rows[i]["FNSCPlusQuantity"].ToString()) + "'"
                             + ",FNTotalPurchaseQuantity ='" + Convert.ToDouble(dtSourcing.Rows[i]["FNTotalPurchaseQuantity"].ToString()) + "'"
                             + ",FNHSysUnitIdPurchase =" + FNHSysToUnitId.Properties.Tag + ""
                             + ",FNPricePurchase ='" + Convert.ToDouble(dtSourcing.Rows[i]["FNPricePurchase"].ToString()) + "'"
                             + ",FNHSysCurId = " + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysCurId"].ToString()) + ""
                             + ",FNHSysSuplId = " + Convert.ToInt32(dtSourcing.Rows[i]["FNHSysSuplId"].ToString()) + ""
                             + ",FTFabricFrontSize = '" + HI.UL.ULF.rpQuoted(FTFabricFrontSize.Text) + "'"
                             + " WHERE "
                             + "     FNHSysRawMatId ='" + dtSourcing.Rows[i]["FNHSysRawMatId"] + "'"
                             + "     AND FTOrderNo = '" + dtSourcing.Rows[i]["FTOrderNo"] + "'"
                             + "     AND FTSubOrderNo ='" + dtSourcing.Rows[i]["FTSubOrderNo"] + "'"
                             + "     AND ISNULL(FTPurchaseNo,'') =''"
                                //+ "     AND FNStateChange ='" + dtSourcing.Rows[i]["FNStateChange"].ToString() + "'"
                             ;

                        }
                        if (HI.Conn.SQLConn.ExecuteTran(strSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0)
                        {
                            HI.Conn.SQLConn.Tran.Rollback();
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                            return false;
                        }
                    };
                }

                HI.Conn.SQLConn.Tran.Commit();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                return true;
            }
            catch (Exception ex)
            {
                HI.Conn.SQLConn.Tran.Rollback();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                return false;
            }
           
        }
        private bool DeleteData(DataTable dt, HI.TL.SplashScreen objspl)
        {
            try
            {
                string strSQL = string.Empty;
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN);
                HI.Conn.SQLConn.SqlConnectionOpen();
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand();
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction();
                objspl.UpdateInformation("Deleting Style No " + FNHSysStyleId.Properties.Tag);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    strSQL = " DELETE  FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTOrder_Sourcing "
                        + " WHERE "
                        + "     FNHSysRawMatId ='" + dt.Rows[i]["FNHSysRawMatId"] + "'"
                        + "     AND FTOrderNo = '" + dt.Rows[i]["FTOrderNo"] + "'"
                        + "     AND FTSubOrderNo ='" + dt.Rows[i]["FTSubOrderNo"] + "'"
                        + "     AND FNStateChange ='" + dt.Rows[i]["FNStateChange"] + "'"
                        + "     AND ISNULL(FTPurchaseNo,'') =''"
                        ;
                    HI.Conn.SQLConn.Execute_Tran(strSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran);
                }
                HI.Conn.SQLConn.Tran.Commit();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                return true;
            }
            catch (Exception ex)
            {
                HI.Conn.SQLConn.Tran.Rollback();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                return false;
            }
        }

        private DataTable NonSourcing(String iRawMatId, string iJobNo, string iSubJobNo, int iStateChange)
        {
            DataTable dtTemp = new DataTable();
            DataTable dt = (DataTable)ogcsc.DataSource;
            DataRow drr;
            dtTemp = dt.Clone();
            foreach (DataRow dr in dt.Select("FNHSysRawMatId ='" + iRawMatId + "' "
                + " AND FTOrderNo = '" + iJobNo + "'"
                + " AND FTSubOrderNo = '" + iSubJobNo + "'"
                + " AND FNStateChange =" + iStateChange + ""))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable SplitOptiplan(DataTable dt)
        {
            Double Optiplan = 0.00;
            
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Optiplan = Convert.ToDouble(dt.Rows[i]["FNOptiplanQuantity"].ToString());
                if (Optiplan > 0)
                {
                    //dt.Rows[i]["FNTotalPurchaseQuantity"] =Convert.ToDouble( dt.Rows[i]["FNSCQuantity"].ToString() )+ Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"].ToString());
                    dt.Rows[i]["FNTotalPurchaseQuantity"] = dt.Rows[i]["FNOptiplanQuantity"];
                }
                else
                {
                    //dt.Rows[i]["FNTotalPurchaseQuantity"] = dt.Rows[0]["FNUsedQuantity"];
                }
            }
            return dt;
        }
        private DataTable UpdateSourcing(DataTable dt, string iRawMatId)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["FTPurchaseNo"].ToString()== "")//เปิด Po ไปแล้ว
                {
                    if ((dt.Rows[i]["FNHSysRawMatId"].ToString() == iRawMatId)
                        && (dt.Rows[i]["FTOrderNo"].ToString() == OrderNo)
                        && (dt.Rows[i]["FTSubOrderNo"].ToString() == SubOrderNo)
                        )
                    {
                        dt.Rows[i]["FNSCQuantity"] = ocepurqty.Value;
                        dt.Rows[i]["FNSCPlusQuantity"] = ocepurplusqty.Value;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = FNSCQty.Value;
                        dt.Rows[i]["FNPricePurchase"] = FNPrice.Value;
                        dt.Rows[i]["TotalPrice"] = TotalPrice.Value;
                        dt.Rows[i]["FTFabricFrontSize"] = FTFabricFrontSize.Text;
                        dt.Rows[i]["FNHSysCurId"] = FNHSysCurId.Properties.Tag.ToString();
                        if (FNHSysSuplId.Properties.Tag.ToString() != "")
                        {
                            dt.Rows[i]["FNHSysSuplId"] = FNHSysSuplId.Properties.Tag.ToString();
                        }
                        //update uthai 20141020
                        dt.Rows[i]["FNReserveQuantity"] = ocersvqty.Value;
                        dt.Rows[i]["FNTransferQuantity"] = ocetransqty.Value;
                    }
                    //กรณี item color size suplier เดียวกัน
                     //if ((dt.Rows[i]["FNHSysRawMatId"].ToString() == iRawMatId)
                    //    && (dt.Rows[i]["FTSuplCode"].ToString()==SuplCode))
                     //{
                    //     dt.Rows[i]["FNPricePurchase"] = FNPrice.Value;
                    // }
                    dt.Rows[i]["FNHSysCurId"] = FNHSysCurId.Properties.Tag.ToString();
                    if (FNHSysSuplId.Properties.Tag.ToString() != "")
                    {
                        dt.Rows[i]["FNHSysSuplId"] = FNHSysSuplId.Properties.Tag.ToString();
                    }
                    dt.Rows[i]["FNPricePurchase"] = FNPrice.Value;
                }
            }
            return dt;
        }
       
        private DataTable ItemDuplicate(String iRawMatCode)
        {
            DataTable dtTemp = new DataTable();
            DataTable dt = (DataTable)ogcsc.DataSource;
            DataRow drr;
            dtTemp = dt.Clone();
            foreach (DataRow dr in dt.Select("FTRawMatCode ='" + iRawMatCode + "'  "))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable ItemDuplicateUpdateResource(String iRawMatCode)
        {
            DataTable dtTemp = new DataTable();
            DataTable dt = SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text, FNHSysSeasonId.Text.Trim());
            DataRow drr;
            dtTemp = dt.Clone();
            foreach (DataRow dr in dt.Select("FTRawMatCode ='" + iRawMatCode + "' "))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable NonSourcingCheck(String ch)
        {
            DataTable dtTemp = new DataTable();
            DataTable dt = (DataTable)ogcsc.DataSource;
            DataRow drr;
            dtTemp = dt.Clone();
            foreach (DataRow dr in dt.Select("ch ='" + ch + "' "))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable CountSourcing(DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataRow drr;
            dtTemp = dt.Clone();
            foreach (DataRow dr in dt.Select("ch = '1' "))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable ItemColorSizeDuplicate(String iRawMatCode, string colorCode, string sizeCode, string jobCode, DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataRow drr;
            dtTemp = dt.Clone();
            string strWhere = string.Empty;
            if (iRawMatCode != "")
                strWhere += " FTRawMatCode ='" + HI.UL.ULF.rpQuoted(iRawMatCode) + "'";
            if (colorCode != "")
            {
                strWhere += " AND FTRawMatColorCode = '" +HI.UL.ULF.rpQuoted( colorCode) + "'";
            }
            else
            {
                strWhere += "AND FTRawMatColorCode IS NULL ";
            }
            if (sizeCode != "")
            {
                strWhere += " AND FTRawMatSizeCode = '" + HI.UL.ULF.rpQuoted(sizeCode) + "'";
            }
            else
            {
                strWhere += "AND FTRawMatSizeCode IS NULL ";
            }
            if (jobCode != "")
                strWhere += " AND FTOrderNo = '" + jobCode + "'";

           // if (strWhere != "") { strWhere += " AND ch <>'1'"; };
            foreach (DataRow dr in dt.Select(strWhere))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable HadCheck(String ich, string iRawMatCode, string colorCode, string sizeCode, string jobCode, DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataRow drr;
            dtTemp = dt.Clone();
            string strWhere = string.Empty;
            if (ich != "")
                strWhere += " ch ='" + ich + "'";
            if (iRawMatCode != "")
                strWhere += "AND FTRawMatCode ='" + iRawMatCode + "'";
            if (colorCode != "")
            {
                strWhere += " AND FTRawMatColorCode = '" + colorCode + "'";
            }
            else
            {
                strWhere += "AND FTRawMatColorCode is null ";
            }
            if (sizeCode != "")
            {
                strWhere += " AND FTRawMatSizeCode = '" + sizeCode + "'";
            }
            else
            {
                strWhere += "AND FTRawMatSizeCode is null ";
            }
            if (jobCode != "")
                strWhere += " AND FTOrderNo = '" + jobCode + "'";
            foreach (DataRow dr in dt.Select(strWhere))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable ItemNonDuplicate(string RawMatId, string RawMatCode, string ColorCode, string SizeCode, string OrderNo, string subjobCode, DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataRow drr;
            dtTemp = dt.Clone();
            string strWhere = string.Empty;
            if (RawMatId != "")
                strWhere += " FNHSysRawMatId ='" + RawMatId + "'";
            if (RawMatCode != "")
                strWhere += " AND FTRawMatCode ='" + RawMatCode + "'";
            if (ColorCode != "")
                strWhere += " AND FTRawMatColorCode = '" + ColorCode + "'";
            if (SizeCode != "")
                strWhere += " AND FTRawMatSizeCode = '" + SizeCode + "'";
            if (OrderNo != "")
                strWhere += " AND FTOrderNo = '" + OrderNo + "'";
            if (subjobCode != "")
                strWhere += " AND FTSubOrderNo = '" + subjobCode + "'";
            foreach (DataRow dr in dt.Select(strWhere))
            {
                drr = dtTemp.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drr[dc.ColumnName] = dr[dc.ColumnName];
                }
                dtTemp.Rows.Add(drr);
            }
            return dtTemp;
        }
        private DataTable DeleteSelectItem(string RawMatId, string RawMatCode, string ColorCode, string SizeCode, string OrderNo, string subjobCode, DataTable dt)
        {
            string strWhere = string.Empty;
            if (RawMatId != "")
                strWhere += " FNHSysRawMatId ='" + RawMatId + "'";
            if (RawMatCode != "")
                strWhere += " AND FTRawMatCode ='" + RawMatCode + "'";
            if (ColorCode != "")
                strWhere += " AND FTRawMatColorCode = '" + ColorCode + "'";
            if (SizeCode != "")
                strWhere += " AND FTRawMatSizeCode = '" + SizeCode + "'";
            if (OrderNo != "")
                strWhere += " AND FTOrderNo = '" + OrderNo + "'";
            if (subjobCode != "")
                strWhere += " AND FTSubOrderNo = '" + subjobCode + "'";
            foreach (DataRow dr in dt.Select(strWhere))
            {
                dt.Rows.Remove(dr);
            }
            return dt;
        }
        private int IndexRow(string strRawMatId,string OrderNo, string SubOrderNo,DataTable dt)
        {
            int j=0;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                    if ((dt.Rows[i]["FNHSysRawMatId"].ToString() == strRawMatId)
                        && (dt.Rows[i]["FTOrderNo"].ToString() == OrderNo)
                        && (dt.Rows[i]["FTSubOrderNo"].ToString() == SubOrderNo)
                        )
                {
                    j = i;
                }
            }
            return j;
        }

        private DataTable SearchData(string BuyCode, string StyleCode, string JobNo, string SeasonCode)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string strSQL = string.Empty;
                string strWhere = string.Empty;
                ogcsc.DataSource = null;

                if (BuyCode != "")
                    //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                    strWhere += " B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                if (StyleCode.Trim() != "")
                    //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                    strWhere += " AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                if (JobNo != "")
                    //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                    strWhere += " AND M.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                if (SeasonCode != "")
                    //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                    strWhere += " AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";

                strSQL = "    CREATE TABLE #Reserve (FTReserveNo nvarchar(30),FTOrderNo nvarchar(30),FNHSysRawMatId int,FNQuantity numeric(18,2))CREATE INDEX [IDX_TmpReserve] ON #Reserve(FTReserveNo,FTOrderNo,FNHSysRawMatId)";
                strSQL += Environment.NewLine + " CREATE TABLE #Reserveinfo (FTOrderNo nvarchar(30),FNHSysRawMatId int,FTReserveNo nvarchar(500))CREATE INDEX [IDX_TmpReserveinfo] ON #Reserveinfo(FTOrderNo,FNHSysRawMatId,FTReserveNo) ";

                strSQL += Environment.NewLine + " CREATE TABLE #ReserveQty (FTOrderNo nvarchar(30),FNHSysRawMatId int,FNQuantity numeric(18,2))CREATE INDEX [IDX_TmpReserveQty] ON #Reserve(FTOrderNo,FNHSysRawMatId)";
                strSQL += Environment.NewLine + " INSERT INTO #Reserve(FTReserveNo,FTOrderNo,FNHSysRawMatId,FNQuantity)";
                strSQL += Environment.NewLine + " SELECT  A.FTReserveNo, A.FTOrderNo, C.FNHSysRawMatId,SUM(BB.FNQuantity) AS FNQuantity";
                strSQL += Environment.NewLine  +   " FROM         [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENReserve AS A WITH(NOLOCK) INNER JOIN";
                strSQL += Environment.NewLine + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode_IN AS BB WITH(NOLOCK) ON A.FTReserveNo = BB.FTDocumentNo INNER JOIN";
                strSQL += Environment.NewLine + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON BB.FTBarcodeNo = C.FTBarcodeNo   ";
                strSQL += Environment.NewLine + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS OO  (NOLOCK)  ON A.FTOrderNo = OO.FTOrderNo  ";
                strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH (NOLOCK) ON OO.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH (NOLOCK) ON OO.FNHSysBuyId = B.FNHSysBuyId  ";
                strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS WITH (NOLOCK) ON OO.FNHSysStyleId = TS.FNHSysStyleId ";
                strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON OO.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                strSQL += Environment.NewLine + "  WHERE  (OO.FTOrderNo <>'') AND ISNULL(OO.FNHSysStyleIdPull,0) <=0   ";
                strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";

                if (BuyCode != "")
                            //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                            strSQL += Environment.NewLine + "	AND B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                        if (StyleCode.Trim() != "")
                            //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                            strSQL += Environment.NewLine + "	 AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                        if (JobNo != "")
                            //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                            strSQL += Environment.NewLine + "	 AND OO.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                        if (SeasonCode != "")
                            //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                            strSQL += Environment.NewLine + "	 AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";

                        strSQL += Environment.NewLine + "  GROUP BY   A.FTReserveNo, A.FTOrderNo, C.FNHSysRawMatId ";
                        strSQL += Environment.NewLine + " INSERT INTO #Reserve(FTReserveNo,FTOrderNo,FNHSysRawMatId,FNQuantity)";
                        strSQL += Environment.NewLine + " SELECT  A.FTTransferOrderNo, A.FTOrderNoTo, C.FNHSysRawMatId,SUM(BB.FNQuantity) AS FNQuantity";
                        strSQL += Environment.NewLine + " FROM         [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN";
                        strSQL += Environment.NewLine + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode_IN AS BB WITH(NOLOCK) ON A.FTTransferOrderNo = BB.FTDocumentNo INNER JOIN";
                        strSQL += Environment.NewLine + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON BB.FTBarcodeNo = C.FTBarcodeNo   ";
                        strSQL += Environment.NewLine + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS OO  (NOLOCK)  ON A.FTOrderNoTo = OO.FTOrderNo  ";
                        strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH (NOLOCK) ON OO.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                        strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH (NOLOCK) ON OO.FNHSysBuyId = B.FNHSysBuyId  ";
                        strSQL += Environment.NewLine + "	 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS WITH (NOLOCK) ON OO.FNHSysStyleId = TS.FNHSysStyleId ";
                        strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON OO.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                        strSQL += Environment.NewLine + "  WHERE  (OO.FTOrderNo <>'') AND ISNULL(OO.FNHSysStyleIdPull,0) <=0   ";
                        strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";

                        if (BuyCode != "")
                            //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                            strSQL += Environment.NewLine + "	AND B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                        if (StyleCode.Trim() != "")
                            //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                            strSQL += Environment.NewLine + "	 AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                        if (JobNo != "")
                            //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                            strSQL += Environment.NewLine + "	 AND OO.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                        if (SeasonCode != "")
                            //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                            strSQL += Environment.NewLine + "	 AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";

                strSQL += Environment.NewLine + "  GROUP BY   A.FTTransferOrderNo, A.FTOrderNoTo, C.FNHSysRawMatId ";

                strSQL += Environment.NewLine + "  INSERT INTO #Reserve(FTReserveNo,FTOrderNo,FNHSysRawMatId,FNQuantity)";
                strSQL += Environment.NewLine + "  SELECT R.FTPurchaseNo, R.FTOrderNoTo, R.FNHSysRawMatId, SUM(R.FNQuantityTo) AS FNQuantityTo  ";
                strSQL += Environment.NewLine  + " FROM              [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTReservePurchase AS R WITH(NOLOCK) ";
                strSQL += Environment.NewLine  + "			     INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS OO  (NOLOCK)  ON R.FTOrderNoTo = OO.FTOrderNo  ";
                strSQL += Environment.NewLine  + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH (NOLOCK) ON OO.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                strSQL += Environment.NewLine  + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH (NOLOCK) ON OO.FNHSysBuyId = B.FNHSysBuyId  ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS WITH (NOLOCK) ON OO.FNHSysStyleId = TS.FNHSysStyleId ";
                strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON OO.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                strSQL += Environment.NewLine + "  WHERE  (OO.FTOrderNo <>'') AND ISNULL(OO.FNHSysStyleIdPull,0) <=0   ";
                strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";

                if (BuyCode != "")
                    //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                    strSQL += Environment.NewLine + "	AND B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                if (StyleCode.Trim() != "")
                    //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                    strSQL += Environment.NewLine + "	 AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                if (JobNo != "")
                    //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                    strSQL += Environment.NewLine + "	 AND OO.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                if (SeasonCode != "")
                    //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                        strSQL += Environment.NewLine + "	 AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";
               strSQL += Environment.NewLine + "  GROUP BY   R.FTPurchaseNo, R.FTOrderNoTo, R.FNHSysRawMatId";

                strSQL += Environment.NewLine  +   " INSERT INTO #Reserveinfo(FTOrderNo,FNHSysRawMatId,FTReserveNo) ";
                strSQL += Environment.NewLine  +   " SELECT FTOrderNo ";
                strSQL += Environment.NewLine  +   ",FNHSysRawMatId ";
                strSQL += Environment.NewLine  +   " , ISNULL((  ";
                strSQL += Environment.NewLine  +   " select  STUFF((SELECT  ',' + FTReserveNo  ";
                strSQL += Environment.NewLine  +   "	From (SELECT      Distinct FTReserveNo ";
                strSQL += Environment.NewLine  +   "	FROM    #Reserve AS XX ";
                strSQL += Environment.NewLine  +   "  WHERE     (XX.FTOrderNo=A.FTOrderNo) ";
                strSQL += Environment.NewLine  +   "   and (XX.FNHSysRawMatId =A.FNHSysRawMatId) ";
                strSQL += Environment.NewLine  +   "   ) AS T ";
                strSQL += Environment.NewLine  +   "	FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') ";
                strSQL += Environment.NewLine  +   "),'')    AS FTReserveNo ";
                strSQL += Environment.NewLine  +   " FROM #Reserve AS A ";
                strSQL += Environment.NewLine + "  GROUP BY FTOrderNo,FNHSysRawMatId ";


                strSQL += Environment.NewLine + "  INSERT INTO #ReserveQty(FTOrderNo,FNHSysRawMatId,FNQuantity) ";
                strSQL += Environment.NewLine + "  SELECT FTOrderNo,FNHSysRawMatId,SUM(FNQuantity) AS FNQuantity ";
                strSQL += Environment.NewLine + "  FROM #Reserve ";
                strSQL += Environment.NewLine + "  GROUP BY FTOrderNo,FNHSysRawMatId ";


                if (HI.ST.Lang.Language == ST.Lang.eLang.TH)
                {
                            strSQL += Environment.NewLine  +   " SELECT ";
                            strSQL += Environment.NewLine  +   " CASE WHEN isnull(SC.FTOrderNo,'')<>'' THEN '1' ELSE '0' END AS ch ";
                            strSQL += Environment.NewLine  + ",M.FTOrderNo, M.FTSubOrderNo, M.FNHSysRawMatId ";
                            //+ ",M.FNUsedQuantity,M.FNUsedPlusQuantity "
                            //+ ",CASE WHEN FTPurchaseNo IS NULL  THEN M.FNUsedQuantity ELSE SC.FNUsedQuantity  END AS FNUsedQuantity "
                            //+ ",CASE WHEN FTPurchaseNo IS NULL THEN M.FNUsedPlusQuantity ELSE SC.FNSCPlusQuantity END AS FNUsedPlusQuantity "
                            strSQL += Environment.NewLine  + ", ISNULL(M.FNUsedQuantity,0) AS FNUsedQuantity ";
                            strSQL += Environment.NewLine  + ", ISNULL(M.FNUsedPlusQuantity,0)  AS FNUsedPlusQuantity ";
                            //+ ",CASE WHEN isnull(M.FNOptiplanQuantity,0)<>0 THEN M.FNOptiplanQuantity ELSE 0.0000 END AS FNOptiplanQuantity "
                            // + ",CASE WHEN isnull(M.FNStateChange,0)<>0 THEN 0.0000 ELSE M.FNOptiplanQuantity END AS FNOptiplanQuantity " 
                            strSQL += Environment.NewLine + " , M.FNOptiplanQuantity     AS FNOptiplanQuantity ";
                            strSQL += Environment.NewLine + " , SC.FNOptiplanQuantity     AS FNOptiplanQuantityPur ";
                           // strSQL += Environment.NewLine + " ,ISNULL(CASE WHEN SC.FNOptiplanQuantity IS NULL THEN  M.FNOptiplanQuantity  ELSE SC.FNOptiplanQuantity END,0)    AS FNOptiplanQuantity ";
                            strSQL += Environment.NewLine  + ", RM.FTRawMatCode, RM.FTRawMatNameTH, RM.FNHSysRawMatColorId ";
                            strSQL += Environment.NewLine  + ",RM.FNHSysRawMatSizeId, U.FTUnitCode,U.FNHSysUnitId, RC.FTRawMatColorCode,  RS.FTRawMatSizeCode ";
                            strSQL += Environment.NewLine  + ",RS.FTRawMatSizeNameTH AS FTRawMatSizeName";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(S.FTSuplCode ,'')<>'' THEN S.FTSuplCode ELSE S2.FTSuplCode END AS FTSuplCode ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(S.FTSuplNameTH ,'')<>'' THEN S.FTSuplNameTH ELSE S2.FTSuplNameTH END AS FTSuplNameTH ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(CU.FTCurCode ,'')<>'' THEN CU.FTCurCode ELSE CU2.FTCurCode END AS FTCurCode ";
                            strSQL += Environment.NewLine  + ",M.FNPrice ";
                            strSQL += Environment.NewLine  + ",SC.FTPurchaseNo ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNHSysCurId ,0)<>0 THEN SC.FNHSysCurId ELSE M.FNHSysCurId END AS FNHSysCurId ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNHSysSuplId ,0)<>0 THEN SC.FNHSysSuplId ELSE M.FNHSysSuplId END AS FNHSysSuplId ";
                            strSQL += Environment.NewLine  + ",RM.FTRawMatNameTH AS FTRawMatName,M.FTStateNominate";
                            strSQL += Environment.NewLine  + ",O.FNHSysStyleId ";
                            strSQL += Environment.NewLine  + ",M.FNHSysUnitId,M.FDInsDate ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNHSysUnitIdPurchase,0)<>0 THEN U2.FTUnitCode ELSE U.FTUnitCode END AS UnitCodePurchase  ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNSCQuantity,0)<>0 THEN SC.FNSCQuantity ELSE 0.0000 END AS FNSCQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNSCPlusQuantity,0)<>0 THEN SC.FNSCPlusQuantity ELSE 0.0000 END AS FNSCPlusQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN SC.FNTotalPurchaseQuantity ELSE 0.0000 END AS FNTotalPurchaseQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNPricePurchase,0)<>0 THEN SC.FNPricePurchase ELSE 0.0000 END AS FNPricePurchase ";
                            //+ ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN (SC.FNTotalPurchaseQuantity*SC.FNPrice) ELSE 0.0000 END AS TotalPrice "
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN (SC.FNTotalPurchaseQuantity*SC.FNPricePurchase) ELSE 0.0000 END AS TotalPrice ";
                            strSQL += Environment.NewLine  + ",C.FTCustNameTH AS FTCustName ";
                            strSQL += Environment.NewLine  + ",CONVERT(VARCHAR(10), CONVERT(DATETIME, O.FDOrderDate, 120), 103) AS FDOrderDate";
                            strSQL += Environment.NewLine  + ",CONVERT(VARCHAR(10), CONVERT(DATETIME, OCS.FDShipDate, 120), 103) AS FDShipDate";
                            strSQL += Environment.NewLine  + ",M.FNStateChange ";
                            strSQL += Environment.NewLine  + ",CASE WHEN isnull(SC.FTFabricFrontSize,'')<>'' THEN SC.FTFabricFrontSize ";
                            strSQL += Environment.NewLine  + " ELSE ";
                            strSQL += Environment.NewLine  + " (";
                            strSQL += Environment.NewLine  + "    CASE  WHEN isnull(RM.FTFabricFrontSize,'')<>'' THEN M.FTFabricFrontSize ELSE RM.FTFabricFrontSize ";
                            strSQL += Environment.NewLine  + "    END ";
                            strSQL += Environment.NewLine  + " )";
                            strSQL += Environment.NewLine  + " END AS FTFabricFrontSize ";
                            //strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNReserveQuantity,0)<>0 THEN SC.FNReserveQuantity ELSE 0.0000 END +  CASE WHEN SC.FTOrderNo IS NULL THEN ISNULL(PRsv.FNQuantity,0) ELSE 0 END AS FNReserveQuantity ";
                            //strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0) +  CASE WHEN SC.FTOrderNo IS NULL THEN ISNULL(PRsv.FNQuantity,0) ELSE 0 END  AS FNReserveQuantity ";
                            //strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0) +   ISNULL(PRsv.FNQuantity,0)  AS FNReserveQuantity ";
                            strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0)   AS FNReserveQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNTransferQuantity,0)<>0 THEN SC.FNTransferQuantity ELSE 0.0000 END AS FNTransferQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNUsedQuantity,0)<>0 THEN SC.FNUsedQuantity ELSE 0.0000 END AS FNUsedQuantitySC ";
                            strSQL += Environment.NewLine  + ",RC.FNRawMatColorSeq, RS.FNRawMatSizeSeq ";
                            strSQL += Environment.NewLine  + ",TS.FTStyleCode ";
                            // strSQL += Environment.NewLine  + ",ISNULL((SELECT HITECH_INVENTORY.dbo.fn_Sourcing_FTReserveNo(M.FTOrderNo,M.FNHSysRawMatId) ),'') +  CASE WHEN SC.FTOrderNo IS NULL THEN  ISNULL(PRsv.FTPurchaseNo,'') ELSE '' END  AS FTReserveNo";

                            //strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'') +  CASE WHEN SC.FTOrderNo IS NULL THEN  ISNULL(PRsv.FTPurchaseNo,'') ELSE '' END  AS FTReserveNo ";
                            //strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'') +   ISNULL(PRsv.FTPurchaseNo,'')   AS FTReserveNo ";
                            strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'')    AS FTReserveNo ";
                    
                            strSQL += Environment.NewLine  + ", U3.FTUnitCode AS TINVENMMaterialFNHSysUnitCode ,U3.FNHSysUnitId AS TINVENMMaterialFNHSysUnitId ";
                            strSQL += Environment.NewLine  + ",B.FTBuyCode,CASE WHEN PRsv.FTOrderNo IS NULL THEN '0' ELSE '1' END AS FTStateReservePo,ISNULL(M.FTStateFree,'0') AS FTStateFree,SSA.FTSeasonCode,ISNULL(ORMC.FTRawMatColorNameTH,'') AS FTRawMatColorName ";
                    //strSQL += Environment.NewLine + ",[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.fn_SC_GetPOInfo(M.FTOrderNo,  M.FNHSysRawMatId) AS FTPOInfo ";
                    strSQL += Environment.NewLine + ",SC.FTPurchaseNo AS FTPOInfo ";
                }
                else
                {
                            strSQL += Environment.NewLine  +   " SELECT ";
                            strSQL += Environment.NewLine  +  " CASE WHEN isnull(SC.FTOrderNo,'')<>'' THEN '1' ELSE '0' END AS ch ";
                            strSQL += Environment.NewLine  + ",M.FTOrderNo, M.FTSubOrderNo, M.FNHSysRawMatId ";
                            //+ ",M.FNUsedQuantity,M.FNUsedPlusQuantity "
                            //+ ",CASE WHEN FTPurchaseNo IS NULL  THEN M.FNUsedQuantity ELSE SC.FNUsedQuantity  END AS FNUsedQuantity "
                            //+ ",CASE WHEN FTPurchaseNo IS NULL THEN M.FNUsedPlusQuantity ELSE SC.FNSCPlusQuantity END AS FNUsedPlusQuantity "
                            strSQL += Environment.NewLine  +", ISNULL(M.FNUsedQuantity,0) AS FNUsedQuantity ";
                            strSQL += Environment.NewLine + ", ISNULL(M.FNUsedPlusQuantity,0)  AS FNUsedPlusQuantity ";
                            // + ", CASE WHEN isnull(M.FNOptiplanQuantity,0)<>0 THEN M.FNOptiplanQuantity ELSE 0.0000 END AS FNOptiplanQuantity "
                            //+ ",CASE WHEN isnull(M.FNStateChange,0)<>0 THEN 0.0000 ELSE M.FNOptiplanQuantity END AS FNOptiplanQuantity " 
                            strSQL += Environment.NewLine + " , M.FNOptiplanQuantity     AS FNOptiplanQuantity ";
                            strSQL += Environment.NewLine + " , SC.FNOptiplanQuantity     AS FNOptiplanQuantityPur ";
                            //strSQL += Environment.NewLine + " ,ISNULL(CASE WHEN SC.FNOptiplanQuantity IS NULL THEN  M.FNOptiplanQuantity  ELSE SC.FNOptiplanQuantity END,0)    AS FNOptiplanQuantity ";
                            strSQL += Environment.NewLine + ",RM.FTRawMatCode, RM.FTRawMatNameEN, RM.FNHSysRawMatColorId ";
                            strSQL += Environment.NewLine + ",RM.FNHSysRawMatSizeId, U.FTUnitCode,U.FNHSysUnitId, RC.FTRawMatColorCode, RS.FTRawMatSizeCode ";
                            strSQL += Environment.NewLine + ",RS.FTRawMatSizeNameEN AS FTRawMatSizeName ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(S.FTSuplCode ,'')<>'' THEN S.FTSuplCode ELSE S2.FTSuplCode END AS FTSuplCode ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(S.FTSuplNameTH ,'')<>'' THEN S.FTSuplNameTH ELSE S2.FTSuplNameTH END AS FTSuplNameTH ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(CU.FTCurCode ,'')<>'' THEN CU.FTCurCode ELSE CU2.FTCurCode END AS FTCurCode ";
                            strSQL += Environment.NewLine + ",M.FNPrice ";
                            strSQL += Environment.NewLine + ",SC.FTPurchaseNo ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNHSysCurId ,0)<>0 THEN SC.FNHSysCurId ELSE M.FNHSysCurId END AS FNHSysCurId ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNHSysSuplId ,0)<>0 THEN SC.FNHSysSuplId ELSE M.FNHSysSuplId END AS FNHSysSuplId ";
                            strSQL += Environment.NewLine + ",RM.FTRawMatNameEN AS FTRawMatName,M.FTStateNominate ";
                            strSQL += Environment.NewLine + ",O.FNHSysStyleId ";
                            strSQL += Environment.NewLine + ",M.FNHSysUnitId,M.FDInsDate ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNHSysUnitIdPurchase,0)<>0 THEN U2.FTUnitCode ELSE U.FTUnitCode END AS UnitCodePurchase ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNSCQuantity,0)<>0 THEN SC.FNSCQuantity ELSE 0.0000 END AS FNSCQuantity ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNSCPlusQuantity,0)<>0 THEN SC.FNSCPlusQuantity ELSE 0.0000 END AS FNSCPlusQuantity ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN SC.FNTotalPurchaseQuantity ELSE 0.0000 END AS FNTotalPurchaseQuantity ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNPricePurchase,0)<>0 THEN SC.FNPricePurchase ELSE 0.0000 END AS FNPricePurchase ";
                            //+ ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN (SC.FNTotalPurchaseQuantity*SC.FNPrice) ELSE 0.0000 END AS TotalPrice "
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNTotalPurchaseQuantity,0)<>0 THEN (SC.FNTotalPurchaseQuantity*SC.FNPricePurchase) ELSE 0.0000 END AS TotalPrice ";
                            strSQL += Environment.NewLine + ",C.FTCustNameTH AS FTCustName ";
                            strSQL += Environment.NewLine + ",CONVERT(VARCHAR(10), CONVERT(DATETIME, O.FDOrderDate, 120), 103) AS FDOrderDate";
                            strSQL += Environment.NewLine + ",CONVERT(VARCHAR(10), CONVERT(DATETIME, OCS.FDShipDate, 120), 103) AS FDShipDate";
                            strSQL += Environment.NewLine + ",M.FNStateChange ";
                            strSQL += Environment.NewLine + ",CASE WHEN isnull(SC.FTFabricFrontSize,'')<>'' THEN SC.FTFabricFrontSize ";
                            strSQL += Environment.NewLine + " ELSE ";
                            strSQL += Environment.NewLine + " (";
                            strSQL += Environment.NewLine + "    CASE  WHEN isnull(RM.FTFabricFrontSize,'')<>'' THEN M.FTFabricFrontSize ELSE RM.FTFabricFrontSize ";
                            strSQL += Environment.NewLine + "    END ";
                            strSQL += Environment.NewLine + " )";
                            strSQL += Environment.NewLine + " END AS FTFabricFrontSize ";
                            //strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNReserveQuantity,0)<>0 THEN SC.FNReserveQuantity ELSE 0.0000 END +  CASE WHEN SC.FTOrderNo IS NULL THEN ISNULL(PRsv.FNQuantity,0) ELSE 0 END  AS FNReserveQuantity ";
                            //strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0) +  CASE WHEN SC.FTOrderNo IS NULL THEN ISNULL(PRsv.FNQuantity,0) ELSE 0 END  AS FNReserveQuantity ";
                            //strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0) +   ISNULL(PRsv.FNQuantity,0)  AS FNReserveQuantity ";
                            strSQL += Environment.NewLine + ", ISNULL(RIFQTY.FNQuantity,0)  AS FNReserveQuantity ";
                            strSQL += Environment.NewLine + ", CASE WHEN isnull(SC.FNTransferQuantity,0)<>0 THEN SC.FNTransferQuantity ELSE 0.0000 END AS FNTransferQuantity ";
                            strSQL += Environment.NewLine  + ", CASE WHEN isnull(SC.FNUsedQuantity,0)<>0 THEN SC.FNUsedQuantity ELSE 0.0000 END AS FNUsedQuantitySC ";
                            strSQL += Environment.NewLine + ",RC.FNRawMatColorSeq, RS.FNRawMatSizeSeq ";
                            strSQL += Environment.NewLine + ",TS.FTStyleCode ";
                            // strSQL += Environment.NewLine + ",ISNULL((SELECT HITECH_INVENTORY.dbo.fn_Sourcing_FTReserveNo(M.FTOrderNo,M.FNHSysRawMatId) ),'') +  CASE WHEN SC.FTOrderNo IS NULL THEN ISNULL(PRsv.FTPurchaseNo,'') ELSE '' END  AS FTReserveNo";
                            //strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'')  +  CASE WHEN SC.FTOrderNo IS NULL THEN  ISNULL(PRsv.FTPurchaseNo,'') ELSE '' END  AS FTReserveNo ";
                            //strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'')  +   ISNULL(PRsv.FTPurchaseNo,'')   AS FTReserveNo ";
                            strSQL += Environment.NewLine + "   ,ISNULL(RIF.FTReserveNo,'')    AS FTReserveNo ";
                            strSQL += Environment.NewLine + ", U3.FTUnitCode AS TINVENMMaterialFNHSysUnitCode ,U3.FNHSysUnitId AS TINVENMMaterialFNHSysUnitId ";
                            strSQL += Environment.NewLine + ",B.FTBuyCode,CASE WHEN PRsv.FTOrderNo IS NULL THEN '0' ELSE '1' END AS FTStateReservePo,ISNULL(M.FTStateFree,'0') AS FTStateFree,SSA.FTSeasonCode,ISNULL(ORMC.FTRawMatColorNameEN,'') AS FTRawMatColorName ";
                          //strSQL += Environment.NewLine + ",[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.fn_SC_GetPOInfo(M.FTOrderNo,  M.FNHSysRawMatId) AS FTPOInfo ";
                          strSQL += Environment.NewLine + ",SC.FTPurchaseNo AS FTPOInfo ";
                }

                strSQL += Environment.NewLine  + " FROM ";
                strSQL += Environment.NewLine  + " [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder_Resource AS M WITH(NOLOCK) ";
                strSQL += Environment.NewLine  + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK) ON M.FTOrderNo = O.FTOrderNo ";
                strSQL += Environment.NewLine  + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON M.FNHSysRawMatId = RM.FNHSysRawMatId ";
                strSQL += Environment.NewLine  + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit AS U WITH(NOLOCK)  ON M.FNHSysUnitId = U.FNHSysUnitId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTOrder_Sourcing AS SC WITH(NOLOCK) ON M.FNHSysRawMatId = SC.FNHSysRawMatId ";
                strSQL += Environment.NewLine  + " AND M.FTSubOrderNo = SC.FTSubOrderNo ";
                strSQL += Environment.NewLine  + " AND M.FTOrderNo = SC.FTOrderNo ";
                strSQL += Environment.NewLine  + " AND M.FNStateChange = SC.FNStateChange ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON SC.FNHSysCurId = CU.FNHSysCurId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier AS S WITH(NOLOCK)  ON SC.FNHSysSuplId = S.FNHSysSuplId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMatSize AS RS WITH(NOLOCK) ON RM.FNHSysRawMatSizeId = RS.FNHSysRawMatSizeId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMatColor AS RC WITH(NOLOCK) ON RM.FNHSysRawMatColorId = RC.FNHSysRawMatColorId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit AS U2 WITH(NOLOCK) ON SC.FNHSysUnitIdPurchase = U2.FNHSysUnitId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCustomer AS C WITH(NOLOCK) ON O.FNHSysCustId = C.FNHSysCustId ";
                //+ " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrderSub TOS ON M.FTOrderNo = TOS.FTOrderNo AND M.FTSubOrderNo = TOS.FTSubOrderNo "
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.V_TMERTOrder_Cut_ShipDate OCS ON M.FTOrderNo = OCS.MFTOrderNo ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TFINMCurrency AS CU2 WITH(NOLOCK) ON M.FNHSysCurId = CU2.FNHSysCurId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier AS S2 WITH(NOLOCK)  ON M.FNHSysSuplId = S2.FNHSysSuplId ";
                strSQL += Environment.NewLine + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS  WITH(NOLOCK) ON O.FNHSysStyleId = TS.FNHSysStyleId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit AS U3  WITH(NOLOCK) ON RM.FNHSysUnitId = U3.FNHSysUnitId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH(NOLOCK)  ON O.FNHSysBuyId = B.FNHSysBuyId ";

                strSQL += Environment.NewLine + " LEFT OUTER JOIN   (SELECT FTOrderNoTo AS FTOrderNo,FNHSysRawMatId,SUM(FNQuantityTo) AS FNQuantity,Max(FTPurchaseNo) AS FTPurchaseNo ";
                strSQL += Environment.NewLine + " FROM (SELECT R.FTPurchaseNo, R.FTOrderNoTo, R.FNQuantityTo, R.FNHSysRawMatId  ";
                strSQL += Environment.NewLine + " FROM              [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTReservePurchase AS R WITH(NOLOCK) ";
                strSQL += Environment.NewLine + "			     INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS OO  (NOLOCK)  ON R.FTOrderNoTo = OO.FTOrderNo  ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH (NOLOCK) ON OO.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH (NOLOCK) ON OO.FNHSysBuyId = B.FNHSysBuyId  ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS WITH (NOLOCK) ON OO.FNHSysStyleId = TS.FNHSysStyleId ";
                strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON OO.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                strSQL += Environment.NewLine + "  WHERE  (OO.FTOrderNo <>'') AND ISNULL(OO.FNHSysStyleIdPull,0) <=0   ";
                strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";


                if (BuyCode != "")
                    //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                    strSQL += Environment.NewLine + "	AND B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                if (StyleCode.Trim() != "")
                    //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                    strSQL += Environment.NewLine + "	 AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                if (JobNo != "")
                    //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                    strSQL += Environment.NewLine + "	 AND OO.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                if (SeasonCode != "")
                    //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                    strSQL += Environment.NewLine + "	 AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";

                strSQL += Environment.NewLine + " ) AS M  ";
                strSQL += Environment.NewLine + " GROUP BY FTOrderNoTo,FNHSysRawMatId) AS PRsv  ON M.FTOrderNo=PRsv.FTOrderNo AND M.FNHSysRawMatId = PRsv.FNHSysRawMatId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH(NOLOCK)  ON O.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                strSQL += Environment.NewLine  + " LEFT OUTER JOIN (  SELECT  RM.FNHSysRawMatId,A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN";
                strSQL += Environment.NewLine  + "   FROM     [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN";
                strSQL += Environment.NewLine  + "   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN";
                strSQL += Environment.NewLine  + "    [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode";

                strSQL += Environment.NewLine + "			     INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS OO  (NOLOCK)  ON A.FTOrderNo = OO.FTOrderNo  ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMSeason AS SSA WITH (NOLOCK) ON OO.FNHSysSeasonId = SSA.FNHSysSeasonId ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMBuy AS B WITH (NOLOCK) ON OO.FNHSysBuyId = B.FNHSysBuyId  ";
                strSQL += Environment.NewLine + "				 LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMStyle AS TS WITH (NOLOCK) ON OO.FNHSysStyleId = TS.FNHSysStyleId ";
                strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON OO.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                strSQL += Environment.NewLine + "  WHERE  (OO.FTOrderNo <>'') AND ISNULL(OO.FNHSysStyleIdPull,0) <=0   ";
                strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";

                if (BuyCode != "")
                    //strWhere += " B.FTBuyCode LIKE '%" + BuyCode + "%'";
                    strSQL += Environment.NewLine + "	AND B.FTBuyCode = '" + HI.UL.ULF.rpQuoted(BuyCode) + "'";
                if (StyleCode.Trim() != "")
                    //strWhere += " AND TS.FTStyleCode LIKE '%" + StyleCode + "%'";
                    strSQL += Environment.NewLine + "	 AND TS.FTStyleCode = '" + HI.UL.ULF.rpQuoted(StyleCode) + "'";
                if (JobNo != "")
                    //strWhere += " AND M.FTOrderNo LIKE '%" + JobNo + "%'";
                    strSQL += Environment.NewLine + "	 AND OO.FTOrderNo = '" + HI.UL.ULF.rpQuoted(JobNo) + "'";
                if (SeasonCode != "")
                    //strWhere += " AND SSA.FTSeasonCode LIKE '%" + SeasonCode + "%'";
                    strSQL += Environment.NewLine + "	 AND SSA.FTSeasonCode = '" + HI.UL.ULF.rpQuoted(SeasonCode) + "'";
                        
                strSQL += Environment.NewLine  + " ) AS ORMC ON M.FTOrderNo = ORMC.FTOrderNo AND M.FNHSysRawMatId=ORMC.FNHSysRawMatId ";
                strSQL += Environment.NewLine + " LEFT OUTER JOIN #Reserveinfo AS RIF ON M.FTOrderNo = RIF.FTOrderNo AND M.FNHSysRawMatId=RIF.FNHSysRawMatId  ";
                strSQL += Environment.NewLine + " LEFT OUTER JOIN #ReserveQty AS RIFQTY ON M.FTOrderNo = RIFQTY.FTOrderNo AND M.FNHSysRawMatId=RIFQTY.FNHSysRawMatId  ";
                strSQL += Environment.NewLine + "	 INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH (NOLOCK) ON O.FNHSysCmpRunId = CR.FNHSysCmpRunId ";
                strSQL += Environment.NewLine  + " WHERE ";
                strSQL += Environment.NewLine + " M.FNHSysRawMatId >0  AND ISNULL(O.FNHSysStyleIdPull,0) <=0   ";
                strSQL += Environment.NewLine + "  AND CR.FTStateHire ='1' ";
                ;

                if (HI.ST.SysInfo.Admin != true)
                {

                    string _Qry = null;

                    _Qry = "    SELECT TOP 1 B.FTStatePurchase ";
                    _Qry += System.Environment.NewLine + "  FROM     [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin AS A WITH(NOLOCK)  INNER JOIN ";
                    _Qry += System.Environment.NewLine + "      [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMTeamGrp AS B WITH(NOLOCK) ON A.FNHSysTeamGrpId = B.FNHSysTeamGrpId ";
                    _Qry += System.Environment.NewLine + "  WHERE (A.FTUserName ='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ";
                    string temp = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY, "");
                    if (HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY, "") == "1")
                    {
                        //strSQL = strSQL + " AND  O.FTOrderBy IN (SELECT FTUserName FROM  dbo.FT_GetOrderPermission_PurchseTeam('" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ) ";
                        string _Qry_M = null;
                        _Qry_M = "(SELECT FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.FT_GetOrderPermission_PurchseTeam('" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ) ";
                        if (HI.Conn.SQLConn.GetField(_Qry_M, HI.Conn.DB.DataBaseName.DB_MERCHAN, "") != "")
                        {
                            strSQL = strSQL + " AND  O.FTOrderBy IN (SELECT FTUserName FROM  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.FT_GetOrderPermission_PurchseTeam('" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "') ) ";
                        }
                        
                    };

                };

                if (strWhere != "")
                {
                    if (BuyCode != "")
                        strSQL = strSQL + " AND " + strWhere;
                    else
                        strSQL = strSQL + " " + strWhere;
                }
                ;
                
                strSQL += Environment.NewLine + " ORDER BY ";
                strSQL += Environment.NewLine + "  RM.FTRawMatCode,RC.FNRawMatColorSeq, RS.FNRawMatSizeSeq,M.FTOrderNo,M.FTSubOrderNo ";
                strSQL += Environment.NewLine + "  Drop table #Reserve ";
                strSQL += Environment.NewLine + "  Drop table #Reserveinfo";
                strSQL += Environment.NewLine + "  Drop table #ReserveQty";
               
                dtSourcing = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MERCHAN);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.Cursor = Cursors.Default;

            return dtSourcing;

        }
        private DataTable SearchSourcingPurchaseHistory(int strMatId)
        {
            string strSQL = string.Empty;
            DataTable dtPOHistory = new DataTable();

            strSQL = "SELECT TOP 10 "
                   + " CONVERT(VARCHAR(10), CONVERT(DATETIME, P.FDPurchaseDate, 120), 103) AS FDPurchaseDate"
                   + ",P.FTPurchaseNo,S.FTSuplCode,sum(PO.FNQuantity) AS FNQuantity,U.FTUnitCode,PO.FNPrice "
                   + ",PO.FTOrderNo,PO.FNHSysRawMatId "
                   + "FROM "
                   + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTPurchase P WITH(NOLOCK) "
                   + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTPurchase_OrderNo PO WITH(NOLOCK)  ON P.FTPurchaseNo = PO.FTPurchaseNo "
                   + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier S WITH(NOLOCK)  ON P.FNHSysSuplId = S.FNHSysSuplId "
                   + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit U  WITH(NOLOCK) ON PO.FNHSysUnitId = U.FNHSysUnitId "
                   + "WHERE "
                   + "PO.FNHSysRawMatId = " + strMatId + " "
                   + "GROUP BY P.FTPurchaseNo,PO.FTOrderNo,PO.FNHSysRawMatId,P.FDPurchaseDate,S.FTSuplCode,U.FTUnitCode,PO.FNPrice "
                   + "ORDER BY P.FDPurchaseDate DESC"
                   ;
            dtPOHistory = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_PUR);
            return dtPOHistory;
        }
        private DataTable SearchReserve(string strOrderNo, int strRawMatId)
        {
            string strSQL = string.Empty;
            DataTable dtReserve = new DataTable();
            strSQL = " SELECT "
                + " CONVERT(VARCHAR(10), CONVERT(DATETIME, R.FDReserveDate, 120), 103) AS FDReserveDate"
                + " ,R.FTReserveNo,R.FTReserveBy,W.FTWHCode,SUM(BO.FNQuantity) AS ocersvqty,R.FTRemark,B.FNHSysRawMatId "
                + ",0.00 AS quantity "
                + ",0.00 AS balance "
                + ",U.FTUnitCode "
                + " FROM "
                + "[HITECH_INVENTORY].dbo.TINVENReserve AS R WITH(NOLOCK) "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON R.FTReserveNo = BO.FTDocumentNo  "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode AS B WITH(NOLOCK)  ON BO.FTBarcodeNo = B.FTBarcodeNo "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON BO.FNHSysWHId = W.FNHSysWHId "
                + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit AS U  WITH(NOLOCK) ON B.FNHSysUnitId = U.FNHSysUnitId "
                + "WHERE "
                + "R.FTOrderNo = '" + strOrderNo + "'"
                + " AND  B.FNHSysRawMatId = " + strRawMatId + " "
                + " GROUP BY  R.FTReserveNo,B.FNHSysRawMatId,R.FDReserveDate,R.FTReserveBy,W.FTWHCode ,R.FTRemark,U.FTUnitCode"
                ;
            dtReserve = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_INVEN);
            return dtReserve;
        }
        private DataTable SearchTransfer(string strOrderNo, int strRawMatId)
        {
            string strSQL = string.Empty;
            DataTable dtTransfer = new DataTable();
            strSQL = " SELECT "
                + " CONVERT(VARCHAR(10), CONVERT(DATETIME, T.FDTransferOrderDate, 120), 103) AS FDTransferOrderDate"
                + ",T.FTTransferOrderNo,T.FTOrderNo,T.FTTransferOrderBy,W.FTWHCode,SUM(BO.FNQuantity) AS ocetransqty,T.FTRemark,B.FNHSysRawMatId "
                + ",0.00 AS quantity "
                + ",0.00 AS balance "
                + " FROM "
                + "[HITECH_INVENTORY].dbo.TINVENTransferOrder AS T WITH(NOLOCK) "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON T.FTTransferOrderNo = BO.FTDocumentNo  "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) + "].dbo.TINVENBarcode AS B WITH(NOLOCK)  ON BO.FTBarcodeNo = B.FTBarcodeNo "
                + " INNER JOIN  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON BO.FNHSysWHId = W.FNHSysWHId "
                + "WHERE "
                + "T.FTOrderNoTo = '" + strOrderNo + "'"
                + " AND  B.FNHSysRawMatId = " + strRawMatId + ""
                + " GROUP BY  T.FTTransferOrderNo,T.FTOrderNo,B.FNHSysRawMatId,T.FDTransferOrderDate,T.FTTransferOrderBy,W.FTWHCode,T.FTRemark "
                ;
            dtTransfer = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_INVEN);
            return dtTransfer;
        }
        protected void setBindingData()
        {
            try
            {
                ogcsc.DataSource = null;
                if (dtSourcing.Rows.Count > 0)
                {
                    ogcsc.DataSource = dtSourcing;
                    ogcsc.Refresh();
                    ogvsc.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    ogvsc.OptionsNavigation.AutoFocusNewRow = true;
                    ogvsc.OptionsView.ShowAutoFilterRow = true;
                    ogvsc.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void setBindingPOHistory(DataTable dtPOHistory)
        {
            try
            {
                ogcpo.DataSource = null;
                if (dtPOHistory.Rows.Count > 0)
                {
                    ogcpo.DataSource = dtPOHistory.Copy();
                    ogcpo.Refresh();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void setBindingReserve(DataTable dtReserve)
        {
            try
            {
                ogcrsv.DataSource = null;
                if (dtReserve.Rows.Count > 0)
                {
                    ogcrsv.DataSource = dtReserve.Copy();
                    ogcrsv.Refresh();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void setBindingTransfer(DataTable dtTransfer)
        {
            try
            {
                ogctrans.DataSource = null;
                if (dtTransfer.Rows.Count > 0)
                {
                    ogctrans.DataSource = dtTransfer.Copy();
                    ogctrans.Refresh();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void clearSourcing()
        {
            try
            {
                TextEdit1.Text = "";
                FTCustomerName.Text = "";
                FDJobOrderDate.Text = "";
                FDJobShipDate.Text = "";
                FNTSysMatId.Text = "";
                FTMatColorCode.Text = "";
                FTMatSizeCode.Text = "";
                FNTSysMatId_None.Text = "";
                oceusedqty.Text = "";
                oceusedplusqty.Text = "";
                FNHSysUnitId.Text = "";
                ocersvqty.Text = "";
                ocetransqty.Text = "";
                ocepurqty.Text = "";
                ocepurplusqty.Text = "";
                FNHSysToUnitId.Text = "";
                FNHSysToUnitId.Properties.Tag = "";
                oceconv1.Text = "";
                oceconv2.Text = "";
                FNSCQty.Text = "";
                FNPrice.Text = "";
                FNHSysCurId.Properties.Tag = "";
                FNHSysCurId.Text = "";
                FNHSysSuplId_None.Text = "";
                FNHSysSuplId.Properties.Tag = "";
                FNHSysSuplId.Text = "";
                FNHSysSuplId_None.Text = "";
                TotalPrice.Text = "";
                FNOptiplanQuantity.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetUnit()
        {
            string strSQL1 = string.Empty;
            if (oceconv2.Text == "")
            {
                oceconv2.Text = oceconv1.Text;
            }
            strSQL1 = "SELECT TOP 1 FNHSysUnitId "
                    + "FROM "
                    + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit U WITH(NOLOCK) "
                    + "WHERE "
                    + " U.FTUnitCode = '" + FNHSysUnitId.Text + "'"
                    ;
            FNHSysUnitId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL1, Conn.DB.DataBaseName.DB_MASTER, "");
            string strSQL2 = string.Empty;
            strSQL2 = "SELECT TOP 1 FNHSysUnitId "
                    + "FROM "
                    + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit U WITH(NOLOCK)"
                    + "WHERE "
                    + " U.FTUnitCode = '" + FNHSysToUnitId.Text + "'"
                    ;
            FNHSysToUnitId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL2, Conn.DB.DataBaseName.DB_MASTER, "");
        }
        private DataTable SearchUnitFromRT(string strTINVENMMaterialFNHSysUnitId)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT "
                + "FNRateFrom,FNRateTo "
                + "FROM "
                + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitConvert WITH(NOLOCK) "
                + "WHERE "
                + " FNHSysUnitId = '" + TMERTOrder_ResourceFNHSysUnitId + "'"
                + " AND FNHSysUnitIdTo = '" + strTINVENMMaterialFNHSysUnitId + "'"
                ;
            dt = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dt;
        }
        private DataTable SearchUnitToRT(string strTINVENMMaterialFNHSysUnitId)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT "
                + "FNRateFrom,FNRateTo "
                + "FROM "
                + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitConvert WITH(NOLOCK)  "
                + "WHERE "
                + " FNHSysUnitId = '" + strTINVENMMaterialFNHSysUnitId + "'"
                + " AND FNHSysUnitIdTo = '" + TMERTOrder_ResourceFNHSysUnitId + "'"
                ;
            dt = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dt;
        }
        private DataTable SearchUnitFrom()
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT "
                + "FNRateFrom,FNRateTo "
                + "FROM "
                + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitConvert WITH(NOLOCK) "
                + "WHERE "
                + " FNHSysUnitId = '" + FNHSysUnitId.Properties.Tag + "'"
                + " AND FNHSysUnitIdTo = '" + FNHSysToUnitId.Properties.Tag + "'"
                ;
            dt = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dt;
        }
        private DataTable SearchUnitTo()
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT "
                + "FNRateFrom,FNRateTo "
                + "FROM "
                + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitConvert WITH(NOLOCK) "
                + "WHERE "
                + " FNHSysUnitId = '" + FNHSysToUnitId.Properties.Tag + "'"
                + " AND FNHSysUnitIdTo = '" + FNHSysUnitId.Properties.Tag + "'"
                ;
            dt = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dt;
        }
        private void ConvertUnit(DataTable dt)
        {
            Double From, To;
            if (dt.Rows.Count > 0)
            {
                From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                g_From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                g_To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                oceconv1.Text = dt.Rows[0]["FNRateFrom"].ToString();
                oceconv2.Text = dt.Rows[0]["FNRateTo"].ToString();
                if (status == '0')
                {
                    if (FNHSysUnitId.Text == FNHSysToUnitId.Text)
                    {
                        FNSCQty.Value = ocepurqty.Value + ocepurplusqty.Value;
                    }
                    else {
                        FNSCQty.Value = Convert.ToDecimal(((Convert.ToDouble(ocepurqty.Value) + Convert.ToDouble(ocepurplusqty.Value)) * To) / From);
                    
                    
                    };
                }
            }
        }
        private void ConvertUnitRT(string TINVENMMaterialFNHSysUnitId)
        {
            DataTable dtFrom; 
            DataTable dtTo;
            bool found = true;

            if (TINVENMMaterialFNHSysUnitId == FNHSysUnitId.Properties.Tag.ToString())
            {
                ocersvqty.Value = ocersvqty.Value;
            }
            else
            {
                dtFrom = SearchUnitFromRT(TINVENMMaterialFNHSysUnitId);
                if (dtFrom != null && dtFrom.Rows.Count > 0)
                {
                    ConvertUnitReserves(dtFrom);
                    ConvertUnitTransfer(dtFrom);
                }
                else
                {
                    dtTo = SearchUnitToRT(TINVENMMaterialFNHSysUnitId);
                    if (dtTo != null && dtTo.Rows.Count > 0)
                    {
                        ConvertUnitReserves(dtTo);
                        ConvertUnitTransfer(dtTo);
                    }
                    else { found = false; };
                }
            };

        }
        private void ConvertUnitReserves(DataTable dt)
        {
            Double From, To;
            if (dt.Rows.Count > 0)
            {
                From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                g_From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                g_To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                //oceconv1.Text = dt.Rows[0]["FNRateFrom"].ToString();
                //oceconv2.Text = dt.Rows[0]["FNRateTo"].ToString();
                if (status == '1')
                {
                   
                    if (FNHSysUnitId.Text == FNHSysToUnitId.Text)
                    {
                        ocersvqty.Value = ocersvqty.Value;
                    }
                    else
                    {
                        ocersvqty.Value = Convert.ToDecimal(((Convert.ToDouble(ocersvqty.Value))*To)/From);

                    };
                }
            }
            return ;
        }
        private void ConvertUnitTransfer(DataTable dt)
        {
            Double From, To;
            if (dt.Rows.Count > 0)
            {
                From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                g_From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                g_To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                //oceconv1.Text = dt.Rows[0]["FNRateFrom"].ToString();
                //oceconv2.Text = dt.Rows[0]["FNRateTo"].ToString();
                if (status == '1')
                {
                    if (FNHSysUnitId.Text == FNHSysToUnitId.Text)
                    {
                        ocetransqty.Value = ocetransqty.Value;
                    }
                    else
                    {
                        ocetransqty.Value = Convert.ToDecimal(((Convert.ToDouble(ocetransqty.Value)) * To) / From);

                    };
                }
            }
            return;
        }
        private Double ConvertUnitR_Head(string TINVENMMaterialFNHSysUnitId, Double qty)
        {
            DataTable dtFrom;
            DataTable dtTo;
            Double conReserv = 0;
            bool found = true;

            if (TINVENMMaterialFNHSysUnitId != FNHSysUnitId.Properties.Tag.ToString())
            {
                dtFrom = SearchUnitFromRT(TINVENMMaterialFNHSysUnitId);
                if (dtFrom != null && dtFrom.Rows.Count > 0)
                {
                    conReserv=ConvertUnitReserves_Head(dtFrom, qty);
                }
                else
                {
                    dtTo = SearchUnitToRT(TINVENMMaterialFNHSysUnitId);
                    if (dtTo != null && dtTo.Rows.Count > 0)
                    {
                        conReserv=ConvertUnitReserves_Head(dtTo, qty);
                    }
                    else { found = false; };
                }
            };
            return conReserv;
        }
        private Double ConvertUnitT_Head(string TINVENMMaterialFNHSysUnitId, Double qty)
        {
            DataTable dtFrom;
            DataTable dtTo;
            Double ConvertTran = 0;
            bool found = true;

            if (TINVENMMaterialFNHSysUnitId != FNHSysUnitId.Properties.Tag.ToString())
            {
                dtFrom = SearchUnitFromRT(TINVENMMaterialFNHSysUnitId);
                if (dtFrom != null && dtFrom.Rows.Count > 0)
                {
                    ConvertTran = ConvertUnitTransfer_Head(dtFrom,qty);
                }
                else
                {
                    dtTo = SearchUnitToRT(TINVENMMaterialFNHSysUnitId);
                    if (dtTo != null && dtTo.Rows.Count > 0)
                    {
                        ConvertTran = ConvertUnitTransfer_Head(dtTo,qty);
                    }
                    else { found = false; };
                }
            };
            return ConvertTran;
        }
        private Double ConvertUnitReserves_Head(DataTable dt, Double qty)
        {
            Double From, To;
            Double ocersvqty = 0;
            if (dt.Rows.Count > 0)
            {
                From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());
                if (status == '1')
                {

                    if (FNHSysUnitId.Text == FNHSysToUnitId.Text)
                    {
                        ocersvqty = qty;
                    }
                    else
                    {
                        ocersvqty =((qty * To) * From);
                    };
                }
            }
            return ocersvqty;
        }
        private Double ConvertUnitTransfer_Head(DataTable dt, double qty)
        {
            Double From, To, ocetransqty=0.00;
            if (dt.Rows.Count > 0)
            {
                From = Convert.ToDouble(dt.Rows[0]["FNRateFrom"].ToString());
                To = Convert.ToDouble(dt.Rows[0]["FNRateTo"].ToString());

                if (status == '1')
                {
                    if (FNHSysUnitId.Text == FNHSysToUnitId.Text)
                    {
                        ocetransqty = qty;
                    }
                    else
                    {
                        ocetransqty = ((qty * To) * From); 
                    };
                }
            }
            return ocetransqty;
        }
        private void CalUnit()
        {
            Decimal usedqty = 0;
            Decimal usedplusqty = 0;
            //Update UTHAI 2014-07-11
            if (v_FNOptiplanQuantity > 0)
            {
                usedqty = v_FNOptiplanQuantity;
                usedplusqty=0;
            }
            else
            {
                usedqty = oceusedqty.Value + oceusedplusqty.Value ;
                usedplusqty = oceusedplusqty.Value;
            }

                oceconv1.Text = "1.0000";
                oceconv2.Text = "1.0000";
                if (ocersvqty.Value > 0 || ocetransqty.Value > 0) {
                    if (ocersvqty.Value > 0)
                    {
                        //if ((usedqty + oceusedplusqty.Value) < (ocersvqty.Value + ocetransqty.Value))
                        if ((usedqty) < (ocersvqty.Value + ocetransqty.Value))
                        {
                            ocepurqty.Value = 0;
                            ocepurplusqty.Value = 0;
                        }
                        else
                        {
                            ocepurqty.Value = (usedqty) - (ocersvqty.Value + ocetransqty.Value);
                            ocepurplusqty.Value = 0;
                        }
                    }
                    else
                    {
                        if (ocetransqty.Value > 0)
                        {
                            ocepurqty.Value = (usedqty) - (ocersvqty.Value + ocetransqty.Value);
                            ocepurplusqty.Value = 0;
                        }
                        else
                        {
                            ocepurqty.Value = usedqty - ocersvqty.Value - ocetransqty.Value;
                            ocepurplusqty.Value = usedplusqty;
                        }
                    };
                    
                    
                } else{
                            ocepurqty.Value = (usedqty - usedplusqty);
                            ocepurplusqty.Value = usedplusqty;

                };
               
          
        }
       
        private void CalculateTotalPrice()
        {
            double Poqty2;
            DataTable dtFrom;
            DataTable dtTo;

            bool found = true;
            SetUnit();
            FNSCQty.Value = 0;
            if (FNHSysToUnitId.Properties.Tag.ToString() == FNHSysUnitId.Properties.Tag.ToString() ) {

                oceconv1.Text = "1";
                oceconv2.Text = "1";
                FNSCQty.Value = Convert.ToDecimal((Convert.ToDouble(ocepurqty.Value) + Convert.ToDouble(ocepurplusqty.Value)) );

            }
            else
            {
                dtFrom = SearchUnitFrom();
                if (dtFrom != null && dtFrom.Rows.Count > 0)
                {
                    ConvertUnit(dtFrom);
                    oceconv1.Text = dtFrom.Rows[0]["FNRateFrom"].ToString();
                    oceconv2.Text = dtFrom.Rows[0]["FNRateTo"].ToString();
                }
                else
                {
                    dtTo = SearchUnitTo();
                    if (dtTo != null && dtTo.Rows.Count > 0)
                    {
                        ConvertUnit(dtTo);
                        oceconv1.Text = dtTo.Rows[0]["FNRateFrom"].ToString();
                        oceconv2.Text = dtTo.Rows[0]["FNRateTo"].ToString();
                    }
                    else { found = false; };
                }
            };
            HI.TL.HandlerControl.CalEdit_Leave(FNSCQty, new System.EventArgs());
            Poqty2 = Convert.ToDouble(FNSCQty.Value) * Convert.ToDouble(FNPrice.Value);
            TotalPrice.Value = Convert.ToDecimal(Poqty2);

            if (FNHSysUnitId.Text != "" && FNHSysToUnitId.Text != "")
            {
             if (found == false) {
                HI.MG.ShowMsg.mInfo("ไม่พบการตั้งค่าหน่วยแปลงกรุณาทำการตรวจสอบ !!!", 1409010001, this.Text);
              };
            };
        }
        private DataTable CalculateNonSourcing(DataTable dtDp, DataTable dtSelect)
        {
            DataTable dtReserve = new DataTable();
            DataTable dtTransfer = new DataTable();
            DataTable dtGroup = new DataTable();
            Double v_totalRT = 0.00;
            Double v_totalReserve = 0.0000;
            Double v_totalTransfer = 0.0000;
            Double v_UP = 0.0000;
            DataTable dtHadCh = new DataTable();
            string v_rawmatcode, v_colorcode, v_sizecode, v_jobcode;

            v_rawmatcode = dtSelect.Rows[0]["FTRawMatCode"].ToString();
            v_colorcode = dtSelect.Rows[0]["FTRawMatColorCode"].ToString();
            v_sizecode = dtSelect.Rows[0]["FTRawMatSizeCode"].ToString();
            v_jobcode = dtSelect.Rows[0]["FTOrderNo"].ToString();
            dtGroup = ItemColorSizeDuplicate(v_rawmatcode, v_colorcode, v_sizecode, v_jobcode, dtDp);
            dtHadCh = HadCheck("1", v_rawmatcode, v_colorcode, v_sizecode, v_jobcode, dtDp);

            dtReserve = SearchReserve(dtSelect.Rows[0]["FTOrderNo"].ToString(), Convert.ToInt32(dtSelect.Rows[0]["FNHSysRawMatId"]));
            dtTransfer = SearchTransfer(dtSelect.Rows[0]["FTOrderNo"].ToString(), Convert.ToInt32(dtSelect.Rows[0]["FNHSysRawMatId"]));
            if (dtReserve != null && dtReserve.Rows.Count > 0)
            {
                object totalReserve = dtReserve.Compute("Sum(ocersvqty)", "");
                v_totalReserve = Convert.ToDouble(totalReserve);
                if (dtTransfer != null && dtTransfer.Rows.Count > 0)
                {
                    object totalTransfer = dtTransfer.Compute("Sum(ocetransqty)", "");
                    v_totalTransfer = Convert.ToDouble(totalTransfer);
                }
            }
            v_totalRT = v_totalReserve + v_totalTransfer;
            if (dtSelect != null && dtSelect.Rows.Count > 0)
            {
                if (dtHadCh != null && dtHadCh.Rows.Count > 0)
                {
                    object totalTransferGroup = dtGroup.Compute("Sum(FNTransferQuantity)", "");

                    if (dtTransfer != null && dtTransfer.Rows.Count > 0)
                    {
                        object totalTransferdtTransfer = dtTransfer.Compute("Sum(ocetransqty)", "");
                        v_totalTransfer = v_totalTransfer - Convert.ToDouble(totalTransferGroup);
                        if (Convert.ToDouble(totalTransferGroup) >= Convert.ToDouble(totalTransferdtTransfer))
                        {
                            v_totalTransfer = 0.00;
                        }
                    }
                    object totalReserveGroup = dtGroup.Compute("Sum(FNReserveQuantity)", "");
                    if (dtReserve != null && dtReserve.Rows.Count > 0)
                    {
                        object totalReservdtReserv = dtReserve.Compute("Sum(ocersvqty)", "");
                        v_totalReserve = v_totalReserve - Convert.ToDouble(totalReserveGroup);
                        if (Convert.ToDouble(totalReserveGroup) >= Convert.ToDouble(totalReservdtReserv))
                        {
                            v_totalReserve = 0.00;
                        }
                    }

                    v_totalRT = v_totalReserve + v_totalTransfer;
                    dtSelect = SelectMatCode(dtSelect, v_totalRT, v_totalReserve, v_totalTransfer, v_UP);
                }
                else
                {
                    dtSelect = SelectMatCode(dtSelect, v_totalRT, v_totalReserve, v_totalTransfer, v_UP);
                }
            }
            return dtSelect;
        }
        private DataTable CalculateSourcing(DataTable dt)
        {
            DataTable dtDistinct = dt.DefaultView.ToTable(true, "FTRawMatCode", "FTRawMatColorCode", "FTRawMatSizeCode", "FTOrderNo");
            DataTable dtGroup = new DataTable();
            DataTable dtMerge = new DataTable();
            DataTable dtSelect = new DataTable();
            DataTable dtReserve = new DataTable();
            DataTable dtTransfer = new DataTable();
            Double v_totalReserve, v_totalTransfer, v_totalRT;
            Double v_UP;
            string v_rawmatcode, v_colorcode, v_sizecode, v_jobcode;

            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                v_rawmatcode = dtDistinct.Rows[i]["FTRawMatCode"].ToString();
                v_colorcode = dtDistinct.Rows[i]["FTRawMatColorCode"].ToString();
                v_sizecode = dtDistinct.Rows[i]["FTRawMatSizeCode"].ToString();
                v_jobcode = dtDistinct.Rows[i]["FTOrderNo"].ToString();
                dtGroup = null; v_totalReserve = 0.0000; v_totalTransfer = 0.0000; v_totalRT = 0.00; v_UP = 0.00;
                dtGroup = ItemColorSizeDuplicate(v_rawmatcode, v_colorcode, v_sizecode, v_jobcode, dt);
                dtSelect = ItemNonDuplicate(RawMatId, RawMatCode, ColorCode, SizeCode, OrderNo, SubOrderNo, dtGroup);
                dtReserve = SearchReserve(dtGroup.Rows[0]["FTOrderNo"].ToString(), Convert.ToInt32(dtGroup.Rows[0]["FNHSysRawMatId"]));
                dtTransfer = SearchTransfer(dtGroup.Rows[0]["FTOrderNo"].ToString(), Convert.ToInt32(dtGroup.Rows[0]["FNHSysRawMatId"]));
                if (dtReserve != null && dtReserve.Rows.Count > 0)
                {
                    object totalReserve = dtReserve.Compute("Sum(ocersvqty)", "");
                    v_totalReserve = Convert.ToDouble(totalReserve);
                    if (dtTransfer != null && dtTransfer.Rows.Count > 0)
                    {
                        object totalTransfer = dtTransfer.Compute("Sum(ocetransqty)", "");
                        v_totalTransfer = Convert.ToDouble(totalTransfer);
                    }
                }
                v_totalRT = v_totalReserve + v_totalTransfer;
                if (dtSelect != null && dtSelect.Rows.Count > 0)
                {
                    dtSelect = SelectMatCode(dtSelect, v_totalRT, v_totalReserve, v_totalTransfer, v_UP);
                    dtGroup = DeleteSelectItem(RawMatId, RawMatCode, ColorCode, SizeCode, OrderNo, SubOrderNo, dtGroup);
                    v_totalReserve = v_totalReserve - Convert.ToDouble(dtSelect.Rows[0]["FNReserveQuantity"]);
                    v_totalTransfer = v_totalTransfer - Convert.ToDouble(dtSelect.Rows[0]["FNTransferQuantity"]);
                    v_totalRT = v_totalReserve + v_totalTransfer;
                    v_UP = (Convert.ToDouble(dt.Rows[i]["FNUsedQuantity"])) + (Convert.ToDouble(dt.Rows[i]["FNUsedPlusQuantity"]));
                    dtGroup = NonSelectMatCode(dtGroup, v_totalRT, v_totalReserve, v_totalTransfer, v_UP);
                    dtGroup.Merge(dtSelect);
                }
                else
                {
                    dtGroup = NonSelectMatCode(dtGroup, v_totalRT, v_totalReserve, v_totalTransfer, v_UP);
                }
                dtMerge.Merge(dtGroup);
            }
            dt = dtMerge.Copy();
            dt = ChangeUnitCode(dt);
            return dt;
        }
        private DataTable SelectMatCode(DataTable dt, Double P_RT, Double P_Reserve, Double P_Transfer, Double P_UP)
        {
            Double l_UP = P_UP;
            Double l_RT = P_RT;
            Double l_Reserve = P_Reserve;
            Double l_Transfer = P_Transfer;
            Double totalRT = P_RT;
            Double usedqty = 0.00;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Update UTHAI 2014-08-20
                if ((Convert.ToDouble(dt.Rows[i]["FNOptiplanQuantity"])) > 0)
                {
                    usedqty = (Convert.ToDouble(dt.Rows[i]["FNOptiplanQuantity"]));
                }
                else
                {
                    usedqty = (Convert.ToDouble(dt.Rows[i]["FNUsedQuantity"]));
                }

                l_UP = usedqty + (Convert.ToDouble(dt.Rows[i]["FNUsedPlusQuantity"]));

                if (l_UP < totalRT)
                {
                    if ((l_UP < l_Reserve))
                    {
                        dt.Rows[i]["FNReserveQuantity"] = l_UP;
                        dt.Rows[i]["FNTransferQuantity"] = 0.00;
                        dt.Rows[i]["FNSCQuantity"] = 0.00;
                        dt.Rows[i]["FNSCPlusQuantity"] = 0.00;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                        dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                        l_Reserve = l_Reserve - l_UP;
                        totalRT = l_Reserve + l_Transfer;
                    }
                    else
                    {
                        l_UP = l_UP - l_Reserve;
                        if (l_UP > l_Transfer)
                        {
                            dt.Rows[i]["FNReserveQuantity"] = l_UP;
                            l_Transfer = 0.00;
                        }
                        else
                        {
                            dt.Rows[i]["FNReserveQuantity"] = l_Reserve;
                            l_Transfer = l_UP;
                        }
                        dt.Rows[i]["FNTransferQuantity"] = l_Transfer;
                        dt.Rows[i]["FNSCQuantity"] = 0.0000;
                        dt.Rows[i]["FNSCPlusQuantity"] = 0.0000;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                        dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                        l_Reserve = l_Reserve - l_Reserve;
                        totalRT = l_Reserve + l_Transfer;
                    }
                }
                else
                {
                    dt.Rows[i]["FNReserveQuantity"] = l_Reserve;
                    dt.Rows[i]["FNTransferQuantity"] = l_Transfer;
                    if (P_RT > 0)
                    {
                        dt.Rows[i]["FNSCQuantity"] = (usedqty + Convert.ToDouble(dt.Rows[i]["FNUsedPlusQuantity"])) - (Convert.ToDouble(dt.Rows[i]["FNReserveQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNTransferQuantity"]));
                    }
                    else
                    {
                        dt.Rows[i]["FNSCQuantity"] = usedqty;
                        dt.Rows[i]["FNSCPlusQuantity"] = dt.Rows[i]["FNUsedPlusQuantity"];
                    }
                    dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                    dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                    dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                    dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                    dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                    dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                }
            }
            return dt;
        }
        private DataTable NonSelectMatCode(DataTable dt, Double P_RT, Double P_Reserve, Double P_Transfer, Double P_UP)
        {
            Double l_UP = P_UP;
            Double l_RT = P_RT;
            Double l_Reserve = P_Reserve;
            Double l_Transfer = P_Transfer;
            Double totalRT = P_RT;
            Double usedqty = 0.00;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Update UTHAI 2014-08-20
                if ((Convert.ToDouble(dt.Rows[i]["FNOptiplanQuantity"])) > 0)
                {
                    usedqty = (Convert.ToDouble(dt.Rows[i]["FNOptiplanQuantity"]));
                }
                else
                {
                    usedqty = (Convert.ToDouble(dt.Rows[i]["FNUsedQuantity"]));
                }
                l_UP = (usedqty) + (Convert.ToDouble(dt.Rows[i]["FNUsedPlusQuantity"]));
                if (l_UP < totalRT)
                {
                    if ((usedqty < l_Reserve))
                    {
                        dt.Rows[i]["FNReserveQuantity"] = l_UP;
                        dt.Rows[i]["FNTransferQuantity"] = 0.00;
                        dt.Rows[i]["FNSCQuantity"] = 0.00;
                        dt.Rows[i]["FNSCPlusQuantity"] = 0.00;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                        dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                        l_Reserve = l_Reserve - l_UP;
                        totalRT = l_Reserve + l_Transfer;
                    }
                    else
                    {
                        l_UP = l_UP - l_Reserve;
                        dt.Rows[i]["FNReserveQuantity"] = l_Reserve;
                        l_Reserve = 0.00;
                        if (l_UP > l_Transfer)
                        {
                            dt.Rows[i]["FNTransferQuantity"] = l_Transfer;
                            l_UP = l_UP - l_Transfer;
                            l_Transfer = 0.00;
                        }
                        else
                        {
                            if (l_UP <= l_Transfer)
                            {
                                dt.Rows[i]["FNTransferQuantity"] = l_UP;
                                l_Transfer = l_Transfer - l_UP;
                                l_UP = 0.00;
                            }
                        }
                        dt.Rows[i]["FNSCQuantity"] = 0.0000;
                        dt.Rows[i]["FNSCPlusQuantity"] = 0.0000;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                        dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                        totalRT = l_Reserve + l_Transfer;
                    }
                }
                else
                {
                    dt.Rows[i]["FNReserveQuantity"] = l_Reserve;
                    dt.Rows[i]["FNTransferQuantity"] = l_Transfer;
                    if (P_RT > 0)
                    {
                        dt.Rows[i]["FNSCQuantity"] = (usedqty + Convert.ToDouble(dt.Rows[i]["FNUsedPlusQuantity"])) - (Convert.ToDouble(dt.Rows[i]["FNReserveQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNTransferQuantity"]));
                    }
                    else
                    {
                        dt.Rows[i]["FNSCQuantity"] = usedqty;
                        dt.Rows[i]["FNSCPlusQuantity"] = dt.Rows[i]["FNUsedPlusQuantity"];
                    }
                    dt.Rows[i]["FNTotalPurchaseQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"]) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"]);
                    dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                    dt.Rows[i]["FNPricePurchase"] = dt.Rows[i]["FNPrice"];
                    dt.Rows[i]["TotalPrice"] = Convert.ToDouble(dt.Rows[i]["FNPrice"]) * Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"]);
                    dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                    dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysSuplId"];
                    l_Reserve = 0.00;
                    l_Transfer = 0.00;
                }
            }
            return dt;
        }
        private DataTable ChangeUnitCode(DataTable dt)
        {
            if (FNHSysUnitId.Text != FNHSysToUnitId.Text)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["UnitCodePurchase"] = FNHSysToUnitId.Properties.Tag;
                    dt.Rows[i]["FNHSysCurId"] = FNHSysCurId.Properties.Tag;
                    dt.Rows[i]["FNHSysSuplId"] = FNHSysSuplId.Properties.Tag;
                    if (g_From > g_To)
                    {
                        dt.Rows[i]["FNSCQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"].ToString()) / g_From;
                        dt.Rows[i]["FNSCPlusQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"].ToString()) / g_From;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = (Convert.ToDouble(dt.Rows[i]["FNSCQuantity"].ToString()) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"].ToString()));
                        dt.Rows[i]["FNPricePurchase"] = Convert.ToDouble(dt.Rows[i]["FNPrice"].ToString());
                        dt.Rows[i]["TotalPrice"] = (Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"].ToString())) * (Convert.ToDouble(dt.Rows[i]["FNPrice"].ToString()));
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNReserveQuantity"] = dt.Rows[i]["FNReserveQuantity"];
                        dt.Rows[i]["FNTransferQuantity"] = dt.Rows[i]["FNTransferQuantity"];
                    }
                    else
                    {
                        dt.Rows[i]["FNSCQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCQuantity"].ToString()) * g_From;
                        dt.Rows[i]["FNSCPlusQuantity"] = Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"].ToString()) * g_From;
                        dt.Rows[i]["FNTotalPurchaseQuantity"] = (Convert.ToDouble(dt.Rows[i]["FNSCQuantity"].ToString()) + Convert.ToDouble(dt.Rows[i]["FNSCPlusQuantity"].ToString()));
                        dt.Rows[i]["FNPricePurchase"] = Convert.ToDouble(dt.Rows[i]["FNPricePurchase"].ToString());
                        dt.Rows[i]["TotalPrice"] = (Convert.ToDouble(dt.Rows[i]["FNTotalPurchaseQuantity"].ToString())) * (Convert.ToDouble(dt.Rows[i]["FNPricePurchase"].ToString()));
                        dt.Rows[i]["FNHSysCurId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysSuplId"] = dt.Rows[i]["FNHSysCurId"];
                        dt.Rows[i]["FNHSysUnitId"] = dt.Rows[i]["FNHSysUnitId"];
                        dt.Rows[i]["FNReserveQuantity"] = dt.Rows[i]["FNReserveQuantity"];
                        dt.Rows[i]["FNTransferQuantity"] = dt.Rows[i]["FNTransferQuantity"];
                    }
                }
            }
            return dt;
        }
        private DataTable CalBalanceReserve(DataTable dtR, DataTable dtG)
        {
            Double FNReserveQuantity = 0.00;
            Double temp = 0.00;
            object _FNReserveQuantity=0;
            if (dtR != null && dtR.Rows.Count > 0)
            {
                //object _FNReserveQuantity = dtG.Compute("Sum(FNReserveQuantity)", "");

                temp = Convert.ToDouble(dtG.Compute("Sum(FNReserveQuantity)", ""));
                if (ch == "1")         
                {
                 //_FNReserveQuantity = Convert.ToDecimal(ConvertUnitR_Head(TMERTOrder_ResourceFNHSysUnitId, temp));
                    _FNReserveQuantity = Convert.ToDecimal(ConvertUnitR_Head(TINVENMMaterialFNHSysUnitId, temp));
                }
                if (!(_FNReserveQuantity is System.DBNull))
                {
                    FNReserveQuantity = Convert.ToDouble(_FNReserveQuantity);
                }
            }
            for (int i = 0; i < dtR.Rows.Count; i++)
            {
                if (Convert.ToDouble(dtR.Rows[i]["ocersvqty"]) <= FNReserveQuantity)
                {
                    dtR.Rows[i]["quantity"] = dtR.Rows[i]["ocersvqty"];
                    FNReserveQuantity = FNReserveQuantity - Convert.ToDouble(dtR.Rows[i]["quantity"]);
                }
                else
                {
                    dtR.Rows[i]["quantity"] = FNReserveQuantity;
                }
                dtR.Rows[i]["balance"] = Convert.ToInt32(dtR.Rows[i]["ocersvqty"]) - Convert.ToInt32(dtR.Rows[i]["quantity"]);
            }
            return dtR;
        }
        private DataTable CalBalanceTransfer(DataTable dtT, DataTable dtG, DataTable dtR)
        {
            Double sumFNTransferQuantity = 0.00;
            Double temp = 0.00;
            object _FNTransferQuantity=0;
            if (dtT != null && dtT.Rows.Count > 0)
            {
                if (dtG != null && dtG.Rows.Count > 0)
                {
                    //object _FNTransferQuantity = dtG.Compute("Sum(FNTransferQuantity)", "");

                    temp = Convert.ToDouble(dtG.Compute("Sum(FNReserveQuantity)", ""));
                    if (ch == "1")
                    {
                        _FNTransferQuantity = Convert.ToDecimal(ConvertUnitR_Head(TINVENMMaterialFNHSysUnitId, temp));
                    }
                    if (!(_FNTransferQuantity is System.DBNull))
                    {
                        sumFNTransferQuantity = Convert.ToDouble(_FNTransferQuantity);
                    }
                }
            }
            for (int i = 0; i < dtT.Rows.Count; i++)
            {
                if (Convert.ToDouble(dtT.Rows[i]["ocetransqty"]) <= sumFNTransferQuantity)
                {
                    dtT.Rows[i]["quantity"] = dtT.Rows[i]["ocetransqty"];
                    sumFNTransferQuantity = sumFNTransferQuantity - Convert.ToDouble(dtT.Rows[i]["quantity"]);
                }
                else
                {
                    dtT.Rows[i]["quantity"] = sumFNTransferQuantity;
                }
                dtT.Rows[i]["balance"] = Convert.ToInt32(dtT.Rows[i]["ocetransqty"]) - Convert.ToInt32(dtT.Rows[i]["quantity"]);
            }
            return dtT;
        }
        private void CalDisplayReserve(DataTable dtReserve, DataTable dtGroup)
        {
            Decimal _ocersvqty = 0;
            Decimal _ocersvqtych = 0;

            if (dtReserve != null && dtReserve.Rows.Count > 0)
            {
                _ocersvqty = Convert.ToDecimal(dtReserve.Compute("Sum(ocersvqty)", ""));
            }

            _ocersvqtych = Convert.ToDecimal(dtGroup.Compute("Sum(FNReserveQuantity)", ""));

            if (Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNReserveQuantity")) != 0)
            {
                ocersvqty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNReserveQuantity"));
            }
            else
            {
                if (_ocersvqtych < _ocersvqty)
                {
                    if (dtReserve != null && dtReserve.Rows.Count > 0)
                    {
                        ocersvqty.Value = _ocersvqty - _ocersvqtych;
                    }
                }
                else
                {
                    ocersvqty.Value = 0;
                }
                if (Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTransferQuantity")) != 0)
                {
                    ocersvqty.Value = 0;
                }

            }
        }
        private void CalDisplayTransfer(DataTable dtTransfer, DataTable dtGroup)
        {
            Decimal _ocetransqty = 0;
            Decimal _ocetransqtych = 0;
            Decimal _ocersvqtych = 0;
            Decimal x = 0;
            Decimal y = 0;

            if (dtTransfer != null && dtTransfer.Rows.Count > 0)
            {
                _ocetransqty = Convert.ToDecimal(dtTransfer.Compute("Sum(ocetransqty)", ""));
            }
            _ocetransqtych = Convert.ToDecimal(dtGroup.Compute("Sum(FNTransferQuantity)", ""));
            _ocersvqtych = Convert.ToDecimal(dtGroup.Compute("Sum(FNReserveQuantity)", ""));

            if (Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTransferQuantity")) != 0)
            {
                ocetransqty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTransferQuantity"));
            }
            else
            {
                if (_ocetransqtych < _ocetransqty)
                {
                    if (dtTransfer != null && dtTransfer.Rows.Count > 0)
                    {
                        x = oceusedqty.Value + oceusedplusqty.Value;
                        y = _ocetransqty;
                        if (x < y)
                        {
                            if (x < (_ocetransqty - _ocetransqtych))
                            {
                                ocetransqty.Value = x - ocersvqty.Value;
                                if (ocetransqty.Value < 0)
                                {
                                    ocetransqty.Value = _ocetransqty;
                                }
                                //จำนวนโอนมากกว่าจำนวนใช้
                                //if()
                                //{
                                //}
                            }
                            else
                            {
                                ocetransqty.Value = _ocetransqty - _ocetransqtych;
                            }
                        }
                        else
                        {
                            if (!(_ocetransqtych == 0))
                            {
                                ocetransqty.Value = _ocetransqty - _ocetransqtych;
                            }
                            else
                            {
                                ocetransqty.Value = _ocetransqty;
                            }

                        }
                        if (x == ocersvqty.Value)
                        {
                            ocetransqty.Value = 0;
                        }
                    }
                }
                else
                {
                    ocetransqty.Value = 0;
                }
            }
        }

        #endregion

        #region Event
       
        private void FNHSysBuyId_EditValueChanged(object sender, EventArgs e)
        {
            if (!stateload)
            {
                /*nothing*/
                //stateload = true;
            }
            else
            {
                //if (FNHSysBuyId.Text == "" && FNHSysStyleId.Text == "" && FTOrderNo.Text == "")
                //{
                //    ogcsc.DataSource = null;
                //    dtSourcing = null;
                //    HI.TL.HandlerControl.ClearControl(this.otbsc);
                //}
                //else
                //{
                //    SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text);
                //    setBindingData();
                //}
                //if (FNHSysBuyId.Text == "")
                //{
                //    FNHSysBuyId_None.Text = "";
                //}
                
                  int _FNHSysBuyId = 0;
                _FNHSysBuyId = Convert.ToInt32(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysBuyId FROM  TMERMBuy  WITH(NOLOCK) WHERE FTBuyCode='" +  HI.UL.ULF.rpQuoted(FNHSysBuyId.Text ) + "' ",Conn.DB.DataBaseName.DB_MASTER,"0"));
               

                if (_FNHSysBuyId > 0)
                {
                    refreshdata();
                };
            }
        }
        private void FNHSysStyleId_EditValueChanged(object sender, EventArgs e)
        {

            if (!stateload)
            {
                //nothing
            }
            else
            {
                //if (FNHSysBuyId.Text == "" && FNHSysStyleId.Text == "" && FTOrderNo.Text == "")
                //{
                //    ogcsc.DataSource = null;
                //    dtSourcing = null;
                //    HI.TL.HandlerControl.ClearControl(this.otbsc);
                //}
                //else
                //{
                //    SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text);
                //    setBindingData();
                //}
                //if (FNHSysStyleId.Text == "")
                //{
                //    FNHSysStyleId_None.Text = "";
                //}
                int _FNHSysStyleId = 0;
                _FNHSysStyleId = Convert.ToInt32(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysStyleId FROM  TMERMStyle  WITH(NOLOCK) WHERE FTStyleCode='" +  HI.UL.ULF.rpQuoted(FNHSysStyleId.Text ) + "' ",Conn.DB.DataBaseName.DB_MASTER,"0"));
               
                if (_FNHSysStyleId > 0)
                {
                    refreshdata();
                };
            }
        }
        private void FTOrderNo_EditValueChanged(object sender, EventArgs e)
        {

            if (!stateload)
            {
                /*nothing*/
            }
            else
            {
                //if (FNHSysBuyId.Text == "" && FNHSysStyleId.Text == "" && FTOrderNo.Text == "")
                //{
                //    ogcsc.DataSource = null;
                //    dtSourcing = null;
                //    HI.TL.HandlerControl.ClearControl(this.otbsc);
                //}
                //else
                //{
                //    SearchData(FNHSysBuyId.Text, FNHSysStyleId.Text, FTOrderNo.Text);
                //    setBindingData();
                //}

                String _FTOrderNo = "";
                _FTOrderNo = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM  TMERTOrder  WITH(NOLOCK) WHERE FTOrderNo='" + HI.UL.ULF.rpQuoted(FTOrderNo.Text) + "' ", Conn.DB.DataBaseName.DB_MERCHAN, "");
               
                if (_FTOrderNo != "")
                {
                    refreshdata();
                };
            }
        }
        private void FNTSysToUnitId_EditValueChanged(object sender, EventArgs e)
        {
            if (staterowclick == false)
            {
                //staterowclick = true;
                v_FNOptiplanQuantity = 0;
                //CalUnit();
                FNTotalPurchaseQuantity = ocepurqty.Value + ocepurplusqty.Value;
                CalculateTotalPrice();
                //staterowclick = false;
            };
        }
        private void ocepurplusqty_EditValueChanged(object sender, EventArgs e)
        {
               if (staterowclick == false)
            {
            //    staterowclick = true;
                v_FNOptiplanQuantity = 0;
                FNTotalPurchaseQuantity = ocepurqty.Value + ocepurplusqty.Value;
                CalculateTotalPrice();
             //   staterowclick = false;
            };
        }
        private void ocepurqty_EditValueChanged(object sender, EventArgs e)
        {
            if (staterowclick == false)
            {
            //    staterowclick = true;
                v_FNOptiplanQuantity = 0;
                FNTotalPurchaseQuantity = ocepurqty.Value + ocepurplusqty.Value;
                CalculateTotalPrice();
             //   staterowclick = false;
            };
        }
        private void FNSCQty_EditValueChanged(object sender, EventArgs e)
        {
            if (staterowclick == false)
            {
                double Poqty2;
                Poqty2 = Convert.ToDouble(FNSCQty.Value) * Convert.ToDouble(FNPrice.Value);
                TotalPrice.Value = Convert.ToDecimal(Poqty2);
            }
        }
        private void FNPrice_EditValueChanged(object sender, EventArgs e)
        {
            //CalculateTotalPrice();
            //Uthai 20141030
            double Poqty2;
            Poqty2 = Convert.ToDouble(FNSCQty.Value) * Convert.ToDouble(FNPrice.Value);
            TotalPrice.Value = Convert.ToDecimal(Poqty2);

        }
        private void FNHSysSuplId_EditValueChanged(object sender, EventArgs e)
        {
            string strSQL = string.Empty;
            DataTable dtSupplier = new DataTable();
            strSQL = "SELECT TOP 1 TP.FNHSysSuplId "
                    + "FROM "
                    + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier TP WITH(NOLOCK) "
                    + "WHERE "
                    + " TP.FTSuplCode = '" + FNHSysSuplId.Text + "'"
                    ;
            FNHSysSuplId.Properties.Tag = HI.Conn.SQLConn.GetField(strSQL, Conn.DB.DataBaseName.DB_MERCHAN, "");
            strSQL = "SELECT TOP 1 PO.FNPrice, CU.FTCurCode "
                  + "FROM "
                  + "[" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTPurchase P WITH(NOLOCK) "
                  + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTPurchase_OrderNo PO WITH(NOLOCK)  ON P.FTPurchaseNo = PO.FTPurchaseNo "
                  + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier S WITH(NOLOCK)  ON P.FNHSysSuplId = S.FNHSysSuplId "
                  + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnit U  WITH(NOLOCK) ON PO.FNHSysUnitId = U.FNHSysUnitId "
                  + "INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TFINMCurrency AS CU WITH(NOLOCK)  ON P.FNHSysCurId = CU.FNHSysCurId "
                  + "WHERE "
                  + " PO.FNHSysRawMatId = '" + RawMatId + "' "
                  + " AND "
                  + " P.FNHSysSuplId = '" + FNHSysSuplId.Properties.Tag + "'"
                  + " ORDER BY P.FDPurchaseDate DESC"
                  ;
            dtSupplier = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            if (dtSupplier != null && dtSupplier.Rows.Count > 0)
            {
                FNPrice.Text = dtSupplier.Rows[0]["FNPrice"].ToString();
                FNHSysCurId.Text = dtSupplier.Rows[0]["FTCurCode"].ToString();
            }
        }

        private void ogcsc_Click(object sender, EventArgs e)
        {

              Point pt = ogvsc.GridControl.PointToClient(Control.MousePosition);
              GridHitInfo info  = ogvsc.CalcHitInfo(pt);

              if (info.InRow || info.InRowCell) {
                  staterowclick = true;
                  DataTable dtPOHistory = null;
                  DataTable dtReserve = null;
                  DataTable dtTransfer = null;
                  DataTable dtTemp = null;
                  DataTable dtGroup = null;
                  try
                  {
                      if (ogvsc.DataSource != null)
                      {
                          if (ogvsc.FocusedRowHandle >= 0)
                          {
                              status = '1';

                              ch = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "ch").ToString();
                              RawMatId = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNHSysRawMatId").ToString();
                              RawMatCode = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatCode").ToString();
                              OrderNo = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTOrderNo").ToString();
                              SubOrderNo = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTSubOrderNo").ToString();
                              ColorCode = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatColorCode").ToString();
                              SizeCode = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatSizeCode").ToString();
                              strPurchaseNo = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTPurchaseNo").ToString();
                              SuplCode = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTSuplCode").ToString();
                              FNTotalPurchaseQuantity = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTotalPurchaseQuantity"));
                              v_FNOptiplanQuantity = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNOptiplanQuantity"));

                              n = ogvsc.FocusedRowHandle;

                              strStateChange = Convert.ToInt32(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNStateChange"));
                              //PurchaseHistory
                              dtPOHistory = SearchSourcingPurchaseHistory(Convert.ToInt32(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNHSysRawMatId")));
                              setBindingPOHistory(dtPOHistory);
                              //Clear binding
                              clearSourcing();
                              //Binding sourcing
                              TextEdit1.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTOrderNo").ToString();
                              FTCustomerName.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTCustName").ToString();
                              FDJobOrderDate.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FDOrderDate").ToString();
                              FDJobShipDate.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FDShipDate").ToString();
                              FNTSysMatId.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatCode").ToString();
                              FTMatColorCode.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatColorCode").ToString();
                              FTMatSizeCode.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatSizeCode").ToString();
                              FTFabricFrontSize.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTFabricFrontSize").ToString();
                              FNOptiplanQuantity.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNOptiplanQuantity"));
                              FNTSysMatId_None.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTRawMatName").ToString();

                              oceusedqty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNUsedQuantity"));
                              oceusedplusqty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNUsedPlusQuantity"));
                              FNHSysUnitId.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTUnitCode").ToString();
                              TMERTOrder_ResourceFNHSysUnitId = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNHSysUnitId").ToString();
                              TINVENMMaterialFNHSysUnitId = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "TINVENMMaterialFNHSysUnitId").ToString();
                              TINVENMMaterialFNHSysUnitCode = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "TINVENMMaterialFNHSysUnitCode").ToString();

                              dtTemp = ItemDuplicate(RawMatCode);
                              dtGroup = ItemColorSizeDuplicate(RawMatCode, ColorCode, SizeCode, OrderNo, dtTemp);
                              dtReserve = SearchReserve(OrderNo, Convert.ToInt32(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNHSysRawMatId")));
                              dtTransfer = SearchTransfer(OrderNo, Convert.ToInt32(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNHSysRawMatId")));

                              dtReserve = CalBalanceReserve(dtReserve, dtGroup);
                              setBindingReserve(dtReserve);
                              dtTransfer = CalBalanceTransfer(dtTransfer, dtGroup, dtReserve);
                              setBindingTransfer(dtTransfer);

                              CalDisplayReserve(dtReserve, dtGroup);
                              CalDisplayTransfer(dtTransfer, dtGroup);

                              string temp = ocersvqty.Value.ToString();
                              if (FNHSysUnitId.Text != TINVENMMaterialFNHSysUnitCode)
                              {
                                  if (ch == "0")
                                  {
                                      ConvertUnitRT(TINVENMMaterialFNHSysUnitId);
                                  }
                              }

                              FNSCQty.Value = 0;
                              FNPrice.Value = 0;
                              TotalPrice.Value = 0;

                              ocepurqty.Value = Convert.ToDecimal(Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNSCQuantity")));

                              ocepurplusqty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNSCPlusQuantity"));
                              FNHSysToUnitId.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "UnitCodePurchase").ToString();
                              FNSCQty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTotalPurchaseQuantity"));
                              FNPrice.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNPricePurchase"));
                              if (FNPrice.Value == 0)
                              {
                                  if (ch == "0")
                                  {
                                      FNPrice.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNPrice"));
                                  };

                              }
                              FNHSysCurId.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTCurCode").ToString();
                              TotalPrice.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "TotalPrice"));
                              TotalPrice.Value = Convert.ToDecimal(FNSCQty.Value * FNPrice.Value);
                              FNHSysSuplId.Text = ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FTSuplCode").ToString();
                              //uthai 20141030
                              if (ch == "1")
                              {
                                  FNSCQty.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNTotalPurchaseQuantity"));
                                  FNPrice.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "FNPricePurchase"));
                                  TotalPrice.Value = Convert.ToDecimal(ogvsc.GetRowCellValue(ogvsc.FocusedRowHandle, "TotalPrice"));
                              }
                              else
                              {
                                  //FNSCQty.Value = 0;
                                  //FNPrice.Value = 0;
                                  //TotalPrice.Value = 0;
                              }

                              status = '0';
                          }
                      }
                  }
                  catch (Exception ex)
                  {
                      //throw ex;
                  }
                  CalUnit();
                  if (ch == "0")
                  {
                      CalculateTotalPrice();
                  }
                  staterowclick = false;


                  try {
                       dtPOHistory.Dispose();
                       dtReserve.Dispose();
                       dtTransfer.Dispose();
                       dtTemp.Dispose();
                       dtGroup.Dispose();
                  }
                  catch { }
              };

        }
        private void wSourcing_Load(object sender, EventArgs e)
        {   
               //remove { RemoveHandler(e, sender)};
            HI.TL.HandlerControl.ClearControl(this);
            ogvsc.OptionsView.ShowFooter = true;
            ogvsc.Columns["FNUsedQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNUsedQuantity", "{0:n4}");
            ogvsc.Columns["FNUsedPlusQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNUsedPlusQuantity", "{0:n4}");
            ogvsc.Columns["FNReserveQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNReserveQuantity", "{0:n4}");
            ogvsc.Columns["FNTransferQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTransferQuantity", "{0:n4}");
            ogvsc.Columns["FNSCQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNSCQuantity", "{0:n4}");
            ogvsc.Columns["FNSCPlusQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNSCPlusQuantity", "{0:n4}");
            ogvsc.Columns["FNTotalPurchaseQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalPurchaseQuantity", "{0:n4}");
            ogvsc.Columns["FNPricePurchase"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNPricePurchase", "{0:n4}");
            ogvsc.Columns["TotalPrice"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "{0:n4}");
            ogvsc.Columns["FNOptiplanQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNOptiplanQuantity", "{0:n4}");
            ogvsc.LayoutChanged();
            FNHSysBuyId.EditValueChanged -= HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged;
            FNHSysBuyId.Leave -= HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly;
            FTOrderNo.EditValueChanged -= HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged;
            FTOrderNo.Leave -= HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly;
            FNHSysStyleId.EditValueChanged -= HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged;
            FNHSysStyleId.Leave -= HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly;
            FNHSysStyleId.Text = "";

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(this, this.ogvsc);

            if (!stateload)
            {
                stateload = true;
            }

            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString();
        }
        private void ogvsc_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            object _ch = ogvsc.GetRowCellValue(e.RowHandle, "ch");
            string check = Convert.ToString(_ch);
            string FTStateReservePo = Convert.ToString(ogvsc.GetRowCellValue(e.RowHandle, "FTStateReservePo"));
            object _FNUsedQuantity = ogvsc.GetRowCellValue(e.RowHandle, "FNUsedQuantity");
            Double FNUsedQuantity = Convert.ToDouble(_FNUsedQuantity);
            object _FNUsedQuantitySC = ogvsc.GetRowCellValue(e.RowHandle, "FNUsedQuantitySC");
            Double FNUsedQuantitySC = Convert.ToDouble(_FNUsedQuantitySC);
            if (check == "1")
            {
                e.Appearance.ForeColor = System.Drawing.Color.Green;
                if (FNUsedQuantity < FNUsedQuantitySC)
                {
                    e.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
                }
            }else if ( FTStateReservePo =="1") 
            {
                e.Appearance.ForeColor = System.Drawing.Color.Blue ;
            }

        }
      
        private void ocmgenpo_Click(object sender, EventArgs e)
        {

            HI.PO.wAutoGeneratePOFactory wAutoGenPo = new HI.PO.wAutoGeneratePOFactory("");
            wAutoGenPo.WindowState = FormWindowState.Maximized;
            wAutoGenPo.ShowDialog();

            //if (wAutoGenPo == null) {
            //    wAutoGenPo = new HI.PO.wAutoGeneratePOFactory("");

            //};

            //try { wAutoGenPo.Show(); wAutoGenPo.PrepareDataGenerate(""); }
            //catch { wAutoGenPo = new HI.PO.wAutoGeneratePOFactory(""); wAutoGenPo.Show(); }

            //wAutoGenPo.WindowState = FormWindowState.Maximized; 
            // HI.PO.wAutoGeneratePO wAutoGenPo = new HI.PO.wAutoGeneratePO();
            //wAutoGenPo.ShowDialog();
        }
        private void ocmpreview_Click(object sender, EventArgs e)
        {
            string strCon = string.Empty;

            HI.RP.Report _report = new HI.RP.Report();
            _report.ReportFolderName = "Purchase Report\\";
            _report.ReportName = "SourcingReport.rpt";
            _report.AddParameter("FNHsysCmpID", Convert.ToString(HI.ST.SysInfo.CmpID));

            if (FNHSysStyleId.Text != "")
                strCon += "{TMERTStyle.FTStyleCode}='" + FNHSysStyleId.Text + "'";
            if (FTOrderNo.Text != "")
                strCon += "AND {TMERTOrder_Resource.FTOrderNo}='" + FTOrderNo.Text + "'";
            if (FNHSysBuyId.Text != "")
                strCon += "AND {TINVENMMaterial.FTRawMatCode}='" + FNHSysBuyId.Text + "'";

            if (strCon.Substring(0, 3) == "AND")
            {
                strCon = strCon.Replace("AND", "");
            }

            _report.Formular = strCon;
            _report.Preview();
        }
        #endregion

        private void ocmexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmsavelayout_Click(object sender, EventArgs e)
        {
            HI.UL.AppRegistry.SaveLayoutGridToRegistry(this, this.ogvsc);
            HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, this.Text,null , System.Windows.Forms.MessageBoxIcon.Information);
        }


        private void FTOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { refreshdata(); }; 
        }

        private void FNHSysStyleId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { refreshdata(); }; 
        }

        private void FNHSysBuyId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { refreshdata(); };
        }

        private void TextEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try{
            if (TextEdit1.Text == "") {
                this.FNTotalOrder.Value = 0;
            } else {
                string _Qry = "";
                _Qry = "SELECT SUM(ISNULL(FNQuantity, 0) + ISNULL(FNQuantityExtra, 0) + ISNULL(FNGarmentQtyTest, 0)) AS TotalOrder ";
                _Qry += " FROM   [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) ";
                _Qry += " WHERE  (FTOrderNo = N'" +  HI.UL.ULF.rpQuoted(TextEdit1.Text.Trim() )  + "') ";
                decimal torder=0;
                torder = decimal.Parse(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0"));

                this.FNTotalOrder.Value = torder;

            };
           
            }catch {
            };
            
        }

        private void ocmgenautopofreeitem_Click(object sender, EventArgs e)
        {
            if (this.FNHSysBuyId.Text.Trim() != "" || this.FNHSysStyleId.Text.Trim() != "" || this.FTOrderNo.Text.Trim() != "")
            {

                FNHSysBuyId.Properties.Tag = Convert.ToInt32(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysBuyId FROM  TMERMBuy  WITH(NOLOCK) WHERE FTBuyCode='" + HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) + "' ", Conn.DB.DataBaseName.DB_MASTER, "0"));            
                FNHSysStyleId.Properties.Tag = Convert.ToInt32(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysStyleId FROM  TMERMStyle  WITH(NOLOCK) WHERE FTStyleCode='" + HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) + "' ", Conn.DB.DataBaseName.DB_MASTER, "0"));
               
                string _qry = "";

                _qry = "SELECT TOP 1  O.FTOrderNo,'' AS FTSubOrderNo,A.FNHSysRawMatId,A.FNUsedQuantity,A.FNUsedPlusQuantity";           
                _qry += Environment.NewLine  + "	FROM     [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder_Resource AS A WITH(NOLOCK) INNER JOIN";
                _qry += Environment.NewLine + "	         [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo INNER JOIN";
                _qry += Environment.NewLine + "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier AS S WITH(NOLOCK)   ON A.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN";
                _qry += Environment.NewLine + "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTOrder_Sourcing AS SC WITH(NOLOCK)   ON A.FTOrderNo = SC.FTOrderNo AND A.FTSubOrderNo = SC.FTSubOrderNo AND A.FNHSysRawMatId = SC.FNHSysRawMatId AND ";
                _qry += Environment.NewLine + "  A.FNStateChange = SC.FNStateChange ";
                _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)   ON  A.FNHSysRawMatId = IM.FNHSysRawMatId  ";
                _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMainMat AS MM WITH(NOLOCK)   ON  IM.FTRawMatCode = MM.FTMainMatCode  ";
                _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMMatType AS MT WITH(NOLOCK)   ON  MM.FNHSysMatTypeId = MT.FNHSysMatTypeId  ";
                _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH(NOLOCK)   ON  O.FNHSysCmpRunId = CR.FNHSysCmpRunId  ";
                _qry += Environment.NewLine + "  WHERE  (A.FTStateFree = '1') ";
                _qry += Environment.NewLine + "   AND (SC.FTOrderNo IS NULL)";
                _qry += Environment.NewLine + "   AND (CR.FTStateHire ='1')";
                _qry += Environment.NewLine + "   AND (O.FNHSysCmpId =" + HI.ST.SysInfo.CmpID + ")";

                if (this.FNHSysBuyId.Text.Trim() != "") {
                            _qry += Environment.NewLine + " AND O.FNHSysBuyId  =" + int.Parse(FNHSysBuyId.Properties.Tag.ToString()) + "";
                        };

                        if (this.FNHSysStyleId.Text.Trim() != "")
                        {
                            _qry += Environment.NewLine + " AND O.FNHSysStyleId =" + int.Parse(FNHSysStyleId.Properties.Tag.ToString()) + "";
                        };

                        if (this.FTOrderNo.Text.Trim() != "")
                        {
                            _qry += Environment.NewLine + " AND O.FTOrderNo  ='" + HI.UL.ULF.rpQuoted(FTOrderNo.Text ) + "'";
                        };
                        


                        DataTable dt = null;
                        DataTable dtgrp = null;
                        dt = HI.Conn.SQLConn.GetDataTable(_qry, HI.Conn.DB.DataBaseName.DB_PUR);

                        _qry = "SELECT MT.FTMatTypeCode AS FNHSysMatTypeId,MT.FNHSysMatTypeId AS FNHSysMatTypeId_Hide,'' AS FNHSysPurGrpId,Convert(int,0) AS FNHSysPurGrpId_Hide ";
                        _qry += Environment.NewLine + "	FROM     [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder_Resource AS A WITH(NOLOCK) INNER JOIN";
                        _qry += Environment.NewLine + "	         [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo INNER JOIN";
                        _qry += Environment.NewLine + "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier AS S WITH(NOLOCK)   ON A.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN";
                        _qry += Environment.NewLine + "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.TPURTOrder_Sourcing AS SC WITH(NOLOCK)   ON A.FTOrderNo = SC.FTOrderNo AND A.FTSubOrderNo = SC.FTSubOrderNo AND A.FNHSysRawMatId = SC.FNHSysRawMatId AND ";
                        _qry += Environment.NewLine + "  A.FNStateChange = SC.FNStateChange ";
                        _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)   ON  A.FNHSysRawMatId = IM.FNHSysRawMatId  ";
                        _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TMERMMainMat AS MM WITH(NOLOCK)   ON  IM.FTRawMatCode = MM.FTMainMatCode  ";
                        _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMMatType AS MT WITH(NOLOCK)   ON  MM.FNHSysMatTypeId = MT.FNHSysMatTypeId  ";
                _qry += Environment.NewLine + "  INNER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmpRun AS CR WITH(NOLOCK)   ON  O.FNHSysCmpRunId = CR.FNHSysCmpRunId  ";
                _qry += Environment.NewLine + "  WHERE  (A.FTStateFree = '1') ";
                       _qry += Environment.NewLine + "   AND (SC.FTOrderNo IS NULL)";
                _qry += Environment.NewLine + "   AND (CR.FTStateHire ='1')";
                _qry += Environment.NewLine + "   AND (O.FNHSysCmpId =" + HI.ST.SysInfo.CmpID + ")";

                        if (this.FNHSysBuyId.Text.Trim() != "")
                        {
                            _qry += Environment.NewLine + " AND O.FNHSysBuyId  =" + int.Parse(FNHSysBuyId.Properties.Tag.ToString()) + "";
                        };

                        if (this.FNHSysStyleId.Text.Trim() != "")
                        {
                            _qry += Environment.NewLine + " AND O.FNHSysStyleId =" + int.Parse(FNHSysStyleId.Properties.Tag.ToString()) + "";
                        };

                        if (this.FTOrderNo.Text.Trim() != "")
                        {
                            _qry += Environment.NewLine + " AND O.FTOrderNo  ='" + HI.UL.ULF.rpQuoted(FTOrderNo.Text) + "'";
                        };

                        _qry += Environment.NewLine + " GROUP BY   MT.FTMatTypeCode,MT.FNHSysMatTypeId ";
                        dtgrp = HI.Conn.SQLConn.GetDataTable(_qry, HI.Conn.DB.DataBaseName.DB_PUR);

                        if (dt.Rows.Count > 0 && dtgrp.Rows.Count >0)
                        {
                            wAutoRawmatFree _wauto = new wAutoRawmatFree();
                               _wauto.ProcType =false;
                                 HI.TL.HandlerControl.AddHandlerObj(_wauto);
                                HI.ST.SysLanguage _Lang =new HI.ST.SysLanguage();
                                _Lang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wauto.Name.ToString().Trim(), _wauto);
                                _wauto.ocmok.Enabled = true;
                                _wauto.ocmcancel.Enabled = true;
                                _wauto.ogclistsupplier.DataSource = dtgrp.Copy();
                                _wauto.ogvlistmat.OptionsView.ShowAutoFilterRow = false;
                                _wauto.ShowDialog();
                            
                                if (_wauto.ProcType == true) {

                                    int _FNHSysPurGrpId = 0;
                                    string _FTPurGrp = "";
                                    int _FNHSysCmpRunId = 0;
                                    string _FTCmpRun = "";
                                    int _FNHSysBuyId = 0;
                                    int _FNHSysStyleId = 0;
                                    Double _FNExchangeRate ;
                                    string _Admin = "";
                                    string _CmpPrefix = "";

                                    dtgrp = (DataTable)(_wauto.ogclistsupplier.DataSource);

                                    _qry = " delete from [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) + "].dbo.TTempPurchaseFreeRawmat WHERE FTUserLogin='" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)  + "'";
                                    HI.Conn.SQLConn.ExecuteNonQuery(_qry,Conn.DB.DataBaseName.DB_PUR );

                                   foreach( DataRow  R in dtgrp.Rows){

                                       _qry = " insert into [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) + "].dbo.TTempPurchaseFreeRawmat";
                                       _qry += Environment.NewLine + "(FTUserLogin, FNHSysMatTypeId, FNHSysPurGrpId, FTurGrpCode)";
                                       _qry += Environment.NewLine + "select  '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                                       _qry += Environment.NewLine + "," + Convert.ToInt32(R[1]) + "";
                                       _qry += Environment.NewLine + "," + Convert.ToInt32(R[3]) + "";
                                       _qry += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(R[2].ToString()) + "'";

                                       HI.Conn.SQLConn.ExecuteNonQuery(_qry, Conn.DB.DataBaseName.DB_PUR);

                                    }

                                    _CmpPrefix = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" + (int)(HI.ST.SysInfo.CmpID) + " ", Conn.DB.DataBaseName.DB_SYSTEM, "");
                                        
                                    _FNExchangeRate = 0.0;

                                    if (FNHSysBuyId.Text != "") { _FNHSysBuyId = int.Parse(FNHSysBuyId.Properties.Tag.ToString()); };
                                    if (FNHSysStyleId.Text != "") { _FNHSysStyleId = int.Parse(FNHSysStyleId.Properties.Tag.ToString()); };

                                    _FNHSysPurGrpId = 0;
                                    _FTPurGrp = "";
                                    _FNHSysCmpRunId = int.Parse(_wauto.FNHSysCmpRunId.Properties.Tag.ToString());
                                    _FTCmpRun = _wauto.FNHSysCmpRunId.Text;

                                    if (HI.ST.SysInfo.Admin == true) { _Admin = "1"; };

                                    HI.TL.SplashScreen _Spls = new HI.TL.SplashScreen("Auto Generating.... Please Wait.");
                                    _qry = " EXEC [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) + "].dbo.SP_Generate_AutoPO_FreeRawmat_Factory '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "','" + _Admin + "'"
                                                     + "," + _FNHSysBuyId + ",'" + _FNHSysStyleId + "','" + HI.UL.ULF.rpQuoted(FTOrderNo.Text ) + "'"
                                                     + "," + (int)(HI.ST.SysInfo.CmpID) + ",'" + HI.UL.ULF.rpQuoted(_CmpPrefix) + "'"
                                                     + ",'" + HI.UL.ULF.rpQuoted(_FTPurGrp) + "'," + (int)(_FNHSysPurGrpId) + ""
                                                     + ",'" + HI.UL.ULF.rpQuoted(_FTCmpRun) + "'," + (int)(_FNHSysCmpRunId) + ""
                                                     + "," + _FNExchangeRate + "";

                                    DataTable dtpo = null;
                                    try {

                                        dtpo = HI.Conn.SQLConn.GetDataTable(_qry, HI.Conn.DB.DataBaseName.DB_PUR);
                                        _Spls.Close();
                                    }catch {
                                        _Spls.Close();
                                    };

                                    if (dtpo != null) {
                                        if (dtpo.Rows.Count > 0) { 
                                            HI.PO.wListAutoPurchaseOrderNo _wautolist = new HI.PO.wListAutoPurchaseOrderNo();

                                            HI.TL.HandlerControl.AddHandlerObj(_wautolist);
                                            HI.ST.SysLanguage _Lang2 = new HI.ST.SysLanguage();
                                            _Lang2.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wautolist.Name.ToString().Trim(), _wautolist);

                                            //_wautolist.ocmsendapprove.Visible = false;
                                            //_wautolist.ogvlist.Columns.ColumnByFieldName("FTSelect").Visible = false;
                                            _wautolist.DataPO  = dtpo.Copy();
                                            _wautolist.RefreshDataPO();
                                            _wautolist.ShowDialog();

                                            refreshdata(); 

                                        };
                                        
                                    
                                    } else { 
                                    
                                    
                                    
                                    };


                                } 


                        } else {

                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล วัตถุดิบ Free ที่สามารถทำการ Auto ได้ !!!", 15052104437, this.Text, null, MessageBoxIcon.Warning);
                        };


            }
            else { 
                    
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุเงื่อนไข !!!",1505210037,this.Text,null,MessageBoxIcon.Warning);
            };
        }

        private void FNHSysStyleId_lbl_Click(object sender, EventArgs e)
        {

        }
  
    }
}
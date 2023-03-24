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
    public partial class wSamStylePopup : DevExpress.XtraEditors.XtraForm
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
        HI.PO.wAutoGeneratePO wAutoGenPo = null;

        #endregion

        public  wSamStyle _Main { get; set; }
        #region Constructor
        public wSamStylePopup()
        {
            InitializeComponent();
        }
        #endregion


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //check already SAM
                String rowCount = String.Empty;
                String _query = String.Empty;
                _query = @" SELECT COUNT(*) AS n 
                              FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + @"].[dbo].[TRDMSamStyle]
                              WHERE FNHSysStyleId = '" + lbFNHSysStyleId.Text + "'" ;
                rowCount = HI.Conn.SQLConn.GetField(_query, Conn.DB.DataBaseName.DB_PLANNING, "0");

                SaveData(rowCount);

                //refresh
                //this
                //wSamStyle w = new wSamStyle();
                //w.formload();
                //Call CallByName(Me.MainObject, "LoadBarcode", CallType.Method, { DocumentNo})

                // wSamStyle p = new wSamStyle();

                _Main.formload();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool SaveData(String RowCount)
        {
            try
            {
                Decimal FNSampleSam = 0;
                Decimal FNGTMSam = 0;
                Decimal FN1stProdSam = 0;
                Decimal FNBulkSam = 0;
                Decimal FNSMVSam = 0;


                if (txtFNSampleSam.Text != "")
                { FNSampleSam = Convert.ToDecimal(txtFNSampleSam.Text); }
                if (txtFNGTMSam.Text != "")
                { FNGTMSam = Convert.ToDecimal(txtFNGTMSam.Text); }
                if (txtFN1stProdSam.Text != "")
                { FN1stProdSam = Convert.ToDecimal(txtFN1stProdSam.Text); }
                if (txtFNBulkSam.Text != "")
                { FNBulkSam = Convert.ToDecimal(txtFNBulkSam.Text); }
                if (txtFNSMVSam.Text != "")
                { FNSMVSam = Convert.ToDecimal(txtFNSMVSam.Text); }

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR);
                HI.Conn.SQLConn.SqlConnectionOpen();
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand();
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction();

                if (RowCount != "0")
                {
                    //UPDATE
                    strSQL = "UPDATE [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + "].[dbo].[TRDMSamStyle]  "
                            + " SET "
                            + "FTUpdUser = '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'"
                            + ",FDUpdDate = " + HI.UL.ULDate.FormatDateDB + ""
                            + ",FTUpdTime = " + HI.UL.ULDate.FormatTimeDB + ""
                            + ",FNSampleSam= '" + FNSampleSam.ToString() + "'"
                            + ",FNGTMSam = '" + FNGTMSam.ToString() + "'"
                            + ",FN1stProdSam ='" + FN1stProdSam.ToString() + "'"
                            + ",FNBulkSam ='" + FNBulkSam.ToString() + "'"
                            + ",FNSMVSam ='" + FNSMVSam.ToString() + "'"

                            + " WHERE "
                            + "     FNHSysStyleId ='" + lbFNHSysStyleId.Text+ "'";

                }
                else
                {
                    //iNSERT
                    strSQL = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + "].[dbo].[TRDMSamStyle] "
                        + "( "
                        + "FTInsUser,FDInsDate,FTInsTime"
                        + ",FNHSysStyleId,FNSampleSam ,FNGTMSam,FN1stProdSam,FNBulkSam ,FNSMVSam "
                        + " ) "
                        + "VALUES "
                        + "('" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'"
                        + "," + HI.UL.ULDate.FormatDateDB + ""
                        + "," + HI.UL.ULDate.FormatTimeDB + ""
                        + "," + Convert.ToInt32(lbFNHSysStyleId.Text) + " "
                        + "," + FNSampleSam.ToString() + " "
                        + "," + FNGTMSam.ToString() + " "
                        + "," + FN1stProdSam.ToString() + " "
                        + "," + FNBulkSam.ToString() + " "
                        + "," + FNSMVSam.ToString() + " "
                        + ")";

                }
                if (HI.Conn.SQLConn.ExecuteTran(strSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0)
                {
                    HI.Conn.SQLConn.Tran.Rollback();
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                    return false;
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการทำารลบข้อมูล SAM ใช่หรือไม่ ?", 201906610001, lbFTStyleCode.Text))
                {
                    DeleteData();
                    this.Close();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DeleteData()
        {
            try
            {
                string strSQL = string.Empty;
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING);
                HI.Conn.SQLConn.SqlConnectionOpen();
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand();
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction();

                strSQL = " DELETE  FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + "].dbo.TRDMSamStyle "
                    + " WHERE FNHSysStyleId ='" + lbFNHSysStyleId.Text + "'";
                    HI.Conn.SQLConn.Execute_Tran(strSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran);
               
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtFNSampleSam.Text = "";
            txtFNGTMSam.Text = "";
            txtFN1stProdSam.Text = "";
            txtFNBulkSam.Text = "";
            txtFNSMVSam.Text = "";
        }

        private void Ocmexit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
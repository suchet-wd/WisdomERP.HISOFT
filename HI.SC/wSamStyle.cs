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
    public partial class wSamStyle : DevExpress.XtraEditors.XtraForm
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
        HI.PO.wAutoGeneratePO wAutoGenPo = null  ;

        #endregion
        wSamStylePopup w;
        #region Constructor
        public wSamStyle()
        {

            InitializeComponent();
             w = new wSamStylePopup();

            ST.SysLanguage oSysLang;
            oSysLang = new ST.SysLanguage();
            oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, w.Name, w);

        }
        #endregion

        public void formload()
        {
            DataTable dt = new DataTable();
            dt = BindAlready();
            gridControl1.DataSource = dt;
            GVMERMStyle.RefreshData();
            gridControl1.Refresh();
            GVMERMStyle.OptionsView.ShowFooter = true;
            GVMERMStyle.Columns["FTStyleCode"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;

            DataTable dt_blank = new DataTable();
            dt_blank = BindBlank();
            ogcmerstyleblank.DataSource = dt_blank;
            GVMERMStyleBlank.RefreshData();
            ogcmerstyleblank.Refresh();
            GVMERMStyleBlank.OptionsView.ShowFooter = true;
            GVMERMStyleBlank.Columns["FTStyleCode"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
        }

        private void Ocmload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = BindAlready();
                gridControl1.DataSource = dt;
                GVMERMStyle.OptionsView.ShowFooter = true;

                DataTable dt_blank = new DataTable();
                dt_blank = BindBlank();
                ogcmerstyleblank.DataSource = dt_blank;
                //gridControl1.DataBindings();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable BindAlready()
        {
            string strSQL = string.Empty;
            DataTable dtStyle = new DataTable();

            String sqlWhere = "";
            sqlWhere = "  WHERE SS.FNHSysStyleId IS NOT NULL ";

            //if (rdWhereAlreadySam.Checked)
            //{
            //    sqlWhere = "  WHERE SS.FNHSysStyleId IS NOT NULL ";
            //}
            //if (rdWhereBlankSam.Checked)
            //{
            //    sqlWhere = "  WHERE SS.FNHSysStyleId IS NULL ";
            //}

            strSQL = @"  SELECT S.FNHSysStyleId
                          ,FTStyleCode
                          ,FTStyleNameTH
                          ,FTStyleNameEN
                          ,FTStateActive
                          ,FNHSysCustId
                          ,FNCMDisPer
                          ,FNNetCM
	                     ,CAST((SS.FNSampleSam) as  decimal(18,3)) AS [FNSampleSam]
						  ,CAST((SS.FNGTMSam) as  decimal(18,3)) AS [FNGTMSam]
						  ,CAST((SS.FN1stProdSam) as  decimal(18,3)) AS [FN1stProdSam]
						  ,CAST((SS.FNBulkSam) as  decimal(18,3)) AS [FNBulkSam]
						  ,CAST((SS.FNSMVSam) as  decimal(18,3)) AS [FNSMVSam]
                      FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + @"].[dbo].[TMERMStyle] S
                      LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + "].[dbo].[TRDMSamStyle] SS (NOLOCK) ON S.FNHSysStyleId = SS.FNHSysStyleId " +
                      sqlWhere;
            dtStyle = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dtStyle;
        }

        private DataTable BindBlank()
        {
            string strSQL = string.Empty;
            DataTable dtStyle = new DataTable();

            String sqlWhere = "";
            sqlWhere = "  WHERE SS.FNHSysStyleId IS NULL ";

            //if (rdWhereAlreadySam.Checked)
            //{
            //    sqlWhere = "  WHERE SS.FNHSysStyleId IS NOT NULL ";
            //}
            //if (rdWhereBlankSam.Checked)
            //{
            //    sqlWhere = "  WHERE SS.FNHSysStyleId IS NULL ";
            //}

            strSQL = @"  SELECT S.FNHSysStyleId
                          ,FTStyleCode
                          ,FTStyleNameTH
                          ,FTStyleNameEN
                          ,FTStateActive
                          ,FNHSysCustId
                          ,FNCMDisPer
                          ,FNNetCM
	                      ,CAST((SS.FNSampleSam) as  decimal(18,3)) AS [FNSampleSam]
						  ,CAST((SS.FNGTMSam) as  decimal(18,3)) AS [FNGTMSam]
						  ,CAST((SS.FN1stProdSam) as  decimal(18,3)) AS [FN1stProdSam]
						  ,CAST((SS.FNBulkSam) as  decimal(18,3)) AS [FNBulkSam]
						  ,CAST((SS.FNSMVSam) as  decimal(18,3)) AS [FNSMVSam]
                      FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + @"].[dbo].[TMERMStyle] S
                      LEFT JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) + "].[dbo].[TRDMSamStyle] SS (NOLOCK) ON S.FNHSysStyleId = SS.FNHSysStyleId " +
                      sqlWhere;
            dtStyle = HI.Conn.SQLConn.GetDataTable(strSQL, Conn.DB.DataBaseName.DB_MASTER);
            return dtStyle;
        }

        private void GVMERMStyle_RowCellClick(object sender, RowCellClickEventArgs e)
        {

            //wSamStylePopup w = new wSamStylePopup();

            //ST.SysLanguage oSysLang;
            //oSysLang = new ST.SysLanguage();
            //oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, w.Name, w);
            HI.TL.HandlerControl.ClearControl(w);

            w.lbFNHSysStyleId.Text = GVMERMStyle.GetFocusedRowCellValue("FNHSysStyleId").ToString();
            w.lbFTStyleCode.Text = GVMERMStyle.GetFocusedRowCellValue("FTStyleCode").ToString();
            w.lbFTStyleNameTH.Text = GVMERMStyle.GetFocusedRowCellValue("FTStyleNameTH").ToString();
            w.lbFTStyleNameEN.Text = GVMERMStyle.GetFocusedRowCellValue("FTStyleNameEN").ToString();

            String FNSampleSam = "0.000";
            String FNGTMSam = "0.000";
            String FN1stProdSam = "0.000";
            String FNBulkSam = "0.000";
            String FNSMVSam = "0.000";

            if (GVMERMStyle.GetFocusedRowCellValue("FNSampleSam").ToString() != "")
            { FNSampleSam = GVMERMStyle.GetFocusedRowCellValue("FNSampleSam").ToString(); }
            if (GVMERMStyle.GetFocusedRowCellValue("FNGTMSam").ToString() != "")
            { FNGTMSam = GVMERMStyle.GetFocusedRowCellValue("FNGTMSam").ToString(); }
            if (GVMERMStyle.GetFocusedRowCellValue("FN1stProdSam").ToString() != "")
            { FN1stProdSam = GVMERMStyle.GetFocusedRowCellValue("FN1stProdSam").ToString(); }
            if (GVMERMStyle.GetFocusedRowCellValue("FNBulkSam").ToString() != "")
            { FNBulkSam = GVMERMStyle.GetFocusedRowCellValue("FNBulkSam").ToString(); }
            if (GVMERMStyle.GetFocusedRowCellValue("FNSMVSam").ToString() != "")
            { FNSMVSam = GVMERMStyle.GetFocusedRowCellValue("FNSMVSam").ToString(); }

            w.txtFNSampleSam.Text = string.Format("{0:0.000}", FNSampleSam);
            w.txtFNGTMSam.Text = string.Format("{0:0.000}", FNGTMSam);
            w.txtFN1stProdSam.Text = string.Format("{0:0.000}", FN1stProdSam);
            w.txtFNBulkSam.Text = string.Format("{0:0.000}", FNBulkSam);
            w.txtFNSMVSam.Text = string.Format("{0:0.000}", FNSMVSam);

            w._Main = this;
            w.ShowDialog();

            formload();

        }

        //private void GridControl1_DoubleClick(object sender, EventArgs e)
        //{

        //}
        private void GVMERMStyleBlank_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //wSamStylePopup w = new wSamStylePopup();
            //ST.SysLanguage oSysLang;
            //oSysLang = new ST.SysLanguage();
            //oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, w.Name, w);
            HI.TL.HandlerControl.ClearControl(w);
            w.lbFNHSysStyleId.Text = GVMERMStyleBlank.GetFocusedRowCellValue("FNHSysStyleId").ToString();
            w.lbFTStyleCode.Text = GVMERMStyleBlank.GetFocusedRowCellValue("FTStyleCode").ToString();
            w.lbFTStyleNameTH.Text = GVMERMStyleBlank.GetFocusedRowCellValue("FTStyleNameTH").ToString();
            w.lbFTStyleNameEN.Text = GVMERMStyleBlank.GetFocusedRowCellValue("FTStyleNameEN").ToString();

            String FNSampleSam = "0.000";
            String FNGTMSam = "0.000";
            String FN1stProdSam = "0.000";
            String FNBulkSam = "0.000";
            String FNSMVSam = "0.000";

            if (GVMERMStyleBlank.GetFocusedRowCellValue("FNSampleSam").ToString() != "")
            { FNSampleSam = GVMERMStyleBlank.GetFocusedRowCellValue("FNSampleSam").ToString(); }
            if (GVMERMStyleBlank.GetFocusedRowCellValue("FNGTMSam").ToString() != "")
            { FNGTMSam = GVMERMStyleBlank.GetFocusedRowCellValue("FNGTMSam").ToString(); }
            if (GVMERMStyleBlank.GetFocusedRowCellValue("FN1stProdSam").ToString() != "")
            { FN1stProdSam = GVMERMStyleBlank.GetFocusedRowCellValue("FN1stProdSam").ToString(); }
            if (GVMERMStyleBlank.GetFocusedRowCellValue("FNBulkSam").ToString() != "")
            { FNBulkSam = GVMERMStyleBlank.GetFocusedRowCellValue("FNBulkSam").ToString(); }
            if (GVMERMStyleBlank.GetFocusedRowCellValue("FNSMVSam").ToString() != "")
            { FNSMVSam = GVMERMStyleBlank.GetFocusedRowCellValue("FNSMVSam").ToString(); }

            w.txtFNSampleSam.Text = string.Format("{0:0.000}", FNSampleSam);
            w.txtFNGTMSam.Text = string.Format("{0:0.000}", FNGTMSam); 
            w.txtFN1stProdSam.Text = string.Format("{0:0.000}", FN1stProdSam); 
            w.txtFNBulkSam.Text = string.Format("{0:0.000}", FNBulkSam); 
            w.txtFNSMVSam.Text = string.Format("{0:0.000}", FNSMVSam); 

            //w.txtFNSampleSam.Text = FNSampleSam;
            //w.txtFNGTMSam.Text = FNGTMSam;
            //w.txtFN1stProdSam.Text = FN1stProdSam;
            //w.txtFNBulkSam.Text = FNBulkSam;
            //w.txtFNSMVSam.Text = FNSMVSam;

            w._Main = this;

            w.ShowDialog();

            formload();
        }

        private void WSamStyle_Load(object sender, EventArgs e)
        {
            formload();
        }

        private void Ocmexit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WSamStyle_Activated(object sender, EventArgs e)
        {
            formload();
        }

        private void Ocmload_Click_1(object sender, EventArgs e)
        {
            formload();
        }
    }
}
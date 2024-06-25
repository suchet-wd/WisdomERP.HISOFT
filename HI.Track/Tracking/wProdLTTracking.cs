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
            this.lbRemarkQCRMDS.Text = ((HI.ST.Lang.Language).ToString() == "TH") ?
                "*** รายงานนี้แสดงข้อมูลเฉพาะ \"Q-CRMDS\" เท่านั้น ***" : "*** This report will show only \"Q-CRMDS\" ***";
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
                    
                    // Load Data to Grid
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

        private void ogvDetail_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (ogvDetail.GetRowCellValue(e.RowHandle, "StyleParent").ToString() == "")
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
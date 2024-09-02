using System;
using System.Data;

namespace HI.Track
{
    public partial class wBomDevImportTracking : DevExpress.XtraEditors.XtraForm
    {
        public wBomDevImportTracking()
        {
            InitializeComponent();
        }

        public string CallMethodName
        {
            get; set;
        }

        public string CallMenuName
        {
            get; set;
        }

        public string CallMethodParm
        {
            get; set;
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            //if (VerifyField())
            //{
            String _Qry = "";
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                ogcDetail.DataSource = null;

                _Qry = "EXEC " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".[dbo].[Get_BOM_Dev_Import_Original_Tracking] ";

                //if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "" && FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
                //{
                //    _Qry += ", ";
                //}
                if (chBOMOriginal.Checked && chBOMDev.Checked)
                {
                    _Qry += "@GetData = 'all' ";
                }
                else if (chBOMOriginal.Checked && !chBOMDev.Checked)
                {
                    _Qry += "@GetData = 'originalonly' ";
                }
                else if (!chBOMOriginal.Checked && chBOMDev.Checked)
                {
                    _Qry += "@GetData = 'devonly' ";
                }

                if (FDDateStart.Text != "" && FDDateEnd.Text != "")
                {
                    _Qry += ",@FDDateStart = '" + UL.ULDate.ConvertEnDB(FDDateStart.Text) + "', @FDDateEnd = '" + UL.ULDate.ConvertEnDB(FDDateEnd.Text) + "'";
                }

                _Qry += ", @LangShow = '" + (HI.ST.Lang.Language).ToString() + "'";

                DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN);
                ogcDetail.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _Spls.Close();
            //}
            //else
            //{
            //    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            //}
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        } // End ocmClear_Click

        private bool VerifyField()
        {
            bool checkfield = false;
            if (FDDateStart.Text != "" && FDDateEnd.Text != "")
            {
                checkfield = true;
            }
            else if (FDDateStart.Text == "" && FDDateEnd.Text == "")
            {
                checkfield = true;
            }

            return checkfield;
        } // End VerifyField

        private void ogvDetail_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (ogvDetail.GetRowCellValue(e.RowHandle, "BOMType").ToString() == "BOM Dev")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Blue;
                }
                if (ogvDetail.GetRowCellValue(e.RowHandle, "BOMType").ToString() == "BOM Original")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Fuchsia;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } // End ogvDetail_RowStyle

    } // End Class
} // End namespace HI.Track
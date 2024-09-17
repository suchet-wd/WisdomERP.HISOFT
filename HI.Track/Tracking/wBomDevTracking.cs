﻿using System;
using System.Data;

namespace HI.Track
{
    public partial class wBomDevTracking : DevExpress.XtraEditors.XtraForm
    {
        public wBomDevTracking()
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
            String _Qry = "";
            var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
            try
            {
                ogcDetail.DataSource = null;

                _Qry = "EXEC " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".[dbo].[Get_BOM_Dev_Import_Original_Tracking] ";

                if (chBOMDev.Checked)
                {
                    _Qry += "@GetData = 'devonly' ";
                }

                if (FDDateStart.Text != "" && FDDateEnd.Text != "")
                {
                    _Qry += ",@FDDateStart = '" + UL.ULDate.ConvertEnDB(FDDateStart.Text) + "', @FDDateEnd = '" + UL.ULDate.ConvertEnDB(FDDateEnd.Text) + "'";
                }

                if (cFTSeason.Text.Trim() != "")
                {
                    _Qry += ",@Season = '" + cFTSeason.Text + "'";
                    _Qry += ",@SeasonTo = '" + cFTSeason.Text + "'";
                }

                if (cFTStyle.Text.Trim() != "")
                {
                    _Qry += ",@Style = '" + cFTStyle.Text + "'";
                    _Qry += ",@StyleTo = '" + cFTStyle.Text + "'";
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
            CheckFormatDate();
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            ClearForm();
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

        private void CheckFormatDate()
        {
            if (FDDateStart.Text == "01-01-0001")
            {
                FDDateStart.Text = "";
            }
            if (FDDateEnd.Text == "01-01-0001")
            {
                FDDateEnd.Text = "";
            }
        } // End VerifyField

        private void ClearForm()
        {
            HI.TL.HandlerControl.ClearControl(this);
            FDDateStart.Text = "";
            FDDateEnd.Text = "";
            cFTSeason.Text = "";
            cFTStyle.Text = "";
        } // End ClearForm

    } // End Class
} // End namespace HI.Track
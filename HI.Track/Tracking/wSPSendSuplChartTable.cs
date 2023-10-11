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
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.XtraTab;

namespace HI.Track
{
    public partial class wSPSendSuplChartTable : DevExpress.XtraEditors.XtraForm
    {
        public wSPSendSuplChartTable()
        {
            InitializeComponent();
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode;

            //CreateTabDynamic(null, null);
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            try
            {

                if (VerifyField())
                {
                    var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                    try
                    {
                        LoadData();
                        _Spls.Close();
                    }
                    catch (Exception ex)
                    {
                        _Spls.Close();
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }


        private bool VerifyField()
        {
            bool checkstate = true;
            try
            {
                if (FTOrderNo.Text == "" & FTOrderNoTo.Text == "")
                {
                    if (FTStartSendSupl.Text == "")
                    {
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FTStartSendSupl_lbl.Text);
                        FTStartSendSupl.Focus();
                        checkstate = false;
                    }
                    if (FTEndSendSupl.Text == "")
                    {
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FTEndSendSupl_lbl.Text);
                        FTEndSendSupl.Focus();
                        checkstate = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return checkstate;
        }

        private void LoadData()
        {
            string _Qry = "";
            DataTable _dt = new DataTable();

            try
            {
                _Qry = "SELECT SUM (D.FNQuantity) AS FNQuantity, P.FTSuplCode, P.FTSuplNameEN, P.FTSuplNameTH " +
                    " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) + "].dbo.TSMPTReceiveSupl AS R WITH(NOLOCK ) " +
                    " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) + "].dbo.TSMPTReceiveSupl_Barcode AS B WITH(NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo " +
                    " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) + "].dbo.TSMPTBarcode_SendSupl AS S WITH(NOLOCK) ON B.FTBarcodeSendSuplNo = S.FTBarcodeSendSuplNo " +
                    " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) + "].dbo.V_TSMPTBundle_MainBarcode AS D WITH(NOLOCK) ON S.FTBarcodeBundleNo = D.FTBarcodeBundleNo " +
                    " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSupplier AS P WITH(NOLOCK) ON S.FNHSysSuplId = P.FNHSysSuplId " +
                    " LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPRODTOrderProd AS PX2 WITH(NOLOCK) ON S.FTOrderProdNo = PX2.FTOrderProdNo " +
                    " WHERE P.FTSuplCode <> '' AND R.FDRcvSuplDate >= '2022/11/16' AND R.FDRcvSuplDate <= '2022/11/16' " +
                    " AND S.FNHSysCmpId = '1306010001' " +
                    " GROUP BY P.FTSuplCode, P.FTSuplNameEN, P.FTSuplNameTH ";
                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE);


                CreateTabDynamic(_dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateTabDynamic(DataTable _dt)
        {
            try
            {
                _dt = new DataTable();
                _dt.Columns.Add("1");
                _dt.Columns.Add("2");
                _dt.Columns.Add("3");
                otabDetail.TabPages.Clear();
                //_dtTab = _dt;// (Select "FTSuplCode <>''").CopyToDataTable();
                //XtraTabControl otabDetail = new XtraTabControl();
                //otabDetail.Dock = DockStyle.Fill;
                otabDetail.Visible = true;
                //foreach (XtraTabPage tabPage in otabDetail.TabPages)
                //string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };

                foreach (DataRow r in _dt.Rows)
                {
                    XtraTabPage newPage = new XtraTabPage();
                    newPage.Text = r["FTSuplCode"].ToString();
                    newPage.Controls.AddRange(otabDetail.Controls.OfType<Control>().ToArray());
                    otabDetail.TabPages.Add(newPage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
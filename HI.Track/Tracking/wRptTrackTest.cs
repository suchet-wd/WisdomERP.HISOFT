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
    public partial class wRptTrackTest : Form
    {
        public wRptTrackTest()
        {
            InitializeComponent();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool VerifyField()
        {
            return FTCusCode.Text != "" ? true : false;
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            String Qry = "";
            DataTable dt = new DataTable();
            if (VerifyField())
            {
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                _Spls.Close();
            }
        }
    }
}

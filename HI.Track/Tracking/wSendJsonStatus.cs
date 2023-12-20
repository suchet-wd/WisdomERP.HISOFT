using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HI.Track.Tracking
{
    public partial class wSendJsonStatus : Form
    {
        public wSendJsonStatus()
        {
            InitializeComponent();
        }

        private void ocmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ogvdetail_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (IsPass(ogvdetail, e.RowHandle))
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
        private bool IsPass(GridView view, int row)
        {
            return (Convert.ToString(view.GetRowCellValue(row, "SendStatus")) == "True") ? true : false;
        }
    }
}

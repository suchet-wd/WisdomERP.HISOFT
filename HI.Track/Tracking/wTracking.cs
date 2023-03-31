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
    public partial class wTracking : DevExpress.XtraEditors.XtraForm
    {
        public wTracking()
        {
            InitializeComponent();
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
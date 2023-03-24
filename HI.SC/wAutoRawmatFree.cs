using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HI.SC
{
    public partial class wAutoRawmatFree : DevExpress.XtraEditors.XtraForm
    {
       
        public wAutoRawmatFree()
        {
            InitializeComponent();
        }

        public Boolean  ProcType { get; set; }

        private void ocmcancel_Click(object sender, EventArgs e)
        {
            this.ProcType = false;
            this.Close(); 
        }

        private void ocmok_Click(object sender, EventArgs e)
        {
            if (FNHSysCmpRunId.Text != "") {            
            
                 if (this.ogclistsupplier.DataSource != null)
                 {

                     DataTable dt = (DataTable)(this.ogclistsupplier.DataSource);
                     dt.AcceptChanges();
                     if (dt.Select("FNHSysPurGrpId=''").Length <= 0)
                     {
                         this.ProcType = true;
                         this.Close();
                     }
                     else { HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, ""); }; 
                   

                 }
                 else { HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData,""); };                   
            
            } else { HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysCmpRunId_lbl.Text);  };          
        }
    }
}
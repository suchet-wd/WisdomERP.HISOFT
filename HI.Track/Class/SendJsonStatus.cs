using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HI.Track.Class
{
    class SendJsonStatus
    {
        private string costSheetNo;
        private string sendType;
        private string status;
        private string sendRemark;

        public string CostSheetNo { get => costSheetNo; set => costSheetNo = value; }
        public string SendType { get => sendType; set => sendType = value; }
        public string Status { get => status; set => status = value; }
        public string SendRemark { get => sendRemark; set => sendRemark = value; }
    }
}

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
        private string sendStatus;
        private string sendRemark;

        public string CostSheetNo { get => costSheetNo; set => costSheetNo = value; }
        public string SendType { get => sendType; set => sendType = value; }
        public string SendStatus { get => sendStatus; set => sendStatus = value; }
        public string SendRemark { get => sendRemark; set => sendRemark = value; }
    }
}

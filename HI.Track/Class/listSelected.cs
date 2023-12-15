using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HI.Track.Class
{
    class listJSONS
    {
        private string costsheetNo;
        private bool cbd;
        private bool picture;
        private bool mark;
        private string teamMulti;
        private string styleId;
        private string venderPramId;

        public string CostsheetNo
        {
            get { return costsheetNo; }
            set { costsheetNo = value; }
        }

        public bool CBD
        {
            get { return cbd; }
            set { cbd = value; }
        }

        public bool Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public bool Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        public string TeamMulti
        {
            get { return teamMulti; }
            set { teamMulti = value; }
        }

        public string StyleId
        {
            get { return styleId; }
            set { styleId = value; }
        }

        public string VenderPramId
        {
            get { return venderPramId; }
            set { venderPramId = value; }
        }
    }


}

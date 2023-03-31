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
using System.Net.Http;
using System.Net.Http.Headers;


namespace HI.Track
{
    public partial class wTestControl : DevExpress.XtraEditors.XtraForm
    {
        public wTestControl()
        {
            InitializeComponent();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            string token = "Nwf0iwXh87TV6vQypal9wD0Xk7YSdCk7MJsuqtliWLH";
            string url = "https://notify-api.line.me/api/notify";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var line = new MultipartFormDataContent())
                {
                    //////sticker
                    //line.Add(new StringContent(stickerId), "stickerId");
                    //line.Add(new StringContent(stickerPackageId), "stickerPackageId");
                    //////message
                    line.Add(new StringContent("Hello"), "message");
                    //////imgFile need ByteArrayContent
                    //line.Add(new ByteArrayContent(image), "imageFile", "test.png");
                    //////Photo from url
                    //line.Add(new StringContent(photourl), "imageThumbnail");
                    //line.Add(new StringContent(photourl), "imageFullsize");

                    //post to line api
                    var response = client.PostAsync(url, line).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show(content.ToString());
                    //Photo from url และ imgFile ไม่สามารถส่งรวมกันครั้งเดียวได้ ต้องแยกคำสั่งนะครับ
                }
            }
        }
    }
}
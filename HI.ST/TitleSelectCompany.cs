using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.VisualBasic;

namespace HI.ST
{
    public partial class TitleSelectCompany : DevExpress.XtraEditors.XtraForm
    {
        public TitleSelectCompany()
        {
            InitializeComponent();


            //Icon formicon = null;
            //if (imagename != null)
            //{
            //try
            //{
            //    Bitmap bitmap = new Bitmap(imagename);
            //    formicon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());


            //    //this.Icon = System.Drawing.Icon.FromHandle((new Bitmap(Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(pathimage))))).GetHicon());
            //}
            //catch { }
            //}

            tileControl1.Groups.Clear();

            DataTable dt = GetCompany();
            int totalmenu = dt.Rows.Count ;

          
                var group = new DevExpress.XtraEditors.TileGroup();
                tileControl1.Groups.Add(group);
                group.Text = ((HI.ST.Lang.Language == HI.ST.Lang.eLang.TH ) ? "" :"");
                        
              foreach (DataRow Rmenu in dt.Select("FTCmpCode<>''", "FTCmpCode"))
                {

                var item = new DevExpress.XtraEditors.TileItem();
                // item.ItemSize = DevExpress.XtraEditors.TileItemSize.Large ;
               
                //item.ImageScaleMode = TileItemImageScaleMode.Stretch;
        
                //try
                //{
                //    if (Rmenu["FPCmpImage"] != null)
                //    {
                //        try
                //        {

                //            Image CmpImg  = HI.UL.ULImage.ConvertByteArrayToImmage(Rmenu["FPCmpImage"]);
                //            item.Image = CmpImg;//DK.UL.Image.ConvertByteArrayToImmageResize(Rmenu["FPCmpImage"],UL.Image.PicType.SelectCmp);
                //            //item.BackgroundImage = CmpImg;
                //            item.AppearanceItem.Normal.Image = CmpImg;                         
                //            item.AppearanceItem.Normal.BorderColor = Color.Blue;
                //            item.AppearanceItem.Hovered.Image = CmpImg;
                //            item.AppearanceItem.Hovered.BorderColor = Color.Blue;
                //            item.AppearanceItem.Pressed.Image = CmpImg;
                //            item.AppearanceItem.Pressed.BorderColor = Color.Blue;
                //            item.AppearanceItem.Selected.Image = CmpImg;
                //            item.AppearanceItem.Selected.BorderColor = Color.Blue;
                //            //item.BackgroundImageScaleMode = TileItemImageScaleMode.Stretch;
                //        }
                //        catch { }
                //    }//Image
                //}
                //catch { }

                var element = new DevExpress.XtraEditors.TileItemElement();
                element.Appearance.Normal.Font= new System.Drawing.Font("Tahoma", 18F,FontStyle.Bold );
                element.Appearance.Normal.Options.UseFont = true;
                element.Appearance.Normal.ForeColor = Color.Black;

                element.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 18F, FontStyle.Bold);
                element.Appearance.Hovered.Options.UseFont = true;
                element.Appearance.Hovered.ForeColor = Color.Black;

                element.Appearance.Pressed.Font = new System.Drawing.Font("Tahoma", 18F, FontStyle.Bold);
                element.Appearance.Pressed.Options.UseFont = true;
                element.Appearance.Pressed.ForeColor = Color.Black;

                element.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 18F, FontStyle.Bold);
                element.Appearance.Selected.Options.UseFont = true;
                element.Appearance.Selected.ForeColor = Color.Black;

                string descTH = "";
                string descEN = "";

                descTH = Rmenu["FTCmpCode"].ToString() ;
                descTH += Constants.vbCrLf + Constants.vbCrLf + Rmenu["FTCmpNameTH"].ToString();

                descEN = Rmenu["FTCmpCode"].ToString() ;
                descEN += Constants.vbCrLf + Constants.vbCrLf + Rmenu["FTCmpNameEN"].ToString();

                element.Text = ((HI.ST.Lang.Language == HI.ST.Lang.eLang.TH) ? descTH : descEN);

                item.AppearanceItem.Normal.BackColor = Color.MediumAquamarine;
                //try
                //{
                //    if (Rmenu["FPCmpImage"] != null)
                //    {
                //        try
                //        {                                         
                //            element.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rmenu["FPCmpImage"]);
                //            element.ImageScaleMode = TileItemImageScaleMode.Stretch;
                //            element.Appearance.Normal.Image = element.Image;                       
                //            element.Appearance.Hovered.Image = element.Image;
                //            element.Appearance.Pressed.Image = element.Image;
                //            element.Appearance.Selected.Image = element.Image;
                //        }
                //        catch { }
                //    }//Image
                //}
                //catch { }


                element.ImageAlignment = TileItemContentAlignment.MiddleCenter;

                    item.Elements.Add(element);
                    group.Items.Add(item);

                     item.Tag = new HI.ST.Tag.TagCmpData()
                     {
                        CmpCode = Rmenu["FTCmpCode"].ToString(),
                        CmpID = int.Parse(Rmenu["FNHSysCmpId"].ToString()),
                        DocRun = Rmenu["FTDocRun"].ToString(),
                        LangEN = Rmenu["FTCmpNameEN"].ToString(),
                        LangTH = Rmenu["FTCmpNameTH"].ToString(),
              

                     };
                 

               }

        }

        public static Color GetBackColor(int record)
        {
            switch (record)
            {
                case 1:
                    return Color.FromArgb(0x00, 0x87, 0x9C);
                case 2:
                    return Color.FromArgb(0xCC, 0x6D, 0x00);
                //case 3:
                //    return Color.FromArgb(0x40, 0x40, 0x40);
                case 3:
                    return Color.FromArgb(0x00, 0x73, 0xC4);
                case 4:
                    return Color.FromArgb(0x3E, 0x70, 0x38);
                    //32, 0, 64

            }
            return Color.FromArgb(0x40, 0x40, 0x40);
        }


        public TileItem SelecttileItem { get; set; }
        public void cleartextsearch()
        {

            //seachcontrol.Text = "";
        }

        DataTable GetCompany()
        {
            DataTable dt = null;
            string cmd = "";

            cmd = "SELECT  A.FNHSysCmpId, A.FTCmpCode, A.FTCmpNameTH, A.FTCmpNameEN, A.FPCmpImage, A.FTDocRun ";
            cmd += Constants.vbCrLf + " FROM    [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS A With(NOLOCK) ";

            if (!HI.ST.SysInfo.Admin)
            {

                cmd += Constants.vbCrLf + " INNER JOIN (";
                cmd += Constants.vbCrLf + "  SELECT  DISTINCT  B.FNHSysCmpId";
                cmd += Constants.vbCrLf + " FROM            [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A With(NOLOCK)  INNER JOIN ";
                cmd += Constants.vbCrLf + "    [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS B With(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID ";
                cmd += Constants.vbCrLf + " WHERE  A.FTUserName='" + HI.UL.ULF.rpQuoted (HI.ST.UserInfo.UserName ) + "' ";
                cmd += Constants.vbCrLf + "   ) AS B";
                cmd += Constants.vbCrLf + " ON A.FNHSysCmpId = B.FNHSysCmpId";

            }

            cmd += Constants.vbCrLf + " WHERE ISNULL(A.FTStateActive,'0')='1'";
            cmd += Constants.vbCrLf + " ORDER BY A.FTCmpCode ";

            dt = HI.Conn.SQLConn.GetDataTable (cmd, HI.Conn.DB.DataBaseName.DB_SECURITY);

            return dt;

        }

        private void seachcontrol_EditValueChanged(object sender, EventArgs e)
        {
            FilterTileControl();
        }
        private void FilterTileControl()
        {
            foreach (TileGroup group in tileControl1.Groups)
            {
                foreach (TileItem tileItem in group.Items)
                {
                    tileItem.Visible = CheckFilterItem(tileItem);
                }
            }
        }
        private bool CheckFilterItem(TileItem tileItem)
        {
            var filterText = "";// seachcontrol.Text;
            if (string.IsNullOrEmpty(filterText))
                return true;
            foreach (TileItemElement element in tileItem.Elements)
            {
                if (element.Text.ToLower().Contains(filterText))
                    return true;
            }
            return false;
        }

        private void tileControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Escape:

                    SelecttileItem = null;
                    this.Close();
                    break;
                default:
                    break;  

            }
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {

            if (e.Item != null) {
                SelecttileItem = e.Item;
                this.Close();
            }
            
        }

        private void ocmexit_Click(object sender, EventArgs e)
        {
            SelecttileItem = null;
            this.Close();
        }
    }
}
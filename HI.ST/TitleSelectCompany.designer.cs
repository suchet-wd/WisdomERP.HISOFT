namespace HI.ST
{
    partial class TitleSelectCompany
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleSelectCompany));
            this.ocmexit = new DevExpress.XtraEditors.SimpleButton();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.SuspendLayout();
            // 
            // ocmexit
            // 
            this.ocmexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ocmexit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ocmexit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ocmexit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ocmexit.ImageOptions.Image")));
            this.ocmexit.Location = new System.Drawing.Point(1454, 24);
            this.ocmexit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ocmexit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ocmexit.Name = "ocmexit";
            this.ocmexit.Size = new System.Drawing.Size(33, 30);
            this.ocmexit.TabIndex = 3;
            this.ocmexit.Click += new System.EventHandler(this.ocmexit_Click);
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowDragTilesBetweenGroups = false;
            this.tileControl1.AllowItemHover = true;
            this.tileControl1.AppearanceGroupText.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceGroupText.ForeColor = System.Drawing.Color.White;
            this.tileControl1.AppearanceGroupText.Options.UseFont = true;
            this.tileControl1.AppearanceGroupText.Options.UseForeColor = true;
            this.tileControl1.AppearanceItem.Hovered.BorderColor = System.Drawing.Color.Yellow;
            this.tileControl1.AppearanceItem.Hovered.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceItem.Hovered.ForeColor = System.Drawing.Color.Blue;
            this.tileControl1.AppearanceItem.Hovered.Options.UseBorderColor = true;
            this.tileControl1.AppearanceItem.Hovered.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Hovered.Options.UseForeColor = true;
            this.tileControl1.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Transparent;
            this.tileControl1.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceItem.Normal.ForeColor = System.Drawing.Color.White;
            this.tileControl1.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseForeColor = true;
            this.tileControl1.AppearanceItem.Pressed.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceItem.Pressed.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Selected.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceItem.Selected.Options.UseFont = true;
            this.tileControl1.AppearanceText.Font = new System.Drawing.Font("Rockwell", 25F, System.Drawing.FontStyle.Bold);
            this.tileControl1.AppearanceText.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.tileControl1.AppearanceText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tileControl1.AppearanceText.Options.UseFont = true;
            this.tileControl1.AppearanceText.Options.UseForeColor = true;
            this.tileControl1.BackColor = System.Drawing.Color.Silver;
            this.tileControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tileControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tileControl1.ItemSize = 220;
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Padding = new System.Windows.Forms.Padding(21, 8, 21, 18);
            this.tileControl1.RowCount = 9;
            this.tileControl1.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollButtons;
            this.tileControl1.ShowGroupText = true;
            this.tileControl1.ShowText = true;
            this.tileControl1.Size = new System.Drawing.Size(1507, 767);
            this.tileControl1.TabIndex = 1;
            this.tileControl1.Tag = "WISDOM ERP SYSTEM (Press Esc to Exit)";
            this.tileControl1.Text = "WISDOM ERP SYSTEM (Press Esc to Exit)";
            this.tileControl1.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Top;
            this.tileControl1.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileControl1_ItemClick);
            this.tileControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tileControl1_KeyDown);
            // 
            // TitleSelectCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 767);
            this.ControlBox = false;
            this.Controls.Add(this.ocmexit);
            this.Controls.Add(this.tileControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("TitleSelectCompany.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TitleSelectCompany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TitleMenu";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        //public DK.ST.Form.TileControlPT tileControl1;
        public DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.SimpleButton ocmexit;
    }
}
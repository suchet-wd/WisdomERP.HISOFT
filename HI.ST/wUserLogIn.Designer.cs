namespace HI.ST
{
    partial class wUserLogIn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wUserLogIn));
            this.FNLang = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ImgLang = new System.Windows.Forms.ImageList(this.components);
            this.otbPassword = new DevExpress.XtraEditors.TextEdit();
            this.otbLogin = new DevExpress.XtraEditors.TextEdit();
            this.opmexit = new DevExpress.XtraEditors.PictureEdit();
            this.FNHSysCmpId = new DevExpress.XtraEditors.ComboBoxEdit();
            this.FNHSysCmpId_lbl = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.FNLang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otbPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otbLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opmexit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysCmpId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // FNLang
            // 
            this.FNLang.Location = new System.Drawing.Point(115, 377);
            this.FNLang.Name = "FNLang";
            this.FNLang.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.FNLang.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.FNLang.Properties.Appearance.Options.UseBackColor = true;
            this.FNLang.Properties.Appearance.Options.UseForeColor = true;
            this.FNLang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FNLang.Properties.LargeImages = this.ImgLang;
            this.FNLang.Size = new System.Drawing.Size(136, 20);
            this.FNLang.TabIndex = 17;
            this.FNLang.SelectedIndexChanged += new System.EventHandler(this.FNLang_SelectedIndexChanged);
            this.FNLang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.otbLogin_KeyDown);
            // 
            // ImgLang
            // 
            this.ImgLang.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgLang.ImageStream")));
            this.ImgLang.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgLang.Images.SetKeyName(0, "English.jpg");
            this.ImgLang.Images.SetKeyName(1, "Thai.jpg");
            this.ImgLang.Images.SetKeyName(2, "Vitenam.gif");
            this.ImgLang.Images.SetKeyName(3, "KM.jpg");
            this.ImgLang.Images.SetKeyName(4, "Myanmar.jpg");
            this.ImgLang.Images.SetKeyName(5, "laos.jpg");
            this.ImgLang.Images.SetKeyName(6, "China.jpg");
            // 
            // otbPassword
            // 
            this.otbPassword.EditValue = "";
            this.otbPassword.Location = new System.Drawing.Point(114, 333);
            this.otbPassword.Name = "otbPassword";
            this.otbPassword.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.otbPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.otbPassword.Properties.Appearance.Options.UseBackColor = true;
            this.otbPassword.Properties.Appearance.Options.UseForeColor = true;
            this.otbPassword.Properties.MaxLength = 20;
            this.otbPassword.Properties.PasswordChar = '*';
            this.otbPassword.Size = new System.Drawing.Size(136, 20);
            this.otbPassword.TabIndex = 16;
            this.otbPassword.EditValueChanged += new System.EventHandler(this.otbPassword_EditValueChanged);
            this.otbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.otbLogin_KeyDown);
            // 
            // otbLogin
            // 
            this.otbLogin.EditValue = "";
            this.otbLogin.Location = new System.Drawing.Point(114, 311);
            this.otbLogin.Name = "otbLogin";
            this.otbLogin.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.otbLogin.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.otbLogin.Properties.Appearance.Options.UseBackColor = true;
            this.otbLogin.Properties.Appearance.Options.UseForeColor = true;
            this.otbLogin.Properties.MaxLength = 50;
            this.otbLogin.Size = new System.Drawing.Size(136, 20);
            this.otbLogin.TabIndex = 15;
            this.otbLogin.EditValueChanged += new System.EventHandler(this.otbLogin_EditValueChanged);
            this.otbLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.otbLogin_KeyDown);
            // 
            // opmexit
            // 
            this.opmexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opmexit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.opmexit.EditValue = ((object)(resources.GetObject("opmexit.EditValue")));
            this.opmexit.Location = new System.Drawing.Point(277, 321);
            this.opmexit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.opmexit.Name = "opmexit";
            this.opmexit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.opmexit.Properties.Appearance.Options.UseBackColor = true;
            this.opmexit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.opmexit.Properties.ReadOnly = true;
            this.opmexit.Properties.ShowMenu = false;
            this.opmexit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.opmexit.Size = new System.Drawing.Size(27, 20);
            this.opmexit.TabIndex = 23;
            this.opmexit.Visible = false;
            this.opmexit.Click += new System.EventHandler(this.opmexit_Click);
            // 
            // FNHSysCmpId
            // 
            this.FNHSysCmpId.EditValue = "";
            this.FNHSysCmpId.EnterMoveNextControl = true;
            this.FNHSysCmpId.Location = new System.Drawing.Point(115, 355);
            this.FNHSysCmpId.Name = "FNHSysCmpId";
            this.FNHSysCmpId.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.FNHSysCmpId.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.FNHSysCmpId.Properties.Appearance.Options.UseBackColor = true;
            this.FNHSysCmpId.Properties.Appearance.Options.UseForeColor = true;
            this.FNHSysCmpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan;
            this.FNHSysCmpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue;
            this.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.FNHSysCmpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow;
            this.FNHSysCmpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue;
            this.FNHSysCmpId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.FNHSysCmpId.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan;
            this.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue;
            this.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.FNHSysCmpId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FNHSysCmpId.Properties.Tag = "FNPoState";
            this.FNHSysCmpId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.FNHSysCmpId.Size = new System.Drawing.Size(136, 20);
            this.FNHSysCmpId.TabIndex = 22;
            this.FNHSysCmpId.Tag = "2|";
            this.FNHSysCmpId.SelectedIndexChanged += new System.EventHandler(this.FNHSysCmpId_SelectedIndexChanged);
            this.FNHSysCmpId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.otbLogin_KeyDown);
            // 
            // FNHSysCmpId_lbl
            // 
            this.FNHSysCmpId_lbl.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FNHSysCmpId_lbl.Appearance.Options.UseFont = true;
            this.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = true;
            this.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = true;
            this.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.FNHSysCmpId_lbl.Location = new System.Drawing.Point(7, 356);
            this.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl";
            this.FNHSysCmpId_lbl.Size = new System.Drawing.Size(104, 15);
            this.FNHSysCmpId_lbl.TabIndex = 21;
            this.FNHSysCmpId_lbl.Text = "Company :";
            // 
            // LabelControl3
            // 
            this.LabelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LabelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LabelControl3.Appearance.Options.UseFont = true;
            this.LabelControl3.Appearance.Options.UseForeColor = true;
            this.LabelControl3.Appearance.Options.UseTextOptions = true;
            this.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LabelControl3.Location = new System.Drawing.Point(7, 375);
            this.LabelControl3.Name = "LabelControl3";
            this.LabelControl3.Size = new System.Drawing.Size(104, 19);
            this.LabelControl3.TabIndex = 20;
            this.LabelControl3.Text = "Language :";
            // 
            // LabelControl2
            // 
            this.LabelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LabelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LabelControl2.Appearance.Options.UseFont = true;
            this.LabelControl2.Appearance.Options.UseForeColor = true;
            this.LabelControl2.Appearance.Options.UseTextOptions = true;
            this.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LabelControl2.Location = new System.Drawing.Point(7, 332);
            this.LabelControl2.Name = "LabelControl2";
            this.LabelControl2.Size = new System.Drawing.Size(104, 18);
            this.LabelControl2.TabIndex = 19;
            this.LabelControl2.Text = "Password :";
            // 
            // LabelControl1
            // 
            this.LabelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LabelControl1.Appearance.Options.UseFont = true;
            this.LabelControl1.Appearance.Options.UseForeColor = true;
            this.LabelControl1.Appearance.Options.UseTextOptions = true;
            this.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LabelControl1.Location = new System.Drawing.Point(7, 310);
            this.LabelControl1.Name = "LabelControl1";
            this.LabelControl1.Size = new System.Drawing.Size(104, 15);
            this.LabelControl1.TabIndex = 18;
            this.LabelControl1.Text = "User Name :";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(316, 307);
            this.pictureEdit1.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl4.Location = new System.Drawing.Point(13, 415);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(305, 11);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Copyright © 2015, Wisdom ERP Co., Ltd. All rights reserved. ";
            // 
            // wUserLogIn
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 431);
            this.ControlBox = false;
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.FNLang);
            this.Controls.Add(this.otbPassword);
            this.Controls.Add(this.otbLogin);
            this.Controls.Add(this.opmexit);
            this.Controls.Add(this.FNHSysCmpId);
            this.Controls.Add(this.FNHSysCmpId_lbl);
            this.Controls.Add(this.LabelControl3);
            this.Controls.Add(this.LabelControl2);
            this.Controls.Add(this.LabelControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("wUserLogIn.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "wUserLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User LogIn";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.otbLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.FNLang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otbPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otbLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opmexit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysCmpId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevExpress.XtraEditors.ImageComboBoxEdit FNLang;
        internal System.Windows.Forms.ImageList ImgLang;
        internal DevExpress.XtraEditors.TextEdit otbPassword;
        internal DevExpress.XtraEditors.TextEdit otbLogin;
        internal DevExpress.XtraEditors.PictureEdit opmexit;
        internal DevExpress.XtraEditors.ComboBoxEdit FNHSysCmpId;
        internal DevExpress.XtraEditors.LabelControl FNHSysCmpId_lbl;
        internal DevExpress.XtraEditors.LabelControl LabelControl3;
        internal DevExpress.XtraEditors.LabelControl LabelControl2;
        internal DevExpress.XtraEditors.LabelControl LabelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
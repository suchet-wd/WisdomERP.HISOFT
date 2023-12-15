
namespace HI.Track
{
    partial class wTrackProdLT
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
            this.FNHSysSeasonIdTo = new DevExpress.XtraEditors.ButtonEdit();
            this.ogbmainprocbutton = new DevExpress.XtraEditors.PanelControl();
            this.ocmExit = new DevExpress.XtraEditors.SimpleButton();
            this.ocmLoadData = new DevExpress.XtraEditors.SimpleButton();
            this.ocmClear = new DevExpress.XtraEditors.SimpleButton();
            this.ogc = new DevExpress.XtraGrid.GridControl();
            this.ogv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FTSeason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTStyleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FNHSysSeasonId = new DevExpress.XtraEditors.ButtonEdit();
            this.FNHSysStyleIdTo = new DevExpress.XtraEditors.ButtonEdit();
            this.FNHSysStyleId = new DevExpress.XtraEditors.ButtonEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysSeasonIdTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).BeginInit();
            this.ogbmainprocbutton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysSeasonId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysStyleIdTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysStyleId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FNHSysSeasonIdTo
            // 
            this.FNHSysSeasonIdTo.Location = new System.Drawing.Point(367, 55);
            this.FNHSysSeasonIdTo.Name = "FNHSysSeasonIdTo";
            this.FNHSysSeasonIdTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FNHSysSeasonIdTo.Size = new System.Drawing.Size(143, 20);
            this.FNHSysSeasonIdTo.TabIndex = 9;
            // 
            // ogbmainprocbutton
            // 
            this.ogbmainprocbutton.Controls.Add(this.ocmExit);
            this.ogbmainprocbutton.Controls.Add(this.ocmLoadData);
            this.ogbmainprocbutton.Controls.Add(this.ocmClear);
            this.ogbmainprocbutton.Location = new System.Drawing.Point(2, 549);
            this.ogbmainprocbutton.Name = "ogbmainprocbutton";
            this.ogbmainprocbutton.Size = new System.Drawing.Size(1026, 31);
            this.ogbmainprocbutton.TabIndex = 13;
            // 
            // ocmExit
            // 
            this.ocmExit.Location = new System.Drawing.Point(167, 5);
            this.ocmExit.Name = "ocmExit";
            this.ocmExit.Size = new System.Drawing.Size(75, 23);
            this.ocmExit.TabIndex = 2;
            this.ocmExit.Text = "Exit";
            this.ocmExit.Click += new System.EventHandler(this.ocmExit_Click);
            // 
            // ocmLoadData
            // 
            this.ocmLoadData.Location = new System.Drawing.Point(5, 5);
            this.ocmLoadData.Name = "ocmLoadData";
            this.ocmLoadData.Size = new System.Drawing.Size(75, 23);
            this.ocmLoadData.TabIndex = 0;
            this.ocmLoadData.Text = "Load Data";
            this.ocmLoadData.Click += new System.EventHandler(this.ocmLoadData_Click);
            // 
            // ocmClear
            // 
            this.ocmClear.Location = new System.Drawing.Point(86, 5);
            this.ocmClear.Name = "ocmClear";
            this.ocmClear.Size = new System.Drawing.Size(75, 23);
            this.ocmClear.TabIndex = 1;
            this.ocmClear.Text = "Clear";
            this.ocmClear.Click += new System.EventHandler(this.ocmClear_Click);
            // 
            // ogc
            // 
            this.ogc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogc.Location = new System.Drawing.Point(2, 23);
            this.ogc.MainView = this.ogv;
            this.ogc.Name = "ogc";
            this.ogc.Size = new System.Drawing.Size(1042, 480);
            this.ogc.TabIndex = 0;
            this.ogc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ogv});
            // 
            // ogv
            // 
            this.ogv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FTSeason,
            this.FTStyleCode});
            this.ogv.GridControl = this.ogc;
            this.ogv.Name = "ogv";
            // 
            // FTSeason
            // 
            this.FTSeason.Caption = "FTSeason";
            this.FTSeason.FieldName = "FTSeason";
            this.FTSeason.Name = "FTSeason";
            this.FTSeason.Visible = true;
            this.FTSeason.VisibleIndex = 0;
            // 
            // FTStyleCode
            // 
            this.FTStyleCode.Caption = "FTStyleCode";
            this.FTStyleCode.FieldName = "FTStyleCode";
            this.FTStyleCode.Name = "FTStyleCode";
            this.FTStyleCode.Visible = true;
            this.FTStyleCode.VisibleIndex = 1;
            // 
            // FNHSysSeasonId
            // 
            this.FNHSysSeasonId.Location = new System.Drawing.Point(99, 53);
            this.FNHSysSeasonId.Name = "FNHSysSeasonId";
            this.FNHSysSeasonId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FNHSysSeasonId.Size = new System.Drawing.Size(143, 20);
            this.FNHSysSeasonId.TabIndex = 8;
            // 
            // FNHSysStyleIdTo
            // 
            this.FNHSysStyleIdTo.Location = new System.Drawing.Point(367, 29);
            this.FNHSysStyleIdTo.Name = "FNHSysStyleIdTo";
            this.FNHSysStyleIdTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FNHSysStyleIdTo.Size = new System.Drawing.Size(143, 20);
            this.FNHSysStyleIdTo.TabIndex = 7;
            // 
            // FNHSysStyleId
            // 
            this.FNHSysStyleId.Location = new System.Drawing.Point(99, 29);
            this.FNHSysStyleId.Name = "FNHSysStyleId";
            this.FNHSysStyleId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FNHSysStyleId.Size = new System.Drawing.Size(143, 20);
            this.FNHSysStyleId.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Controls.Add(this.groupControl3);
            this.groupControl1.Controls.Add(this.ogbmainprocbutton);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1050, 602);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Root";
            // 
            // groupControl2
            // 
            this.groupControl2.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl2.Controls.Add(this.ogc);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 95);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1046, 505);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Detail";
            // 
            // groupControl3
            // 
            this.groupControl3.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.FNHSysSeasonId);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.FNHSysStyleId);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.FNHSysStyleIdTo);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.FNHSysSeasonIdTo);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(2, 2);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1046, 93);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Criteria";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "FNHSysSeasonId";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "FNHSysStyleId";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(264, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(83, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "FNHSysStyleIdTo";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(264, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(94, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "FNHSysSeasonIdTo";
            // 
            // wTrackProdLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 602);
            this.Controls.Add(this.groupControl1);
            this.Name = "wTrackProdLT";
            this.Text = "Production Leadtime Tracking";
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysSeasonIdTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).EndInit();
            this.ogbmainprocbutton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ogc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysSeasonId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysStyleIdTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FNHSysStyleId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ButtonEdit FNHSysStyleId;
        private DevExpress.XtraEditors.ButtonEdit FNHSysSeasonIdTo;
        private DevExpress.XtraEditors.ButtonEdit FNHSysSeasonId;
        private DevExpress.XtraEditors.ButtonEdit FNHSysStyleIdTo;
        private DevExpress.XtraGrid.GridControl ogc;
        private DevExpress.XtraGrid.Views.Grid.GridView ogv;
        private DevExpress.XtraGrid.Columns.GridColumn FTSeason;
        private DevExpress.XtraGrid.Columns.GridColumn FTStyleCode;
        private DevExpress.XtraEditors.SimpleButton ocmExit;
        private DevExpress.XtraEditors.SimpleButton ocmClear;
        private DevExpress.XtraEditors.SimpleButton ocmLoadData;
        private DevExpress.XtraEditors.PanelControl ogbmainprocbutton;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}

namespace HI.Track
{
    partial class wTracking
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
            this.ogcCriteria = new DevExpress.XtraEditors.GroupControl();
            this.ogbDetail = new DevExpress.XtraEditors.GroupControl();
            this.ogbmainprocbutton = new DevExpress.XtraEditors.PanelControl();
            this.ocmLoadData = new DevExpress.XtraEditors.SimpleButton();
            this.ocmClear = new DevExpress.XtraEditors.SimpleButton();
            this.ocmExit = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ogcCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbDetail)).BeginInit();
            this.ogbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).BeginInit();
            this.ogbmainprocbutton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ogcCriteria
            // 
            this.ogcCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.ogcCriteria.Location = new System.Drawing.Point(0, 0);
            this.ogcCriteria.Name = "ogcCriteria";
            this.ogcCriteria.Size = new System.Drawing.Size(842, 117);
            this.ogcCriteria.TabIndex = 0;
            this.ogcCriteria.Text = "Criteria";
            // 
            // ogbDetail
            // 
            this.ogbDetail.Controls.Add(this.gridControl1);
            this.ogbDetail.Controls.Add(this.ogbmainprocbutton);
            this.ogbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogbDetail.Location = new System.Drawing.Point(0, 117);
            this.ogbDetail.Name = "ogbDetail";
            this.ogbDetail.Size = new System.Drawing.Size(842, 419);
            this.ogbDetail.TabIndex = 1;
            this.ogbDetail.Text = "Detail";
            // 
            // ogbmainprocbutton
            // 
            this.ogbmainprocbutton.Controls.Add(this.ocmExit);
            this.ogbmainprocbutton.Controls.Add(this.ocmClear);
            this.ogbmainprocbutton.Controls.Add(this.ocmLoadData);
            this.ogbmainprocbutton.Location = new System.Drawing.Point(12, 176);
            this.ogbmainprocbutton.Name = "ogbmainprocbutton";
            this.ogbmainprocbutton.Size = new System.Drawing.Size(92, 100);
            this.ogbmainprocbutton.TabIndex = 0;
            // 
            // ocmLoadData
            // 
            this.ocmLoadData.Location = new System.Drawing.Point(6, 6);
            this.ocmLoadData.Name = "ocmLoadData";
            this.ocmLoadData.Size = new System.Drawing.Size(75, 23);
            this.ocmLoadData.TabIndex = 0;
            this.ocmLoadData.Text = "Load Data";
            // 
            // ocmClear
            // 
            this.ocmClear.Location = new System.Drawing.Point(6, 36);
            this.ocmClear.Name = "ocmClear";
            this.ocmClear.Size = new System.Drawing.Size(75, 23);
            this.ocmClear.TabIndex = 1;
            this.ocmClear.Text = "Clear";
            this.ocmClear.Click += new System.EventHandler(this.ocmClear_Click);
            // 
            // ocmExit
            // 
            this.ocmExit.Location = new System.Drawing.Point(6, 66);
            this.ocmExit.Name = "ocmExit";
            this.ocmExit.Size = new System.Drawing.Size(75, 23);
            this.ocmExit.TabIndex = 2;
            this.ocmExit.Text = "Exit";
            this.ocmExit.Click += new System.EventHandler(this.ocmExit_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(838, 394);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // wTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 536);
            this.Controls.Add(this.ogbDetail);
            this.Controls.Add(this.ogcCriteria);
            this.Name = "wTracking";
            this.Text = "wTracking";
            ((System.ComponentModel.ISupportInitialize)(this.ogcCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbDetail)).EndInit();
            this.ogbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).EndInit();
            this.ogbmainprocbutton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl ogcCriteria;
        private DevExpress.XtraEditors.GroupControl ogbDetail;
        private DevExpress.XtraEditors.PanelControl ogbmainprocbutton;
        private DevExpress.XtraEditors.SimpleButton ocmExit;
        private DevExpress.XtraEditors.SimpleButton ocmClear;
        private DevExpress.XtraEditors.SimpleButton ocmLoadData;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
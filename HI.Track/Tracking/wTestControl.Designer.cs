
namespace HI.Track
{
    partial class wTestControl
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
            this.ogbmainprocbutton = new DevExpress.XtraEditors.PanelControl();
            this.ocmExit = new DevExpress.XtraEditors.SimpleButton();
            this.ocmClear = new DevExpress.XtraEditors.SimpleButton();
            this.ocmLoadData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).BeginInit();
            this.ogbmainprocbutton.SuspendLayout();
            this.SuspendLayout();
            // 
            // ogbmainprocbutton
            // 
            this.ogbmainprocbutton.Controls.Add(this.ocmLoadData);
            this.ogbmainprocbutton.Controls.Add(this.ocmExit);
            this.ogbmainprocbutton.Controls.Add(this.ocmClear);
            this.ogbmainprocbutton.Location = new System.Drawing.Point(12, 358);
            this.ogbmainprocbutton.Name = "ogbmainprocbutton";
            this.ogbmainprocbutton.Size = new System.Drawing.Size(86, 101);
            this.ogbmainprocbutton.TabIndex = 1;
            // 
            // ocmExit
            // 
            this.ocmExit.Location = new System.Drawing.Point(5, 34);
            this.ocmExit.Name = "ocmExit";
            this.ocmExit.Size = new System.Drawing.Size(75, 23);
            this.ocmExit.TabIndex = 1;
            this.ocmExit.Text = "Exit";
            this.ocmExit.Click += new System.EventHandler(this.ocmExit_Click);
            // 
            // ocmClear
            // 
            this.ocmClear.Location = new System.Drawing.Point(5, 5);
            this.ocmClear.Name = "ocmClear";
            this.ocmClear.Size = new System.Drawing.Size(75, 23);
            this.ocmClear.TabIndex = 0;
            this.ocmClear.Text = "Clear";
            this.ocmClear.Click += new System.EventHandler(this.ocmClear_Click);
            // 
            // ocmLoadData
            // 
            this.ocmLoadData.Location = new System.Drawing.Point(6, 64);
            this.ocmLoadData.Name = "ocmLoadData";
            this.ocmLoadData.Size = new System.Drawing.Size(75, 23);
            this.ocmLoadData.TabIndex = 2;
            this.ocmLoadData.Text = "Load Data";
            this.ocmLoadData.UseVisualStyleBackColor = true;
            this.ocmLoadData.Click += new System.EventHandler(this.ocmLoadData_Click);
            // 
            // wTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 471);
            this.Controls.Add(this.ogbmainprocbutton);
            this.Name = "wTestControl";
            this.Text = "wTestControl";
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).EndInit();
            this.ogbmainprocbutton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl ogbmainprocbutton;
        private DevExpress.XtraEditors.SimpleButton ocmExit;
        private DevExpress.XtraEditors.SimpleButton ocmClear;
        private System.Windows.Forms.Button ocmLoadData;
    }
}
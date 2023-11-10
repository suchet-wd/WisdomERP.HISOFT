
namespace HI.Track
{
    partial class wEmpListingTracking
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
            this.chkPermission = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowActive = new DevExpress.XtraEditors.CheckEdit();
            this.ogbDetail = new DevExpress.XtraEditors.GroupControl();
            this.ogbmainprocbutton = new DevExpress.XtraEditors.PanelControl();
            this.ocmLoadData = new DevExpress.XtraEditors.SimpleButton();
            this.ocmExit = new DevExpress.XtraEditors.SimpleButton();
            this.ocmClear = new DevExpress.XtraEditors.SimpleButton();
            this.ogcDetail = new DevExpress.XtraGrid.GridControl();
            this.ogvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTCmpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FNEmpStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTStateActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FNHSysEmpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FDDateStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FDDateEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTDeptDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTDivisonName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FTUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ogcCriteria)).BeginInit();
            this.ogcCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermission.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbDetail)).BeginInit();
            this.ogbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).BeginInit();
            this.ogbmainprocbutton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogcDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // ogcCriteria
            // 
            this.ogcCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ogcCriteria.Controls.Add(this.chkPermission);
            this.ogcCriteria.Controls.Add(this.chkShowActive);
            this.ogcCriteria.Location = new System.Drawing.Point(6, 12);
            this.ogcCriteria.Name = "ogcCriteria";
            this.ogcCriteria.Size = new System.Drawing.Size(1472, 70);
            this.ogcCriteria.TabIndex = 0;
            this.ogcCriteria.Text = "Employee Listing Criteria";
            // 
            // chkPermission
            // 
            this.chkPermission.Location = new System.Drawing.Point(25, 50);
            this.chkPermission.Name = "chkPermission";
            this.chkPermission.Properties.Caption = "Show Permission";
            this.chkPermission.Size = new System.Drawing.Size(127, 20);
            this.chkPermission.TabIndex = 1;
            // 
            // chkShowActive
            // 
            this.chkShowActive.Location = new System.Drawing.Point(25, 27);
            this.chkShowActive.Name = "chkShowActive";
            this.chkShowActive.Properties.Caption = "Show Active Only";
            this.chkShowActive.Size = new System.Drawing.Size(127, 20);
            this.chkShowActive.TabIndex = 0;
            // 
            // ogbDetail
            // 
            this.ogbDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ogbDetail.Controls.Add(this.ogbmainprocbutton);
            this.ogbDetail.Controls.Add(this.ogcDetail);
            this.ogbDetail.Location = new System.Drawing.Point(6, 88);
            this.ogbDetail.Name = "ogbDetail";
            this.ogbDetail.Size = new System.Drawing.Size(1472, 544);
            this.ogbDetail.TabIndex = 1;
            this.ogbDetail.Text = "Employee Listing Detail";
            // 
            // ogbmainprocbutton
            // 
            this.ogbmainprocbutton.Controls.Add(this.ocmLoadData);
            this.ogbmainprocbutton.Controls.Add(this.ocmExit);
            this.ogbmainprocbutton.Controls.Add(this.ocmClear);
            this.ogbmainprocbutton.Location = new System.Drawing.Point(25, 423);
            this.ogbmainprocbutton.Name = "ogbmainprocbutton";
            this.ogbmainprocbutton.Size = new System.Drawing.Size(88, 101);
            this.ogbmainprocbutton.TabIndex = 0;
            // 
            // ocmLoadData
            // 
            this.ocmLoadData.Location = new System.Drawing.Point(6, 6);
            this.ocmLoadData.Name = "ocmLoadData";
            this.ocmLoadData.Size = new System.Drawing.Size(75, 23);
            this.ocmLoadData.TabIndex = 0;
            this.ocmLoadData.Tag = "2|";
            this.ocmLoadData.Text = "Load Data";
            this.ocmLoadData.Click += new System.EventHandler(this.ocmLoadData_Click);
            // 
            // ocmExit
            // 
            this.ocmExit.Location = new System.Drawing.Point(5, 64);
            this.ocmExit.Name = "ocmExit";
            this.ocmExit.Size = new System.Drawing.Size(75, 23);
            this.ocmExit.TabIndex = 2;
            this.ocmExit.Tag = "2|";
            this.ocmExit.Text = "Exit";
            this.ocmExit.Click += new System.EventHandler(this.ocmExit_Click);
            // 
            // ocmClear
            // 
            this.ocmClear.Location = new System.Drawing.Point(6, 35);
            this.ocmClear.Name = "ocmClear";
            this.ocmClear.Size = new System.Drawing.Size(75, 23);
            this.ocmClear.TabIndex = 1;
            this.ocmClear.Tag = "2|";
            this.ocmClear.Text = "Clear";
            this.ocmClear.Click += new System.EventHandler(this.ocmClear_Click);
            // 
            // ogcDetail
            // 
            this.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogcDetail.Location = new System.Drawing.Point(2, 23);
            this.ogcDetail.MainView = this.ogvDetail;
            this.ogcDetail.Name = "ogcDetail";
            this.ogcDetail.Size = new System.Drawing.Size(1468, 519);
            this.ogcDetail.TabIndex = 0;
            this.ogcDetail.TabStop = false;
            this.ogcDetail.Tag = "2|";
            this.ogcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ogvDetail});
            // 
            // ogvDetail
            // 
            this.ogvDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Number,
            this.FTCmpCode,
            this.FNEmpStatus,
            this.FTStateActive,
            this.EmpName,
            this.FNHSysEmpID,
            this.EmpCode,
            this.FTDeptDesc,
            this.FTDivisonName,
            this.FTUserName,
            this.FDDateStart,
            this.FDDateEnd});
            this.ogvDetail.CustomizationFormBounds = new System.Drawing.Rectangle(759, 412, 252, 266);
            this.ogvDetail.GridControl = this.ogcDetail;
            this.ogvDetail.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.ogvDetail.Name = "ogvDetail";
            this.ogvDetail.OptionsView.ColumnAutoWidth = false;
            this.ogvDetail.OptionsView.ShowGroupPanel = false;
            // 
            // Number
            // 
            this.Number.AppearanceCell.Options.UseTextOptions = true;
            this.Number.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Number.AppearanceHeader.Options.UseTextOptions = true;
            this.Number.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Number.Caption = "Number";
            this.Number.FieldName = "Seq";
            this.Number.Name = "Number";
            this.Number.OptionsColumn.AllowEdit = false;
            this.Number.Visible = true;
            this.Number.VisibleIndex = 0;
            // 
            // FTCmpCode
            // 
            this.FTCmpCode.AppearanceCell.Options.UseTextOptions = true;
            this.FTCmpCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTCmpCode.AppearanceHeader.Options.UseTextOptions = true;
            this.FTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTCmpCode.Caption = "FTCmpCode";
            this.FTCmpCode.FieldName = "FTCmpCode";
            this.FTCmpCode.Name = "FTCmpCode";
            this.FTCmpCode.OptionsColumn.AllowEdit = false;
            this.FTCmpCode.Visible = true;
            this.FTCmpCode.VisibleIndex = 3;
            this.FTCmpCode.Width = 79;
            // 
            // FNEmpStatus
            // 
            this.FNEmpStatus.AppearanceCell.Options.UseTextOptions = true;
            this.FNEmpStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FNEmpStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.FNEmpStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FNEmpStatus.Caption = "FNEmpStatus";
            this.FNEmpStatus.FieldName = "FNEmpStatus";
            this.FNEmpStatus.Name = "FNEmpStatus";
            this.FNEmpStatus.OptionsColumn.AllowEdit = false;
            this.FNEmpStatus.Visible = true;
            this.FNEmpStatus.VisibleIndex = 6;
            this.FNEmpStatus.Width = 135;
            // 
            // FTStateActive
            // 
            this.FTStateActive.AppearanceCell.Options.UseTextOptions = true;
            this.FTStateActive.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTStateActive.AppearanceHeader.Options.UseTextOptions = true;
            this.FTStateActive.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTStateActive.Caption = "FTStateActive";
            this.FTStateActive.FieldName = "FTStateActive";
            this.FTStateActive.Name = "FTStateActive";
            this.FTStateActive.OptionsColumn.AllowEdit = false;
            this.FTStateActive.Visible = true;
            this.FTStateActive.VisibleIndex = 2;
            this.FTStateActive.Width = 153;
            // 
            // FNHSysEmpID
            // 
            this.FNHSysEmpID.AppearanceCell.Options.UseTextOptions = true;
            this.FNHSysEmpID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FNHSysEmpID.AppearanceHeader.Options.UseTextOptions = true;
            this.FNHSysEmpID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FNHSysEmpID.Caption = "FNHSysEmpID";
            this.FNHSysEmpID.FieldName = "FNHSysEmpID";
            this.FNHSysEmpID.Name = "FNHSysEmpID";
            this.FNHSysEmpID.OptionsColumn.AllowEdit = false;
            this.FNHSysEmpID.Width = 108;
            // 
            // EmpName
            // 
            this.EmpName.AppearanceCell.Options.UseTextOptions = true;
            this.EmpName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpName.AppearanceHeader.Options.UseTextOptions = true;
            this.EmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpName.Caption = "EmpName";
            this.EmpName.FieldName = "EmpName";
            this.EmpName.Name = "EmpName";
            this.EmpName.OptionsColumn.AllowEdit = false;
            this.EmpName.Visible = true;
            this.EmpName.VisibleIndex = 8;
            this.EmpName.Width = 193;
            // 
            // FDDateStart
            // 
            this.FDDateStart.AppearanceCell.Options.UseTextOptions = true;
            this.FDDateStart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FDDateStart.AppearanceHeader.Options.UseTextOptions = true;
            this.FDDateStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FDDateStart.Caption = "FDDateStart";
            this.FDDateStart.DisplayFormat.FormatString = "d";
            this.FDDateStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.FDDateStart.FieldName = "FDDateStart";
            this.FDDateStart.Name = "FDDateStart";
            this.FDDateStart.OptionsColumn.AllowEdit = false;
            this.FDDateStart.Visible = true;
            this.FDDateStart.VisibleIndex = 9;
            this.FDDateStart.Width = 118;
            // 
            // FDDateEnd
            // 
            this.FDDateEnd.AppearanceCell.Options.UseTextOptions = true;
            this.FDDateEnd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FDDateEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.FDDateEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FDDateEnd.Caption = "FDDateEnd";
            this.FDDateEnd.DisplayFormat.FormatString = "d";
            this.FDDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.FDDateEnd.FieldName = "FDDateEnd";
            this.FDDateEnd.Name = "FDDateEnd";
            this.FDDateEnd.OptionsColumn.AllowEdit = false;
            this.FDDateEnd.Visible = true;
            this.FDDateEnd.VisibleIndex = 10;
            this.FDDateEnd.Width = 90;
            // 
            // FTDeptDesc
            // 
            this.FTDeptDesc.AppearanceCell.Options.UseTextOptions = true;
            this.FTDeptDesc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTDeptDesc.AppearanceHeader.Options.UseTextOptions = true;
            this.FTDeptDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTDeptDesc.Caption = "FTDeptDesc";
            this.FTDeptDesc.FieldName = "FTDeptDesc";
            this.FTDeptDesc.Name = "FTDeptDesc";
            this.FTDeptDesc.OptionsColumn.AllowEdit = false;
            this.FTDeptDesc.Visible = true;
            this.FTDeptDesc.VisibleIndex = 5;
            this.FTDeptDesc.Width = 182;
            // 
            // FTDivisonName
            // 
            this.FTDivisonName.AppearanceCell.Options.UseTextOptions = true;
            this.FTDivisonName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTDivisonName.AppearanceHeader.Options.UseTextOptions = true;
            this.FTDivisonName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTDivisonName.Caption = "FTDivisonName";
            this.FTDivisonName.FieldName = "FTDivisonName";
            this.FTDivisonName.Name = "FTDivisonName";
            this.FTDivisonName.OptionsColumn.AllowEdit = false;
            this.FTDivisonName.Visible = true;
            this.FTDivisonName.VisibleIndex = 4;
            this.FTDivisonName.Width = 222;
            // 
            // FTUserName
            // 
            this.FTUserName.AppearanceCell.Options.UseTextOptions = true;
            this.FTUserName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTUserName.AppearanceHeader.Options.UseTextOptions = true;
            this.FTUserName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FTUserName.Caption = "FTUserName";
            this.FTUserName.FieldName = "FTUserName";
            this.FTUserName.Name = "FTUserName";
            this.FTUserName.OptionsColumn.AllowEdit = false;
            this.FTUserName.Visible = true;
            this.FTUserName.VisibleIndex = 1;
            this.FTUserName.Width = 97;
            // 
            // EmpCode
            // 
            this.EmpCode.AppearanceCell.Options.UseTextOptions = true;
            this.EmpCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpCode.AppearanceHeader.Options.UseTextOptions = true;
            this.EmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.EmpCode.Caption = "EmpCode";
            this.EmpCode.FieldName = "FTEmpCode";
            this.EmpCode.Name = "EmpCode";
            this.EmpCode.OptionsColumn.AllowEdit = false;
            this.EmpCode.Visible = true;
            this.EmpCode.VisibleIndex = 7;
            this.EmpCode.Width = 105;
            // 
            // wEmpListingTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1483, 635);
            this.Controls.Add(this.ogbDetail);
            this.Controls.Add(this.ogcCriteria);
            this.Name = "wEmpListingTracking";
            this.Text = "wEmpListingTracking";
            ((System.ComponentModel.ISupportInitialize)(this.ogcCriteria)).EndInit();
            this.ogcCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPermission.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogbDetail)).EndInit();
            this.ogbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ogbmainprocbutton)).EndInit();
            this.ogbmainprocbutton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ogcDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.GroupControl ogcCriteria;
        internal DevExpress.XtraEditors.GroupControl ogbDetail;
        internal DevExpress.XtraEditors.PanelControl ogbmainprocbutton;
        internal DevExpress.XtraEditors.SimpleButton ocmExit;
        internal DevExpress.XtraEditors.SimpleButton ocmClear;
        internal DevExpress.XtraEditors.SimpleButton ocmLoadData;
        internal DevExpress.XtraGrid.Views.Grid.GridView ogvDetail;
        internal DevExpress.XtraGrid.GridControl ogcDetail;
        private DevExpress.XtraGrid.Columns.GridColumn FNEmpStatus;
        private DevExpress.XtraGrid.Columns.GridColumn FNHSysEmpID;
        private DevExpress.XtraGrid.Columns.GridColumn EmpName;
        private DevExpress.XtraGrid.Columns.GridColumn FDDateStart;
        private DevExpress.XtraGrid.Columns.GridColumn FDDateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn FTDeptDesc;
        private DevExpress.XtraGrid.Columns.GridColumn FTDivisonName;
        private DevExpress.XtraGrid.Columns.GridColumn FTUserName;
        private DevExpress.XtraGrid.Columns.GridColumn FTStateActive;
        private DevExpress.XtraEditors.CheckEdit chkShowActive;
        private DevExpress.XtraGrid.Columns.GridColumn FTCmpCode;
        private DevExpress.XtraGrid.Columns.GridColumn Number;
        private DevExpress.XtraEditors.CheckEdit chkPermission;
        private DevExpress.XtraGrid.Columns.GridColumn EmpCode;
    }
}

namespace HI.Track.Tracking
{
    partial class wSendJsonStatus
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
            this.ogbOptRptOrderNo = new DevExpress.XtraEditors.GroupControl();
            this.ogcdetail = new DevExpress.XtraGrid.GridControl();
            this.ogvdetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CostSheetNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SendType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SendRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepositoryFTSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ocmClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ogbOptRptOrderNo)).BeginInit();
            this.ogbOptRptOrderNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogcdetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogvdetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryFTSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // ogbOptRptOrderNo
            // 
            this.ogbOptRptOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ogbOptRptOrderNo.Controls.Add(this.ogcdetail);
            this.ogbOptRptOrderNo.Location = new System.Drawing.Point(12, 12);
            this.ogbOptRptOrderNo.Name = "ogbOptRptOrderNo";
            this.ogbOptRptOrderNo.Size = new System.Drawing.Size(768, 405);
            this.ogbOptRptOrderNo.TabIndex = 8;
            this.ogbOptRptOrderNo.Text = "Send JSON Status";
            // 
            // ogcdetail
            // 
            this.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogcdetail.Location = new System.Drawing.Point(2, 23);
            this.ogcdetail.MainView = this.ogvdetail;
            this.ogcdetail.Name = "ogcdetail";
            this.ogcdetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepositoryFTSelect});
            this.ogcdetail.Size = new System.Drawing.Size(764, 380);
            this.ogcdetail.TabIndex = 4;
            this.ogcdetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ogvdetail});
            // 
            // ogvdetail
            // 
            this.ogvdetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CostSheetNo,
            this.SendType,
            this.Status,
            this.SendRemark});
            this.ogvdetail.GridControl = this.ogcdetail;
            this.ogvdetail.Name = "ogvdetail";
            this.ogvdetail.OptionsCustomization.AllowQuickHideColumns = false;
            this.ogvdetail.OptionsView.ColumnAutoWidth = false;
            this.ogvdetail.OptionsView.ShowGroupPanel = false;
            this.ogvdetail.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.ogvdetail_RowStyle);
            // 
            // CostSheetNo
            // 
            this.CostSheetNo.AppearanceCell.Options.UseTextOptions = true;
            this.CostSheetNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CostSheetNo.AppearanceHeader.Options.UseTextOptions = true;
            this.CostSheetNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CostSheetNo.Caption = "CostSheetNo";
            this.CostSheetNo.FieldName = "CostSheetNo";
            this.CostSheetNo.Name = "CostSheetNo";
            this.CostSheetNo.Visible = true;
            this.CostSheetNo.VisibleIndex = 0;
            this.CostSheetNo.Width = 102;
            // 
            // SendType
            // 
            this.SendType.AppearanceCell.Options.UseTextOptions = true;
            this.SendType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SendType.AppearanceHeader.Options.UseTextOptions = true;
            this.SendType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SendType.Caption = "Send Type";
            this.SendType.FieldName = "SendType";
            this.SendType.Name = "SendType";
            this.SendType.OptionsColumn.AllowEdit = false;
            this.SendType.OptionsColumn.ReadOnly = true;
            this.SendType.Visible = true;
            this.SendType.VisibleIndex = 1;
            this.SendType.Width = 62;
            // 
            // Status
            // 
            this.Status.AppearanceCell.Options.UseTextOptions = true;
            this.Status.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Status.AppearanceHeader.Options.UseTextOptions = true;
            this.Status.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Status.Caption = "Status";
            this.Status.FieldName = "Status";
            this.Status.Name = "Status";
            this.Status.OptionsColumn.AllowEdit = false;
            this.Status.OptionsColumn.ReadOnly = true;
            this.Status.Visible = true;
            this.Status.VisibleIndex = 2;
            this.Status.Width = 64;
            // 
            // SendRemark
            // 
            this.SendRemark.Caption = "Send Remark";
            this.SendRemark.FieldName = "SendRemark";
            this.SendRemark.Name = "SendRemark";
            this.SendRemark.OptionsColumn.AllowEdit = false;
            this.SendRemark.OptionsColumn.ReadOnly = true;
            this.SendRemark.Visible = true;
            this.SendRemark.VisibleIndex = 3;
            this.SendRemark.Width = 439;
            // 
            // RepositoryFTSelect
            // 
            this.RepositoryFTSelect.AutoHeight = false;
            this.RepositoryFTSelect.Caption = "Check";
            this.RepositoryFTSelect.Name = "RepositoryFTSelect";
            this.RepositoryFTSelect.ValueChecked = "1";
            this.RepositoryFTSelect.ValueUnchecked = "0";
            // 
            // ocmClose
            // 
            this.ocmClose.Location = new System.Drawing.Point(12, 423);
            this.ocmClose.Name = "ocmClose";
            this.ocmClose.Size = new System.Drawing.Size(120, 25);
            this.ocmClose.TabIndex = 9;
            this.ocmClose.Text = "CLOSE";
            this.ocmClose.Click += new System.EventHandler(this.ocmClose_Click);
            // 
            // wSendJsonStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 460);
            this.Controls.Add(this.ocmClose);
            this.Controls.Add(this.ogbOptRptOrderNo);
            this.Name = "wSendJsonStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wSendJsonStatus";
            ((System.ComponentModel.ISupportInitialize)(this.ogbOptRptOrderNo)).EndInit();
            this.ogbOptRptOrderNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ogcdetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogvdetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryFTSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.GroupControl ogbOptRptOrderNo;
        internal DevExpress.XtraGrid.GridControl ogcdetail;
        internal DevExpress.XtraGrid.Views.Grid.GridView ogvdetail;
        internal DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit RepositoryFTSelect;
        internal DevExpress.XtraGrid.Columns.GridColumn SendType;
        internal DevExpress.XtraGrid.Columns.GridColumn Status;
        internal DevExpress.XtraGrid.Columns.GridColumn SendRemark;
        private DevExpress.XtraGrid.Columns.GridColumn CostSheetNo;
        internal DevExpress.XtraEditors.SimpleButton ocmClose;
    }
}
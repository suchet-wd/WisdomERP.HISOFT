<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wRDSamFactoryTracking
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ogbDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcCostsheet = New DevExpress.XtraGrid.GridControl()
        Me.ogvCostsheet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRDSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNGESam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNGTMSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSamDiff = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcCostsheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvCostsheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbDetail
        '
        Me.ogbDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbDetail.Controls.Add(Me.ogcCostsheet)
        Me.ogbDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbDetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbDetail.Name = "ogbDetail"
        Me.ogbDetail.Size = New System.Drawing.Size(1263, 551)
        Me.ogbDetail.TabIndex = 1
        Me.ogbDetail.Text = "Detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(147, 152)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(639, 68)
        Me.ogbmainprocbutton.TabIndex = 139
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(24, 11)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 109
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(226, 11)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 107
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(564, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(70, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogcCostsheet
        '
        Me.ogcCostsheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcCostsheet.Location = New System.Drawing.Point(2, 20)
        Me.ogcCostsheet.MainView = Me.ogvCostsheet
        Me.ogcCostsheet.Name = "ogcCostsheet"
        Me.ogcCostsheet.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcCostsheet.Size = New System.Drawing.Size(1259, 529)
        Me.ogcCostsheet.TabIndex = 0
        Me.ogcCostsheet.Tag = "2|"
        Me.ogcCostsheet.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvCostsheet})
        '
        'ogvCostsheet
        '
        Me.ogvCostsheet.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStyleCode, Me.FTSeason, Me.FNHSysStyleId, Me.FNHSysSeasonId, Me.CFTCmpCode, Me.CFNRDSam, Me.CFNGESam, Me.CFNGTMSam, Me.CFNSamDiff})
        Me.ogvCostsheet.GridControl = Me.ogcCostsheet
        Me.ogvCostsheet.Name = "ogvCostsheet"
        Me.ogvCostsheet.OptionsView.ColumnAutoWidth = False
        Me.ogvCostsheet.OptionsView.ShowGroupPanel = False
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTSeason
        '
        Me.FTSeason.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSeason.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSeason.Caption = "FTSeason"
        Me.FTSeason.FieldName = "FTSeasonCode"
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.OptionsColumn.AllowEdit = False
        Me.FTSeason.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSeason.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSeason.Visible = True
        Me.FTSeason.VisibleIndex = 1
        Me.FTSeason.Width = 115
        '
        'FTStyleCode
        '
        Me.FTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 0
        Me.FTStyleCode.Width = 101
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysStyleId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.FNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.OptionsColumn.AllowEdit = False
        Me.FNHSysSeasonId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysSeasonId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'CFTCmpCode
        '
        Me.CFTCmpCode.Caption = "FTCmpCode"
        Me.CFTCmpCode.FieldName = "FTCmpCode"
        Me.CFTCmpCode.Name = "CFTCmpCode"
        Me.CFTCmpCode.OptionsColumn.AllowEdit = False
        Me.CFTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCmpCode.Visible = True
        Me.CFTCmpCode.VisibleIndex = 2
        Me.CFTCmpCode.Width = 98
        '
        'CFNRDSam
        '
        Me.CFNRDSam.Caption = "R && D Sam"
        Me.CFNRDSam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNRDSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRDSam.FieldName = "FNRDSam"
        Me.CFNRDSam.Name = "CFNRDSam"
        Me.CFNRDSam.OptionsColumn.AllowEdit = False
        Me.CFNRDSam.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNRDSam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNRDSam.Visible = True
        Me.CFNRDSam.VisibleIndex = 3
        Me.CFNRDSam.Width = 100
        '
        'CFNGESam
        '
        Me.CFNGESam.Caption = "GE Sam"
        Me.CFNGESam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNGESam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNGESam.FieldName = "FNGESam"
        Me.CFNGESam.Name = "CFNGESam"
        Me.CFNGESam.OptionsColumn.AllowEdit = False
        Me.CFNGESam.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNGESam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNGESam.Visible = True
        Me.CFNGESam.VisibleIndex = 4
        Me.CFNGESam.Width = 112
        '
        'CFNGTMSam
        '
        Me.CFNGTMSam.Caption = "GTM Sam"
        Me.CFNGTMSam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNGTMSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNGTMSam.FieldName = "FNGTMSam"
        Me.CFNGTMSam.Name = "CFNGTMSam"
        Me.CFNGTMSam.OptionsColumn.AllowEdit = False
        Me.CFNGTMSam.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNGTMSam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNGTMSam.Visible = True
        Me.CFNGTMSam.VisibleIndex = 5
        Me.CFNGTMSam.Width = 109
        '
        'CFNSamDiff
        '
        Me.CFNSamDiff.Caption = "Different SAM (%)"
        Me.CFNSamDiff.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNSamDiff.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNSamDiff.FieldName = "FNSamDiff"
        Me.CFNSamDiff.Name = "CFNSamDiff"
        Me.CFNSamDiff.OptionsColumn.AllowEdit = False
        Me.CFNSamDiff.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSamDiff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSamDiff.Visible = True
        Me.CFNSamDiff.VisibleIndex = 6
        Me.CFNSamDiff.Width = 135
        '
        'wRDSamFactoryTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1263, 551)
        Me.Controls.Add(Me.ogbDetail)
        Me.Name = "wRDSamFactoryTracking"
        Me.Text = "ตรวจสอบ SAM by Style"
        CType(Me.ogbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbDetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcCostsheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvCostsheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcCostsheet As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvCostsheet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRDSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNGESam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNGTMSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSamDiff As DevExpress.XtraGrid.Columns.GridColumn
End Class

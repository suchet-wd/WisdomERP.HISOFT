<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wRDSamAppTracking
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
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNOperater = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMinuteHour = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNProdPersonPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNWorkingTimeMinuteDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTargetPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSamCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCostCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetCostCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSamPack = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCostPack = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetCostPack = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
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
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(229, 202)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(983, 115)
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
        Me.ocmexit.Location = New System.Drawing.Point(908, 11)
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
        Me.ogvCostsheet.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.FTSeason, Me.FTStyleCode, Me.CFTSMPOrderNo, Me.CFTSMPOrderBy, Me.FNHSysStyleId, Me.FNHSysSeasonId, Me.FTRemark, Me.CFNSam, Me.CFNOperater, Me.FNCost, Me.FNMinuteHour, Me.FNProdPersonPerDay, Me.FNWorkingTimeMinuteDay, Me.CFNTargetPerDay, Me.FNSamCut, Me.FNCostCut, Me.FNNetCostCut, Me.FNSamPack, Me.FNCostPack, Me.FNNetCostPack})
        Me.ogvCostsheet.GridControl = Me.ogcCostsheet
        Me.ogvCostsheet.Name = "ogvCostsheet"
        Me.ogvCostsheet.OptionsView.ColumnAutoWidth = False
        Me.ogvCostsheet.OptionsView.ShowGroupPanel = False
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.RepFTSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 45
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
        Me.FTStyleCode.VisibleIndex = 2
        '
        'CFTSMPOrderNo
        '
        Me.CFTSMPOrderNo.Caption = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.Name = "CFTSMPOrderNo"
        Me.CFTSMPOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderNo.Visible = True
        Me.CFTSMPOrderNo.VisibleIndex = 3
        '
        'CFTSMPOrderBy
        '
        Me.CFTSMPOrderBy.Caption = "FTSMPOrderBy"
        Me.CFTSMPOrderBy.FieldName = "FTSMPOrderBy"
        Me.CFTSMPOrderBy.Name = "CFTSMPOrderBy"
        Me.CFTSMPOrderBy.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderBy.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderBy.Visible = True
        Me.CFTSMPOrderBy.VisibleIndex = 4
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
        'FTRemark
        '
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRemark.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 5
        '
        'CFNSam
        '
        Me.CFNSam.Caption = "FNSam"
        Me.CFNSam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNSam.FieldName = "FNSam"
        Me.CFNSam.Name = "CFNSam"
        Me.CFNSam.OptionsColumn.AllowEdit = False
        Me.CFNSam.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSam.Visible = True
        Me.CFNSam.VisibleIndex = 6
        '
        'CFNOperater
        '
        Me.CFNOperater.Caption = "FNOperater"
        Me.CFNOperater.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNOperater.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOperater.FieldName = "FNOperater"
        Me.CFNOperater.Name = "CFNOperater"
        Me.CFNOperater.OptionsColumn.AllowEdit = False
        Me.CFNOperater.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNOperater.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNOperater.Visible = True
        Me.CFNOperater.VisibleIndex = 7
        '
        'FNCost
        '
        Me.FNCost.Caption = "FNCost"
        Me.FNCost.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCost.FieldName = "FNCost"
        Me.FNCost.Name = "FNCost"
        Me.FNCost.OptionsColumn.AllowEdit = False
        Me.FNCost.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCost.Visible = True
        Me.FNCost.VisibleIndex = 8
        '
        'FNMinuteHour
        '
        Me.FNMinuteHour.Caption = "FNMinuteHour"
        Me.FNMinuteHour.DisplayFormat.FormatString = "{0:n0}"
        Me.FNMinuteHour.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMinuteHour.FieldName = "FNMinuteHour"
        Me.FNMinuteHour.Name = "FNMinuteHour"
        Me.FNMinuteHour.OptionsColumn.AllowEdit = False
        Me.FNMinuteHour.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMinuteHour.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMinuteHour.Visible = True
        Me.FNMinuteHour.VisibleIndex = 9
        '
        'FNProdPersonPerDay
        '
        Me.FNProdPersonPerDay.Caption = "FNProdPersonPerDay"
        Me.FNProdPersonPerDay.DisplayFormat.FormatString = "{0:n0}"
        Me.FNProdPersonPerDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNProdPersonPerDay.FieldName = "FNProdPersonPerDay"
        Me.FNProdPersonPerDay.Name = "FNProdPersonPerDay"
        Me.FNProdPersonPerDay.OptionsColumn.AllowEdit = False
        Me.FNProdPersonPerDay.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNProdPersonPerDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNProdPersonPerDay.Visible = True
        Me.FNProdPersonPerDay.VisibleIndex = 10
        '
        'FNWorkingTimeMinuteDay
        '
        Me.FNWorkingTimeMinuteDay.Caption = "FNWorkingTimeMinuteDay"
        Me.FNWorkingTimeMinuteDay.DisplayFormat.FormatString = "{0:n0}"
        Me.FNWorkingTimeMinuteDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWorkingTimeMinuteDay.FieldName = "FNWorkingTimeMinuteDay"
        Me.FNWorkingTimeMinuteDay.Name = "FNWorkingTimeMinuteDay"
        Me.FNWorkingTimeMinuteDay.OptionsColumn.AllowEdit = False
        Me.FNWorkingTimeMinuteDay.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWorkingTimeMinuteDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWorkingTimeMinuteDay.Visible = True
        Me.FNWorkingTimeMinuteDay.VisibleIndex = 11
        '
        'CFNTargetPerDay
        '
        Me.CFNTargetPerDay.Caption = "FNTargetPerDay"
        Me.CFNTargetPerDay.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTargetPerDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTargetPerDay.FieldName = "FNTargetPerDay"
        Me.CFNTargetPerDay.Name = "CFNTargetPerDay"
        Me.CFNTargetPerDay.OptionsColumn.AllowEdit = False
        Me.CFNTargetPerDay.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTargetPerDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTargetPerDay.Visible = True
        Me.CFNTargetPerDay.VisibleIndex = 12
        '
        'FNSamCut
        '
        Me.FNSamCut.Caption = "FNSamCut"
        Me.FNSamCut.DisplayFormat.FormatString = "{0:n4}"
        Me.FNSamCut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSamCut.FieldName = "FNSamCut"
        Me.FNSamCut.Name = "FNSamCut"
        Me.FNSamCut.OptionsColumn.AllowEdit = False
        Me.FNSamCut.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSamCut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSamCut.Visible = True
        Me.FNSamCut.VisibleIndex = 13
        '
        'FNCostCut
        '
        Me.FNCostCut.Caption = "FNCostCut"
        Me.FNCostCut.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCostCut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCostCut.FieldName = "FNCostCut"
        Me.FNCostCut.Name = "FNCostCut"
        Me.FNCostCut.OptionsColumn.AllowEdit = False
        Me.FNCostCut.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCostCut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCostCut.Visible = True
        Me.FNCostCut.VisibleIndex = 14
        '
        'FNNetCostCut
        '
        Me.FNNetCostCut.Caption = "FNNetCostCut"
        Me.FNNetCostCut.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetCostCut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetCostCut.FieldName = "FNNetCostCut"
        Me.FNNetCostCut.Name = "FNNetCostCut"
        Me.FNNetCostCut.OptionsColumn.AllowEdit = False
        Me.FNNetCostCut.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCostCut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCostCut.Visible = True
        Me.FNNetCostCut.VisibleIndex = 15
        '
        'FNSamPack
        '
        Me.FNSamPack.Caption = "FNSamPack"
        Me.FNSamPack.DisplayFormat.FormatString = "{0:n4}"
        Me.FNSamPack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSamPack.FieldName = "FNSamPack"
        Me.FNSamPack.Name = "FNSamPack"
        Me.FNSamPack.OptionsColumn.AllowEdit = False
        Me.FNSamPack.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSamPack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSamPack.Visible = True
        Me.FNSamPack.VisibleIndex = 16
        '
        'FNCostPack
        '
        Me.FNCostPack.Caption = "FNCostPack"
        Me.FNCostPack.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCostPack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCostPack.FieldName = "FNCostPack"
        Me.FNCostPack.Name = "FNCostPack"
        Me.FNCostPack.OptionsColumn.AllowEdit = False
        Me.FNCostPack.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCostPack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCostPack.Visible = True
        Me.FNCostPack.VisibleIndex = 17
        '
        'FNNetCostPack
        '
        Me.FNNetCostPack.Caption = "FNNetCostPack"
        Me.FNNetCostPack.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetCostPack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetCostPack.FieldName = "FNNetCostPack"
        Me.FNNetCostPack.Name = "FNNetCostPack"
        Me.FNNetCostPack.OptionsColumn.AllowEdit = False
        Me.FNNetCostPack.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCostPack.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCostPack.Visible = True
        Me.FNNetCostPack.VisibleIndex = 18
        '
        'ocmapprove
        '
        Me.ocmapprove.Location = New System.Drawing.Point(24, 51)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(95, 25)
        Me.ocmapprove.TabIndex = 118
        Me.ocmapprove.TabStop = False
        Me.ocmapprove.Tag = "2|"
        Me.ocmapprove.Text = "Approve"
        '
        'wRDSamAppTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1263, 551)
        Me.Controls.Add(Me.ogbDetail)
        Me.Name = "wRDSamAppTracking"
        Me.Text = "อนุมมัติ RD Sam TO GE"
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
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTSMPOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSMPOrderBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNOperater As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMinuteHour As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNProdPersonPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNWorkingTimeMinuteDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTargetPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSamCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCostCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetCostCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSamPack As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCostPack As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetCostPack As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
End Class

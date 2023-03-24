<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wIssueApproveReceive
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTIssueNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDIssueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTIssueBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysWHCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTHoliday = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTStaCalSSO = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEditFTMngApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEditFTDirApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcheckwaiting = New System.Windows.Forms.Timer(Me.components)
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEditFTMngApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEditFTDirApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbemployee
        '
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.oSelectAll)
        Me.ogbemployee.Controls.Add(Me.ogc)
        Me.ogbemployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbemployee.Location = New System.Drawing.Point(0, 0)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.Size = New System.Drawing.Size(1156, 515)
        Me.ogbemployee.TabIndex = 5
        Me.ogbemployee.Text = "Issue Waiting Approve Detail"
        '
        'oSelectAll
        '
        Me.oSelectAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oSelectAll.Location = New System.Drawing.Point(11, 22)
        Me.oSelectAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.AutoHeight = False
        Me.oSelectAll.Properties.Caption = "Select All"
        Me.oSelectAll.Size = New System.Drawing.Size(1143, 20)
        Me.oSelectAll.TabIndex = 4
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.Location = New System.Drawing.Point(4, 40)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.RepColFTLeavePay, Me.RepColFTHoliday, Me.RepFTStaCalSSO, Me.RepFTApproveState, Me.RepFTLeavePay, Me.RepositoryItemCheckEFTSelect, Me.RepositoryItemCheckEditFTMngApproveState, Me.RepositoryItemCheckEditFTDirApproveState})
        Me.ogc.Size = New System.Drawing.Size(1150, 470)
        Me.ogc.TabIndex = 3
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTIssueNo, Me.CFDIssueDate, Me.CFTIssueBy, Me.CFTOrderNo, Me.CFTRemark, Me.CFNHSysCmpId, Me.CFNHSysWHCmpId, Me.CFNHSysRawMatId, Me.CFTRawMatCode, Me.CFTRawMatName, Me.CFTRawMatColorCode, Me.CFTRawMatSizeCode, Me.CFTUnitCode, Me.CFNQuantity})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 47
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'CFTIssueNo
        '
        Me.CFTIssueNo.Caption = "Issue No"
        Me.CFTIssueNo.FieldName = "FTIssueNo"
        Me.CFTIssueNo.Name = "CFTIssueNo"
        Me.CFTIssueNo.OptionsColumn.AllowEdit = False
        Me.CFTIssueNo.OptionsColumn.ReadOnly = True
        Me.CFTIssueNo.Visible = True
        Me.CFTIssueNo.VisibleIndex = 1
        Me.CFTIssueNo.Width = 128
        '
        'CFDIssueDate
        '
        Me.CFDIssueDate.Caption = "Issue Date"
        Me.CFDIssueDate.FieldName = "FDIssueDate"
        Me.CFDIssueDate.Name = "CFDIssueDate"
        Me.CFDIssueDate.OptionsColumn.AllowEdit = False
        Me.CFDIssueDate.OptionsColumn.ReadOnly = True
        Me.CFDIssueDate.Visible = True
        Me.CFDIssueDate.VisibleIndex = 2
        Me.CFDIssueDate.Width = 73
        '
        'CFTIssueBy
        '
        Me.CFTIssueBy.Caption = "Issue By"
        Me.CFTIssueBy.FieldName = "FTIssueBy"
        Me.CFTIssueBy.Name = "CFTIssueBy"
        Me.CFTIssueBy.OptionsColumn.AllowEdit = False
        Me.CFTIssueBy.OptionsColumn.ReadOnly = True
        Me.CFTIssueBy.Visible = True
        Me.CFTIssueBy.VisibleIndex = 3
        Me.CFTIssueBy.Width = 103
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.Caption = "Order No"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 4
        Me.CFTOrderNo.Width = 118
        '
        'CFTRemark
        '
        Me.CFTRemark.Caption = "Remark"
        Me.CFTRemark.FieldName = "FTRemark"
        Me.CFTRemark.Name = "CFTRemark"
        Me.CFTRemark.OptionsColumn.AllowEdit = False
        Me.CFTRemark.OptionsColumn.ReadOnly = True
        Me.CFTRemark.Visible = True
        Me.CFTRemark.VisibleIndex = 5
        Me.CFTRemark.Width = 192
        '
        'CFNHSysCmpId
        '
        Me.CFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.CFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.CFNHSysCmpId.Name = "CFNHSysCmpId"
        Me.CFNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCmpId.OptionsColumn.ReadOnly = True
        Me.CFNHSysCmpId.Width = 103
        '
        'CFNHSysWHCmpId
        '
        Me.CFNHSysWHCmpId.Caption = "FNHSysWHCmpId"
        Me.CFNHSysWHCmpId.FieldName = "FNHSysWHCmpId"
        Me.CFNHSysWHCmpId.Name = "CFNHSysWHCmpId"
        Me.CFNHSysWHCmpId.OptionsColumn.AllowEdit = False
        Me.CFNHSysWHCmpId.OptionsColumn.ReadOnly = True
        Me.CFNHSysWHCmpId.Width = 101
        '
        'CFNHSysRawMatId
        '
        Me.CFNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.CFNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.CFNHSysRawMatId.Name = "CFNHSysRawMatId"
        Me.CFNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.CFNHSysRawMatId.OptionsColumn.ReadOnly = True
        Me.CFNHSysRawMatId.Width = 95
        '
        'CFTRawMatCode
        '
        Me.CFTRawMatCode.Caption = "RawMat Code"
        Me.CFTRawMatCode.FieldName = "FTRawMatCode"
        Me.CFTRawMatCode.Name = "CFTRawMatCode"
        Me.CFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.CFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.CFTRawMatCode.Visible = True
        Me.CFTRawMatCode.VisibleIndex = 6
        Me.CFTRawMatCode.Width = 129
        '
        'CFTRawMatName
        '
        Me.CFTRawMatName.Caption = "RawMat Name"
        Me.CFTRawMatName.FieldName = "FTRawMatName"
        Me.CFTRawMatName.Name = "CFTRawMatName"
        Me.CFTRawMatName.OptionsColumn.AllowEdit = False
        Me.CFTRawMatName.OptionsColumn.ReadOnly = True
        Me.CFTRawMatName.Visible = True
        Me.CFTRawMatName.VisibleIndex = 7
        Me.CFTRawMatName.Width = 275
        '
        'CFTRawMatColorCode
        '
        Me.CFTRawMatColorCode.Caption = "RawMat Color Code"
        Me.CFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.CFTRawMatColorCode.Name = "CFTRawMatColorCode"
        Me.CFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.CFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.CFTRawMatColorCode.Visible = True
        Me.CFTRawMatColorCode.VisibleIndex = 8
        '
        'CFTRawMatSizeCode
        '
        Me.CFTRawMatSizeCode.Caption = "RawMat Size Code"
        Me.CFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.CFTRawMatSizeCode.Name = "CFTRawMatSizeCode"
        Me.CFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.CFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.CFTRawMatSizeCode.Visible = True
        Me.CFTRawMatSizeCode.VisibleIndex = 9
        '
        'CFTUnitCode
        '
        Me.CFTUnitCode.Caption = "UnitCode"
        Me.CFTUnitCode.FieldName = "FTUnitCode"
        Me.CFTUnitCode.Name = "CFTUnitCode"
        Me.CFTUnitCode.OptionsColumn.AllowEdit = False
        Me.CFTUnitCode.OptionsColumn.ReadOnly = True
        Me.CFTUnitCode.Visible = True
        Me.CFTUnitCode.VisibleIndex = 10
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "Quantity"
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.OptionsColumn.AllowEdit = False
        Me.CFNQuantity.OptionsColumn.ReadOnly = True
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 11
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'RepColFTLeavePay
        '
        Me.RepColFTLeavePay.AutoHeight = False
        Me.RepColFTLeavePay.Caption = "Check"
        Me.RepColFTLeavePay.Name = "RepColFTLeavePay"
        Me.RepColFTLeavePay.ValueChecked = "1"
        Me.RepColFTLeavePay.ValueUnchecked = "0"
        '
        'RepColFTHoliday
        '
        Me.RepColFTHoliday.AutoHeight = False
        Me.RepColFTHoliday.Caption = "Check"
        Me.RepColFTHoliday.Name = "RepColFTHoliday"
        Me.RepColFTHoliday.ValueChecked = "1"
        Me.RepColFTHoliday.ValueUnchecked = "0"
        '
        'RepFTStaCalSSO
        '
        Me.RepFTStaCalSSO.AutoHeight = False
        Me.RepFTStaCalSSO.Caption = "Check"
        Me.RepFTStaCalSSO.Name = "RepFTStaCalSSO"
        Me.RepFTStaCalSSO.ValueChecked = "1"
        Me.RepFTStaCalSSO.ValueUnchecked = "0"
        '
        'RepFTApproveState
        '
        Me.RepFTApproveState.AutoHeight = False
        Me.RepFTApproveState.Caption = "Check"
        Me.RepFTApproveState.Name = "RepFTApproveState"
        Me.RepFTApproveState.ValueChecked = "1"
        Me.RepFTApproveState.ValueUnchecked = "0"
        '
        'RepFTLeavePay
        '
        Me.RepFTLeavePay.AutoHeight = False
        Me.RepFTLeavePay.Caption = "Check"
        Me.RepFTLeavePay.Name = "RepFTLeavePay"
        Me.RepFTLeavePay.ValueChecked = "1"
        Me.RepFTLeavePay.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEFTSelect
        '
        Me.RepositoryItemCheckEFTSelect.AutoHeight = False
        Me.RepositoryItemCheckEFTSelect.Name = "RepositoryItemCheckEFTSelect"
        Me.RepositoryItemCheckEFTSelect.ValueChecked = "1"
        Me.RepositoryItemCheckEFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEditFTMngApproveState
        '
        Me.RepositoryItemCheckEditFTMngApproveState.AutoHeight = False
        Me.RepositoryItemCheckEditFTMngApproveState.Name = "RepositoryItemCheckEditFTMngApproveState"
        Me.RepositoryItemCheckEditFTMngApproveState.ValueChecked = "1"
        Me.RepositoryItemCheckEditFTMngApproveState.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEditFTDirApproveState
        '
        Me.RepositoryItemCheckEditFTDirApproveState.AutoHeight = False
        Me.RepositoryItemCheckEditFTDirApproveState.Name = "RepositoryItemCheckEditFTDirApproveState"
        Me.RepositoryItemCheckEditFTDirApproveState.ValueChecked = "1"
        Me.RepositoryItemCheckEditFTDirApproveState.ValueUnchecked = "0"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(199, 141)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(687, 47)
        Me.ogbmainprocbutton.TabIndex = 411
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmapprove
        '
        Me.ocmapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmapprove.Location = New System.Drawing.Point(127, 11)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(95, 25)
        Me.ocmapprove.TabIndex = 96
        Me.ocmapprove.TabStop = False
        Me.ocmapprove.Tag = "2|"
        Me.ocmapprove.Text = "approved"
        '
        'ocmload
        '
        Me.ocmload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmload.Location = New System.Drawing.Point(19, 11)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 96
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(573, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmcheckwaiting
        '
        Me.ocmcheckwaiting.Interval = 60000
        '
        'wIssueApproveReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1156, 515)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbemployee)
        Me.Name = "wIssueApproveReceive"
        Me.Text = "Approve Issue Receive"
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEditFTMngApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEditFTDirApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTHoliday As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTStaCalSSO As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcheckwaiting As System.Windows.Forms.Timer
    Friend WithEvents CFTIssueNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDIssueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTIssueBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysWHCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemCheckEFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RepositoryItemCheckEditFTMngApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEditFTDirApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class

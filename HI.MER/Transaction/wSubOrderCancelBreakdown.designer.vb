<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSubOrderCancelBreakdown
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
        Me.ogbSubOrderNoSrc = New DevExpress.XtraEditors.GroupControl()
        Me.FTStateCancenSub = New DevExpress.XtraEditors.CheckEdit()
        Me.FTRemarkSubOrderNo = New DevExpress.XtraEditors.MemoEdit()
        Me.FTRemark_SubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSubOrderNoSrc = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTSubOrderNoSrc_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbViewDivertDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbBalanceBreakdownAfterDivert = New DevExpress.XtraEditors.GroupControl()
        Me.ogdDivertBalance = New DevExpress.XtraGrid.GridControl()
        Me.ogvDivertBalance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColBalanceDivertFTMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColBalanceDivertFTMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C3FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C3FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C3FNTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbTargetBreakdownDivert = New DevExpress.XtraEditors.GroupControl()
        Me.ogdDivertDT = New DevExpress.XtraGrid.GridControl()
        Me.ogvDivertDT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColTargetDivertFTMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepPOLineItem = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.C2FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTNikePOLineItem = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ReposLEFTMatColorCodeNew = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.ogbSrcBreakdownDivert = New DevExpress.XtraEditors.GroupControl()
        Me.ogdDivertSrc = New DevExpress.XtraGrid.GridControl()
        Me.ogvDivertSrc = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColSrcDivertoColFTMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColSrcDivertoColFTMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C1FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C1FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C1FNTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbConfirmDivertSubBreakdown = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbSubOrderNoSrc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbSubOrderNoSrc.SuspendLayout()
        CType(Me.FTStateCancenSub.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemarkSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSubOrderNoSrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbViewDivertDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbViewDivertDetail.SuspendLayout()
        CType(Me.ogbBalanceBreakdownAfterDivert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbBalanceBreakdownAfterDivert.SuspendLayout()
        CType(Me.ogdDivertBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDivertBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbTargetBreakdownDivert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbTargetBreakdownDivert.SuspendLayout()
        CType(Me.ogdDivertDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDivertDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepPOLineItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTNikePOLineItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposLEFTMatColorCodeNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbSrcBreakdownDivert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbSrcBreakdownDivert.SuspendLayout()
        CType(Me.ogdDivertSrc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDivertSrc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbConfirmDivertSubBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbConfirmDivertSubBreakdown.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbSubOrderNoSrc
        '
        Me.ogbSubOrderNoSrc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbSubOrderNoSrc.Controls.Add(Me.FTStateCancenSub)
        Me.ogbSubOrderNoSrc.Controls.Add(Me.FTRemarkSubOrderNo)
        Me.ogbSubOrderNoSrc.Controls.Add(Me.FTRemark_SubOrderNo_lbl)
        Me.ogbSubOrderNoSrc.Controls.Add(Me.FTSubOrderNoSrc)
        Me.ogbSubOrderNoSrc.Controls.Add(Me.FTSubOrderNoSrc_lbl)
        Me.ogbSubOrderNoSrc.Location = New System.Drawing.Point(0, 0)
        Me.ogbSubOrderNoSrc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbSubOrderNoSrc.Name = "ogbSubOrderNoSrc"
        Me.ogbSubOrderNoSrc.Size = New System.Drawing.Size(1202, 140)
        Me.ogbSubOrderNoSrc.TabIndex = 0
        Me.ogbSubOrderNoSrc.Text = "Source Sub Order No. for Divert"
        '
        'FTStateCancenSub
        '
        Me.FTStateCancenSub.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStateCancenSub.EditValue = "0"
        Me.FTStateCancenSub.Location = New System.Drawing.Point(432, 31)
        Me.FTStateCancenSub.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateCancenSub.Name = "FTStateCancenSub"
        Me.FTStateCancenSub.Properties.Caption = "All Breakdown"
        Me.FTStateCancenSub.Properties.Tag = "FTStateLaser"
        Me.FTStateCancenSub.Properties.ValueChecked = "1"
        Me.FTStateCancenSub.Properties.ValueUnchecked = "0"
        Me.FTStateCancenSub.Size = New System.Drawing.Size(330, 20)
        Me.FTStateCancenSub.TabIndex = 501
        Me.FTStateCancenSub.Tag = "2|"
        '
        'FTRemarkSubOrderNo
        '
        Me.FTRemarkSubOrderNo.EditValue = ""
        Me.FTRemarkSubOrderNo.Location = New System.Drawing.Point(191, 60)
        Me.FTRemarkSubOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemarkSubOrderNo.Name = "FTRemarkSubOrderNo"
        Me.FTRemarkSubOrderNo.Properties.MaxLength = 500
        Me.FTRemarkSubOrderNo.Size = New System.Drawing.Size(1005, 72)
        Me.FTRemarkSubOrderNo.TabIndex = 14
        Me.FTRemarkSubOrderNo.Tag = "2|"
        '
        'FTRemark_SubOrderNo_lbl
        '
        Me.FTRemark_SubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTRemark_SubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_SubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_SubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_SubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_SubOrderNo_lbl.Location = New System.Drawing.Point(44, 61)
        Me.FTRemark_SubOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_SubOrderNo_lbl.Name = "FTRemark_SubOrderNo_lbl"
        Me.FTRemark_SubOrderNo_lbl.Size = New System.Drawing.Size(140, 23)
        Me.FTRemark_SubOrderNo_lbl.TabIndex = 501
        Me.FTRemark_SubOrderNo_lbl.Tag = "2|"
        Me.FTRemark_SubOrderNo_lbl.Text = "FTRemark  :"
        '
        'FTSubOrderNoSrc
        '
        Me.FTSubOrderNoSrc.EnterMoveNextControl = True
        Me.FTSubOrderNoSrc.Location = New System.Drawing.Point(191, 30)
        Me.FTSubOrderNoSrc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNoSrc.Name = "FTSubOrderNoSrc"
        Me.FTSubOrderNoSrc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSubOrderNoSrc.Properties.ReadOnly = True
        Me.FTSubOrderNoSrc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTSubOrderNoSrc.Size = New System.Drawing.Size(222, 22)
        Me.FTSubOrderNoSrc.TabIndex = 0
        '
        'FTSubOrderNoSrc_lbl
        '
        Me.FTSubOrderNoSrc_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubOrderNoSrc_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubOrderNoSrc_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubOrderNoSrc_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubOrderNoSrc_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubOrderNoSrc_lbl.Location = New System.Drawing.Point(14, 31)
        Me.FTSubOrderNoSrc_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNoSrc_lbl.Name = "FTSubOrderNoSrc_lbl"
        Me.FTSubOrderNoSrc_lbl.Size = New System.Drawing.Size(170, 22)
        Me.FTSubOrderNoSrc_lbl.TabIndex = 425
        Me.FTSubOrderNoSrc_lbl.Tag = "2|"
        Me.FTSubOrderNoSrc_lbl.Text = "FTSubOrderNo :"
        '
        'ogbViewDivertDetail
        '
        Me.ogbViewDivertDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbViewDivertDetail.Controls.Add(Me.ogbBalanceBreakdownAfterDivert)
        Me.ogbViewDivertDetail.Controls.Add(Me.ogbTargetBreakdownDivert)
        Me.ogbViewDivertDetail.Controls.Add(Me.ogbSrcBreakdownDivert)
        Me.ogbViewDivertDetail.Location = New System.Drawing.Point(0, 148)
        Me.ogbViewDivertDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbViewDivertDetail.Name = "ogbViewDivertDetail"
        Me.ogbViewDivertDetail.ShowCaption = False
        Me.ogbViewDivertDetail.Size = New System.Drawing.Size(1202, 588)
        Me.ogbViewDivertDetail.TabIndex = 1
        '
        'ogbBalanceBreakdownAfterDivert
        '
        Me.ogbBalanceBreakdownAfterDivert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbBalanceBreakdownAfterDivert.Controls.Add(Me.ogdDivertBalance)
        Me.ogbBalanceBreakdownAfterDivert.Location = New System.Drawing.Point(0, 394)
        Me.ogbBalanceBreakdownAfterDivert.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbBalanceBreakdownAfterDivert.Name = "ogbBalanceBreakdownAfterDivert"
        Me.ogbBalanceBreakdownAfterDivert.Size = New System.Drawing.Size(1199, 194)
        Me.ogbBalanceBreakdownAfterDivert.TabIndex = 4
        Me.ogbBalanceBreakdownAfterDivert.Text = "Balance Breakdown Afer Cancel"
        '
        'ogdDivertBalance
        '
        Me.ogdDivertBalance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdDivertBalance.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertBalance.Location = New System.Drawing.Point(2, 25)
        Me.ogdDivertBalance.MainView = Me.ogvDivertBalance
        Me.ogdDivertBalance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertBalance.Name = "ogdDivertBalance"
        Me.ogdDivertBalance.Size = New System.Drawing.Size(1195, 167)
        Me.ogdDivertBalance.TabIndex = 0
        Me.ogdDivertBalance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDivertBalance})
        '
        'ogvDivertBalance
        '
        Me.ogvDivertBalance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColBalanceDivertFTMatColorCode, Me.oColBalanceDivertFTMatColorName, Me.C3FTNikePOLineItem, Me.C3FNHSysMatColorId, Me.C3FNTotal})
        Me.ogvDivertBalance.GridControl = Me.ogdDivertBalance
        Me.ogvDivertBalance.Name = "ogvDivertBalance"
        Me.ogvDivertBalance.OptionsView.ColumnAutoWidth = False
        Me.ogvDivertBalance.OptionsView.ShowFooter = True
        Me.ogvDivertBalance.OptionsView.ShowGroupPanel = False
        '
        'oColBalanceDivertFTMatColorCode
        '
        Me.oColBalanceDivertFTMatColorCode.Caption = "FTMatColorCode"
        Me.oColBalanceDivertFTMatColorCode.FieldName = "FTMatColorCode"
        Me.oColBalanceDivertFTMatColorCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColBalanceDivertFTMatColorCode.Name = "oColBalanceDivertFTMatColorCode"
        Me.oColBalanceDivertFTMatColorCode.OptionsColumn.AllowEdit = False
        Me.oColBalanceDivertFTMatColorCode.OptionsColumn.AllowMove = False
        Me.oColBalanceDivertFTMatColorCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColBalanceDivertFTMatColorCode.OptionsColumn.ReadOnly = True
        Me.oColBalanceDivertFTMatColorCode.Visible = True
        Me.oColBalanceDivertFTMatColorCode.VisibleIndex = 0
        '
        'oColBalanceDivertFTMatColorName
        '
        Me.oColBalanceDivertFTMatColorName.Caption = "FTMatColorName"
        Me.oColBalanceDivertFTMatColorName.FieldName = "FTMatColorName"
        Me.oColBalanceDivertFTMatColorName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColBalanceDivertFTMatColorName.Name = "oColBalanceDivertFTMatColorName"
        Me.oColBalanceDivertFTMatColorName.OptionsColumn.AllowEdit = False
        Me.oColBalanceDivertFTMatColorName.OptionsColumn.AllowMove = False
        Me.oColBalanceDivertFTMatColorName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColBalanceDivertFTMatColorName.OptionsColumn.ReadOnly = True
        Me.oColBalanceDivertFTMatColorName.Visible = True
        Me.oColBalanceDivertFTMatColorName.VisibleIndex = 1
        '
        'C3FTNikePOLineItem
        '
        Me.C3FTNikePOLineItem.Caption = "PO Line Item"
        Me.C3FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.C3FTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C3FTNikePOLineItem.Name = "C3FTNikePOLineItem"
        Me.C3FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.C3FTNikePOLineItem.OptionsColumn.AllowMove = False
        Me.C3FTNikePOLineItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C3FTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.C3FTNikePOLineItem.Visible = True
        Me.C3FTNikePOLineItem.VisibleIndex = 2
        '
        'C3FNHSysMatColorId
        '
        Me.C3FNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.C3FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.C3FNHSysMatColorId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C3FNHSysMatColorId.Name = "C3FNHSysMatColorId"
        Me.C3FNHSysMatColorId.OptionsColumn.AllowEdit = False
        Me.C3FNHSysMatColorId.OptionsColumn.AllowMove = False
        Me.C3FNHSysMatColorId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C3FNHSysMatColorId.OptionsColumn.ReadOnly = True
        '
        'C3FNTotal
        '
        Me.C3FNTotal.Caption = "Total"
        Me.C3FNTotal.DisplayFormat.FormatString = "{0:n0}"
        Me.C3FNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C3FNTotal.FieldName = "FNTotal"
        Me.C3FNTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.C3FNTotal.Name = "C3FNTotal"
        Me.C3FNTotal.OptionsColumn.AllowEdit = False
        Me.C3FNTotal.OptionsColumn.ReadOnly = True
        Me.C3FNTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotal", "{0:n0}")})
        Me.C3FNTotal.Visible = True
        Me.C3FNTotal.VisibleIndex = 3
        '
        'ogbTargetBreakdownDivert
        '
        Me.ogbTargetBreakdownDivert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbTargetBreakdownDivert.Controls.Add(Me.ogdDivertDT)
        Me.ogbTargetBreakdownDivert.Location = New System.Drawing.Point(0, 194)
        Me.ogbTargetBreakdownDivert.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbTargetBreakdownDivert.Name = "ogbTargetBreakdownDivert"
        Me.ogbTargetBreakdownDivert.Size = New System.Drawing.Size(1199, 195)
        Me.ogbTargetBreakdownDivert.TabIndex = 3
        Me.ogbTargetBreakdownDivert.Text = "Target Breakdown  Cancel"
        '
        'ogdDivertDT
        '
        Me.ogdDivertDT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdDivertDT.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertDT.Location = New System.Drawing.Point(2, 25)
        Me.ogdDivertDT.MainView = Me.ogvDivertDT
        Me.ogdDivertDT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertDT.Name = "ogdDivertDT"
        Me.ogdDivertDT.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTNikePOLineItem, Me.RepPOLineItem, Me.ReposLEFTMatColorCodeNew})
        Me.ogdDivertDT.Size = New System.Drawing.Size(1195, 168)
        Me.ogdDivertDT.TabIndex = 1
        Me.ogdDivertDT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDivertDT})
        '
        'ogvDivertDT
        '
        Me.ogvDivertDT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTMatColorCode, Me.oColTargetDivertFTMatColorName, Me.C2FTNikePOLineItem, Me.C2FNHSysMatColorId, Me.C2FNTotal})
        Me.ogvDivertDT.GridControl = Me.ogdDivertDT
        Me.ogvDivertDT.Name = "ogvDivertDT"
        Me.ogvDivertDT.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvDivertDT.OptionsView.ColumnAutoWidth = False
        Me.ogvDivertDT.OptionsView.ShowFooter = True
        Me.ogvDivertDT.OptionsView.ShowGroupPanel = False
        Me.ogvDivertDT.Tag = "2|"
        '
        'cFTMatColorCode
        '
        Me.cFTMatColorCode.Caption = "Colorway Org"
        Me.cFTMatColorCode.FieldName = "FTMatColorCode"
        Me.cFTMatColorCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cFTMatColorCode.Name = "cFTMatColorCode"
        Me.cFTMatColorCode.OptionsColumn.AllowEdit = False
        Me.cFTMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTMatColorCode.OptionsColumn.AllowMove = False
        Me.cFTMatColorCode.OptionsColumn.AllowShowHide = False
        Me.cFTMatColorCode.OptionsColumn.ReadOnly = True
        Me.cFTMatColorCode.Visible = True
        Me.cFTMatColorCode.VisibleIndex = 0
        '
        'oColTargetDivertFTMatColorName
        '
        Me.oColTargetDivertFTMatColorName.Caption = "FTMatColorName"
        Me.oColTargetDivertFTMatColorName.FieldName = "FTMatColorName"
        Me.oColTargetDivertFTMatColorName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColTargetDivertFTMatColorName.Name = "oColTargetDivertFTMatColorName"
        Me.oColTargetDivertFTMatColorName.OptionsColumn.AllowEdit = False
        Me.oColTargetDivertFTMatColorName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColTargetDivertFTMatColorName.OptionsColumn.AllowMove = False
        Me.oColTargetDivertFTMatColorName.OptionsColumn.AllowShowHide = False
        Me.oColTargetDivertFTMatColorName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColTargetDivertFTMatColorName.OptionsColumn.ReadOnly = True
        Me.oColTargetDivertFTMatColorName.Visible = True
        Me.oColTargetDivertFTMatColorName.VisibleIndex = 1
        '
        'C2FTNikePOLineItem
        '
        Me.C2FTNikePOLineItem.Caption = "PO Line Item"
        Me.C2FTNikePOLineItem.ColumnEdit = Me.RepPOLineItem
        Me.C2FTNikePOLineItem.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FTNikePOLineItem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.C2FTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C2FTNikePOLineItem.Name = "C2FTNikePOLineItem"
        Me.C2FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.C2FTNikePOLineItem.OptionsColumn.AllowMove = False
        Me.C2FTNikePOLineItem.OptionsColumn.AllowShowHide = False
        Me.C2FTNikePOLineItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTNikePOLineItem.Visible = True
        Me.C2FTNikePOLineItem.VisibleIndex = 2
        '
        'RepPOLineItem
        '
        Me.RepPOLineItem.AutoHeight = False
        Me.RepPOLineItem.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepPOLineItem.DisplayFormat.FormatString = "{0:n0}"
        Me.RepPOLineItem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepPOLineItem.EditFormat.FormatString = "{0:n0}"
        Me.RepPOLineItem.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepPOLineItem.MaxLength = 4
        Me.RepPOLineItem.Name = "RepPOLineItem"
        '
        'C2FNHSysMatColorId
        '
        Me.C2FNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.C2FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.C2FNHSysMatColorId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C2FNHSysMatColorId.Name = "C2FNHSysMatColorId"
        Me.C2FNHSysMatColorId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysMatColorId.OptionsColumn.AllowMove = False
        Me.C2FNHSysMatColorId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNHSysMatColorId.OptionsColumn.ReadOnly = True
        '
        'C2FNTotal
        '
        Me.C2FNTotal.Caption = "Total"
        Me.C2FNTotal.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNTotal.FieldName = "FNTotal"
        Me.C2FNTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.C2FNTotal.Name = "C2FNTotal"
        Me.C2FNTotal.OptionsColumn.AllowEdit = False
        Me.C2FNTotal.OptionsColumn.ReadOnly = True
        Me.C2FNTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotal", "{0:n0}")})
        Me.C2FNTotal.Visible = True
        Me.C2FNTotal.VisibleIndex = 3
        '
        'RepFTNikePOLineItem
        '
        Me.RepFTNikePOLineItem.AutoHeight = False
        Me.RepFTNikePOLineItem.MaxLength = 10
        Me.RepFTNikePOLineItem.Name = "RepFTNikePOLineItem"
        '
        'ReposLEFTMatColorCodeNew
        '
        Me.ReposLEFTMatColorCodeNew.AutoHeight = False
        Me.ReposLEFTMatColorCodeNew.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposLEFTMatColorCodeNew.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("FTMatColorCode", 50, "Colorway")})
        Me.ReposLEFTMatColorCodeNew.DisplayMember = "FTMatColorCode"
        Me.ReposLEFTMatColorCodeNew.Name = "ReposLEFTMatColorCodeNew"
        Me.ReposLEFTMatColorCodeNew.NullText = ""
        Me.ReposLEFTMatColorCodeNew.Tag = ""
        Me.ReposLEFTMatColorCodeNew.ValueMember = "FTMatColorCode"
        '
        'ogbSrcBreakdownDivert
        '
        Me.ogbSrcBreakdownDivert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbSrcBreakdownDivert.Controls.Add(Me.ogdDivertSrc)
        Me.ogbSrcBreakdownDivert.Location = New System.Drawing.Point(0, 3)
        Me.ogbSrcBreakdownDivert.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbSrcBreakdownDivert.Name = "ogbSrcBreakdownDivert"
        Me.ogbSrcBreakdownDivert.Size = New System.Drawing.Size(1199, 190)
        Me.ogbSrcBreakdownDivert.TabIndex = 2
        Me.ogbSrcBreakdownDivert.Text = "Source Breakdown for Cancel"
        '
        'ogdDivertSrc
        '
        Me.ogdDivertSrc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdDivertSrc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertSrc.Location = New System.Drawing.Point(2, 25)
        Me.ogdDivertSrc.MainView = Me.ogvDivertSrc
        Me.ogdDivertSrc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdDivertSrc.Name = "ogdDivertSrc"
        Me.ogdDivertSrc.Size = New System.Drawing.Size(1195, 163)
        Me.ogdDivertSrc.TabIndex = 2
        Me.ogdDivertSrc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDivertSrc})
        '
        'ogvDivertSrc
        '
        Me.ogvDivertSrc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColSrcDivertoColFTMatColorCode, Me.oColSrcDivertoColFTMatColorName, Me.C1FTNikePOLineItem, Me.C1FNHSysMatColorId, Me.C1FNTotal})
        Me.ogvDivertSrc.GridControl = Me.ogdDivertSrc
        Me.ogvDivertSrc.Name = "ogvDivertSrc"
        Me.ogvDivertSrc.OptionsView.ColumnAutoWidth = False
        Me.ogvDivertSrc.OptionsView.ShowFooter = True
        Me.ogvDivertSrc.OptionsView.ShowGroupPanel = False
        '
        'oColSrcDivertoColFTMatColorCode
        '
        Me.oColSrcDivertoColFTMatColorCode.Caption = "FTMatColorCode"
        Me.oColSrcDivertoColFTMatColorCode.FieldName = "FTMatColorCode"
        Me.oColSrcDivertoColFTMatColorCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColSrcDivertoColFTMatColorCode.Name = "oColSrcDivertoColFTMatColorCode"
        Me.oColSrcDivertoColFTMatColorCode.OptionsColumn.AllowEdit = False
        Me.oColSrcDivertoColFTMatColorCode.OptionsColumn.AllowMove = False
        Me.oColSrcDivertoColFTMatColorCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColSrcDivertoColFTMatColorCode.OptionsColumn.ReadOnly = True
        Me.oColSrcDivertoColFTMatColorCode.Visible = True
        Me.oColSrcDivertoColFTMatColorCode.VisibleIndex = 0
        '
        'oColSrcDivertoColFTMatColorName
        '
        Me.oColSrcDivertoColFTMatColorName.Caption = "FTMatColorName"
        Me.oColSrcDivertoColFTMatColorName.FieldName = "FTMatColorName"
        Me.oColSrcDivertoColFTMatColorName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColSrcDivertoColFTMatColorName.Name = "oColSrcDivertoColFTMatColorName"
        Me.oColSrcDivertoColFTMatColorName.OptionsColumn.AllowEdit = False
        Me.oColSrcDivertoColFTMatColorName.OptionsColumn.AllowMove = False
        Me.oColSrcDivertoColFTMatColorName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColSrcDivertoColFTMatColorName.OptionsColumn.ReadOnly = True
        Me.oColSrcDivertoColFTMatColorName.Visible = True
        Me.oColSrcDivertoColFTMatColorName.VisibleIndex = 1
        '
        'C1FTNikePOLineItem
        '
        Me.C1FTNikePOLineItem.Caption = "PO Line Item"
        Me.C1FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.C1FTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C1FTNikePOLineItem.Name = "C1FTNikePOLineItem"
        Me.C1FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.C1FTNikePOLineItem.OptionsColumn.AllowMove = False
        Me.C1FTNikePOLineItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C1FTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.C1FTNikePOLineItem.Visible = True
        Me.C1FTNikePOLineItem.VisibleIndex = 2
        '
        'C1FNHSysMatColorId
        '
        Me.C1FNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.C1FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.C1FNHSysMatColorId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.C1FNHSysMatColorId.Name = "C1FNHSysMatColorId"
        Me.C1FNHSysMatColorId.OptionsColumn.AllowEdit = False
        Me.C1FNHSysMatColorId.OptionsColumn.AllowMove = False
        Me.C1FNHSysMatColorId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C1FNHSysMatColorId.OptionsColumn.ReadOnly = True
        '
        'C1FNTotal
        '
        Me.C1FNTotal.Caption = "Total"
        Me.C1FNTotal.DisplayFormat.FormatString = "{0:n0}"
        Me.C1FNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C1FNTotal.FieldName = "FNTotal"
        Me.C1FNTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.C1FNTotal.Name = "C1FNTotal"
        Me.C1FNTotal.OptionsColumn.AllowEdit = False
        Me.C1FNTotal.OptionsColumn.ReadOnly = True
        Me.C1FNTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotal", "{0:n0}")})
        Me.C1FNTotal.Visible = True
        Me.C1FNTotal.VisibleIndex = 3
        '
        'ogbConfirmDivertSubBreakdown
        '
        Me.ogbConfirmDivertSubBreakdown.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbConfirmDivertSubBreakdown.Controls.Add(Me.ocmcancel)
        Me.ogbConfirmDivertSubBreakdown.Controls.Add(Me.ocmok)
        Me.ogbConfirmDivertSubBreakdown.Location = New System.Drawing.Point(0, 741)
        Me.ogbConfirmDivertSubBreakdown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbConfirmDivertSubBreakdown.Name = "ogbConfirmDivertSubBreakdown"
        Me.ogbConfirmDivertSubBreakdown.ShowCaption = False
        Me.ogbConfirmDivertSubBreakdown.Size = New System.Drawing.Size(1202, 37)
        Me.ogbConfirmDivertSubBreakdown.TabIndex = 2
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1094, 2)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(103, 31)
        Me.ocmcancel.TabIndex = 3
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(940, 2)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(103, 31)
        Me.ocmok.TabIndex = 2
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wSubOrderCancelBreakdown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 780)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbConfirmDivertSubBreakdown)
        Me.Controls.Add(Me.ogbViewDivertDetail)
        Me.Controls.Add(Me.ogbSubOrderNoSrc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wSubOrderCancelBreakdown"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cancel Sub Order No."
        CType(Me.ogbSubOrderNoSrc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbSubOrderNoSrc.ResumeLayout(False)
        CType(Me.FTStateCancenSub.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemarkSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSubOrderNoSrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbViewDivertDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbViewDivertDetail.ResumeLayout(False)
        CType(Me.ogbBalanceBreakdownAfterDivert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbBalanceBreakdownAfterDivert.ResumeLayout(False)
        CType(Me.ogdDivertBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDivertBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbTargetBreakdownDivert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbTargetBreakdownDivert.ResumeLayout(False)
        CType(Me.ogdDivertDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDivertDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepPOLineItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTNikePOLineItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposLEFTMatColorCodeNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbSrcBreakdownDivert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbSrcBreakdownDivert.ResumeLayout(False)
        CType(Me.ogdDivertSrc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDivertSrc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbConfirmDivertSubBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbConfirmDivertSubBreakdown.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbSubOrderNoSrc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSubOrderNoSrc_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbViewDivertDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbConfirmDivertSubBreakdown As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogdDivertSrc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDivertSrc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogdDivertDT As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDivertDT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogdDivertBalance As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDivertBalance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSubOrderNoSrc As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbBalanceBreakdownAfterDivert As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbTargetBreakdownDivert As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbSrcBreakdownDivert As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTRemark_SubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemarkSubOrderNo As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents oColBalanceDivertFTMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColBalanceDivertFTMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColTargetDivertFTMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColSrcDivertoColFTMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColSrcDivertoColFTMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C1FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C3FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C3FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C1FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTNikePOLineItem As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents C2FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepPOLineItem As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents C3FNTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C1FNTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposLEFTMatColorCodeNew As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents FTStateCancenSub As DevExpress.XtraEditors.CheckEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wProdIssueRequestAddItem
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
        Me.ogbselecttablecut = New DevExpress.XtraEditors.GroupControl()
        Me.ockselectalltable = New DevExpress.XtraEditors.CheckEdit()
        Me.ogctablebound = New DevExpress.XtraGrid.GridControl()
        Me.ogvtablebound = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GFTOrderProdNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMarkName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTableNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMarkId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposRawmatFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDFTOrderProdNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDFNHSysMarkId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDFTMarkName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDFNTableNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNReqQuantityBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNStockQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNIssueQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNIssueQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ockselectalldetail = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselecttablecut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselecttablecut.SuspendLayout()
        CType(Me.ockselectalltable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogctablebound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtablebound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposRawmatFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNIssueQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockselectalldetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbselecttablecut
        '
        Me.ogbselecttablecut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ogbselecttablecut.Controls.Add(Me.ockselectalltable)
        Me.ogbselecttablecut.Controls.Add(Me.ogctablebound)
        Me.ogbselecttablecut.Location = New System.Drawing.Point(2, 3)
        Me.ogbselecttablecut.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbselecttablecut.Name = "ogbselecttablecut"
        Me.ogbselecttablecut.Size = New System.Drawing.Size(341, 633)
        Me.ogbselecttablecut.TabIndex = 429
        Me.ogbselecttablecut.Text = "เลือกโต๊ะตัด"
        '
        'ockselectalltable
        '
        Me.ockselectalltable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ockselectalltable.Location = New System.Drawing.Point(166, 3)
        Me.ockselectalltable.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockselectalltable.Name = "ockselectalltable"
        Me.ockselectalltable.Properties.Appearance.Options.UseTextOptions = True
        Me.ockselectalltable.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ockselectalltable.Properties.Caption = "เลือกทั้งหมด"
        Me.ockselectalltable.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ockselectalltable.Properties.ValueChecked = "1"
        Me.ockselectalltable.Properties.ValueUnchecked = "0"
        Me.ockselectalltable.Size = New System.Drawing.Size(170, 21)
        Me.ockselectalltable.TabIndex = 309
        Me.ockselectalltable.Tag = "2|"
        '
        'ogctablebound
        '
        Me.ogctablebound.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogctablebound.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogctablebound.Location = New System.Drawing.Point(5, 27)
        Me.ogctablebound.MainView = Me.ogvtablebound
        Me.ogctablebound.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogctablebound.Name = "ogctablebound"
        Me.ogctablebound.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.RepositoryItemLookUpEdit1, Me.RepFTSelect})
        Me.ogctablebound.Size = New System.Drawing.Size(335, 605)
        Me.ogctablebound.TabIndex = 307
        Me.ogctablebound.TabStop = False
        Me.ogctablebound.Tag = "3|"
        Me.ogctablebound.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtablebound})
        '
        'ogvtablebound
        '
        Me.ogvtablebound.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.GFTOrderProdNo, Me.FTMarkName, Me.ColFTTableNo, Me.FNHSysMarkId})
        Me.ogvtablebound.GridControl = Me.ogctablebound
        Me.ogvtablebound.Name = "ogvtablebound"
        Me.ogvtablebound.OptionsCustomization.AllowGroup = False
        Me.ogvtablebound.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtablebound.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvtablebound.OptionsView.ColumnAutoWidth = False
        Me.ogvtablebound.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtablebound.OptionsView.ShowGroupPanel = False
        Me.ogvtablebound.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 41
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'GFTOrderProdNo
        '
        Me.GFTOrderProdNo.Caption = "FTOrderProdNo"
        Me.GFTOrderProdNo.FieldName = "FTOrderProdNo"
        Me.GFTOrderProdNo.Name = "GFTOrderProdNo"
        Me.GFTOrderProdNo.OptionsColumn.AllowEdit = False
        Me.GFTOrderProdNo.OptionsColumn.ReadOnly = True
        Me.GFTOrderProdNo.Visible = True
        Me.GFTOrderProdNo.VisibleIndex = 1
        Me.GFTOrderProdNo.Width = 82
        '
        'FTMarkName
        '
        Me.FTMarkName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMarkName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMarkName.Caption = "FTMarkName"
        Me.FTMarkName.FieldName = "FTMarkName"
        Me.FTMarkName.Name = "FTMarkName"
        Me.FTMarkName.OptionsColumn.AllowEdit = False
        Me.FTMarkName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMarkName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTMarkName.OptionsColumn.AllowShowHide = False
        Me.FTMarkName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMarkName.OptionsColumn.ReadOnly = True
        Me.FTMarkName.Visible = True
        Me.FTMarkName.VisibleIndex = 3
        Me.FTMarkName.Width = 83
        '
        'ColFTTableNo
        '
        Me.ColFTTableNo.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTTableNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColFTTableNo.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTTableNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTTableNo.Caption = "FTTableNo"
        Me.ColFTTableNo.FieldName = "FNTableNo"
        Me.ColFTTableNo.Name = "ColFTTableNo"
        Me.ColFTTableNo.OptionsColumn.AllowEdit = False
        Me.ColFTTableNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTTableNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTTableNo.OptionsColumn.AllowMove = False
        Me.ColFTTableNo.OptionsColumn.AllowShowHide = False
        Me.ColFTTableNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTTableNo.OptionsColumn.ReadOnly = True
        Me.ColFTTableNo.Visible = True
        Me.ColFTTableNo.VisibleIndex = 2
        Me.ColFTTableNo.Width = 56
        '
        'FNHSysMarkId
        '
        Me.FNHSysMarkId.Caption = "FNHSysMarkId"
        Me.FNHSysMarkId.FieldName = "FNHSysMarkId"
        Me.FNHSysMarkId.Name = "FNHSysMarkId"
        Me.FNHSysMarkId.OptionsColumn.AllowEdit = False
        Me.FNHSysMarkId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.DisplayFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit1.EditFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 0
        '
        'RepositoryItemLookUpEdit1
        '
        Me.RepositoryItemLookUpEdit1.AutoHeight = False
        Me.RepositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit1.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("FTSizeBreakDown", "FTSizeBreakDown"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("FNMatSizeSeq", "FNMatSizeSeq", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.RepositoryItemLookUpEdit1.DisplayMember = "FTSizeBreakDown"
        Me.RepositoryItemLookUpEdit1.Name = "RepositoryItemLookUpEdit1"
        Me.RepositoryItemLookUpEdit1.ValueMember = "FTSizeBreakDown"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Controls.Add(Me.ockselectalldetail)
        Me.ogbdetail.Location = New System.Drawing.Point(348, 47)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(990, 588)
        Me.ogbdetail.TabIndex = 430
        Me.ogbdetail.Text = "รายการวัตถุดิบ"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposRawmatFTStateSelect, Me.ReposFNIssueQuantity})
        Me.ogcdetail.Size = New System.Drawing.Size(986, 561)
        Me.ogcdetail.TabIndex = 309
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateSelect, Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatColorName, Me.FTRawMatSizeCode, Me.FNHSysUnitId, Me.FTUnitCode, Me.GDFTOrderProdNo, Me.GDFNHSysMarkId, Me.GDFTMarkName, Me.GDFNTableNo, Me.FNUsedQuantity, Me.FNReqQuantityBF, Me.FNStockQuantity, Me.FNIssueQuantity})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTStateSelect
        '
        Me.FTStateSelect.Caption = " "
        Me.FTStateSelect.ColumnEdit = Me.ReposRawmatFTStateSelect
        Me.FTStateSelect.FieldName = "FTSelect"
        Me.FTStateSelect.Name = "FTStateSelect"
        Me.FTStateSelect.Visible = True
        Me.FTStateSelect.VisibleIndex = 0
        Me.FTStateSelect.Width = 47
        '
        'ReposRawmatFTStateSelect
        '
        Me.ReposRawmatFTStateSelect.AutoHeight = False
        Me.ReposRawmatFTStateSelect.Caption = "Check"
        Me.ReposRawmatFTStateSelect.Name = "ReposRawmatFTStateSelect"
        Me.ReposRawmatFTStateSelect.ValueChecked = "1"
        Me.ReposRawmatFTStateSelect.ValueUnchecked = "0"
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysRawMatId.OptionsColumn.ReadOnly = True
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatCode.OptionsColumn.FixedWidth = True
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 1
        Me.FTRawMatCode.Width = 117
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTRawMatName"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTMatDesc.OptionsColumn.AllowMove = False
        Me.FTMatDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMatDesc.OptionsColumn.FixedWidth = True
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.Visible = True
        Me.FTMatDesc.VisibleIndex = 2
        Me.FTMatDesc.Width = 176
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatColorCode.OptionsColumn.FixedWidth = True
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 3
        Me.FTRawMatColorCode.Width = 95
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.Caption = "Color Name"
        Me.FTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsColumn.AllowShowHide = False
        Me.FTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorName.OptionsColumn.ShowInCustomizationForm = False
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 4
        Me.FTRawMatColorName.Width = 104
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatSizeCode.OptionsColumn.FixedWidth = True
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 5
        Me.FTRawMatSizeCode.Width = 99
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        Me.FNHSysUnitId.OptionsColumn.AllowEdit = False
        Me.FNHSysUnitId.OptionsColumn.ReadOnly = True
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitCode.OptionsColumn.FixedWidth = True
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 72
        '
        'GDFTOrderProdNo
        '
        Me.GDFTOrderProdNo.Caption = "FTOrderProdNo"
        Me.GDFTOrderProdNo.FieldName = "FTOrderProdNo"
        Me.GDFTOrderProdNo.Name = "GDFTOrderProdNo"
        Me.GDFTOrderProdNo.OptionsColumn.AllowEdit = False
        Me.GDFTOrderProdNo.OptionsColumn.ReadOnly = True
        Me.GDFTOrderProdNo.Visible = True
        Me.GDFTOrderProdNo.VisibleIndex = 9
        Me.GDFTOrderProdNo.Width = 121
        '
        'GDFNHSysMarkId
        '
        Me.GDFNHSysMarkId.Caption = "FNHSysMarkId"
        Me.GDFNHSysMarkId.FieldName = "FNHSysMarkId"
        Me.GDFNHSysMarkId.Name = "GDFNHSysMarkId"
        Me.GDFNHSysMarkId.OptionsColumn.AllowEdit = False
        Me.GDFNHSysMarkId.OptionsColumn.ReadOnly = True
        '
        'GDFTMarkName
        '
        Me.GDFTMarkName.Caption = "FTMarkName"
        Me.GDFTMarkName.FieldName = "FTMarkName"
        Me.GDFTMarkName.Name = "GDFTMarkName"
        Me.GDFTMarkName.OptionsColumn.AllowEdit = False
        Me.GDFTMarkName.OptionsColumn.ReadOnly = True
        Me.GDFTMarkName.Visible = True
        Me.GDFTMarkName.VisibleIndex = 7
        Me.GDFTMarkName.Width = 128
        '
        'GDFNTableNo
        '
        Me.GDFNTableNo.AppearanceCell.Options.UseTextOptions = True
        Me.GDFNTableNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GDFNTableNo.Caption = "FNTableNo"
        Me.GDFNTableNo.DisplayFormat.FormatString = "{0:n0}"
        Me.GDFNTableNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDFNTableNo.FieldName = "FNTableNo"
        Me.GDFNTableNo.Name = "GDFNTableNo"
        Me.GDFNTableNo.OptionsColumn.AllowEdit = False
        Me.GDFNTableNo.OptionsColumn.ReadOnly = True
        Me.GDFNTableNo.Visible = True
        Me.GDFNTableNo.VisibleIndex = 8
        Me.GDFNTableNo.Width = 68
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.Caption = "FNUsedQuantity"
        Me.FNUsedQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNUsedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNUsedQuantity.FieldName = "FNRecomQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 10
        Me.FNUsedQuantity.Width = 86
        '
        'FNReqQuantityBF
        '
        Me.FNReqQuantityBF.Caption = "FNReqQuantityBF"
        Me.FNReqQuantityBF.DisplayFormat.FormatString = "{0:n4}"
        Me.FNReqQuantityBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNReqQuantityBF.FieldName = "FNReqQuantityBF"
        Me.FNReqQuantityBF.Name = "FNReqQuantityBF"
        Me.FNReqQuantityBF.OptionsColumn.AllowEdit = False
        Me.FNReqQuantityBF.OptionsColumn.ReadOnly = True
        Me.FNReqQuantityBF.Visible = True
        Me.FNReqQuantityBF.VisibleIndex = 11
        '
        'FNStockQuantity
        '
        Me.FNStockQuantity.Caption = "FNStockQuantity"
        Me.FNStockQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNStockQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNStockQuantity.FieldName = "FNOnhandQuantity"
        Me.FNStockQuantity.Name = "FNStockQuantity"
        Me.FNStockQuantity.OptionsColumn.AllowEdit = False
        Me.FNStockQuantity.OptionsColumn.ReadOnly = True
        Me.FNStockQuantity.Visible = True
        Me.FNStockQuantity.VisibleIndex = 12
        Me.FNStockQuantity.Width = 92
        '
        'FNIssueQuantity
        '
        Me.FNIssueQuantity.Caption = "FNIssueQuantity"
        Me.FNIssueQuantity.ColumnEdit = Me.ReposFNIssueQuantity
        Me.FNIssueQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNIssueQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNIssueQuantity.FieldName = "FNReqQuantity"
        Me.FNIssueQuantity.Name = "FNIssueQuantity"
        Me.FNIssueQuantity.Visible = True
        Me.FNIssueQuantity.VisibleIndex = 13
        Me.FNIssueQuantity.Width = 97
        '
        'ReposFNIssueQuantity
        '
        Me.ReposFNIssueQuantity.AutoHeight = False
        Me.ReposFNIssueQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNIssueQuantity.Name = "ReposFNIssueQuantity"
        Me.ReposFNIssueQuantity.Precision = 4
        '
        'ockselectalldetail
        '
        Me.ockselectalldetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ockselectalldetail.Location = New System.Drawing.Point(811, 1)
        Me.ockselectalldetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockselectalldetail.Name = "ockselectalldetail"
        Me.ockselectalldetail.Properties.Appearance.Options.UseTextOptions = True
        Me.ockselectalldetail.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ockselectalldetail.Properties.Caption = "เลือกทั้งหมด"
        Me.ockselectalldetail.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ockselectalldetail.Properties.ValueChecked = "1"
        Me.ockselectalldetail.Properties.ValueUnchecked = "0"
        Me.ockselectalldetail.Size = New System.Drawing.Size(170, 21)
        Me.ockselectalldetail.TabIndex = 308
        Me.ockselectalldetail.Tag = "2|"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(981, 8)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(167, 31)
        Me.ocmsave.TabIndex = 431
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1164, 8)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(167, 31)
        Me.ocmexit.TabIndex = 432
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(350, 5)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(279, 31)
        Me.ocmload.TabIndex = 433
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "โหลดรายการวัตถุดิบ"
        '
        'wProdIssueRequestAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 639)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmload)
        Me.Controls.Add(Me.ocmexit)
        Me.Controls.Add(Me.ocmsave)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbselecttablecut)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wProdIssueRequestAddItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Issue Request Add Item"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogbselecttablecut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselecttablecut.ResumeLayout(False)
        CType(Me.ockselectalltable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogctablebound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtablebound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposRawmatFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNIssueQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockselectalldetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselecttablecut As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogctablebound As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtablebound As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTMarkName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTableNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMarkId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents ockselectalltable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockselectalldetail As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposRawmatFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDFTOrderProdNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDFNHSysMarkId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDFTMarkName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDFNTableNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNStockQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNIssueQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNIssueQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents GFTOrderProdNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNReqQuantityBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
End Class
